nuget pack Transformalize.Provider.Mail.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Mail.Autofac.nuspec -OutputDirectory "c:\temp\modules"

nuget push "c:\temp\modules\Transformalize.Provider.Mail.0.10.2-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Provider.Mail.Autofac.0.10.2-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
