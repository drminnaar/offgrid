FROM mongo:8-noble

WORKDIR /data/db_files
COPY ./data/*.json .

COPY ./seed.sh /usr/local/bin/seed.sh
RUN chmod +x /usr/local/bin/seed.sh

ENTRYPOINT ["/usr/local/bin/seed.sh"]