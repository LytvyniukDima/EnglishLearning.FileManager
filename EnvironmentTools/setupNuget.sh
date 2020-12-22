nuget_pass=$NUGET_TOKEN

dotnet nuget update source github -u LytvyniukDima -p $nuget_pass --store-password-in-clear-text
