@echo off 

REM Builds the solution
REM C:\Users\320053826\source\repos\CaseStudyG4
REM C:\Windows\Microsoft.NET\Framework64\v4.0.30319 .\CaseStudyG4.sln

SET PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
SET SolutionPath=C:\Users\320053826\source\repos\CaseStudyG4\CaseStudyG4.sln
Echo Start Time - %Time%
MSbuild %SolutionPath% /p:Configuration=Release /p:Platform="Any CPU"
Echo End Time - %Time%
REM Set /p Wait=Build Process Completed...


REM Runs the console application
"C:\Users\320053826\source\repos\CaseStudyG4\bin\Debug\CaseStudyG4.exe"
