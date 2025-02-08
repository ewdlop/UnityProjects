Param(
  [Parameter(Mandatory=$true)][string] $barToken,       # Token generated at https://maestro-prod.westus2.cloudapp.azure.com/Account/Tokens
  [Parameter(Mandatory=$true)][string] $gitHubPat,      # GitHub personal access token from https://github.com/settings/tokens (no auth scopes needed)
  [Parameter(Mandatory=$true)][string] $azdoPat,        # Azure Dev Ops tokens from https://dev.azure.com/dnceng/_details/security/tokens (code read scope needed)
  [Parameter(Mandatory=$true)][string] $outputFolder,   # Where the graphviz.txt file will be created
  [string] $darcVersion,                                # darc's version
  [string] $graphvizVersion = '2.38',                   # GraphViz version
  [switch] $includeToolset                              # Whether the graph should include toolset dependencies or not. i.e. arcade, optimization. For more about
                                                        # toolset dependencies see https://github.com/dotnet/arcade/blob/master/Documentation/Darc.md#toolset-vs-product-dependencies
)

function CheckExitCode ([string]$stage)
{
  $exitCode = $LASTEXITCODE
  if ($exitCode  -ne 0) {
    Write-PipelineTelemetryError -Category 'Arcade' -Message "Something failed in stage: '$stage'. Check for errors above. Exiting now..."
    ExitWithExitCode $exitCode
  }
}

try {
  $ErrorActionPreference = 'Stop'
  . $PSScriptRoot\tools.ps1
  
  Import-Module -Name (Join-Path $PSScriptRoot 'native\CommonLibrary.psm1')

  Push-Location $PSScriptRoot

  Write-Host 'Installing darc...'
  . .\darc-init.ps1 -darcVersion $darcVersion
  CheckExitCode 'Running darc-init'

  $engCommonBaseDir = Join-Path $PSScriptRoot 'native\'
  $graphvizInstallDir = CommonLibrary\Get-NativeInstallDirectory
  $nativeToolBaseUri = 'https://netcorenativeassets.blob.core.windows.net/resource-packages/external'
  $installBin = Join-Path $graphvizInstallDir 'bin'

  Write-Host 'Installing dot...'
  .\native\install-tool.ps1 -ToolName graphviz -InstallPath $installBin -BaseUri $nativeToolBaseUri -CommonLibraryDirectory $engCommonBaseDir -Version $graphvizVersion -Verbose

  $darcExe = "$env:USERPROFILE\.dotnet\tools"
  $darcExe = Resolve-Path "$darcExe\darc.exe"

  Create-Directory $outputFolder

  # Generate 3 graph descriptions:
  # 1. Flat with coherency information
  # 2. Graphviz (dot) file
  # 3. Standard dependency graph
  $graphVizFilePath = "$outputFolder\graphviz.txt"
  $graphVizImageFilePath = "$outputFolder\graph.png"
  $normalGraphFilePath = "$outputFolder\graph-full.txt"
  $flatGraphFilePath = "$outputFolder\graph-flat.txt"
  $baseOptions = @( '--github-pat', "$gitHubPat", '--azdev-pat', "$azdoPat", '--password', "$barToken" )

  if ($includeToolset) {
    Write-Host 'Toolsets will be includ