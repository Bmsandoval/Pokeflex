#!/bin/bash

alias ct="contoso"
# Use: view/edit documentation
contoso () {
    # if no command given force help page
    local OPTION
	if [[ "$1" != "" ]]; then
        OPTION=$1
    else
        OPTION="help"
    fi
	# handle input options
    case "${OPTION}" in
        'help')
echo "Usage: $ ${FUNCNAME} [option]
Options:
- help: show this menu
- start: launch contoso's docker container(s)
- stop: stop contoso's docker container(s) [[WARNING: RESETS DATABASE]]
- restart: stop contoso-api and start of contoso's docker container(s) that aren't running [[WARNING: RESETS DATABASE]]
- restart-all: stop and start all of contoso's docker container(s) [[WARNING: RESETS DATABASE]]
- purge: stop containers and purge all remnants [[WARNING: EVEN WORSE THAN RESET]]
- reset: purges and rebuilds contoso's docker container(s) [[WARNING: RESETS DATABASE]]"
        ;;
        "start")
            docker-compose up --build
            echo "STARTING contoso services"
        ;;
        'stop')
            echo "STOPPING contoso services"
            docker-compose down
        ;;
        'restart-all')
          docker-compose stop contoso-api
          $FUNCNAME start
        ;;
        'restart')
          docker-compose stop contoso-api
          $FUNCNAME start
        ;;
        'purge')
			$FUNCNAME stop
            docker system prune --all --force --filter=label=base=true
        ;;
        'reset')
          $FUNCNAME purge
          $FUNCNAME start
        ;;
        *)
            echo -e "ERROR: invalid option. Try..\n$ ${FUNCNAME} help"
        ;;
    esac
}
