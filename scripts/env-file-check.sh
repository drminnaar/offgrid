#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Check Pre-requisite Environment Files
#
# Purpose:
#   - To verify existence of a list of required environment files
#
# Prerequisites:
#   - Linux
#   - BASH
# 
# Expected Environment Variables: None
#
# Usage:
#   - chmod +x ./env-file-check.sh
#   - ./env-file-check.sh
#
# ------------------------------------------------------------------------------

readonly DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
readonly ROOT_FILE="$DIR/../"

readonly INFRA_LOCAL_SCRIPT_ENV_KEY="/infra/local/scripts/.env"
readonly INFRA_LOCAL_SCRIPT_ENV_PATH="infra/local/scripts/.env"

readonly APPS_SHOPAPP_ENV_KEY="/apps/shop-app/.env"
readonly APPS_SHOPAPP_ENV_PATH="apps/shop-app/.env"

readonly SERVICES_SHOPAPI_REQUESTS_ENV_KEY="/services/shop/requests/.env"
readonly SERVICES_SHOPAPI_REQUESTS_ENV_PATH="services/shop/requests/.env"


declare -A required_environment_files=(
    ["$INFRA_LOCAL_SCRIPT_ENV_KEY"]="Not created"
    ["$APPS_SHOPAPP_ENV_KEY"]="Not created"
    ["$SERVICES_SHOPAPI_REQUESTS_ENV_KEY"]="Not created"
)

# Function to check if a file exists
check_file() {
    local file_path="$1"
    local key="$2"
    
    if [ -f "$file_path" ]; then
        required_environment_files["$key"]="Created"
    fi
}

check_file "$ROOT_FILE/$INFRA_LOCAL_SCRIPT_ENV_PATH" "$INFRA_LOCAL_SCRIPT_ENV_KEY"
check_file "$ROOT_FILE/$APPS_SHOPAPP_ENV_PATH" "$APPS_SHOPAPP_ENV_KEY"
check_file "$ROOT_FILE/$SERVICES_SHOPAPI_REQUESTS_ENV_PATH" "$SERVICES_SHOPAPI_REQUESTS_ENV_KEY"

# Generate report
echo -e "\n=========================== Environment Files Report ==========================="
echo -e "\n Required Environment Files:"

for file in "${!required_environment_files[@]}"; do
    if [[ ${required_environment_files[$file]} == *Created* ]]; then
        echo "  ✔️  $file: ${required_environment_files[$file]}"
    else
        echo "  ❌  $file: Not created"
    fi
done

# Check if any files are missing
any_missing=false
for file in "${!required_environment_files[@]}"; do
    if [[ ${required_environment_files[$file]} != *Created* ]]; then
        any_missing=true
        break
    fi
done

if [ "$any_missing" = true ]; then
    echo -e "\n⚠️  Some environment files are missing."
    read -p "Would you like to create them from example files? (y/n) " -n 1 -r
    echo -e "\n"
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        for file in "${!required_environment_files[@]}"; do
            if [[ ${required_environment_files[$file]} != *Created* ]]; then
                example_file="$ROOT_FILE${file}.example"
                target_file="$ROOT_FILE${file#/}"
                if [ -f "$example_file" ]; then
                    cp "$example_file" "$target_file"
                    echo "✔️  Created: $file"
                else
                    echo "⚠️  Example file not found: ${file}.example"
                fi
            fi
        done
    fi
fi

