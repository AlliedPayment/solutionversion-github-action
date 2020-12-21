FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env 
COPY ./src/CreateSolutionVersion /src/CreateSolutionVersion
WORKDIR /src/CreateSolutionVersion
RUN dotnet restore  /src/CreateSolutionVersion/CreateSolutionVersion.csproj 
RUN dotnet build  /src/CreateSolutionVersion/CreateSolutionVersion.csproj

FROM mcr.microsoft.com/dotnet/runtime:3.1
COPY --from=build-env /src/CreateSolutionVersion/bin/Debug/netcoreapp3.1 ./app
WORKDIR /app 
ENTRYPOINT ["/app/CreateSolutionVersion"]
