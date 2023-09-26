# Rinha de Backend 2023 Q3 - Daniel Ribeiro

A [Rinha de Backend 2023 Q3](https://github.com/zanfranceschi/rinha-de-backend-2023-q3) foi uma gincana organizada pelo [Francisco Zanfranceschi](https://github.com/zanfranceschi) entre 28 de Julho e 25 de Agosto de 2023.

Eu só tomei conhecimento do evento depois que ele já tinha acabado, mas decidi criar um projeto para poder comparar os resultados com os dos outros participantes.

Esta versão possui as seguintes características:
* Escrita em C# - .NET 6
* Entity Framework Core 6 (Code first)
* Banco de dados Postgres
* Sem cache de nenhum tipo
* Sem usar Async/Await ou qualquer tipo de paralelismo
* Nenhum tipo de otimização avançada

Ou seja: É a API em C# mais simples que se pode imaginar. Do tipo que um desenvolvedor iniciante poderia fazer.

O objetivo obviamente não é figurar entre os melhores resultados da competição, mas sim ter uma ideia do quão ruim uma API não-otimizada pode ser quando comparada a uma API extremamente otimizada.

Escolhi o .NET 6 e o EFCore 6 por serem as versões LTS (Long Term Support), que são as mais usadas em ambientes corporativos. As versões 7 e 8 do .NET e do EFCore possuem diversas melhorias de desempenho, mas a versão 7 não é LTS e a 8 ainda não foi oficialmente lançada.

## Como rodar

Instalar o [Docker](https://www.docker.com/) se você ainda não tiver.

Clonar este repositório: 
```
git clone https://github.com/danarrib/rinha-be-2023q3.git
```

Navegar até o diretório ```src```:
```
cd rinha-be-2023q3
cd src
```

Criar a imagem da API usando o seguinte comando no diretório ```src``` do repositório:
```
docker build -t rinha-be-danarrib -f Dockerfile .
```

Subir os containers todos (duas instâncias da API, o Nginx e o Postgres):
```
docker compose up -d
```

Com os containers todos rodando, você poderá acessar a documentação online da API em http://localhost:9999/swagger

Para remover os containers todos:
```
docker compose down
```

E para excluir a imagem da API:
```
docker rmi rinha-be-danarrib
```

## Resultados

Usando o [stress test](https://github.com/zanfranceschi/rinha-de-backend-2023-q3/tree/main/stress-test) usado para testar as APIs da Rinha, esta API apresenta resultados na faixa de **38.000** registros.

A título de referência API vencedora teve **44.936**. Se estivesse competindo, esta API teria ficado em 10º lugar.

## Possíveis melhorias

### Docker com network-mode = host

A propriedade ```network_mode``` dos serviços descritos no ```docker-compose.yml``` controla como cada serviço irá usar a rede. Por padrão, o valor desta propriedade é ```bridge```, que faz com que o docker crie uma rede privada para os serviços descritos no arquivo, de modo que eles possam enxergar uns aos outros sem expor para o host as portas que expoe nesta rede virtual interna.

Porém, este modo limita bastante a capacidade de resposta da rede para muitas requisições simultâneas. Acima de 1000 usuários simultâneos o docker começa a não mais ser capaz de direcionar corretamente as requisições externas.

![image](https://github.com/danarrib/rinha-be-2023q3/assets/17026744/87c6937f-9c6b-4909-a288-d9477b3a2ca8)

A solução para isso é colocar o ```network_mode``` com valor ```host```, o que faria com que os serviços descritos passassem a expor suas portas diretamente na rede host, eliminando esta camada intermediária do Docker.

O problema é que [este valor só funciona no Linux](https://docs.docker.com/network/network-tutorial-host/#prerequisites). No Docker para Windows ou para Mac, não é possível utilizar este valor.

### Ajuste fino do nginx.conf

O arquivo nginx.conf permite configurar como o servidor nginx, que é o proxy reverso que fica na frente das instâncias da API, direcionando o tráfego no esquema round-robin.

