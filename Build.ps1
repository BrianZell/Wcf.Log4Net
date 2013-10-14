function Build-Solutions
{
    .$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild .\Wcf.Log4Net.msbuild
}

#.\IncrementVersion.ps1
Build-Solutions