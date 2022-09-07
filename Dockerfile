FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/WebApi/Denex.WebApi/Denex.WebApi.csproj ./src/WebApi/Denex.WebApi/
COPY src/Core/Denex.Domain/Denex.Domain.csproj ./src/Core/Denex.Domain/
COPY src/Core/Denex.Application/Denex.Application.csproj ./src/Core/Denex.Application/
COPY src/Infrastructure/Denex.Persistance/Denex.Persistance.csproj ./src/Infrastructure/Denex.Persistance/

RUN dotnet restore

# copy everything else and build app
COPY src/WebApi/Denex.WebApi/. ./src/WebApi/Denex.WebApi/
COPY src/Core/Denex.Domain/. ./src/Core/Denex.Domain/
COPY src/Core/Denex.Application/. ./src/Core/Denex.Application/
COPY src/Infrastructure/Denex.Persistance/. ./src/Infrastructure/Denex.Persistance/

WORKDIR /app/src/WebApi/Denex.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS runtime
WORKDIR /app

COPY --from=build /app/src/WebApi/Denex.WebApi/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Denex.WebApi.dll"]
