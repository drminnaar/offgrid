#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Check Pre-requisite Host file entries
#
# Purpose:
#   - To verify existence of a list of required host file entries
#
# Prerequisites:
#   - Linux
#   - BASH
# 
# Expected Environment Variables: None
#
# Usage:
#   - chmod +x ./host-file-entry-check.sh
#   - ./host-file-entry-check.sh
#
# ------------------------------------------------------------------------------

HOSTS_FILE=""
readonly KEYCLOAK_ENTRY="127.0.0.1 keycloak"

declare -A required_host_file_entries=(
    ["$KEYCLOAK_ENTRY"]="Not Found"
)

check_hosts_entry() {
    local entry="$1"
    
    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
        HOSTS_FILE="/c/Windows/System32/drivers/etc/hosts"
    else
        HOSTS_FILE="/etc/hosts"
    fi

    if grep -qE "^${entry}([[:space:]]|$)" "$HOSTS_FILE" 2>/dev/null; then
        required_host_file_entries["$entry"]="Found"
        return 0
    else
        required_host_file_entries["$entry"]="Not Found"
        return 1
    fi
}

# Check entries and get hosts file path
check_hosts_entry "$KEYCLOAK_ENTRY"
readonly HOSTS_FILE

# Generate report
echo -e "\n=========================== Host File Entries Report ==========================="
echo -e "\n Required Host File Entries:"


for entry in "${!required_host_file_entries[@]}"; do
    echo ${required_host_file_entries[$entry]} >&2    
    if [[ ${required_host_file_entries[$entry]} == Found ]]; then
        echo "  ✔️  $entry: ${required_host_file_entries[$entry]}"
    else
        echo "  ❌  $entry: ${required_host_file_entries[$entry]}"
        echo "      Please add this entry to your hosts file located at: $HOSTS_FILE"
    fi
done
