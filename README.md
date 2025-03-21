# teste_proauto - Sistema de Cadastro

Após clonar o repositório, é necessário que realize os seguintes passos:

### Para o backend:
**1 -** Abra o gerenciador de soluções e clique em 'teste_proauto.sln'. <br>
**2 -** Dentro de ProdutoCadastro.API insira no appsettings.json a connection string do banco de dados. <br>
**3 -** Abra o console do gerenciador de pacotes, insira o projeto 'ProdutoCadastro.Data'. <br>
**4 -** Insira o comando 'update-database'. Caso dê erro crie um migration: 'Add-Migration migration005'. <br>
**5 -** Apenas rode a api normalmente no botão de executar http. <br>

### Para os testes unitários:
**1 -** Acesse a pasta 'ProdutoCadastro.Test'. <br>
**2 -** Clique com o botão direito no 'ProdutoCadastro.Test' e então clique em 'Executar Testes'. <br>

### Para o frontend:
**1 -** É recomendado executá-lo pelo visual studio code. Após entrar no mesmo, acesse pelo terminal a seguinte pasta: 'frontend/teste_proauto_front' <br>
**2 -** Insira então o comando 'npm install --legacy-peer-deps'. <br>
**3 -** Insira então o comando 'npm start'. <br>

**Observações importantes - caso gere algum erro:**
- **.env:** Olhar o arquivo .env, configure a variável de ambiente para rodar juntamente com o backend.
- **appsettings.json:** Certifique-se de que a connection string esteja de acordo com o banco.



