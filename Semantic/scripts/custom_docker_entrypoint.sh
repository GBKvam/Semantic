#!/bin/bash
echo -e "\n*** Starting Fuseki with datasets for SDIR Simulator ***\n\n"

/datasets/scripts/load_datasets.sh &
. /docker-entrypoint.sh /jena-fuseki/fuseki-server