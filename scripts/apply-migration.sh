#!/bin/bash

# Apply the migrations to the database
echo "Applying migrations to the database..."
echo "Using context PizzaContext 🍕"
dotnet ef database update --context PizzaContext --project src/ContosoRESTAPI