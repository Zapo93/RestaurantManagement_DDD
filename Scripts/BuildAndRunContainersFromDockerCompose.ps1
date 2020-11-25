###!!!RUN THIS SCRIPT IN THE DIRECTORY CONTAINING THE docker-compose file.
#build images in docker-compose file
docker-compose build
#run containers from docker-compose file. -d stands for discrete - it reduces the logs in the terminal
docker-compose up -d