docker compose -f docker-compose-host.yml down
docker rmi rinha-be-danarrib
docker build -t rinha-be-danarrib -f Dockerfile .
docker compose -f docker-compose-host.yml up -d