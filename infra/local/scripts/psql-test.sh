#!/bin/bash
set -euo pipefail

DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COMPOSE_FILE="$DIR/../compose.yaml"

clear
echo -e "\n🚀 Testing database connection..."
echo -e "\n"

# Run SQL commands to test connection
docker compose --file "$COMPOSE_FILE" --env-file "$DIR/.env" exec postgres sh -c '
PGPASSWORD="$POSTGRES_PASSWORD" psql \
  -h postgres \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB" \
  <<-EOSQL
    -- Create test schema
    CREATE SCHEMA IF NOT EXISTS test_schema;
    
    -- Create test table
    CREATE TABLE IF NOT EXISTS test_schema.users (
        id SERIAL PRIMARY KEY,
        username VARCHAR(50) NOT NULL,
        email VARCHAR(100) NOT NULL,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );
    
    -- Insert sample data
    INSERT INTO test_schema.users (username, email) VALUES
        ('"'test_user1'"', '"'user1@example.com'"'),
        ('"'test_user2'"', '"'user2@example.com'"'),
        ('"'test_user3'"', '"'user3@example.com'"');
    
    -- Verify data was inserted
    SELECT * FROM test_schema.users;
    
    -- Cleanup
    DROP TABLE test_schema.users;
    DROP SCHEMA test_schema;
    
    -- Confirm cleanup
    SELECT '"'Database connection test completed successfully!'"' AS result;
EOSQL
'

echo -e "✅ Database connection test completed!"
echo -e "\n"