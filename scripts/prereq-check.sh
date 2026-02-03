#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Check Pre-requisites
#
# Purpose:
#   - To verify that all the prerequisites to run infrastructure and apps/api's
#     on the local development environment are met.
#   - This script aggregates checks for:
#     - required tools
#     - host file entries
#     - environment files
#
# Prerequisites:
#   - Linux
#   - BASH
# 
# Expected Environment Variables: None
#
# Usage:
#   - chmod +x ./prereq-check.sh
#   - ./prereq-check.sh
#
# ------------------------------------------------------------------------------

readonly DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
readonly ROOT_DIR="$DIR/../"

# verify tool installation
$ROOT_DIR/scripts/tool-installation-check.sh

# verify environment setting files are created
$ROOT_DIR/scripts/env-file-check.sh

# verify host file entries
$ROOT_DIR/scripts/host-file-entry-check.sh