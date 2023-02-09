# E_equipment_business_web

1. Clone this repository
2. Run "dotnet tool install --global dotnet-ef" 
3. Run "dotnet ef"
4. Run 
"dotnet add package System.Data.SqlClient
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet"

5. Run "dotnet ef dbcontext scaffold -o Models -f -d "Data Source=<name server>;Initial Catalog=<name database>;
TrustServerCertificate=True; Integrated Security=True" "Microsoft.EntityFrameworkCore.SqlServer""
