#!/bin/bash

MODE=$1

if [[ "$MODE" = "ci" ]]; then
    docker compose -f .docker/db-ci.yml down --volumes
else
    docker compose -f .docker/db.yml down --volumes
fi

docker compose \
    -f .docker/bewoning-data-service.yml \
    down --volumes