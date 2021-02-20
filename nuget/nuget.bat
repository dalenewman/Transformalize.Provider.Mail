nuget pack Transformalize.Provider.Mail.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Mail.Autofac.nuspec -OutputDirectory "c:\temp\modules"

REM nuget push "c:\temp\modules\Transformalize.Provider.Mail.0.8.35-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Mail.Autofac.0.8.35-beta.nupkg" -source https://api.nuget.org/v3/index.json






