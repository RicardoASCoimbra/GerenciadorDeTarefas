<!doctype html>
<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">

    <title>Meu Projeto</title>
</head>

<body>
    <h1 style="font-weight: bold; font-family: 'Quickstart';">Gerenciador de Tarefas</h1>

    <p style="font-family: 'Quickstart';">Este é um projeto incrível que faz coisas incríveis.</p>

    <h3 style="font-weight: bold; font-family: 'Quickstart';">Como começar</h3>

    <ol style=" font-family: 'Quickstart';">
        <li>Clone o repositório.</li>
        <li>Execute o "Restaurar Pacote Nuget" para instalar as dependências.</li>
    </ol>

    <h5 style="font-weight: bold; font-family: 'Quickstart';">Para a configuração do banco de dados via Migration</h5>

    <ol style="font-family: 'Quickstart';">
        <li>No seu "appsettings.Development.json", altere o "Server" apontando para seu servidor SQL SERVER.</li>
        <li>Em seu banco SQL SERVER, crie um banco com o nome de "TaskDB".</li>
        <li>Abra o "Console de Gerenciador de Pacotes", selecione o Projeto Padrão "4.Infraestructure\Tarefa.Infra.Data" e rode o "add-migration InitialDB".</li>
        <li>Após rodar o migration, rode o "update-database" para criar o Banco de dados.</li>
    </ol>

    <h5 style="font-weight: bold; font-family: 'Quickstart';">Para a configuração do banco de dados via Script</h5>

    <ol style="font-family: 'Quickstart';">
        <li>Importe o Script "TaskDB" que está na pasta "1.Presentation/Tarefa.Web" e importe no banco de dados.</li>
    </ol>

    <h3 style="font-weight: bold; font-family: 'Quickstart';">Contribuições</h3>

    <p style="font-family: 'Quickstart';">Contribuições são bem-vindas! Sinta-se à vontade para abrir um problema ou enviar uma solicitação de pull.</p>

    <h3 style="font-weight: bold; font-family: 'Quickstart';">Licença</h3>

    <p style="font-family: 'Quickstart';">Este projeto é licenciado sob a Licença MIT. Consulte o arquivo <a href="LICENSE">LICENSE</a> para obter detalhes.</p>

    <!-- Optional JavaScript; choose one of the two! -->

    <!-- Option 1: Bootstrap Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM"
        crossorigin="anonymous"></script>

    <!-- Option 2: Separate Popper and Bootstrap JS -->
    <!--
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    -->
</body>

</html>
