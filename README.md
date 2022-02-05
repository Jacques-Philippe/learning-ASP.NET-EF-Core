![Build & Test workflow](https://github.com/Jacques-Philippe/learning-ASP.NET-EF-Core/actions/workflows/unit-tests.yml/badge.svg)

# Get started (dev)

1. Install dotnet tools (`HTTP REPL`, `csharpier`, `dotnet-ef`)
   ```
   ./scripts/install-dotnet-tools.sh
   ```
1. Install yarn
1. Clone the repo
1. Run `yarn prepare` (You may need to run `yarn install` before this).  
   This will install all husky hooks.

## Working with the server

You can build and run the server with hot-reload enabled with `yarn dev`. This runs `dotnet watch run --project src/[the REST API project]`. Building and running the server will output to console the endpoints the server is listening at.

Connect to the server to investigate your endpoints with `httprepl [your endpoint]`

Note that if you use hot reload, you should **ensure that the ASPNETCORE_ENVIRONMENT environment variable is set to Development**, otherwise it won't find the OpenAPI description properly. For an example of how to see this, check out the `package.json`'s `dev` script at project root.

# On secrets

## You may use Secret Manager for local dev

### Enable secret storage

Enable secret storage with the following command.

```
dotnet user-secrets init --project <path-to-.csproj>
```

The preceding command adds a `user secrets id` to your project, which subsequently allows your project to find the secrets you refer to in your code.

Secrets created with the `Secret Manager` tool are stored at `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`

### Set a secret

Set a project secret with the following

```
dotnet user-secrets set "secret name" "secret value" --project <path-to-.csproj>
```

In our case we have the following

```
dotnet user-secrets set "Db:DataSource" "Data Source=ContosoPizza.db" --project ./src/ContosoRESTAPI
```

The above will ensure our db is created in a file named `ContosoPizza.db` at project root, when we apply our migrations

### Access the secret from within the ASP.NET project

```
//In the ASP.NET Program.cs file,
var builder = WebApplication.CreateBuilder(args);

//Get data source from user secrets
var secretValue = builder.Configuration["secret name"];
```

### List project secrets

Use the following command to list project secrets

```
dotnet user-secrets list --project <path-to-your-.csproj>
```

## Use Azure Key Vault for apps in production

# On databases

## Commands

### Create a migration

In the following, `Context` refers to your data model, so for instance in this project this is a reference to the [`PizzaContext` class](/src/ContosoRESTAPI/Data/PizzaContext.cs), which inherits from DbContext.

```
dotnet ef migrations add "migration name" --context YourContext --project <path-to-.csproj>
```

After executing the above, a `Migrations` directory is created at project root.

### Apply migrations

```
dotnet ef database update --context YourContext --project <path-to-.csproj>
```

Executing the above creates the `.db` file if it doesn't already exist, and applies the migrations to it.

### Scaffold

It's possible to create Model classes from .db files.

```
dotnet ef dbcontext scaffold "Data Source=./Promotions/Promotions.db" Microsoft.EntityFrameworkCore.Sqlite --context-dir ./Data/ --output-dir ./Models
```

The above will create a `Models/Coupon` class and `Data/PromotionsContext` from the provided `Promotions.db` file. Notice, the above specifies:

- to scaffold a `DbContext` and model classes using the provided connection string.
- specifies the framework database provider to use (`Microsoft.EntityFrameworkCore.Sqlite`).
- specifies directories for the resulting `DbContext` and model classes.

## Basics

### Seeding the database

⚠️ Be careful using database seeding in distributed systems, as this doesn't account for race conditions.  
To seed the database, we create an initializer class to take the database Context and populate it. In our case, we create static class [`DbInitializer`](src/ContosoRESTAPI/Data/DbInitializer.cs)

### Devving with Sqlite on VS code

Install the `vscode-sqlite` extension via VS Code's extensions interface. Check out [the extension's repo](https://github.com/AlexCovizzi/vscode-sqlite) for more instructions on its use.

### Tools

`dotnet-ef` is a package we can use to manage our database migrations and scaffolding.

### Tables

- Properties of type `DbSet<T>` represent tables which are to be created in the database.
- EF Core's primary key and foreign key constraint naming conventions are `PK_<Primary key property>` and `FK_<Dependent entity>_<Principal entity>_<Foreign key property>`, respectively. The `<Dependent entity>` and `<Principal entity>` placeholders correspond to the entity class names.
- As is true with ASP.NET Core MVC, EF Core adopts a convention over configuration philosophy. EF Core conventions shorten development time by inferring the developer's intent. For example, a property named `Id` or `<entity name>Id` is inferred to be the generated table's primary key. If you choose not to adopt the naming convention, the property must be annotated with the `[Key]` attribute.

### Local dev vs using a network database

`Sqlite` uses local database files (`.db` files), which is fine for development. However, as you scale the project, we're going to start using network databases, such as `PostgreSQL` or `SQL Server`.

# Packages installed and why

## ContosoRESTAPI

### Microsoft.EntityFrameworkCore.Sqlite

SQL database dependencies

```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.1
```

### Microsoft.EntityFrameworkCore.Design

Entity management dependencies

```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.1
```

# Decimal vs float

In summary, `decimal` is an exact value, whereas `float` is a more approximate value. So for things like money, use `decimal`, and for more apprximate scientific values, use `float`.

# Issues I ran into

## "Unable to find an OpenAPI description" on HTTPREPL connect

See [this](https://stackoverflow.com/questions/69278068/why-is-httprepl-unable-to-find-an-openapi-description-the-command-ls-does-not)  
What fixed the issue for me:

```
dotnet dev-certs https --trust
```

## Tables aren't created on Database.EnsureCreated()

For whatever reason, `Database.EnsureCreated()` wasn't creating the seeded database, and [this](https://stackoverflow.com/a/68796048) turned out to be the fix.
