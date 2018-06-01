#tool "nuget:?package=GitVersion.CommandLine"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./bin") + Directory(configuration);

// We will set this during GitVersion
var nugetVer = "";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./LukeMapper.sln");
});

Task("UpdateAssemblyInfo")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var gitVersion = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });

   

    var semVerMeta = gitVersion.FullBuildMetaData;
    if (!semVerMeta.StartsWith("0")) {
        nugetVer = gitVersion.SemVer + "-beta-" + gitVersion.BuildMetaData;
    }
});

Task("Build")
    .IsDependentOn("UpdateAssemblyInfo")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./LukeMapper.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
      var nuGetPackSettings   = new NuGetPackSettings {
                                     Version                 = nugetVer,
                                     OutputDirectory         = "./nuget"
                                 };

     NuGetPack("./LukeMapper.csproj", nuGetPackSettings);
});

// Task("Run-Unit-Tests")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
//         NoResults = true
//         });
// });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);