#!/bin/bash
set -e
set -o pipefail

# Sends the applicable `.ttl` files to the Fuseki server

script_name=$0
log() {
  echo -e "[$script_name $(date --rfc-3339=seconds)] $*"
}

cd /datasets || (log "datasets directory missing!"; exit 1)
log "Waiting on server to start up..." && sleep 10


error_occured=false

log "\n\n-----------------\nSending APS dataset from datasets/aps directory to Fuseki\n"
for f in ./datasets/APS/*.ttl; do
  log "Sending $f..."
  curl --silent --fail -X POST -H "Content-Type: text/turtle" --data-binary "@$f" \
    http://localhost:3030/APS/data?default || { error_occured=true; log "ERROR: Failed to send $f to Fuseki"; }
done

log "\n\n-----------------\nSending ET dataset from datasets/aps directory to Fuseki\n"
for f in ./datasets/ET/*.ttl; do
  log "Sending $f..."
  curl --silent --fail -X POST -H "Content-Type: text/turtle" --data-binary "@$f" \
    http://localhost:3030/ET/data?default || { error_occured=true; log "ERROR: Failed to send $f to Fuseki"; }
done


if [[ "$error_occured" = true ]]; then
  log "ERROR: One or more datasets were not successfully sent"
  exit 1
fi

log "Completed successfully"