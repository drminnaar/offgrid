#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Run Infrastructure Services (postgres, keycloak, flyway, etc)
#
# Purpose:
#   - To run all relevant infrastructure services locally
#
# Prerequisites:
#   - Linux
#   - BASH
# 
# Expected Environment Variables: None
#
# Usage:
#   - chmod +x ./run-infra-services.sh
#   - ./run-infra-services.sh
#
# ------------------------------------------------------------------------------

readonly DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
readonly ROOT_DIR="$DIR/../"

# start infrastructure stack
$ROOT_DIR/infra/local/scripts/compose.sh up

# run flyway migrations
$ROOT_DIR/infra/local/scripts/flyway.sh migrate