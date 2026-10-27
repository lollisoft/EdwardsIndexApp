@set CONFIGURATION=/p:Configuration=Debug
@rem "/p:Platform=Any CPU"
 
@set VERBOSITY=/verbosity:q
 
@if "%DevEnvDir%" == "" (
	@echo Please start within Visual Studio command line tools.
	@pause Then restart this batch file.
	exit
)

@pause Start building projects


msbuild /nologo          %CONFIGURATION% %VERBOSITY% "Lollisoft.Common.Logging.sln"

msbuild /nologo          %CONFIGURATION% %VERBOSITY% "Lollisoft.DatabaseLogging.ServiceClient.sln"

msbuild /nologo          %CONFIGURATION% %VERBOSITY% "StsService.sln"
msbuild /nologo          %CONFIGURATION% %VERBOSITY% "EdwardsIndexApp.sln"
