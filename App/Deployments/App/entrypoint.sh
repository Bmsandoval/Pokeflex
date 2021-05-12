#!/bin/bash

set -e

until dotnet ef database update; do
    echo "SQL Server is starting up"
    sleep 1
done
echo "Database Updated"

# using msbuild for multi-core compilation reduces build times
#dotnet msbuild . /target:build /restore:false /maxcpucount:2
#run_cmd="dotnet run --no-build"
#run_cmd="dotnet watch msbuild /t:BuildRun"
run_cmd="dotnet watch run --launch-profile Docker"

echo "SQL Server is up - executing command"
eval $run_cmd


