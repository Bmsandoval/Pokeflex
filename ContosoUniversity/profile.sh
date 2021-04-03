#!/bin/bash

CONTOSO_UNIVERSITY_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )"


alias ct="contoso"
# Use: view/edit documentation
contoso () {
  case "${1}" in
    'help'|'')
      echo "Usage: $ ${FUNCNAME[0]} [option]
Options:
- help: show this menu
- start: launch contoso's docker container(s). Optionally specify specific one
- stop: stop contoso's docker container(s) [[WARNING: RESETS DATABASE]]
- restart: stop contoso-api and start of contoso's docker container(s) that aren't running [[WARNING: RESETS DATABASE]]
- restart-all: stop and start all of contoso's docker container(s) [[WARNING: RESETS DATABASE]]
- purge: stop containers and purge all remnants [[WARNING: EVEN WORSE THAN RESET]]
- reset: purges and rebuilds contoso's docker container(s) [[WARNING: RESETS DATABASE]]
- test: runs xunit tests"
    ;;
    'start')
      case "${2}" in
        'api') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" up --build contoso-api ;;
        'db') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" up -d contoso-db ;;
        'all') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" up ;;
        '') echo "'api', 'db', or 'all'"
      esac
      echo "STARTING contoso services"
    ;;
    'stop')
      echo "STOPPING contoso services"
      case "${2}" in
        'api') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" down api ;;
        'db') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" down db ;;
        'all') docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" down ;;
        '') echo "'api', 'db', or 'all'"
      esac
    ;;
    'restart')
      case "${2}" in
        'api') ${FUNCNAME[0]} down api && ${FUNCNAME[0]} start api ;;
        'db') ${FUNCNAME[0]} down db && ${FUNCNAME[0]} start db ;;
        'all') ${FUNCNAME[0]} down && ${FUNCNAME[0]} start ;;
        '') echo "'api', 'db', or 'all'"
      esac
    ;;
    'purge')
      docker stop $(docker ps -q)
      docker system prune --all --force --filter=label=base=true
    ;;
    'reset')
      ${FUNCNAME[0]} purge
      docker-compose -f "${CONTOSO_UNIVERSITY_DIR}/docker-compose.yml" build
    ;;
    'test')
      local start=$(date +%s)
      dotnet test ../Tests/ContosoTests.csproj
      local end=$(date +%s)
      echo ""
      echo "Testing completed after $((end-start)) seconds"
    ;;
#    'test')
#      local start=$(date +%s)
#      # ensure recent build so test runners don't have to build
#      echo "Rebuilding project"
#      dotnet build "${CONTOSO_UNIVERSITY_DIR}" --nologo -v quiet >/dev/null
#      # find all test projects
#      local _csTests=($(find -d "${CONTOSO_UNIVERSITY_DIR}/../Tests" -name "*.csproj"))
#      local _resultsDir="${CONTOSO_UNIVERSITY_DIR}/../Tests/Results"
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