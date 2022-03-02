# ContaCorrenteAPI

Sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos.

O projeto tem uma camada:

- Uma API RESTful que foi desenvolvida em ASP.NET Core 5 com arquitetura DDD (Domain Driven Design), sendo o banco de dados utilizado MySQL com EF Core como ORM;


## Rodando com docker-compose

Para executar o projeto com docker-compose basta executar o seguinte comando a seguir. Após a execução a aplicação estará disponível na porta 5000.
http://localhost:5000/swagger/index.html

### Build

- docker-compose up -d

### Executar em caso de alteração

- docker-compose up --build

o comando acima força um novo build com as alterações realizadas.

### Drop

- docker-compose down

## Rodando no Visual Studio

Para testar o projeto no Visual Studio basta seguir os seguintes passos:

- Definir o projeto ContaCorrente.API.Tests com a opção **Set as Startup Project** nas propriedades da solution;
- Definir os dados do servidor e DB a ser utilizado no arquivo **appsettings.json** dentro do projeto ContaAPI.Application;
- No **Package Manager Console** definir como **Default Project** o projeto ContaCorrente.Infra.Data e executar o comando **Update-Database**;
- Clicar em **Start** (F5).

Ao executar estes passos uma janelaa irá se abrir com o swagger da API.

- Swagger (API REST) - https://localhost:44340/swagger/index.html

## API REST

### Conta Corrente - Endpoints

As chamadas relativas ao controle de contas são as seguintes:

1. **Busca todas as Contas** - [GET] /api/BankAccounts

Retorna uma lista de JSON com os dados de todas as contas cadastradas.

2. **Criação de Conta** - [POST] /api/BankAccounts

Cria uma conta com os dados de Número da Conta, Código do Banco, Número da Agência e Saldo.

3. **Atualização de uma Conta** - [PUT] /api/BankAccounts

Atualiza uma conta com os dados informados.

4. **Busca Saldo da Conta** - [GET] /api/BankAccounts/Balance/{accountNumber}

Retorna um JSON com os dados da conta especificada.

5. **Busca Histórico da Conta** - [GET] /api/BankAccounts/History/{accountNumber}

Retorna um JSON com os dados da conta e todas as transações.

6. **Busca Histórico da Conta Por Período** - [GET] /api/BankAccounts/PeriodHistory/{accountNumber}

Retorna um JSON com os dados da conta especificada.

7. **Deleção de uma Conta** - [DELETE] /api/BankAccounts/{accountNumber}

Deleta uma conta através do seu número {accountNumber}.

#### Carga Inicial de Dados
O sistema faz uma carga inicial na base com três contas:
- Conta: 123456-0, Banco: 371, Agência: 0001, Saldo: 40
- Conta: 678910-2, Banco: 371, Agência: 0001, Saldo: 60
- Conta: 345678-9, Banco: 371, Agência: 0001, Saldo: 150

### Transação - Endpoints

As chamadas relativas ao controle de conta corrente são as seguintes:

1. **Depósito** - [POST] /api/Transactions/Deposit

Adiciona ao saldo da conta corrente o valor informado.

2. **Retirada** - [POST] /api/Transactions/Withdrawl

Subtrai do saldo da conta corrente o valor informado.

3. **Pagamento** - [POST] /api/Transactions/Payment

Subtrai do saldo da conta corrente o valor informado.

### Conta Corrente - Validações

- Número da Conta: Obrigatório, tamanho máximo de 8 caracteres.
- Código do Banco: Obrigatório, tamanho máximo de 3 caracteres.
- Número da Agência: Obrigatório, tamanho máximo de 4 caracteres.
- Saldo da Conta: O Valor Inicial não pode ser negativo.

## Testes

- O projeto ContaCorrente.Domain.Tests testa todas as operações válidas no domínio da aplicação.
- O projeto ContaCorrente.API.Tests testa as operações válidas na camada de API.

## Observações

Ideias do que pode ser implementado ainda no projeto:

- Sistema de transferência entre contas correntes;
- Sistema de autenticação por JWT;
- Expandir a cobertura de testes no projeto da API;
- Testes de segurança;
- Interface de usuário SPA para interagir com o sistema;
- Microservico de rendimento;
- Escrever o detalhamento da documentação swagger.
