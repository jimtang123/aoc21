#!/bin/sh

dotnet new classlib -o "$1"
rm "$1/Class1.cs"
cp stub.cs "$1/$1.cs"
dotnet add "./$1/$1.csproj" reference "./utils/utils.csproj"
dotnet add "./runner/runner.csproj" reference "./$1/$1.csproj"