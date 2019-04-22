#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/aspnetcore:2.0-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1803 AS build
WORKDIR /src
COPY ["FullCalenderIOMVCCore/FullCalenderIOMVCCore.csproj", "FullCalenderIOMVCCore/"]
RUN dotnet restore "FullCalenderIOMVCCore/FullCalenderIOMVCCore.csproj"
COPY . .
WORKDIR "/src/FullCalenderIOMVCCore"
RUN dotnet build "FullCalenderIOMVCCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FullCalenderIOMVCCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FullCalenderIOMVCCore.dll"]