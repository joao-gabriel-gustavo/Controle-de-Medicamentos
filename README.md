# Controle de Medicamentos

![Gif](https://i.imgur.com/9hIVPlg.gif)

## Introdução

O Sistema de Controle de Medicamentos é uma aplicação de console desenvolvida para gerenciar o estoque de medicamentos em instituições de saúde. O aplicativo permite o controle completo do fluxo de medicamentos, desde o cadastro de fornecedores, medicamentos e pacientes, até as requisições de entrada e saída de medicamentos.

## Funcionalidades

O sistema conta com os seguintes módulos principais:

### 1. Gerenciamento de Fornecedores

- Cadastrar novo fornecedor
- Editar dados de fornecedor
- Excluir fornecedor
- Visualizar lista de fornecedores

### 2. Controle de Pacientes

- Cadastrar novo paciente
- Editar dados do paciente
- Excluir paciente
- Visualizar lista de pacientes

### 3. Controle de Medicamentos

- Cadastrar novo medicamento
- Editar informações do medicamento
- Excluir medicamento
- Visualizar estoque de medicamentos

### 4. Controle de Funcionários

- Cadastrar novo funcionário
- Editar dados do funcionário
- Excluir funcionário
- Visualizar lista de funcionários

### 5. Controle de Requisições de Saída

- Registrar saída de medicamentos
- Associar medicamento ao paciente
- Editar requisição de saída
- Excluir requisição de saída
- Visualizar histórico de requisições de saída

### 6. Controle de Requisições de Entrada

- Registrar entrada de medicamentos no estoque
- Associar medicamento ao fornecedor
- Editar requisição de entrada
- Excluir requisição de entrada
- Visualizar histórico de requisições de entrada

## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=git,github,visualstudio,cs,dotnet)](https://skillicons.dev)

## Requisitos

- .NET 8.0 ou superior
- Sistema operacional compatível com .NET (Windows, macOS, Linux)

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:

```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real

```
dotnet run --project ControleDeMedicamentos.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `./ControleDeMedicamentos.ConsoleApp/bin/Release/net8.0/` e execute o arquivo:

```
ControleDeMedicamentos.ConsoleApp.exe
```
