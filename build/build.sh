# build .NET projects
dotnet build -c Release ../Hivemind.Core/Hivemind.Core.csproj
dotnet build -c Release ../Hivemind.Core.Tests/Hivemind.Core.Tests.csproj

# run tests
dotnet test ../Hivemind.Core.Tests/Hivemind.Core.Tests.csproj --results-directory ../testresults --logger 'trx;LogFileName=nunit_results.xml'
dotnet pack ../Hivemind.Core/Hivemind.Core.csproj -c Release --output ../nuget/ --verbosity normal
nuget push ../nuget/Hivemind.Core.1.0.0.nupkg $myget_api_key -Source https://www.myget.org/F/hivemind/api/v2/package

# build Angular project
cd ../Hivemind.Ng
npm install
npm install --dev
npm install -g @angular/cli
ng test --watch=false
ng build
cd ..

# start containers
docker-compose up -d

# run API tests


# shutdown containers
docker-compose down