#!/bin/bash

# makes script exit on errors (-e) and on references to unset variables (-u)
set -eu

# ensure Docker CLI is installed
if ! command -v docker >/dev/null; then
    echo "\n⚠️  Docker CLI not found; please install Docker" >&2
    exit 1
fi

DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE_FILE="$DIR/../compose.yaml"

source "$DIR/.env"

echo -e "\n"

# Check if a parameter is provided
if [ $# -eq 0 ]; then
    echo "Error: No parameter provided"
    echo "Usage: $0 <command> [args...]"
    echo "Available commands:"
    echo "  up      - Start services (docker compose up --detach)"
    echo "  down    - Stop and remove services (docker compose down --volumes --remove-orphans)"
    echo "  logs    - View logs (docker compose logs [-f] [service])"
    echo "  ps      - List containers (docker compose ps)"
    echo "  exec    - Execute command in container (docker compose exec <service> <cmd>)"
    echo -e "\n"
    exit 1
fi

# Get the command parameter and shift it off the argument list
command="$1"
shift

# Define valid commands
valid_commands=("up" "down" "logs" "ps" "exec")

# Check if the command is valid
is_valid=0
for valid_command in "${valid_commands[@]}"; do
    if [ "$command" = "$valid_command" ]; then
        is_valid=1
        break
    fi
done

# If command is not valid, show error and usage
if [ $is_valid -eq 0 ]; then
    echo "Error: Invalid command '$command'"
    echo "Usage: $0 <command> [args...]"
    echo "Available commands: ${valid_commands[*]}"
    echo -e "\n"
    exit 1
fi

# Execute the appropriate docker compose command
case "$command" in
    "up")
        docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" up --detach "$@"
        ;;
    "down")
        docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" down --volumes --remove-orphans "$@"
        ;;
    "logs")
        docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" logs "$@"
        ;;
    "ps")
        docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" ps "$@"
        ;;
    "exec")
        docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" exec "$@"
        ;;
esac

echo -e "\n"