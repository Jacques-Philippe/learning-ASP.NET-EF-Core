![Build & Test workflow](https://github.com/Jacques-Philippe/[REPO NAME HERE]/actions/workflows/unit-tests.yml/badge.svg)

# Get started (dev)

1. Install `HTTP REPL`
   ```
   dotnet tool install -g Microsoft.dotnet-httprepl
   ```
1. Install `csharpier`
   ```
   dotnet tool install csharpier -g
   ```
1. Install `dotnet-ef`
   ```
   dotnet tool install -g dotnet-ef
   ```
1. Install yarn
1. Clone the repo
1. Run `yarn prepare` (You may need to run `yarn install` before this)
1.

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

### Access the secret from within the ASP.NET project

```
//In the ASP.NET Program.cs file,
var builder = WebApplication.CreateBuilder(args);

//Get data source from user secrets
var secretValue = builder.Configuration["secret name"];


```

## Use Azure Key Vault for apps in production

# On databases

## Tools

`dotnet-ef` is a package we can use to manage our database migrations and scaffolding.

## Tables

Properties of type `DbSet<T>` represent tables which are to be created in the database.

## Local dev vs using a network database

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

# Issues I ran into

## "Unable to find an OpenAPI description" on HTTPREPL connect

See [this](https://stackoverflow.com/questions/69278068/why-is-httprepl-unable-to-find-an-openapi-description-the-command-ls-does-not)  
What fixed the issue for me:

```
dotnet dev-certs https --trust
```
