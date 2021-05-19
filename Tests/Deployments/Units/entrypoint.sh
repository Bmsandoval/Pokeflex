#!/bin/bash

set -e

run_cmd="export DotnetTestDbType=inmemory && dotnet run -c Release -p /src/Tests/Tests.csproj -- -i -m -a /src/Tests/Benchs/ --disableLogFile --allCategories Proofs"

eval $run_cmd
