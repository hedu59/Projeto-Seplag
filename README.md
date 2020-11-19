# Projeto-Seplag
Projeto para inclusão e tramitação de processos, anexo  e visualização de documento e histórico de tramitação


***Prototype => API BACKEND***

**Ultidade da aplicação**

Aplicação para inclusão de processo de benefício de servidores públicos.  

1- Inclusão de novos processos 

2- Acesso aos processos

3- Possibilidade de anexar documentos aos processos associando-os a determinados tipos.

4- Listagem dos documentos anexados a um processo.

5- Possibilidade de visualização (em tela ) dos documentos anexados.

6- Possibilidade de tramitação do processo entre setores.

7- Possibilidade de visualização do histórico de tramitações.  


**Tecnologias e  Padrões**

  1- Aplicação: em .NET CORE 3.1 
  
  2- Documentação de endpoints da API com swagger (http://localhost:5001/swagger/index.html)
  
  3- Padõres: Sevice Pattern, Repository Pattern, Command Pattern.
  
  4- ORM: Entity Framework 3.1.10. 
  
  5- Banco de Dados: PostgresSQL 


**Como executar**

1-Acessar a aplicação para geração do banco de dados via migrations:
  No **Console do Gernciador de Pacotes** escolher o projeto padrão **3-Infrastructure\Prototype.Infra.Data**
  Executar o comando: **Update-Database** para que o banco de dados da aplicação seja criado baseado nas migrations. 
  
**Deploy**
Para deploy windows: 

1-Instalar runtime. 
  https://download.visualstudio.microsoft.com/download/pr/c1ea0601-abe4-4c6d-96ed-131764bf5129/a1823d8ff605c30af412776e2e617a36/aspnetcore-runtime-3.1.10-win-x64.exe

2-Baixar e instalar o hosting
  https://download.visualstudio.microsoft.com/download/pr/7e35ac45-bb15-450a-946c-fe6ea287f854/a37cfb0987e21097c7969dda482cebd3/dotnet-hosting-3.1.10-win.exe

3-Acessar a aplicação para geração do banco de dados via migrations:
  No **Console do Gernciador de Pacotes** escolher o projeto padrão **3-Infrastructure\Prototype.Infra.Data**
  Executar o comando: **Update-Database** para que o banco de dados da aplicação seja criado.
  
4-No projeto Prototype.API, clicar com botão direito para geração dos aquivos de deploy.



#############################################################################################################

***ProjetoSeplagFront => ANGULAR FRONTEND***

***EXECUÇÃO DA APLICAÇÃO ***

-Para execução plena das funções da aplicação se faz necessário o uso do ***backend*** e ***banco de dados postgres***.

-Para execução (via Visual Studio Code) se faz necessária a utilização de **Node**.

  1 - Após clonar o fonte, acessar pasta do projeto via terminal e executar o comando **npm install**.
  2 - Apontar o caminho da API na classe **base-service.ts** no método **getHostApi()**
  3 - Após todos os módulos instalados via node, rodar o comando **ng serve**.

-Para deploy da aplicação via IIS:

  1 - Após clonar o fonte, acessar pasta do projeto via terminal e executar o comando **npm install**.
  2 - Apontar o caminho da API na classe **base-service.ts** no método **getHostApi()**
  3 - Após todos os módulos instalados via node, rodar o comando **ng build --prod**. Dentro da pasta da aplicação será criada 
      uma subpasta com o nome **dist** seu conteúdo é o necessário para o deploy da aplicação.
