#!/bin/bash

set -e

run_cmd="export DotnetTestDbType=inmemory && dotnet test /src/Tests/Tests.csproj"

eval $run_cmd
