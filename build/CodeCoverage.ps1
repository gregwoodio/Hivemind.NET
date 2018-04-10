# CodeCoverage.ps1
$CoverageDir = 'D:\Jenkins\workspace\Hivemind\Coverage\'

if (!(Test-Path -Path $CoverageDir)) {
    New-Item -ItemType Directory -Path $CoverageDir
}

& 'D:\BuildTools\OpenCover.4.6.519\tools\OpenCover.Console.exe' -register:user -target:"D:\BuildTools\NUnit.org\nunit-console\nunit3-console.exe" -output:$CoverageDir\OpenCoverOutput.xml -targetargs:"D:\Jenkins\workspace\Hivemind\Hivemind.Tests\Hivemind.Tests.csproj" -mergebyhash -skipautoprops

& D:\BuildTools\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:$CoverageDir\OpenCoverOutput.xml -targetdir:$CoverageDir\GeneratedReport