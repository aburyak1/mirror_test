FROM reggitlab.iiko.ru/devops/tools/sdk:2.2 as builder
COPY . /code
WORKDIR /code
RUN dotnet publish -c Release -o /code/Build dev/iikoTransport.SbpService

FROM reggitlab.iiko.ru/devops/tools/aspnet:2.2
CMD ["dotnet", "iikoTransport.SbpService.dll"]
WORKDIR /app
COPY --from=builder /code/Build .
