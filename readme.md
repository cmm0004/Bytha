## Dev Setup
you will need:
- docker desktop

run docker-compose build, docker-compose up
that will start neo4j, user/password is in the docker-compose.yaml

currently theres no data seed, so youll have to go to the local neo4j that starts up at localhost:7474/browser and log in with the docker username/pass and add some data by hand. add some rooms and some stuff nodes

after you should be able to open in vs, launch from the bytha launch project and go to /playground to start making graph querys

## Data schema

2 models - Room and Stuff
Rooms can be adjacentto other rooms
Stuff can be by other stuff "stuff#1 -> bythe -> stuff#2"

relationships not fleshed out yet.

## long term goals 

voice powered addition of nodes.

