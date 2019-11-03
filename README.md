[![Build Status](https://dev.azure.com/NorikaDE/Xml-Help/_apis/build/status/NorikaDE.Xml-Help?branchName=master)](https://dev.azure.com/NorikaDE/Xml-Help/_build/latest?definitionId=1&branchName=master)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/NorikaDE/Xml-Help/1)
![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NorikaDE/XML-Help/1)

# Xml-Help
This class library adds functionality to your program to deal with comment based xml help.

```xml
  <!--
	 .SYNOPSIS
	 Copies given files to the output directory
	 
	 .DESCRIPTION
	 Copies given files to the output directory
	 
	 .PARAMETER $(CopyDestinationPath)
	 Where should the files go to? 
	 
	 .PARAMETER $(CopyDestinationSource)
	 Where should the files got from?
	 
	 .EXAMPLE
	 <CallTarget Targets="CoptyOutputToDestination" />
	 <!~~ Calls the target "CopyOutputToDestination" >

  -->
  <Target Name="CopyOutputToDestination">
    <ItemGroup>
      <OutputFiles Include="$(OutDir)**\*"></OutputFiles>
    </ItemGroup>
    <Message Text="Copying output file to destination: @(OutputFiles)" Importance="high"/>
    <Copy SourceFiles="@(OutputFiles)" 
          DestinationFolder="$(CopyDestionationPath)\%(RecursiveDir)" 
          OverwriteReadOnlyFiles="true"></Copy>
    <OnError ExecuteTargets="OnErrorTarget" />
  </Target>
```
