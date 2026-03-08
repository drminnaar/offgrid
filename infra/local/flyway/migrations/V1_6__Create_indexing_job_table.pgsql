CREATE TABLE
    product_search.indexing_job (
        id UUID NOT NULL,
        created_at TIMESTAMPTZ NOT NULL,
        completed_at TIMESTAMPTZ,
        is_indexing BOOLEAN NOT NULL DEFAULT FALSE,
        error TEXT,
        retry_count INT NOT NULL DEFAULT 0,
        next_retry_at TIMESTAMPTZ,
        is_deadletter BOOLEAN NOT NULL DEFAULT FALSE,
        CONSTRAINT "pk_productsearch_indexingjob_id" PRIMARY KEY ("id")
    );

-- define indexes
CREATE INDEX "ix_productsearch_indexingjob_completedat" ON product_search.indexing_job ("completed_at")
WHERE
    completed_at IS NULL;

CREATE INDEX "ix_productsearch_indexingjob_nextretryat" ON product_search.indexing_job ("next_retry_at")
WHERE
    completed_at IS NULL;