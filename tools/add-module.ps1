param (
	[Parameter(Mandatory=$true)]
	[string]$Name, 
	[Parameter(Mandatory=$true)]
	[ValidateNotNullOrEmpty()]
	[ValidateSet("Feature","Foundation","Project")]
	[string]$Layer = "Feature"
)

$repoRoot = Split-Path $MyInvocation.MyCommand.Path
$repoRoot = $repoRoot + "\..\"
Push-Location $repoRoot
$SolutionName = "Learn.Helix"
$ShouldUTNeeded = Read-Host "Do you want to create Unit Test for this project $Name ? Y or N"

dotnet new learnhelix-project -n "$SolutionName.$Layer.$Name" -o "./src/$Layer/$Name" -la "$Layer"
dotnet sln (Resolve-Path -Path "$SolutionName.sln").Path add -s "$Layer\$Name" "./src/$Layer/$Name/code/$SolutionName.$Layer.$Name.csproj"
if($ShouldUTNeeded -eq "Y")
{
	dotnet new learnhelix-project-test -n "$SolutionName.$Layer.$Name.Test" -o "./src/$Layer/$Name"
    dotnet sln (Resolve-Path -Path "$SolutionName.sln").Path add -s "$Layer\$Name" "./src/$Layer/$Name/tests/$SolutionName.$Layer.$Name.Test.csproj"
}
Pop-Location