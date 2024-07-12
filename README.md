Motter Backend Challenge - API de Aluguel de Motos e Entregadores
Este projeto é uma solução para o desafio backend da Motter, uma API REST para gerenciar o aluguel de motos e entregadores, permitindo que entregadores habilitados aluguem motos para realizar entregas.

Tecnologias Utilizadas
.NET 8: Framework para desenvolvimento de aplicações web.
C#: Linguagem de programação.
Entity Framework Core: ORM para acesso a dados.
PostgreSQL: Banco de dados relacional para armazenar dados estruturados.
MongoDB: Banco de dados NoSQL para armazenar dados não estruturados (imagens da CNH).
RabbitMQ: Sistema de mensageria para comunicação assíncrona entre componentes.
FluentValidation: Biblioteca para validação de dados.
AutoMapper: Biblioteca para mapeamento de objetos.
MediatR: Biblioteca para implementação do padrão Mediator e CQRS.
Swagger/OpenAPI: Para documentação da API.
Arquitetura
CQRS (Command Query Responsibility Segregation): Separa as operações de leitura (queries) das operações de escrita (commands).
MediatR: Usado como mediador entre comandos, queries e seus respectivos handlers.
Domain-Driven Design (DDD): Organiza o código em torno do domínio do problema, com foco nas entidades, regras de negócio e interações.
Clean Architecture: Separa as camadas da aplicação em responsabilidades distintas, facilitando a manutenção e evolução do código.
Como Rodar a Aplicação
Pré-requisitos:

Docker: Certifique-se de ter o Docker instalado e em execução.
.NET 8 SDK: Instale o .NET 8 SDK.
PostgreSQL: Instale o PostgreSQL localmente ou use um container Docker.
RabbitMQ: Instale o RabbitMQ localmente ou use um container Docker.
Passos:

Clonar o Repositório:

Bash
git clone https://github.com/seu-usuario/mottu-backend-challenge.git
cd mottu-backend-challenge
Use o código com cuidado.

Configurar o Banco de Dados:

PostgreSQL:
Crie um banco de dados chamado mottu_db.
Atualize a string de conexão no arquivo appsettings.json com as credenciais do seu banco de dados.
MongoDB:
Configure a string de conexão no arquivo appsettings.json com as credenciais do seu banco de dados MongoDB.
Configurar o RabbitMQ:

Atualize as configurações do RabbitMQ no arquivo appsettings.json com as credenciais e o host corretos.
Rodar as Migrations:

Bash
dotnet ef database update --project Motter.Infrastructure
Use o código com cuidado.

Executar a Aplicação:

Bash
dotnet run --project Motter.API
Use o código com cuidado.

Acessar a Documentação da API:

Abra o navegador e acesse http://localhost:5000/swagger (ou a porta que você configurou) para visualizar a documentação da API no Swagger UI.
Docker Compose (Opcional):

Você pode usar o Docker Compose para facilitar a execução da aplicação e dos serviços necessários (PostgreSQL e RabbitMQ):

Criar o build:

Bash
docker build -t mottu-api .
Use o código com cuidado.

Executar o projeto:

Bash
docker-compose up
Use o código com cuidado.

Observações:

Certifique-se de ter as portas 5432 (PostgreSQL), 5672 (RabbitMQ) e a porta da sua aplicação (ex: 5000) livres.
