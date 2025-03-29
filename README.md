# Acesso à dados com .NET, C#, Dapper e SQL Server
Link do curso: https://balta.io/player/assistir/63c6ef55-44ee-445d-9b53-62e06fc969b8

- **Pacotes necessários:**
    - dotnet add package Microsoft.Data.SqlClient
    - dotnet add package Dapper

- **ADO.NET** - Fornece uma base para acesso ao banco de dados.
Fazendo uma consulta ao banco de dados diretamente com o ADO.NET, é mais rápida porém mais complexa para uma simples consulta:

- **DAPPER** - Dapper é uma extensão do ADO.NET porém fiferente do ADO.NET ele facilita a forma que acessarmos aos dados. Ele é mais ser leve, rápido e eficiente, funcionando como um mapeador de objetos que converte resultados de consultas SQL diretamente em objetos C#.

- **Observação** - Não concatenar string no insert, update, select pode gerar brechas de SQL INJECTION o certo é passar os dados por parametro.
