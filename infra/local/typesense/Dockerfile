FROM typesense/typesense:30.1

USER root
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# The entrypoint and command are inherited from the base image
# Typesense will start normally with /opt/typesense-server