param(
    [String] $majorMinor = "1.0.0", # 1.4
    [String] $patch = "0",          # $env:APPVEYOR_BUILD_VERSION
    [String] $branch = "master",    # $env:APPVEYOR_REPO_BRANCH
    [String] $customLogger = "",    # C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll
    [Switch] $notouch
)

function Set-AssemblyVersions($informational, $file, $assembly)
{
    (Get-Content assets/CommonAssemblyInfo.cs) |
        ForEach-Object { $_ -replace "AssemblyVersion\(""(.*?)""\)", "AssemblyVersion(""$assembly"")" } |
        ForEach-Object { $_ -replace "AssemblyInformationalVersion\(""(.*?)""\)", "AssemblyInformationalVersion(""$informational"")" } |
        ForEach-Object { $_ -replace "AssemblyFileVersion\(""(.*?)""\)", "AssemblyFileVersion(""$file"")" } |
        Set-Content assets/CommonAssemblyInfo.cs
}

function Install-NuGetPackages()
{
    nuget restore RichardLawley.WebApi.FluentValidation.sln
}

function Invoke-MSBuild($solution, $customLogger)
{
    if ($customLogger)
    {
        msbuild "$solution" /verbosity:minimal /p:Configuration=Release /logger:"$customLogger"
    }
    else
    {
        msbuild "$solution" /verbosity:minimal /p:Configuration=Release
    }
}

function Invoke-NuGetPackProj($csproj)
{
    nuget pack -Prop Configuration=Release -Symbols $csproj
}

function Invoke-NuGetPackSpec($nuspec, $version)
{
    nuget pack $nuspec -Version $version -OutputDirectory ..\..\
}

function Invoke-NuGetPack($version)
{
    Invoke-NuGetPackProj src\RichardLawley.WebApi.FluentValidation\RichardLawley.WebApi.FluentValidation.csproj
    pushd .\src\RichardLawley.WebApi.FluentValidation
    Invoke-NuGetPackSpec "RichardLawley.WebApi.FluentValidation.nuspec" $version
    popd
}

function Invoke-Build($majorMinor, $patch, $branch, $customLogger, $notouch)
{
    $target = "$majorMinor"
    $file = "$target.$patch"
    $package = $target
    if ($branch -ne "master")
    {
        $package = "$target-pre-$patch"
    }

    Write-Output "Building RichardLawley.WebApi.FluentValidation $package"

    if (-not $notouch)
    {
        $assembly = "$majorMinor.0"

        Write-Output "Assembly version will be set to $assembly"
        Set-AssemblyVersions $package $file $assembly
    }

    Install-NuGetPackages
    
    Invoke-MSBuild "RichardLawley.WebApi.FluentValidation.sln" $customLogger

    Invoke-NuGetPack $package
}

$ErrorActionPreference = "Stop"
Invoke-Build $majorMinor $patch $branch $customLogger $notouch
