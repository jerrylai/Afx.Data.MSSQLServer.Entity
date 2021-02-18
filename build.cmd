@echo off
set Build="%SYSTEMDRIVE%\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MsBuild.exe"
if exist publish rd /s /q publish
%Build% "NET40/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" /t:Rebuild /p:Configuration=Release
%Build% "NET45/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" /t:Rebuild /p:Configuration=Release
dotnet build "NETStandard2.0/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" -c Release
dotnet build "NETStandard2.1/Afx.Data.MSSQLServer.Entity/Afx.Data.MSSQLServer.Entity.csproj" -c Release
cd publish
del /q/s *.pdb
del /q/s EntityFramework*
del /q/s Afx.Data.Entity.dll Afx.Data.Entity.dll.config Afx.Data.Entity.xml
del /q/s Afx.Data.dll Afx.Data.xml Afx.Data.MSSQLServer.Entity.dll.config
pause