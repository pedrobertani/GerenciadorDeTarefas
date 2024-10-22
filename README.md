Gerenciador de Tarefas (To-Do List)
---

Descrição
---

Este projeto é uma aplicação web simples de gerenciamento de tarefas (to-do list) desenvolvida com .NET Core (C#) no back-end e Angular no front-end. O sistema permite que os usuários se autentiquem, registrem contas, gerenciem suas tarefas e visualizem a lista de tarefas de forma paginada.

Funcionalidades
---

1. Página de Login
O usuário pode inserir suas credenciais (usuário e senha) para autenticação utilizando JWT.
As credenciais são verificadas em um banco de dados SQL Server.
URL: http://localhost:4200/
2. Página de Registro
O usuário pode criar uma nova conta informando nome de usuário, senha e confirmação de senha.
A senha é armazenada de forma criptografada no banco de dados.
URL: http://localhost:4200/cadastro
3. Página Principal
Após a autenticação, o usuário é redirecionado para a página principal que exibe a lista de tarefas.
As tarefas possuem um título e uma descrição.
A lista de tarefas é paginada.
URL: http://localhost:4200/tarefas
4. Adicionar Tarefa
O usuário pode adicionar uma nova tarefa através de um modal que solicita o título e a descrição.
A nova tarefa é exibida na lista de tarefas após a adição.
5. Marcar Tarefa como Concluída
O usuário pode marcar cada tarefa como concluída.
As tarefas concluídas são exibidas de forma diferenciada na lista.
6. Atualizar Tarefa
O usuário pode atualizar o título e a descrição de uma tarefa existente.
A tarefa atualizada é exibida na lista.
7. Remover Tarefa
O usuário pode remover uma tarefa da lista.

Requisitos Técnicos
---
O banco de dados será mantido por "migrations".
Testes unitários devem ser implementados.
O front-end será desenvolvido utilizando o framework Angular.
O back-end será desenvolvido utilizando o .NET Core (C#), incluindo a criação de uma API RESTful.
O banco de dados utilizado será SQL Server.
A autenticação será realizada utilizando tokens JWT.

Instruções para Execução
---

Clonar o Repositório:
git clone https://github.com/pedrobertani/GerenciadorDeTarefas.git
cd GerenciadorDeTarefas


Backend (.NET Core)
---

Navegue até a pasta do backend.
Execute dotnet restore para restaurar as dependências.
Execute dotnet ef database update para aplicar as migrations.
Execute dotnet run para iniciar o servidor.

Frontend (Angular)
---

Navegue até a pasta do frontend.
Execute npm install para instalar as dependências.
Execute ng serve para iniciar o servidor de desenvolvimento.
Acesse a aplicação em http://localhost:4200


Imagens do Projeto
---
![image](https://github.com/user-attachments/assets/644debaa-51d8-4c9f-b302-5b8ec4ea869a)

![image](https://github.com/user-attachments/assets/722fee04-057a-47e0-9832-0e357fff0046)

![image](https://github.com/user-attachments/assets/90d657b1-41a4-41ba-9a3d-0834f63a2833)

![image](https://github.com/user-attachments/assets/8c794761-d5d4-419e-b150-b599b48ebf35)
