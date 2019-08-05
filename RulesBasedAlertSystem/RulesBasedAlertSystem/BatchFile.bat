@echo off 

SET BASE_PATH=%~dp0

SET PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
SET SolutionPath=%BASE_PATH%RulesBasedAlertSystem.sln

Echo Start Time - %Time%

%BASE_PATH%nuget restore

MSbuild %SolutionPath% /p:Configuration=Release /p:Platform="Any CPU"

Echo End Time - %Time%

%BASE_PATH%packages\NUnit.ConsoleRunner.3.10.0\tools\nunit3-console.exe RulesBasedAlertSystem\bin\Debug\RulesBasedAlertSystem.exe