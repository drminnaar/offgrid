#!/bin/bash
set -euo pipefail

DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE_FILE="$DIR/../compose.yaml"

docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" exec postgres sh -c \
  'PGPASSWORD="$POSTGRES_PASSWORD" \
  psql \
  -h postgres \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB"'