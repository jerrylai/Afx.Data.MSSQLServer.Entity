@echo off
set Build="%SYSTEMDRIVE%\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MsBuild.exe"
if exist publish rd /s /q publish
dotnet build "NETStandard2.0/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" -c Release
dotnet build "NET6.0/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" -c Release
cd publish
del /q/s *.pdb
del /q/s EntityFramework*
del /q/s Afx.Data.Entity.dll Afx.Data.Entity.dll.config Afx.Data.Entity.xml
del /q/s Afx.Data.dll Afx.Data.xml Afx.Data.MSSQLServer.Entity.dll.config
pause