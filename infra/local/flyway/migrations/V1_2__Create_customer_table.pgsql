CREATE TABLE
    customers.customer (
        "created_date" TIMESTAMPTZ NOT NULL,
        "updated_date" TIMESTAMPTZ,
        "deleted_date" TIMESTAMPTZ,
        "customer_id" UUID NOT NULL,
        "keycloak_user_id" TEXT NOT NULL,
        "customer_number" TEXT NOT NULL,
        "last_name" TEXT NOT NULL,
        "first_name" TEXT NOT NULL,
        "email" TEXT,
        "status" VARCHAR(20) NOT NULL,
        "version" bytea NOT NULL DEFAULT '\x',
        CONSTRAINT "pk_customers_customer_customerid" PRIMARY KEY ("customer_id")
    );

-- define unique indexes
CREATE UNIQUE INDEX "ux_customers_customer_email" ON customers.customer ("email");

CREATE UNIQUE INDEX "ux_customers_customer_keycloakuserid" ON customers.customer ("keycloak_user_id");

CREATE UNIQUE INDEX "ux_customers_customer_customernumber" ON customers.customer ("customer_number");

-- define indexes
CREATE INDEX "ix_customers_customer_status" ON customers.customer (status);

CREATE INDEX "ix_customers_customer_lstatus" ON customers.customer (lower(status));