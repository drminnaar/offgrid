#!/bin/sh
set -eu

MONGO_HOST=${MONGO_HOST:-mongo}
MONGO_PORT=${MONGO_PORT:-27017}
USER=${MONGO_INITDB_ROOT_USERNAME:-}
PASS=${MONGO_INITDB_ROOT_PASSWORD:-}

log() { printf "[seed] %s\n" "$*"; }

import_file() {
  file="$1"
  if [ -z "$file" ] || [ ! -f "$file" ]; then
    return 0
  fi
  log "Importing $file"
  tries=0
  while true; do
    if [ -n "$USER" ]; then
      log "Using authentication with user $USER"
      if mongoimport --host "$MONGO_HOST" --port "$MONGO_PORT" -u "$USER" -p "$PASS" --authenticationDatabase=admin --db offgrid --collection products --mode merge --jsonArray --file "$file"; then
        break
      fi
    else
      log "No authentication"
      if mongoimport --host "$MONGO_HOST" --port "$MONGO_PORT" --db offgrid --collection products --mode merge --jsonArray --file "$file"; then
        break
      fi
    fi

    tries=$((tries+1))
    log "mongoimport failed, retrying in 2s (attempt $tries)"
    sleep 2
    if [ "$tries" -ge 30 ]; then
      log "Failed to import $file after $tries attempts"
      return 1
    fi
  done
}

# If shell supports globbing check
for f in /data/db_files/*.json; do
  if [ -e "$f" ]; then
    break
  fi
  # no files matched; exit gracefully
  log "No json files found in /data/db_files"
  exit 0
done

for f in /data/db_files/*.json; do
  import_file "$f" || exit 1
done

log "Seeding complete"
exit 0
