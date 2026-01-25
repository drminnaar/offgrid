#!/bin/bash

# ------------------------------------------------------------------------------
# Title: Check Pre-requisite Tools
#
# Purpose:
#   - To verify a list of required tools install status
#   - To verify a list of optional tools install status
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

declare -A required_tools=(
    ["docker"]="Not installed"
    ["dotnet"]="Not installed"
    ["node"]="Not installed"
    ["npm"]="Not installed"    
    ["git"]="Not installed"    
)

declare -A optional_tools=(
  ["aws"]="Not installed"
  ["az"]="Not installed"
  ["gh"]="Not installed"
  ["jq"]="Not installed"
  ["vim"]="Not installed"
  ["terraform"]="Not installed"
  ["yq"]="Not installed"
)

# Function to check if a command exists and
# whether command relates to required or optional tools
check_command() {
    if command -v "$1" >/dev/null 2>&1; then
        if [[ ${required_tools[$1]+_} ]]; then
            required_tools["$1"]="Installed"
            return
        fi

        if [[ ${optional_tools[$1]+_} ]]; then
            optional_tools["$1"]="Installed"
        fi
    fi
}

# Check each tool
echo "Checking installed tools..."

# Check Docker
check_command "docker"
if [ "${required_tools[docker]}" = "Installed" ]; then
    docker_version=$(docker --version 2>/dev/null)
    required_tools["docker"]="Installed (Version: $docker_version)"
fi

# Check .NET SDK
check_command "dotnet"
if [ "${required_tools[dotnet]}" = "Installed" ]; then
    dotnet_version=$(dotnet --version 2>/dev/null)
    required_tools["dotnet"]="Installed (Version: $dotnet_version)"
fi

# Check Node.js
check_command "node"
if [ "${required_tools[node]}" = "Installed" ]; then
    node_version=$(node --version 2>/dev/null | sed 's/^v//')
    required_tools["node"]="Installed (Version: $node_version)"
fi

# Check NPM
check_command "npm"
if [ "${required_tools[npm]}" = "Installed" ]; then
    npm_version=$(npm --version 2>/dev/null)
    required_tools["npm"]="Installed (Version: $npm_version)"
fi

# Check Terraform CLI
check_command "terraform"
if [ "${optional_tools[terraform]}" = "Installed" ]; then
    terraform_version=$(terraform --version 2>/dev/null | awk '/Terraform/ {print $2}' | sed 's/^v//')
    optional_tools["terraform"]="Installed (Version: $terraform_version)"
fi

# Check Git
check_command "git"
if [ "${required_tools[git]}" = "Installed" ]; then
    git_version=$(git --version | awk '{print $3}' 2>/dev/null)
    required_tools["git"]="Installed (Version: $git_version)"
fi

# Check Azure CLI
check_command "az"
if [ "${optional_tools[az]}" = "Installed" ]; then
    az_version=$(az --version | awk '/azure-cli/ {print $2}' 2>/dev/null)
    optional_tools["az"]="Installed (Version: $az_version)"
fi

# Check AWS CLI
check_command "aws"
if [ "${optional_tools[aws]}" = "Installed" ]; then
    aws_version=$(aws --version 2>/dev/null)
    optional_tools["aws"]="Installed (Version: $aws_version)"
fi

# Check Vim
check_command "vim"
if [ "${optional_tools[vim]}" = "Installed" ]; then
    vim_version=$(vim --version 2>/dev/null | head -n 1 | awk '{print $5}')
    optional_tools["vim"]="Installed (Version: $vim_version)"
fi

# Check jq
check_command "jq"
if [ "${optional_tools[jq]}" = "Installed" ]; then
    jq_version=$(jq --version 2>/dev/null)
    optional_tools["jq"]="Installed (Version: $jq_version)"
fi

# Check yq
check_command "yq"
if [ "${optional_tools[yq]}" = "Installed" ]; then
    yq_version=$(yq --version 2>/dev/null)
    optional_tools["yq"]="Installed (Version: $yq_version)"
fi

# Check gh
check_command "gh"
if [ "${optional_tools[gh]}" = "Installed" ]; then
    gh_version=$(gh --version 2>/dev/null | head -n 1 | awk '{print $3}')
    optional_tools["gh"]="Installed (Version: $gh_version)"
fi

# Check aws
check_command "aws"
if [ "${optional_tools[aws]}" = "Installed" ]; then
    aws_version=$(aws --version 2>/dev/null | sed 's/^aws-cli\///' | awk '{print $1}')
    optional_tools["aws"]="Installed (Version: $aws_version)"
fi

# Generate report
echo -e "\n=========================== Tool Installation Report ==========================="
echo -e "\n Required Tools:"
for tool in "${!required_tools[@]}"; do
    if [[ ${required_tools[$tool]} == *Installed* ]]; then
      echo "  ✔️  $tool: ${required_tools[$tool]}"
    else
      echo "  ❌  $tool: Not installed"
    fi
done

echo -e "\n\n Optional Tools:"
for tool in "${!optional_tools[@]}"; do
    if [[ ${optional_tools[$tool]} == *Installed* ]]; then
      echo "  ✔️  $tool: ${optional_tools[$tool]}"
    else
      echo "  ❌  $tool: Not installed"
    fi
done
echo -e "\n================================================================================"