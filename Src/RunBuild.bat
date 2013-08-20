%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Build.proj /p:TargetFrameworkVersion=v4.0 /target:BeforeBuild;BuildSolution;RunTestsOnLocalMachine;CopyToArtifacts
pause