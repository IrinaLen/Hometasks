#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool nuget:?package=Antlr&version=3.5.0.2
#tool "nuget:?package=DotNet.Contracts&version=1.10.20606.1"
#tool nuget:?package=DotParser&version=1.0.6
#tool nuget:?package=FSharp.Core&version=4.2.3
#tool nuget:?package=FSharpx.Collections&version=1.17.0
#tool nuget:?package=FSharpx.Core&version=1.8.32
#tool nuget:?package=GraphViz4Net&version=2.0.33
#tool nuget:?package=YC.QuickGraph&version=3.7.3


var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");
var buildDir = Directory("./Grammar/Grammar/bin") + Directory(configuration);



Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./Grammar/Grammar.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./Grammar/Grammar.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./Grammar/Grammar.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});


RunTarget(target);