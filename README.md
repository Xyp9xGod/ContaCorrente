# ContaCorrenteAPI

Sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos.

O projeto tem uma camada:

- Uma API RESTful que foi desenvolvida em ASP.NET Core 5 com arquitetura DDD (Domain Driven Design), sendo o banco de dados utilizado MySQL com EF Core como ORM;

## Rodando no Visual Studio

Para testar o projeto no Visual Studio basta seguir os seguintes passos:

- Definir como **Multiple Startup Projects** os projetos ContaAPI.Application e ContaAPI.WebMVC nas propriedades da solution;
- Definir os dados do servidor e DB a ser utilizado no arquivo **appsettings.json** dentro do projeto ContaAPI.Application;
- No **Package Manager Console** definir como **Default Project** o projeto ContaAPI.Infra.Data e executar o comando **Update-Database**;
- Clicar em **Start** (F5).

Ao executar estes passos uma janelaa irá se abrir com o swagger da API.

- Swagger (API REST) - https://localhost:44340/swagger/index.html

## API RESTful

### Conta Corrente - Endpoints

As chamadas relativas ao controle de contas são as seguintes:

1. **Criação de Conta** - [POST] /api/BankAccounts

Cria uma conta com os dados de Número da Conta, Código do Banco, Número da Agência e Saldo.

2. **Busca todas as Contas** - [GET] /api/BankAccounts

Retorna uma lista de JSON com os dados de todas as contas cadastradas.

3. **Busca Conta por Número** - [GET] /api/BankAccounts/{accountNumber}

Retorna um JSON com os dados da conta especificada.

4. **Atualização de uma Conta** - [PUT] /api/BankAccounts

Atualiza uma conta com os dados informados.

5. **Deleção de uma Conta** - [DELETE] /api/BankAccounts/{accountNumber}

Deleta uma conta através do seu número {accountNumber}.

### Transação - Endpoints

As chamadas relativas ao controle de conta corrente são as seguintes:

1. **Extrato de uma Conta** - [GET] /api/Transactions/{accountNumber}

Retorna uma lista de JSON com os dados de todas as transações da conta corrente.

2. **Extrato de uma Conta por período** - [GET] /api/Transactions/{accountNumber}/{startDate}/{finalDate}

Retorna um JSON com os dados de todas as transações da conta corrente no período.

3. **Depósito** - [POST] /api/Transactions/Deposit

Adiciona ao saldo da conta corrente o valor informado.

4. **Retirada** - [POST] /api/Transactions/Withdrawl

Subtrai do saldo da conta corrente o valor informado.

5. **Pagamento** - [POST] /api/Transactions/Payment

Subtrai do saldo da conta corrente o valor informado.

### Conta Corrente - Validações

- Número da Conta: Obrigatório, tamanho máximo de 8 caracteres.
- Código do Banco: Obrigatório, tamanho máximo de 3 caracteres.
- Número da Agência: Obrigatório, tamanho máximo de 4 caracteres.
- Saldo da Conta: O Valor Inicial não pode ser negativo.

## Aplicativo Web MVC

O aplicativo web MVC desenvolvido tem as funções de **login**, **logout**, **registro**, **saldo**, **depósito**, **retirada**, **pagamento**, **rentabilização** e **histórico** com sessão persistente e mensagens de erro das validações da API REST. O botão **Passar 1 dia** simula um dia de rentabilização com 100% do CDI. Os campos de **valor** das operações financeiras na interface visual aceitam ou **vírgula** ou **ponto** para as casas decimais, dependendo da língua utilizada no sistema.

## Teste

O projeto ContaAPI.IntegrationTests testa todas as operações válidas e a integração com o database. Para rodar deve-se definir os dados do servidor e DB a ser utilizado no arquivo **appsettings.json** deste projeto.

## Observações

Ideias do que pode ser implementado ainda no projeto:

- Sistema de transferência entre contas correntes;
- Migration automática;
- Sistema de autenticação por JWT.
