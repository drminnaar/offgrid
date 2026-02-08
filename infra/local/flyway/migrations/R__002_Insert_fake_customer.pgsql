INSERT INTO
  customers.customer (
    created_date,
    updated_date,
    deleted_date,
    customer_id,
    keycloak_user_id,
    customer_number,
    last_name,
    first_name,
    email,
    status,
    version
  )
VALUES
  (
    NOW(), -- created_date
    NULL, -- updated_date
    NULL, -- deleted_date
    '29417bdb-fd94-4533-8983-02a3c6904102', -- customer_id
    'kc-user-123', -- keycloak_user_id
    'CUST-0001', -- customer_number
    'Doe', -- last_name
    'Jane', -- first_name
    'jane.doe@example.com', -- email
    'Active', -- status
    '\x'::bytea -- version (defaults to empty if omitted)
  );