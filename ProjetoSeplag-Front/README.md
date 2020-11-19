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


Informações Adicionais:

Angular: 10.1.5
... animations, cli, common, compiler, compiler-cli, core, forms
... platform-browser, platform-browser-dynamic, router
Ivy Workspace: Yes

Package                         Version
---------------------------------------------------------
@angular-devkit/architect       0.1001.5
@angular-devkit/build-angular   0.1001.5
@angular-devkit/core            10.1.5
@angular-devkit/schematics      10.1.5
@angular/localize               10.2.3
@schematics/angular             10.1.5
@schematics/update              0.1001.5
rxjs                            6.6.3
typescript                      4.0.3