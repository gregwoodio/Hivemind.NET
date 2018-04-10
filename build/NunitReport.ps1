# NunitReport.ps1
$CoverageDir = 'D:\Jenkins\workspace\Hivemind\Coverage'

if (!(Test-Path -Path $CoverageDir)) {
    New-Item -ItemType Directory -Path $CoverageDir
}

& 'D:\BuildTools\NUnit.org\nunit-console\nunit3-console.exe' 'D:\Jenkins\workspace\Hivemind\Hivemind.Tests\bin\Debug\Hivemind.Tests.dll' --result=$CoverageDir\nunit_results.xml

& 'D:\BuildTools\ReportUnit.1.5.0-beta1\tools\ReportUnit.exe' $CoverageDir\nunit_results.xml $CoverageDir\nunit_report.html