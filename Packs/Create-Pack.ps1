param(
    [Parameter(Position = 0)]
    [string]$TargetPack,
    [Parameter(Position = 1)]
    [string]$Version,
    [Parameter(ValueFromRemainingArguments=$true)][String[]]$Sources
)

function SetPropertyValue([System.Xml.XmlDocument]$projFile, [string]$propName, [string]$value) {
    $propertiesNode = $projFile.SelectSingleNode("//PropertyGroup")
    $node = $propertiesNode.SelectSingleNode($propName)
    if ($node) { $propertiesNode.RemoveChild($node) }

    $node = $projFile.CreateElement($propName)
    $node.InnerText = $value
    $propertiesNode.AppendChild($node)
}

Remove-Item $TargetPack -Recurse -ErrorAction Ignore
New-Item -Path $TargetPack -ItemType Directory

$projPath = "$TargetPack/$TargetPack.csproj"
Copy-Item "PackTemplate.csproj" $projPath

$fullProjPath = Join-Path (pwd) $projPath

$projFile = New-Object System.Xml.XmlDocument
$projFile.Load($fullProjPath)

SetPropertyValue $projFile "PackageId" $TargetPack
SetPropertyValue $projFile "Version" $Version
SetPropertyValue $projFile "AssemblyName" $TargetPack

$sources = @("Codoxide.Outcome.Core") + $Sources

$releaseNote = "Composition of:"
foreach ($src in $sources) {
    $srcPath = Resolve-Path(Join-Path (pwd) "../$src/src")
    Copy-Item "$srcPath/*.cs" $TargetPack

    $srcProjXml = [xml](Get-Content "$srcPath/$src.csproj")
    $srcVersion = $srcProjXml.Project.PropertyGroup.Version

    $releaseNote = "$releaseNote`n$src $srcVersion"
}

SetPropertyValue $projFile "PackageReleaseNotes" $releaseNote

$projFile.Save($fullProjPath)