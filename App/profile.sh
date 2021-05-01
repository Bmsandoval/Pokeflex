#!/bin/bash


POKEFLEX_APP_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )"


_pokeflex_base_options=\
"\tstart\t:\tlaunch pokeflex's docker container(s). Optionally specify specific one
\tstop\t:\tstop pokeflex's docker container(s)
\tremake\t:\tforce rebuild. probably don't need to do this, it's built into the start and reset commands
\treset\t:\tpurges and rebuilds pokeflex's docker container(s) [[WARNING: RESETS DATABASE]]
\tpurge\t:\tstop containers and purge all remnants [[WARNING: EVEN WORSE THAN RESET]]
\ttest\t:\truns unit|bench tests
\tsync\t:\tsyncs this project with a non-local host
\trepro\t:\treloads this file
\thelp\t:\tshow this menu"


_pokeflex_purge_options=\
"\t\t\tOne of the following is required
\tapi\t:\tThis project's backend's docker container (db unaffected)
\tproject\t:\tAll docker containers for this project
\teverywhere\t:\tAll non-database docker containers, regardless of project
\teverything\t:\tAll docker containers, regardless of project
\tkillme\t:\tPurges base docker image. You probably don't want to do this
\thelp\t:\tdisplays this menu"


_pokeflex_test_bench_options=\
"\t\t\tAll following arguments are optional
\t-inmemorydb\t:\t[default] runs tests against an inmemory sqlite database.
\t-nativedb\t:\truns tests against a native db on local host (I use this against a docker db)
\t-verbose\t:\t[default] no holes barred
\t-concise\t:\tparses output using perl regex for more concise output
\t-filter-any\t:\tRuns tests that are in ANY of the provided categories
\t-filter-all\t:\tRuns tests that are in ALL of the provided categories
\t-quick\t:\tRuns benchmarks in-process, on short-job setting. not bad, but not the best.
\t-debug\t:\tRuns benchmarks in fastest possible setting. bad benchmarks, but good for testing/debugging broken benchmarks
\t-dry\t:\tprint the resulting command instead of running it
\t-help\t:\tdisplays this menu"


_pokeflex_test_unit_options=\
"\t\t\tAll following arguments are optional
\t-inmemorydb\t:\t[default] runs tests against an inmemory sqlite database.
\t-nativedb\t:\truns tests against a native db on local host (I use this against a docker db)
\t-filter\t:\toptionally filter to specific tests
\t-dry\t:\tprint the resulting command instead of running it
\t-help\t:\tdisplays this menu"


alias pf="pokeflex"
# Use: view/edit documentation
pokeflex () {
  local _option="${1}"
  shift
  local _subOption="${1}"
  case "${_option}" in
    'help'|'')
      echo -e "Usage: $ ${FUNCNAME[0]} [option]
