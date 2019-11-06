# Xml-Help
This class library adds functionality to your program to deal with comment based xml help. Like shown in the following sample you could use the dotnet xml implementation to access the xml based help.

## Build
[![Build Status](https://dev.azure.com/NorikaDE/Xml-Help/_apis/build/status/NorikaDE.Xml-Help?branchName=master)](https://dev.azure.com/NorikaDE/Xml-Help/_build/latest?definitionId=1&branchName=master)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/NorikaDE/Xml-Help/1)
![Azure DevOps tests (compact)](https://img.shields.io/azure-devops/tests/NorikaDE/XML-Help/1?compact_message)
[![CodeFactor](https://www.codefactor.io/repository/github/norikade/xml-help/badge/master)](https://www.codefactor.io/repository/github/norikade/xml-help/overview/master)

## Sample

### XML Comment based help
This following sample represents a msbuild target (xml) with a comment based help. The next step shows how to access the help with the code provided by this repository. 

The help syntax is derived from the [PowerShell Comment Based Help](https://docs.microsoft.com/en-us/PowerShell/module/microsoft.PowerShell.core/about/about_comment_based_help?view=powershell-6).

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
	   <!~~ Calls the target "CopyOutputToDestination" ~~>

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

### Accessing the comment based help
You can use the method `GetHelp()` as extension on every XmlElement or XmlNode.


```cs
 XmlDocument document = new XmlDocument();
 document.Load(myFile);
 
 XmlElement myWantedElement = document.GetElementsByName("Target").First();
 IXmlHelp myWantedElementHelp = myWantedElement.GetHelp();
 
 IXmlCommentHelpParagraph descriptionHelpParagraph = myWantedElementHelp.LookUp("Description");
 Console.WriteLine(descriptionHelpParagraph.Content);
 // Writes "Copies given files to the output directory" to the console output
```
