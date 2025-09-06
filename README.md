# Desafio Backend - Sistema Bancário Console

Este é um projeto de um simples sistema bancário que roda no console, desenvolvido em C#. O sistema permite criar contas, consultar saldo, depositar, sacar, transferir valores entre contas e consultar o histórico de transações.

## Funcionalidades

* **Gerenciamento de Contas:**
    * Criar uma nova conta bancária para um titular.
    * Consultar o saldo de uma conta existente.
* **Operações Financeiras:**
    * Realizar depósitos.
    * Realizar saques (com validação de saldo).
    * Transferir valores entre duas contas (com validação de saldo).
* **Histórico:**
    * Consultar o histórico completo de transações de uma conta.

## Estrutura do Projeto

O projeto segue uma arquitetura em camadas para separar as responsabilidades:

* **`Controllers`**: Responsáveis por interagir com o usuário, capturando as entradas do console e exibindo os resultados.
* **`Services`**: Contêm a lógica de negócio da aplicação. As regras de como as operações devem funcionar estão implementadas aqui.
* **`Repository`**: Camada de acesso a dados. É responsável por toda a comunicação com o banco de dados (leitura e escrita).
* **`Models`**: Define as entidades da aplicação, como `Conta` e `Transacao`.
* **`Data`**: Contém a configuração e inicialização do banco de dados SQLite.

## Como Rodar o Projeto

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
* Um terminal ou console (como PowerShell, CMD, ou o terminal do VS Code)

### Passos para Execução

1.  **Clone o repositório:**
    ```bash
    git clone <url-do-seu-repositorio>
    cd <nome-do-repositorio>
    ```

2.  **Restaure as dependências:**
    O projeto utiliza a biblioteca `Microsoft.Data.Sqlite` para se comunicar com o banco de dados. Execute o comando abaixo para instalar as dependências necessárias:
    ```bash
    dotnet restore
    ```

3.  **Execute a aplicação:**
    O banco de dados (`banco.db`) será criado automaticamente na primeira execução.
    ```bash
    dotnet run
    ```

4.  **Use o sistema:**
    Após a execução, um menu interativo será exibido no console, permitindo que você escolha as operações que deseja realizar.

## Banco de Dados

O projeto utiliza **SQLite** como banco de dados. Um arquivo chamado `banco.db` será criado na raiz do projeto (ou no diretório especificado na string de conexão em `Data/Database.cs`) quando a aplicação for iniciada pela primeira vez.

As tabelas criadas são:
* `Contas`: Armazena as informações das contas dos titulares.
* `Transacoes`: Registra todas as transações (depósito, saque, transferência) realizadas.