Options:
${_pokeflex_base_options}"
    ;;
    'start')
      case "${_subOption}" in
        'api') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" up --build pokeflex-api ;;
        'db') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" up -d pokeflex-db ;;
        'all') ${FUNCNAME[0]} start db && ${FUNCNAME[0]} start api ;;
        ''|*) echo "'api', 'db', or 'all'"
      esac
      echo "STARTING pokeflex services"
    ;;
    'stop')
      echo "STOPPING pokeflex services"
      case "${_subOption}" in
        'api') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" down pokeflex-api ;;
        'db') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" down pokeflex-db ;;
        'all') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" down ;;
        ''|*) echo "'api', 'db', or 'all'"
      esac
    ;;
    'rebuild')
      docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" build
    ;;
    'purge')
      case "${_subOption}" in
        'api') ${FUNCNAME[0]} stop api; docker system prune --all --force --filter=label=base=true --filter=label=notdb=true --filter=label=pokeflex=true ;;
        'project') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true --filter=label=pokeflex=true ;;
        'everywhere') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true --filter=label=notdb=true ;;
        'everything') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true ;;
        'killme') docker stop $(docker ps -q); docker system prune --all --force ;;
        ''|*) echo "'api', 'project', 'everywhere', 'everything', 'killme'"
      esac
    ;;
    'reset')
      case "${_subOption}" in
        'api') ${FUNCNAME[0]} purge api; ${FUNCNAME[0]} rebuild ;;
        'all') ${FUNCNAME[0]} purge project; ${FUNCNAME[0]} rebuild ;;
        ''|*) echo "'api' or 'all'"
      esac
    ;;
    'test')
      local start=$(date +%s)
      case "${_subOption}" in
        'unit') 
          local db help dryrun filtering filter
          db="inmemory"
          while shift; do
            if [[ "${1}" == "--" ]]; then 
              shift
              break
            fi
            if [[ "${1}" == "" ]]; then
              shift
              break
            fi
            case "${1}" in
            '-i'|'-inmemory') db="inmemory"; filtering=0 ;;
            '-n'|'-native') db="native"; filtering=0 ;;
            '-dry') dryrun=1; filtering=0 ;;
            '-f'|'-filter') filter="--filter '"; filtering=1 ;;
            '-h'|'-help') help=1; echo -e "${_pokeflex_test_unit_options}"; filtering=0 ;;
            *) [[ $filtering ]] && filter+="${1}|" ;;
            esac
          done
          [ -n "${filter}" ] && filter=${filter%?} && filter+="'"
          local command="export DotnetTestDbType=${db} && dotnet test ${POKEFLEX_APP_DIR}/../Tests/Tests.csproj ${filter} ${@}" 
          case "1" in
          "${dryrun}") echo "${command}" ;;
          "${help}") eval "dotnet test ${POKEFLEX_APP_DIR}/../Tests/Tests.csproj --help" ;;
          *) eval "${command}" ;;
          esac
          ;;
        'bench')
          local build db verbosity dryrun help filter filtering quick debug
          # defaults
          build="-c Release"
          db="inmemory"
          while shift; do
            if [[ "${1}" == "--" ]]; then 
              shift
              break
            fi
            case "${1}" in 
            '-i'|'-inmemorydb') db="inmemory" && filtering=false ;;
            '-n'|'-nativedb') db="native" && filtering=false ;;
            '-c'|'-concise') verbosity="| perl -wlne 'print if /^[|]|\/\/ Benchmark: |\/\/ [*]{5} |\s+[-]{3}|\s+at\s+/'" && filtering=false ;;
            '-v'|'-verbose') verbosity="" && filtering=false ;;
            '-fl'|'-filter-all') filter="--allCategories " && filtering=true ;;
            '-fy'|'-filter-any') filter="--anyCategories " && filtering=true ;;
            '-q'|'-quick') quick="-j short -i" ;; # short run job, with in-process benchmarks
            '-d'|'-debug') debug="--warmupCount 1 --iterationCount 1 --invocationCount 1 --unrollFactor 1 --runOncePerIteration true" ;;
            '-dry') dryrun=true && filtering=false ;;
            '-h'|'-help') help=true && echo -e "${_pokeflex_test_bench_options}" && filtering=false ;;
            *) [[ $filtering ]] && filter+=" ${1}" ;;
            esac
          done
          local command="export DotnetTestDbType=${db} && sudo -E dotnet run -c Release -p ${POKEFLEX_APP_DIR}/../Tests/Tests.csproj -- ${filter} ${debug} ${quick} ${@} ${verbosity}" 
          if [[ $dryrun ]]; then
            echo "${command}"
          elif [[ $help ]]; then
            eval "dotnet run -p ${POKEFLEX_APP_DIR}/../Tests/Tests.csproj -- --help"
          else
            eval "${command}"
          fi
        ;;
        ''|*) echo "'unit' or 'bench'"
      esac
      local end=$(date +%s)
      echo ""
      echo "Testing completed after $((end-start)) seconds"
    ;; 
    'repro')
      . ${POKEFLEX_APP_DIR}/profile.sh
    ;;
	'sync')
		case "${2}" in
		'-b'|'-begin')
			local _watchDir="${POKEFLEX_APP_DIR}/.."
			if [[ -n "${FSWATCH_PID}" ]]; then
				echo "already syncing"
			else
				fswatch -o "${_watchDir}/" \
				| while read -r _; do
          rsync -az "${_watchDir}" hunin:~/projects/Pokeflex \
            --exclude "bin/" --exclude ".git/" --exclude "obj/" \
            --exclude ".idea/" --exclude ".vs/" --exclude "BenchmarkDotNet.Artifacts/"
        done \
        &
				export FSWATCH_PID=$!
			fi
		;;
		'-c'|'-cease')
			kill ${FSWATCH_PID}
			unset FSWATCH_PID
		;;
		''|*)
			echo "
-begin : start syncing
-cease : stop syncing
"
		;;
		esac
	;;
  *) echo -e "ERROR: invalid option. Try..\n$ ${FUNCNAME[0]} help" ;;
  esac
} 2>/dev/null


__pokeflext_list_bench_tests () {
  local _testFiles=($(cd ${POKEFLEX_APP_DIR}/../Tests && find . -type f -not -path "*/Units/*" -not -path "*/obj/*" -not -path "*/bin/*" -not -path "*.Artifacts/*" -not -path "*/ServiceDataGenerator/*" -not -name "Tests.csproj" -not -name "Program.cs" -not -name "appsettings.*.json" -not -path "*/.idea/*"))
  rm pipe0; mkfifo pipe0
  _pids=()
  for _testFile in "${_testFiles[@]}"; do
    perl -e '
    foreach $line (<>) {
      $line =~ s/^\s+|\s+$//;
      $line =~ s/^[\/]+.*$//;
      if($line=~/\[BenchmarkCategory\((.*)\)\]/) {
        $r=$1;
        $r=~s/"| //g;
        $r=~s/,/\n/g;
        print "$r\n";
      }
    }
    $bench=0;' "${POKEFLEX_APP_DIR}/../Tests/$(echo "${_testFile}" | perl -pe "s/^\.\///")" \
    > pipe0 &
    _pids+=($!)
  done
  cat <pipe0 \
  | perl -e '
    for $line (<>) {
      unless ($seen{$line}) {
        $seen{$line} = 1;
        print "$line"
      }
    }' \
  &
  wait ${_pids[@]} 2>/dev/null
  rm pipe0
} 2>/dev/null


