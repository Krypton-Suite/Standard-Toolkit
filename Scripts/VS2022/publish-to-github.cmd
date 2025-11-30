@echo off

dotnet nuget push "../bin/Release/*.nupkg" --source "github"