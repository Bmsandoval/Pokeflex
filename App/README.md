Create new unit tests:

# Not sure why you'd have multiple test projects, but you can
$ dotnet new xunit -o Tests
$ dotnet add ./Tests/PokeflexTests.csproj reference ./App/Pokeflex.csproj