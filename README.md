# DeskFlow API — Gestão de Chamados e Helpdesk de TI

## Sobre o Projeto

A **DeskFlow API** é uma Web API RESTful construída em .NET Core 10 utilizando Entity Framework Core e SQL Server. O sistema automatiza o gerenciamento de chamados de suporte técnico, histórico de interações e acompanhamento de status do atendimento.

## Tecnologias Utilizadas

- .NET Core 10 / Web API
- Entity Framework Core 10
- SQL Server
- Swagger / OpenAPI

## Como Executar a Aplicação

### Pré-requisitos

- .NET SDK 10 (ou superior)
- SQL Server em execução (LocalDB, SQL Server Express ou Docker)
- Ferramenta do EF Core (caso ainda não tenha instalada):

```bash
dotnet tool install --global dotnet-ef
```

### Passo a Passo

1. Clone este repositório:

```bash
git clone https://github.com/mmdanova/DeskFlowAPI.git
```

2. Acesse a pasta do projeto:

```bash
cd DeskFlowAPI
```

3. Configure a Connection String no arquivo `appsettings.json` de acordo com o seu SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DeskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Exemplos: LocalDB → `Server=(localdb)\\MSSQLLocalDB;...` · SQL Server padrão/Docker → `Server=localhost;...`

4. Execute as Migrations para criar a estrutura no banco de dados (já inclui os dados de exemplo):

```bash
dotnet ef database update
```

5. Execute a API:

```bash
dotnet run
```

6. Acesse a documentação do Swagger para testar os endpoints:

```
http://localhost:5059/swagger
```

> Para rodar com HTTPS: `dotnet run --launch-profile https` e acesse `https://localhost:7281/swagger`.

## Dados de Exemplo

Ao executar `dotnet ef database update`, o banco é populado com os seguintes registros para testes:

**Categorias**

| Id | Nome | Em uso |
|---|---|---|
| 1 | Hardware | Sim |
| 2 | Rede e Internet | Sim |
| 3 | Software | Sim |
| 4 | Acessos e Senhas | Sim |
| 5 | Impressoras | Não (pode ser excluída) |
| 6 | Telefonia | Não (pode ser excluída) |

**Chamados**

| Id | Título | Status | Categoria | Interações |
|---|---|---|---|---|
| 1 | Notebook não liga | EmAndamento | Hardware | 2 |
| 2 | Wi-Fi caindo na sala de reuniões | Aberto | Rede e Internet | 0 |
| 3 | Instalação do pacote Office | Aberto | Software | 1 |
| 4 | Senha do e-mail bloqueada | Fechado | Acessos e Senhas | 1 |
| 5 | Monitor com listras na tela | EmAndamento | Hardware | 0 |

## Endpoints

### Categorias

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/categorias` | Lista todas as categorias |
| GET | `/api/categorias/{id}` | Busca uma categoria pelo Id |
| POST | `/api/categorias` | Cadastra uma categoria |
| PUT | `/api/categorias/{id}` | Altera o nome de uma categoria |
| DELETE | `/api/categorias/{id}` | Remove uma categoria (somente se não tiver chamados) |

### Chamados

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/chamados?status=&prioridade=&categoriaId=` | Lista chamados com filtros opcionais e combináveis |
| GET | `/api/chamados/{id}` | Detalhes do chamado com categoria e interações |
| POST | `/api/chamados` | Abre um novo chamado |
| PATCH | `/api/chamados/{id}/iniciar` | Inicia o atendimento (Aberto → EmAndamento) |
| PATCH | `/api/chamados/{id}/encerrar` | Encerra o chamado (exige solução) |
| POST | `/api/chamados/{id}/interacoes` | Adiciona uma interação (não permitido em chamado Fechado) |

### Exemplos de corpo das requisições

**POST /api/categorias** e **PUT /api/categorias/{id}**

```json
{ "nome": "Servidores" }
```

**POST /api/chamados** — `prioridade`: 1 = Baixa, 2 = Media, 3 = Alta

```json
{
  "titulo": "Impressora não imprime",
  "descricao": "A impressora do 3º andar não responde.",
  "prioridade": 2,
  "solicitanteNome": "Fernanda Costa",
  "categoriaId": 5
}
```

**PATCH /api/chamados/{id}/encerrar**

```json
{ "solucao": "Driver reinstalado e impressora reconfigurada." }
```

**POST /api/chamados/{id}/interacoes**

```json
{ "autor": "Suporte - Marcos", "mensagem": "Equipamento em análise." }
```

**Filtros (query string)** — `status`: Aberto, EmAndamento, Fechado · `prioridade`: Baixa, Media, Alta

```
GET /api/chamados?status=Aberto&prioridade=Alta&categoriaId=1
```

## Ciclo de Vida do Chamado

- **Aberto**: Chamado registrado pelo solicitante.
- **EmAndamento**: Suporte em atendimento ao chamado.
- **Fechado**: Chamado encerrado com texto de solução e data de conclusão.

## Arquitetura em Camadas

- **Controllers**: Recebem as requisições HTTP e definem os Status Codes.
- **Services**: Contêm as regras de negócio e validação dos status.
- **Repositories**: Executam comandos e consultas de banco via EF Core.
- **Middlewares**: Tratamento e padronização de erros globais da API.

Todos os erros retornam um JSON padronizado, sem stack trace:

```json
{ "erro": "Chamado Id : 99 não encontrado" }
```

## Vídeo de Apresentação

[Clique aqui para assistir ao vídeo de demonstração do projeto](https://link-do-seu-video.com)
