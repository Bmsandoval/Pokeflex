Create new unit tests:

# Not sure why you'd have multiple test projects, but you can
$ dotnet new xunit -o ContosoTests/ControllerTests
$ dotnet add ./ContosoTests/ControllerTests/ControllerTests.csproj reference ./ContosoUniversity/ContosoUniversity.csproj