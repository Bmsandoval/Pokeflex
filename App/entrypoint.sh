#!/bin/bash

set -e
run_cmd="dotnet run --no-build"

until dotnet ef database update; do
    echo "SQL Server is starting up"
    sleep 1
done

# using msbuild for multi-core compilation reduces build times
dotnet msbuild . /target:build /restore:false /maxcpucount:2

echo "SQL Server is up - executing command"
exec $run_cmd