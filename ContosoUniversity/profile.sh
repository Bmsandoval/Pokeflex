#!/bin/bash

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
- reset: purges and rebuilds contoso's docker container(s) [[WARNING: RESETS DATABASE]]"
    ;;
    'start')
      case "${2}" in
        'api') docker-compose up --build contoso-api ;;
        'db') docker-compose up -d contoso-db ;;
        'all'|'') docker-compose up ;;
      esac
      echo "STARTING contoso services"
    ;;
    'stop')
      echo "STOPPING contoso services"
      case "${2}" in
        'api') docker-compose down api ;;
        'db') docker-compose down db ;;
        'all'|'') docker-compose down ;;
      esac
    ;;
    'restart')
      case "${2}" in
        'api') ${FUNCNAME[0]} down api && ${FUNCNAME[0]} start api ;;
        'db') ${FUNCNAME[0]} down db && ${FUNCNAME[0]} start db ;;
        'all'|'') ${FUNCNAME[0]} down && ${FUNCNAME[0]} start ;;
      esac
    ;;
    'purge')
      ${FUNCNAME[0]} stop
      docker system prune --all --force --filter=label=base=true
    ;;
    'reset')
      ${FUNCNAME[0]} purge
      docker-compose build
    ;;
    *)
      echo -e "ERROR: invalid option. Try..\n$ ${FUNCNAME[0]} help"
    ;;
  esac
}
