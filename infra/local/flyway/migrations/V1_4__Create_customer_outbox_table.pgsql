CREATE TABLE
    customers.customer_outbox_message (
        created_at TIMESTAMPTZ NOT NULL,
        id UUID NOT NULL,
        event_type_id VARCHAR(255) NOT NULL,
        event_type VARCHAR(255) NOT NULL,
        payload JSONB NOT NULL,
        occurred_at TIMESTAMPTZ NOT NULL,
        processed_at TIMESTAMPTZ,
        error TEXT,
        retry_count INT NOT NULL DEFAULT 0,
        next_retry_at TIMESTAMPTZ,
        is_deadletter BOOLEAN NOT NULL DEFAULT FALSE,
        CONSTRAINT "pk_customers_customeroutboxmessage_id" PRIMARY KEY ("id")
    );

-- define indexes
CREATE INDEX "ix_customers_customeroutboxmessage_occurredat" ON customers.customer_outbox_message ("occurred_at")
WHERE
    processed_at IS NULL;

CREATE INDEX "ix_customers_customeroutbox_nextretryat" ON customers.customer_outbox_message ("next_retry_at")
WHERE
    processed_at IS NULL;