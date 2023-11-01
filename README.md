# Gerenciador de Tarefas

Este é um projeto incrível que faz coisas incríveis.

### Como começar

1. Clone o repositório.
2. Execute o "Restaurar Pacote Nuget" para instalar as dependências.

#### Para a configuração do banco de dados via Migration

 No seu "appsettings.Development.json", altere o "Server" apontando para seu servidor SQL SERVER.
2. Em seu banco SQL SERVER, crie um banco com o nome de "TaskDB".
3. Abra o "Console de Gerenciador de Pacotes", selecione o Projeto Padrão "4.Infraestructure\Tarefa.Infra.Data" e rode o "add-migration InitialDB".
4. Após rodar o migration, rode o "update-database" para criar o Banco de dados.

#### Para a configuração do banco de dados via Script

1. Importe o Script "TaskDB" que está na pasta "1.Presentation/Tarefa.Web" e importe no banco de dados.

### Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir um problema ou enviar uma solicitação de pull.

### Licença

Este projeto é licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para obter detalhes.