__pokeflext_list_unit_tests () {
  local _testFiles=($(cd ${POKEFLEX_APP_DIR}/../Tests && find . -type f -not -path "*/Benchs/*" -not -path "*/obj/*" -not -path "*/bin/*" -not -path "*.Artifacts/*" -not -path "*/ServiceDataGenerator/*" -not -name "Tests.csproj" -not -name "Program.cs" -not -name "appsettings.*.json" -not -path "*/.idea/*"))
  rm pipe0; mkfifo pipe0
  _pids=()
  for _testFile in "${_testFiles[@]}"; do
    perl -e '
    my $unit=0;
    foreach $line (<>) {
      $line =~ s/^\s+|\s+$//;
      $line =~ s/^[\/]+.*$//;
      if($line =~ /\[(?:Fact|Theory)\]/) {
        $unit=1;
      }
      if($unit==1){
        $line =~ s/^(\[[^\]]+\])+//;
        unless($line =~/^\s+$/) {
          $unit=0;
          $line =~ /([^\s]+)\(/;
          print "$1\n";
        }
      }
    }
    $bench=0;' "${POKEFLEX_APP_DIR}/../Tests/$(echo "${_testFile}" | perl -pe "s/^\.\///")" \
    > pipe0 &
    _pids+=($!)
  done
  cat <pipe0 &
  wait ${_pids[@]} 2>/dev/null
  rm pipe0
} 2>/dev/null


_fzf_complete_pokeflex () {
  #####################
  ## HANDLE BASE OPTIONS
  if [[ "${COMP_CWORD}" == "1" ]]; then
    if [[ "${COMP_WORDS[COMP_CWORD]}" == "**" ]]; then
      which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
        echo -e "${_pokeflex_base_options}")
    else 
      COMPREPLY=($(compgen -W "$(echo -e "${_pokeflex_base_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}"))
    fi
  elif [[ "${COMP_WORDS[COMP_CWORD]}" != "**" ]]; then
    #####################
    ## HANDLE NORMAL OPTIONS
    case "${COMP_WORDS[COMP_CWORD-1]}" in
    "start"|"stop") COMPREPLY=($(compgen -W "api db all" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "purge") COMPREPLY=("" $(compgen -W "$(echo -e "${_pokeflex_purge_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "reset") COMPREPLY=($(compgen -W "api all" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "test") COMPREPLY=($(compgen -W "unit bench" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "bench") COMPREPLY=("" $(compgen -W "$(echo -e "${_pokeflex_test_bench_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "unit") COMPREPLY=("" $(compgen -W "$(echo -e "${_pokeflex_test_unit_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "-f"|"-filter")
      [[ " ${COMP_WORDS[@]} " =~ " unit " ]] && COMPREPLY=("" $(compgen -W "$(__pokeflext_list_unit_tests)" "${COMP_WORDS[COMP_CWORD]}"))
    ;;
    "-fy"|"-filter-any"|"-fl"|"-filter-all")
      [[ " ${COMP_WORDS[@]} " =~ " bench " ]] && COMPREPLY=("" $(compgen -W "$(__pokeflext_list_bench_tests)" "${COMP_WORDS[COMP_CWORD]}"))
    ;;
    esac  
  else
  #####################
  ## HANDLE FZF OPTIONS
      case "${COMP_WORDS[COMP_CWORD-1]}" in
      "purge")
        which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
          echo -e "${_pokeflex_purge_options}")
      ;;
      "unit")
        which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
          echo -e "${_pokeflex_test_unit_options}")
      ;;
      "bench")
        which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
          echo -e "${_pokeflex_test_bench_options}")
      ;;
      "-f"|"-filter")
        if [[ " ${COMP_WORDS[@]} " =~ " unit " ]]; then
          which fzf >/dev/null && _fzf_complete --multi --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
            echo -e "$(__pokeflext_list_unit_tests)")
        fi
      ;;
      "-fy"|"-filter-any"|"-fl"|"-filter-all")
        if [[ " ${COMP_WORDS[@]} " =~ " bench " ]]; then
          which fzf >/dev/null && _fzf_complete --multi --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
            echo -e "$(__pokeflext_list_bench_tests)")
        fi
      ;;
      
      esac
  fi
}


_fzf_complete_pokeflex_post () {
  local result
  read -ra result
  case "${COMP_WORDS[COMP_CWORD-1]}" in
  "-f"|"-filter"|"-fy"|"-filter-any"|"-fl"|"-filter-all")
    while read -ra line; do
      result+=" ${line}"
    done
  esac
  echo "${result}"
}


complete -F _fzf_complete_pokeflex -o default -o bashdefault pokeflex
complete -F _fzf_complete_pokeflex -o default -o bashdefault pf
