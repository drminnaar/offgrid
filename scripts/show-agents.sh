#!/usr/bin/env bash
set -euo pipefail

start_dir="${1:-$PWD}"

if [[ ! -d "$start_dir" ]]; then
  echo "Not a directory: $start_dir" >&2
  exit 2
fi

dir="$(cd "$start_dir" && pwd)"

while true; do
  if [[ -f "$dir/agents.md" ]]; then
    echo "Agents guide: $dir/agents.md"
    echo
    cat "$dir/agents.md"
    exit 0
  fi

  parent="$(cd "$dir/.." && pwd)"
  if [[ "$parent" == "$dir" ]]; then
    break
  fi
  dir="$parent"
done

echo "No agents.md found in current or parent directories." >&2
exit 1
