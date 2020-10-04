# Semantic

Will be using the dataset provided by Sjøfartsdirektoratet
https://github.com/Sjofartsdirektoratet/APS-Simulator/tree/master/testdata_d1

## Backend
Using docker to run a fuseki server, with a dotnet core api 

To run the fuseki server with the dataset run (Available at http://localhost:3030)
  docker-compose up
 
### To add you own dataset or files to the fuseki server
There are 2 default datasets APS and ET.

Defined in the docker-compose.yml file
      - FUSEKI_DATASET_1=APS
      - FUSEKI_DATASET_2=ET
      
The dataset are populated from the datasets directory in the project (this will change to a github repo in future versions)
Data loaded into the dataset are defined in the scripts/load_datasets.sh file
APS in line 19 to 23
ET in line 26 to 30

Start the webapi (Available at http://localhost:5000 or https://localhost:5001)


## Frontend
TODO make a test app for frontend
