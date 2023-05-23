# OpenAI based QAChat App : AskQuestion
A simple C# .NET QAChat app to use with OpenAI's GPT-3.5 API.

![gif](https://user-images.githubusercontent.com/34706028/213882005-78262cde-b02f-4345-b170-5ab4044e7a10.gif)

# Requirements
This library is based on .NET Core 6.0, so it should work across .NET Framework >=4.7.2 and .NET Core >= 3.0. It should work across console apps, winforms, wpf, asp.net, etc. 
It should work across Windows, Linux, and Mac, although I have only tested on Windows so far.

# Getting Started

### Install from NuGet

Install package [`OpenAI` from Nuget](https://www.nuget.org/packages/OpenAI/).  Here's how via commandline:
```powershell
Install-Package OpenAI
```
### Authentication
To provide Authentication to OpenAI:

1. Log in [OpenAI web site](https://beta.openai.com/)
2. Go to [Personal / View API Keys page ](https://beta.openai.com/account/api-keys)
3. Create new secret key and copy it
4. Add user secrets management to your project as shown in figure below
![image](https://user-images.githubusercontent.com/34706028/213882551-e9f1ac25-fb47-43f3-b4f0-9075d9b23943.png)
5. Create APIKey-Value pair in secret.json file

```json 
{ 
  "APIKey": "INSERT YOUR API KEY HERE"  
} 
```

### Create single-file deployment and executable
Bundling all application-dependent files into a single binary provides an application developer with the attractive 
option to deploy and distribute the application as a single file. For details: [Single-file deployment and executable](https://learn.microsoft.com/en-us/dotnet/core/deploying/single-file/overview?WT.mc_id=DX-MVP-5004571&tabs=cli) 

According to this logic, I convert the QAChat App to a single-file executable in below steps. 


1. Add below code blog to App csproj file
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <PublishSingleFile>true</PublishSingleFile>
    <SelfContained>true</SelfContained>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PublishReadyToRun>true</PublishReadyToRun>
  </PropertyGroup>

</Project>
```
2. Run ```dotnet publish -r win-x64 ```

You can find your executable file in **...\AskQuestion\AskQuestion\bin\Debug\net6.0\win-x64** folder (it can change depend on your file structure)
