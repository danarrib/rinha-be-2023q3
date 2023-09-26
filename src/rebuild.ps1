docker compose down
docker rmi rinha-be-danarrib
docker build -t rinha-be-danarrib -f Dockerfile .
docker compose up -d