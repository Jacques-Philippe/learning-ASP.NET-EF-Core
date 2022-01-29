![Build & Test workflow](https://github.com/Jacques-Philippe/[REPO NAME HERE]/actions/workflows/unit-tests.yml/badge.svg)

# Get started (dev)

1. Install HTTP REPL
   ```
   dotnet tool install -g Microsoft.dotnet-httprepl
   ```
1. Install csharpier
   ```
   dotnet tool install csharpier -g
   ```
1. Install yarn
1. Clone the repo
1. Run `yarn prepare` (You may need to run `yarn install` before this)

## Working with the server

You can build and run the server with hot-reload enabled with `yarn dev`. This runs `dotnet watch run --project src/[the REST API project]`. Building and running the server will output to console the endpoints the server is listening at.

Connect to the server to investigate your endpoints with `httprepl [your endpoint]`

# Issues I ran into

## "Unable to find an OpenAPI description" on HTTPREPL connect

See [this](https://stackoverflow.com/questions/69278068/why-is-httprepl-unable-to-find-an-openapi-description-the-command-ls-does-not)  
What fixed the issue for me:

```
dotnet dev-certs https --trust
```
