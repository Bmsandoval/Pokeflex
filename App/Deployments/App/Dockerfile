FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
LABEL base=true
LABEL pokeflex=true


FROM base AS apirunner
LABEL notdb=true
LABEL pokeflex=true
ENV PATH $PATH:/root/.dotnet/tools

RUN ["dotnet","tool","install","-g","dotnet-ef"]

WORKDIR "/src/App/"

COPY *.csproj .
RUN ["dotnet", "restore"]
EXPOSE 5001

ENTRYPOINT ["/bin/bash", "./Deployments/App/entrypoint.sh"]
