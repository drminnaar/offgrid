CREATE TABLE
    customers.customer_change (
        "created_date" TIMESTAMPTZ NOT NULL,
        "customer_change_id" UUID NOT NULL,
        "customer_id" UUID NOT NULL,
        "changed_by" TEXT NOT NULL,
        "changed_at" TIMESTAMPTZ NOT NULL,
        "changes" JSONB NOT NULL,
        CONSTRAINT "pk_customers_customerchange_customerchangeid" PRIMARY KEY ("customer_change_id")
    );

-- define indexes
CREATE INDEX "ix_customers_customerchange_customerid" ON customers.customer_change ("customer_id");

CREATE INDEX "ix_customers_customerchange_changedby" ON customers.customer_change ("changed_by");