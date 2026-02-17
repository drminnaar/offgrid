#!/usr/bin/env bash
set -euo pipefail

DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE_FILE="$DIR/../compose.yaml"

docker compose --file $COMPOSE_FILE --env-file "$DIR/.env" exec -it keycloak bash -ic '
  echo -e "\n"
  cd /opt/keycloak/bin || exit 1
  ./kcadm.sh config credentials \
    --server http://localhost:8080 \
    --realm master \
    --user admin \
    --password admin || true
  echo -e "\e[32mEntering container shell (type exit to return)...\e[0m\n"
  exec bash
'
