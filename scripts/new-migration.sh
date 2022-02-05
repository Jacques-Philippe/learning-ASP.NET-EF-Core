#!/bin/bash

# Given provided name for migration,
#   Create a new migration using the PizzaContext context

name=$1
if [ -z "$name" ];
then
    echo "❌ No name for the migration was set, please provide one."
    echo "Example usage: $0 'your migration name here'"
    exit 2
else
    echo "✔ Received name $name. Creating migration..."
    echo "Using context PizzaContext 🍕"
    dotnet tool run dotnet-ef migrations add $name --context PizzaContext --project src/ContosoRESTAPI
fi