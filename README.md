# Desafio Backend

## Sobre este projeto

Este projeto exemplifica a criação de uma API em .Net Core fazendo uso de boas práticas para resolver um problema simples de agregação e disponibilização de dados.

## Problema

Os clientes têm custodia em vários tipos de investimentos, que vem de serviços distintos, 
para isso devemos consolidar estes dados e devolver para canais diversos.
A seguir temos 3 endpoints para consulta de valores:

[Tesouro Direto](http://www.mocky.io/v2/5e3428203000006b00d9632a)

[Renda Fixa](http://www.mocky.io/v2/5e3429a33000008c00d96336)

[Fundos](http://www.mocky.io/v2/5e342ab33000008c00d96342)


## Resultado

Um endpoint que retorna o valor total do investimento do cliente e lista dos seus investimentos. 
Cada item da lista contém seu valor unitário, cálculo de IR conforme regra abaixo e valor 
calculado caso o cliente queira resgatar seu investimento na data. 
O contrato esperado para o retorno é o seguinte:

    {
        "valorTotal": 829.68,
        "investimentos": [
                        {
                            "nome": "Tesouro Selic 2025",
                            "valorInvestido": 799.4720,
                            "valorTotal": 829.68,
                            "vencimento": "2025-03-01T00:00:00",
                            "Ir": 3.0208,
                            "valorResgate": 705.228
                        }
        ]
    }


## Regras para cálculos:

### IR:

Taxa sobre a rentabilidade*
    1.Tesouro Direto 10%
    2.LCI 5%
    3.Fundos 15%

    *Rentabilidade = Valor Total – Valor Investido (Pode ser negativo)


### Cálculo para resgate:

    *Investimento com mais da metade do tempo em custódia: Perde 15% do valor investido
    *Investimento com até 3 meses para vencer: Perde 6% do valor investido
    *Outros: Perde 30% do valor investido

## Tecnologias utilizadas
    .Net Core 3.1
    .Swagger
    .HealthChecks
    .Docker
    .Polly
    .

## Patterns abordados
    .DDD
    .SOLID
    .CQRS
    .

## Como executar
### Pré requisitos
. É necessário estar em um ambiente com o Docker instalado e executando.
. Necessáiro o Redis para o cache

###Executando os testes
Para executar os testes na linha de comando, executar o comando abaixo dentro da pasta Code.
  dotnet test Investimentos.Custodia.Test

### Executando o Redis apartado
Dentro da pasta Code, executar o comando docker
. Para iniciar o redis
  docker run --name redis -p "6379:6379" -d redis redis-server --requirepass s3nh@s3gur4!
. Para parar o resis
  docker rm redis -f

### Iniciando a aplicação 
Dentro da pasta Code, executar o comando docker
  docker-compose up -d

### Parando a aplicação
  docker-compose down

### Executando o deploy com o Redis
. Iniciar
  docker-compose -f docker-compose.redis.yml up -d
.parar
  docker-compose -f docker-compose.redis.yml down

#Endpoints executando localmente
##HealthCheck
    http://localhost:5000/api/v1/healthcheck

##Swagger
    http://localhost:5000/swagger
    http://localhost:5000/api/v1/docs/swagger.json