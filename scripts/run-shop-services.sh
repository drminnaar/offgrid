#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Run Shop services
#
# Purpose:
#   - To run the shop applications, API's, and related services
#
# Prerequisites:
#   - Linux
#   - BASH
# 
# Expected Environment Variables: None
#
# Usage:
#   - chmod +x ./run-shop-services.sh
#   - ./run-shop-services.sh
#
# ------------------------------------------------------------------------------

readonly DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
readonly ROOT_DIR="$DIR/../"

# start shop services
docker compose --file $ROOT_DIR/apps/shop/infra/compose.yaml up -d

echo -e "\nShop services are starting up. You can check their status below:\n"

# show shop services status
docker compose --file $ROOT_DIR/apps/shop/infra/compose.yaml ps
