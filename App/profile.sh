#!/bin/bash

POKEFLEX_APP_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )"

_pokeflex_base_options=\
"\thelp\t:\tshow this menu
\tstart\t:\tlaunch pokeflex's docker container(s). Optionally specify specific one
\tstop\t:\tstop pokeflex's docker container(s)
\tremake\t:\tforce rebuild. probably don't need to do this, it's built into the start and reset commands
\treset\t:\tpurges and rebuilds pokeflex's docker container(s) [[WARNING: RESETS DATABASE]]
\tpurge\t:\tstop containers and purge all remnants [[WARNING: EVEN WORSE THAN RESET]]
\ttest\t:\truns unit|bench tests
\tsync\t:\tsyncs this project with a non-local host
\trepro\t:\treloads this file"

_pokeflex_test_bench_options=\
"\t\t\tAll following arguments are optional
\t-releasebuild\t:\t[default] in release mode, optimizations are enabled for truer benchmarks
\t-debugbuild\t:\t(deprecated) I think it maybe runs faster? you really shouldn't though
\t-mockdb\t:\t(deprecated) Just don't. It's far easier, more performant, and less error prone to use an inmem db
\t-inmemorydb\t:\t[default] runs tests against an inmemory sqlite database.
\t-nativedb\t:\truns tests against a native db on local host (I use this against a docker db)
\t-priority\t:\truns using sudo to enable high priority mode on the process. Allows for more accurate benchmarks
\t-verbose\t:\t[default] no holes barred
\t-concise\t:\tparses output using perl regex for more concise output
\t-help\t:\tdisplays this menu"

alias pf="pokeflex"
# Use: view/edit documentation
pokeflex () {
  case "${1}" in
    'help'|'')
      echo -e "Usage: $ ${FUNCNAME[0]} [option]
Options:
${_pokeflex_base_options}"
    ;;
    'start')
      case "${2}" in
        'api') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" up --build pokeflex-api ;;
        'db') docker-compose -f "${POKEFLEX_APP_DIR}/docker-compose.yml" up -d pokeflex-db ;;
        'all') ${FUNCNAME[0]} start db && ${FUNCNAME[0]} start api ;;
        ''|*) echo "'api', 'db', or 'all'"
      esac
      echo "STARTING pokeflex services"
    ;;
    'stop')
      echo "STOPPING pokeflex services"
      case "${2}" in
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
      case "${2}" in
        'api') ${FUNCNAME[0]} stop api; docker system prune --all --force --filter=label=base=true --filter=label=notdb=true --filter=label=pokeflex=true ;;
        'project') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true --filter=label=pokeflex=true ;;
        'everywhere') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true --filter=label=notdb=true ;;
        'everything') docker stop $(docker ps -q); docker system prune --all --force --filter=label=base=true ;;
        'killme') docker stop $(docker ps -q); docker system prune --all --force ;;
        ''|*) echo "'api', 'project', 'everywhere', 'everything', 'killme'"
      esac
    ;;
    'reset')
      case "${2}" in
        'api') ${FUNCNAME[0]} purge api; ${FUNCNAME[0]} rebuild ;;
        'all') ${FUNCNAME[0]} purge project; ${FUNCNAME[0]} rebuild ;;
        ''|*) echo "'api' or 'all'"
      esac
    ;;
    'test')
      local start=$(date +%s)
      case "${2}" in
        'unit') dotnet test "${POKEFLEX_APP_DIR}/../Tests/Units/Units.csproj" ;;
        'bench')
          local build db usesudo verbosity dryrun help
          # defaults
          build="-c Release"
          db="inmemory"
          for var in "$@"; do
            case "${var}" in
            '-r'|'-release') build="-c Release" ;;
            '-d'|'-debug') build="" ;;
            '-i'|'-inmemory') db="inmemory" ;;
            '-n'|'-native') db="native" ;;
            '-m'|'-mock') db="mock" ;;
            '-p'|'-priority') usesudo="sudo" ;;
            '-c'|'-concise') verbosity="| perl -wlne 'print if /^[|]|\/\/ Benchmark: |\/\/ [*]{5} |\s+[-]{3}|\s+at\s+/'" ;;
            '-v'|'-verbose') verbosity="" ;;
            '-dry') dryrun=true ;;
            '-h'|'-help') help=true && echo -e "${_pokeflex_test_bench_options}"
            esac
          done
          local command="${usesudo} dotnet run ${build} -p ${POKEFLEX_APP_DIR}/../Tests/Benchmarks/Benchmarks.csproj ${db} ${verbosity}" 
          if [[ $dryrun ]]; then
            echo "${command}"
          elif [[ ! $help ]]; then
            eval "${command}"
          fi
#dotnet run -c Release -f netcoreapp2.1 -- --filter *IntroNativeMemory.Alloc* --profiler NativeMemory
#top -pid 98921 >> newfile
#top -b -n 3 | sed -n '8, 12{s/^ *//;s/ *$//;s/  */,/gp;};12q' >> newfile.csv
#sudo docker inspect –format=”{{.State.Pid}}” pokeflex-db | grep "Pid"
#docker top pokeflex-db
#docker stats
        ;;
        ''|*) echo "'unit' or 'bench'"
      esac
      local end=$(date +%s)
      echo ""
      echo "Testing completed after $((end-start)) seconds"
    ;; 
    'reprofile')
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
		''|'*')
			echo "
-begin : start syncing
-cease : stop syncing
"
		;;
		esac
	;;

