FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
LABEL base=true
LABEL pokeflex=true


FROM base AS unitrunner
LABEL notdb=true
LABEL pokeflex=true
ENV PATH $PATH:/root/.dotnet/tools

RUN ["dotnet","tool","install","-g","dotnet-ef"]

WORKDIR "/src/App"
COPY App/*.csproj .

WORKDIR "/src/Tests"
COPY Tests/*.csproj .


RUN ["dotnet", "restore"]
RUN ls /src/Tests/
RUN ls /src/Tests/Deployments/Units/

#RUN /bin/bash -c "source ../App/profile.sh"; eval "pokeflex ${POKEFLEX_RUN_CMD}"
#ENTRYPOINT ["/bin/bash", "-c", "source", "../App/profile.sh"]
#USER "NT Authority\System"
RUN ["/bin/bash", "/src/Tests/Deployments/Units/entrypoint.sh"]
