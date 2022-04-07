## How it works

This is the main repository.

The [Area](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-6.0) feature in ASP.NET Core is used to separate the teams.

---

## Forking Workflow

- Each team will fork this repository and work inside their own forked repository.
- Make a Pull Request(PR) to the main repository when an update is ready to be integrated

## Points of Interest

- Add your Dbsets to [HospitalContext](./data/HospitalContext.cs)
- Create your own areas in [Areas](./Areas). Use Visual Studio to scaffold 👍
- Add your newly created Area route to [Startup.cs](./Startup.cs)

## Prerequisite

1. [SQL Server](https://www.microsoft.com/en-sg/sql-server/sql-server-downloads)

## Initial Setup for Database

```bash
dotnet ef migrations add InitialCreate --context HospitalContext
dotnet ef database update --context HospitalContext

// Mod2 DB
dotnet ef migrations add InitialCreate --context MvcConsultationChecklistContext
dotnet ef database update --context MvcConsultationChecklistContext

dotnet ef migrations add InitialCreate --context MvcChecklistContext
dotnet ef database update --context MvcChecklistContext

```

For more information, read [Database Migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

> Note: Migration folder is .gitignored.
>
> For any subsequent setup for database, delete the Migrations folder generated from ef migrations, and delete the database from your local mysql server. Then repeat the `migrations add InitialCreate` and `database update` commands.

### Accounts to Log In
Administrator 1 Username: "Abby"
Password: 123123

Administrator 2 Username: "Bob"
Password: 123123

Practitioner Username: "Cindy"
Password: 123123