#    'test')
#      local start=$(date +%s)
#      # ensure recent build so test runners don't have to build
#      echo "Rebuilding project"
#      dotnet build "${POKEFLEX_APP_DIR}" --nologo -v quiet >/dev/null
#      # find all test projects
#      local _csTests=($(find -d "${POKEFLEX_APP_DIR}/../Tests" -name "*.csproj"))
#      local _resultsDir="${POKEFLEX_APP_DIR}/../Tests/Results"
#      local _jobPids=()
#      echo "Beginning tests"
#      echo ""
#      for _csTest in ${_csTests[*]}; do
#        local _testName="$(basename $_csTest)"
#        local _resultsFile="${_resultsDir}/${_testName%.*}"
#        rm -rf "${_resultsFile}.*"
#        touch "${_resultsFile}.stdout"
#        $(dotnet test --no-build --nologo "${_csTest}" -v quiet -l:"trx;LogFileName=${_resultsFile}" 1>"${_resultsFile}.stdout") &
#        _jobPids+=($!)
#      done; unset _csTest
##        | perl -ne 'print "[$1] $5 -- f:$2, p:$3, d:$4\n" if /^(Passed|Failed).+Failed:\s+([0-9]+).+Passed:\s+([0-9]+).+Duration:\s+([0-9]+\s+[a-z]+)\s+-\s+(.+)\/bin\/Debug\//' \
#      tail -f -q ${_resultsDir}/*.stdout \
#        | perl -ne 'print "[$1] $2" if /^Failed.+Failed:\s+[0-9]+.+Passed:\s+[0-9]+.+Duration:\s+[0-9]+\s+[a-z]+\s+-\s+(.+)\/bin\/Debug\//' \
#        | while read -r _lineStdout; do
#            local _name=$(echo "${_lineStdout}" | perl -ne 'print "$1/Results/$2" if /\[Failed\](.+)\/([^\s]+) /')
##            echo "$_lineStdout"
#            perl -ne 'if (/<ErrorInfo>/../<\/ErrorInfo>/) {print unless /<ErrorInfo>/ or /<\/ErrorInfo>/}' "$_name" \
#              | while read -r _errorInfo; do 
#                  echo "${_errorInfo}" | perl -ne 'print "$1\n" if /<StackTrace>\s+at\s+(.+)<\/StackTrace>/'
##                  echo "${_errorInfo}" | perl -0777 -lne 'print $1 if /<Message>(.*?)<\/Message>/s'
#                done
#          done 1>/dev/tty &
##        | perl -ne 'print "[$1] $5 -- f:$2, p:$3, d:$4\n" if /^(Passed|Failed).+Failed:\s+([0-9]+).+Passed:\s+([0-9]+).+Duration:\s+([0-9]+\s+[a-z]+).+\/(.+)\.dll \(.+\)/' 1>/dev/tty &
#      local _tailPids=($(ps | perl -ne 'print "$1\n" if /^[ ]?([0-9]+) [^ ]+\s+[^ ]+ tail -f -q/'))
#      wait "${_jobPids[@]}"
#      disown && kill -9 "${_tailPids[@]}"
#      wait "${_tailPids[@]}" 2/dev/null
#      fg
#      wait
#      local end=$(date +%s)
#      echo ""
#      echo "Testing completed after $((end-start)) seconds"
#    ;;
    *)
      echo -e "ERROR: invalid option. Try..\n$ ${FUNCNAME[0]} help"
    ;;
  esac
} 2>/dev/null

_fzf_complete_pokeflex () {
  case "${COMP_CWORD}" in
  "1")
    if [[ "${COMP_WORDS[COMP_CWORD]}" != "**" ]]; then
      COMPREPLY=($(compgen -W "$(echo -e "${_pokeflex_base_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}"))
    else 
      which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
        echo -e "${_pokeflex_base_options}"
      )
    fi
  ;;
  "2")
    case "${COMP_WORDS[COMP_CWORD-1]}" in
    "start"|"stop") COMPREPLY=($(compgen -W "api db all" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "purge") COMPREPLY=($(compgen -W "api project everywhere everything killme" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "reset") COMPREPLY=($(compgen -W "api all" "${COMP_WORDS[COMP_CWORD]}")) ;;
    "test") COMPREPLY=($(compgen -W "unit bench" "${COMP_WORDS[COMP_CWORD]}")) ;;
    esac
  ;;
  "3")
    if [[ "bench" == "${COMP_WORDS[COMP_CWORD-1]}" ]]; then
      if [[ "${COMP_WORDS[COMP_CWORD]}" != "**" ]]; then
        # Prepend empty string if optional arguments
        COMPREPLY=("" $(compgen -W "$(echo -e "${_pokeflex_test_bench_options}" | perl -ne 'print "$1 " if /\t([^\t]+)\t/')" "${COMP_WORDS[COMP_CWORD]}"))
      else 
        which fzf >/dev/null && _fzf_complete --with-nth=2 --delimiter='\t' --preview='echo -e {4}' --preview-window=up:sharp:wrap:40% --prompt="pokeflex> " -- "$@" < <(
          echo -e "${_pokeflex_test_bench_options}"
        )
      fi
    fi
  esac
}
_fzf_complete_pokeflex_post () {
  perl -ne 'print "$1 " if /\t([^\t]+)\t:/'
}
complete -F _fzf_complete_pokeflex -o default -o bashdefault pokeflex
complete -F _fzf_complete_pokeflex -o default -o bashdefault pf
