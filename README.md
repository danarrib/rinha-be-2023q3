# Rinha de Backend 2023 Q3 - Daniel Ribeiro

A [Rinha de Backend 2023 Q3](https://github.com/zanfranceschi/rinha-de-backend-2023-q3) foi uma gincana organizada pelo [Francisco Zanfranceschi](https://github.com/zanfranceschi) entre 28 de Julho e 25 de Agosto de 2023.

Eu só tomei conhecimento do evento depois que ele já tinha acabado, mas decidir criar um projeto para poder comparar os resultados com o dos outros participantes.

Esta versão possui as seguintes características:
* .NET 6
* EFCore 6
* Code-first
* Banco de dados Postgres
* Sem cache de nenhum tipo

Ou seja: É a API mais simples que se pode imaginar.

O objetivo obviamente não é figurar entre os melhores resultados da competição, mas sim ter uma ideia do quão ruim uma API não-otimizada pode ser comparada a uma API extremamente otimizada.

## Como rodar

Instalar o [Docker](https://www.docker.com/) se você ainda não tiver.

Clonar este repositório: 
```
git clone https://github.com/danarrib/rinha-be-2023q3.git
```

Navegar até o diretório ```src```:
```
cd rinha-br-2023q3
cd src
```

Criar a imagem da API usando o seguinte comando no diretório ```src``` do repositório:
```
docker build -t rinha-be-danarrib -f Dockerfile .
```

Subir os containers todos (duas instâncias da API, o Nginx e o Postgres):
```
docker-compose up -d
```

Com os containers todos rodando, você poderá acessar a documentação online da API em http://localhost:9999

Para remover os containers todos:
```
docker-compose down
```

E para excluir a imagem da API:
```
docker rmi rinha-be-danarrib
```