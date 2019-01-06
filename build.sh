echo "Building Hivemind.Core.csproj..."
dotnet build -c Release ./Hivemind.Core/Hivemind.Core.csproj
echo "Done."
echo "Building Hivemind.Core.Tests.csproj..."
dotnet build -c Release ./Hivemind.Core.Tests/Hivemind.Core.Tests.csproj
echo "Done."

echo "Running unit tests..."
dotnet test ./Hivemind.Core.Tests/Hivemind.Core.Tests.csproj --results-directory ../testresults --logger 'trx;LogFileName=nunit_results.xml'
echo "Done."

echo "Packaging Hivemind.Core nuget package."
dotnet pack ./Hivemind.Core/Hivemind.Core.csproj -c Release --output ../nuget/ --verbosity normal
echo "Pushing Hivemind.Core nuget package to myget."
nuget push ./nuget/Hivemind.Core.1.0.0.nupkg $myget_api_key -Source https://www.myget.org/F/hivemind/api/v2/package

echo "Building Angular project."
cd ./Hivemind.Ng
npm install
npm install --only=dev
npm install -g @angular/cli
ng test --watch=false
ng build
cd ..

echo "Starting docker containers..."
docker-compose up -d

echo "Building API test project..."
nuget restore ./Hivemind.Api.Tests/Hivemind.Api.Tests.csproj -ConfigFile ./Hivemind.Api.Tests/NuGet.config -SolutionDirectory ./packages
msbuild /p:Configuration=Debug ./Hivemind.Api.Tests/Hivemind.Api.Tests.csproj
echo "Running API tests..."
./packages/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Hivemind.Api.Tests/bin/Debug/Hivemind.Api.Tests.dll
echo "Done."

echo "Shutting down docker containers..."
docker-compose down
echo "Done."