# msbuild ColorSharp.csproj /t:Build /p:Configuration="Release 4.0"
#nuget pack ColorSharp.csproj -build -Prop Platform=AnyCPU -Prop Configuration="Release 4.5" -IncludeReferencedProjects

xbuild /target:Build /property:Configuration="Release 4.5"  ColorSharp.csproj
xbuild /target:Build;Package; /property:Configuration="Release 4.0" ColorSharp.csproj