# 📋 Especificação do Sistema: LimeFlow API

Este documento detalha os critérios de aceitação e as regras de negócio que devem ser implementadas em cada camada da arquitetura, bem como as rotas da API e as especificações dos testes automatizados.

## 🏢 Camada de Domínio (Domain)

Esta é a base do sistema. Na pasta `LimeFlow.Domain/Models`, deves implementar as entidades garantindo que protegem as suas próprias regras e o seu estado.

- **Entidade `Account`:** A classe deve conter `Id` (Guid), `Name`, `Bank` e `Balance`. O `Balance` (saldo) deve sempre iniciar a `0` no construtor. Deves criar os métodos `AddCredit(decimal amount)` e `Debit(decimal amount)`.
  - **Regra:** Não permitir que o `amount` seja menor ou igual a zero em nenhum dos métodos. Se for, dispara uma `ArgumentException`.
  - **Dica:** A propriedade `Balance` deve ter um `private set;`. O único local que deve atualizar esta informação são os métodos `AddCredit` e `Debit`. Pensa no saldo real de um banco: o saldo não é "alterado" para um valor arbitrário; ele sofre depósitos ou levantamentos que resultam nesse valor.

- **Entidade `Transaction`:** A classe deve conter `Id`, `TransactionDate`, `Description`, `Amount`, `Type` (Enum: Income/Expense), `AccountId` e `CategoryId`.
  - **Regra:** A data da transação não pode ser uma data futura. Ao ser instanciada, o sistema deve validar: `if (transactionDate > DateTime.UtcNow) throw new ArgumentException("A data não pode ser no futuro");`.

---

## ⚙️ Camada de Aplicação (Application)

Aqui implementas os Casos de Uso (Use Cases) utilizando o MediatR. Fica localizado na pasta `LimeFlow.Application/UseCases`.

- **`CreateTransactionCommand`:** Esta classe (que atua como o DTO de entrada da requisição) deve receber `AccountId`, `CategoryId`, `Amount`, `Type` e `Description`.
- **`CreateTransactionCommandHandler`:** O manipulador deste comando deve executar o seguinte fluxo:
  1. Buscar a conta na base de dados utilizando o `IAccountRepository`. Se não existir, lançar uma exceção indicando "Conta não encontrada".
  2. Instanciar a nova `Transaction`.
  3. Se o tipo for `Income`, invocar `account.AddCredit(amount)`. Se for `Expense`, invocar `account.Debit(amount)`.
  4. Adicionar a transação através do `ITransactionRepository`.
  5. Atualizar o estado da conta através do `IAccountRepository`.
  6. Invocar `await _unitOfWork.CommitAsync()` para persistir todas as alterações na base de dados de forma atómica.

---

## 🗄️ Camada de Infraestrutura (Infrastructure)

Aqui definimos como os dados são efetivamente guardados. Fica na pasta `LimeFlow.Infrastructure/Mappings`.

- **`AccountConfiguration`:** Ao configurar a tabela pelo Entity Framework (usando Fluent API), certifica-te de que o campo `Balance` seja mapeado como numérico com precisão para evitar problemas de arredondamento de moeda: `builder.Property(a => a.Balance).HasColumnType("decimal(18,2)");`.
- **`UnitOfWork`:** O método `CommitAsync` deve invocar diretamente o método da base de dados (`await _context.SaveChangesAsync() > 0`) e retornar `true` caso alguma linha tenha sido alterada.

---

## 🌐 Rotas da Aplicação

Agora que a base está pronta, deves abrir as classes de Controladores (Controllers) na camada `LimeFlow.API` e completar as rotas para expor os Casos de Uso.

- **POST /accounts:** A rota deve receber `name` e `bank` dentro do corpo da requisição. Ao cadastrar uma nova conta, o sistema deve retornar um JSON com a conta criada no seguinte formato: `{ "id": "uuid", "name": "Nome do Banco", "balance": 0 }`. Certifica-te de que a API retorna o Status HTTP `201 Created`.
- **GET /accounts:** A rota deve listar (retornar um array) todas as contas do utilizador atual.
- **POST /transactions:** A rota deve receber os dados da transação. **Atenção:** Esta rota não apenas cria a transação; o *Handler* que é invocado por ela atualizará automaticamente o saldo da conta associada.
  - **Dica de Negócio:** O cliente (Frontend) nunca vai chamar uma rota "PUT /accounts/saldo" para atualizar o saldo diretamente. O saldo é um reflexo das transações. Ao utilizar o POST para criar uma despesa, a regra de negócio na camada de Application já se encarrega de abater o valor da conta.
- **DELETE /transactions/:id:** A rota deve eliminar a transação identificada pelo ID presente nos parâmetros da rota.
  - **Regra:** Ao apagar uma transação, o sistema deve "estornar" o valor na conta. Por exemplo, se apagares uma despesa de 50€, a conta deve receber um crédito de 50€ de volta.

---

## 🧪 Especificação dos Testes

Em cada teste, existe uma breve descrição do que a tua aplicação (especificamente os *Handlers* e *Entities*) deve cumprir para que o teste passe utilizando o framework `xUnit`.

- **`should_be_able_to_create_a_new_account`:** Para que este teste passe, a tua entidade `Account` deve ser instanciada corretamente, gerando um `Id` automático do tipo Guid e iniciando o `Balance` estritamente com o valor `0`.
- **`should_not_be_able_to_add_negative_credit`:** Para que este teste passe, deves garantir que ao invocar `account.AddCredit(-10)` o sistema dispare uma exceção (`Throws<ArgumentException>`), não permitindo que a operação seja concluída.
- **`should_process_transaction_and_update_account_balance`:** Para que este teste do *Handler* passe, ao simular (utilizando *Moq*) o envio de um `CreateTransactionCommand` de despesa (Expense) no valor de `50` para uma conta que tem `100` de saldo, o repositório de conta deve ser atualizado (`_accountRepository.Update(...)`) e o novo saldo final a ser salvo na base de dados deve ser obrigatoriamente `50`.
- **`should_rollback_if_account_does_not_exist`:** Para que este teste passe, ao tentar enviar uma transação para um `AccountId` inexistente na base de dados, o sistema deve retornar um erro (ex: `NotFoundException`) e o método `_unitOfWork.CommitAsync()` **não deve** ser invocado em nenhuma circunstância.
- **`should_not_be_able_to_update_balance_manually`:** Para que este teste (de compilação) passe, o programador não deve conseguir fazer a atribuição direta `account.Balance = 1000;` no código de teste. A propriedade deve estar devidamente protegida e inacessível para escrita externa.