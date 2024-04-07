---
lab:
    title: 'GitHub Security For Azure DevOps'
    
---

## Exercise overview

In this lab, you'll configure **GitHub Advanced Security** for Azure DevOps.

## Objectives

After you complete this exercise, you will be able to:

- Understand GitHub Advanced Security with CodeQL and Dependabot
- Understand Azure DevOps Project Security using GitHub Advanced Security

## Instructions

### Exercise 1: Set Up GitHub Advanced Security in Azure DevOps

#### Task 1: Enable ADO Advanced Security for your repo

1. Go to your Project settings for your Azure DevOps project.
1. Select **Repos / Repositories**.
1. Select the **Settings** tab.
1. Select **Enable all** in the **Advanced Security** section; and you'll see an estimate for the number of active committers for your project appear.
1. Select Begin billing to activate Advanced Security for every existing repository in your project.
1. Optionally, select Automatically enable Advanced Security for new repositories so that any newly created repositories have Advanced Security enabled upon creation.
> Note: GitHub Advanced Security can be enabled on **Repo** level or on **Project** level or on **Organization** level.

#### Task 2: Create the Dotnet CI Build Pipeline in ADO Pipelines

1. From **Pipelines**, **create** new Pipeline.
1. Select **Azure Repos Git**, select the **MyRetailBizzNizz** repo
1. Select **Starter Pipeline**
1. **Remove** the sample code from the Starter Pipeline, and replace with the following snippet:

```
#NAME THE PIPELINE SAME AS FILE (WITHOUT ".yml")
# trigger:
# - main

resources:
  repositories:
    - repository: self
      trigger: none

stages:

- stage: Build
  dependsOn:
  variables:
    dotnetSdkVersion: "6.0.x"
    buildConfiguration: "Release"
  displayName: Build .Net Core Solution
  jobs:
  - job: build_dotnet
    displayName: build the dotnet functions
    steps:
      - checkout: self

      - task: UseDotNet@2
        displayName: "Use .NET SDK $(dotnetSdkVersion)"
        inputs:
          version: "$(dotnetSdkVersion)"

      - task: DotNetCoreCLI@2
        displayName: "Restore project dependencies"
        inputs:
          command: "restore"
          projects: "src/MyRetailBizzNizz-Tollbooth/**/*.csproj"

      - task: DotNetCoreCLI@2
        displayName: "Build the project - $(buildConfiguration)"
        inputs:
          command: "build"
          arguments: "--no-restore --configuration $(buildConfiguration)"
          projects: "src/MyRetailBizzNizz-Tollbooth/**/*.csproj"

      - task: DotNetCoreCLI@2
        displayName: "Publish the project - $(buildConfiguration)"
        inputs:
          command: "publish"
          projects: "src/MyRetailBizzNizz-Tollbooth/**/*.csproj"
          publishWebProjects: false
          arguments: "--no-build --configuration $(buildConfiguration) --output $(dotnetFunctionZipPath)"
          zipAfterPublish: true

      - publish: $(dotnetFunctionZipPath)
        artifact: dotnet
```
1. This pipeline has everything that is required to perform a **Build-CI** operation for a CSharp-Dotnet project.
1. Click **Save and Run**
1. Wait for the Pipeline to run, and wait to successfully complete the build stage.

### Exercise 2: COnfigure the AdvancedSecurity Tasks in ADO Pipelines

#### Task 1: Add the necessary AdvancedSecurity tasks

1. The AdvancedSecurity tasks consists of 3 steps, which should be added as individual tasks within your Build (CI) Pipeline:
- AdvancedSecurityCodeQLInit - Initializes the GitHub Security CodeQL engine
- AdvancedSecurityDependencyScanning - Runs the Dependency Scanning of the Built Code
- AdvancedSecurityCodeQLAnalyze - RUns the analyzer / actual scanning of the code

1. **Edit** the current Pipeline, and add the following snippet right **below** the **- checkout: self** section:

```
 - task: AdvancedSecurity-Codeql-Init@1
        inputs:
          languages: "csharp"

```

1. At the end of the current Pipeline code, right **below** the **artifact: dotnet** line of the **- publish: $(dotnetFunctionZipPath)** section, add the following Tasks:

```
      - task: AdvancedSecurity-Dependency-Scanning@1


      - task: AdvancedSecurity-Codeql-Analyze@1
```

1. CLick **Save and Run** and watch the new build progress.
1. The **AdvancedSecurityCodeqlInit** step initializes the CodeQL database, showing the following output:

```
Created skeleton CodeQL database at /home/vsts/work/_temp/advancedsecurity.codeql/d/csharp. This in-progress database is ready to be populated by an extractor.
The CodeQL database has been initialized.

====================================================================================================
Analyzing CodeQL execution results.
The Initialize task succeeded without any issues.
====================================================================================================

Learn more about the scan for the CodeQL build tasks:
https://aka.ms/advanced-security/code-scanning/detection

Finishing: AdvancedSecurityCodeqlInit
```
1. Next, the usual Code Build process starts running, identical to the previous Pipeline run
1. The **AdvancedSecurityDependencyScanning** runs the necessary detection of the Code Set, in our example for Nuget (DotNet), showing the following output:
```
                               Detection Summary                                
┌───────────────────┬───────────────────┬───────────────────┬──────────────────┐
│ Component         │ Detection Time    │ # Components      │ # Explicitly     │
│ Detector Id       │                   │ Found             │ Referenced       │
├───────────────────┼───────────────────┼───────────────────┼──────────────────┤
│ CocoaPods         │ 0.11 seconds      │ 0                 │ 0                │
│ Go                │ 0.11 seconds      │ 0                 │ 0                │
│ Gradle            │ 0.11 seconds      │ 0                 │ 0                │
│ MvnCli            │ 3 seconds         │ 0                 │ 0                │
│ Npm               │ 0.27 seconds      │ 0                 │ 0                │
│ NpmLockfile3      │ 0.11 seconds      │ 0                 │ 0                │
│ NpmWithRoots      │ 0.11 seconds      │ 0                 │ 0                │
│ NuGet             │ 0.11 seconds      │ 0                 │ 0                │
│ NuGetPackagesConf │ 0.11 seconds      │ 0                 │ 0                │
│ ig                │                   │                   │                  │
│ NuGetProjectCentr │ 0.25 seconds      │ 108               │ 11               │
│ ic                │                   │                   │      
...
...

Submitted 108 registrations.
Took 0.52 seconds to submit the registrations.
Waiting for vulnerabilities to be processed for submitted registrations...
Retrieved vulnerability processing status of "Completed".
Waiting for Advanced Security SARIF processing to complete...
##[warning] Dependency Scanning has detected 2 package vulnerabilities.

View these alerts in Advanced Security:
https://dev.azure.com/<yourproject>/myretailbizznizz/_git/myretailbizznizz/alerts?_t=dependencies

  1. "High": Azure Identity SDK Remote Code Execution Vulnerability (CVE-2023-36414)
     package: NuGet azure.identity 1.4.0
     alert #5: https://dev.azure.com/<yourproject>/myretailbizznizz/_git/myretailbizznizz/alerts/5?branch=refs%2Fheads%2Fmain
     locations:
     - path: s/src/MyRetailBizzNizz-Tollbooth/FastCarDev.TollBoothFunction/TollBooth/TollBooth.csproj

  2. "Medium": Microsoft: CBC Padding Oracle in Azure Blob Storage Encryption Library (CVE-2022-30187)
     package: NuGet azure.storage.queues 12.10.0
     alert #6: https://dev.azure.com/<yourproject>/myretailbizznizz/_git/myretailbizznizz/alerts/6?branch=refs%2Fheads%2Fmain
     locations:
     - path: s/src/MyRetailBizzNizz-Tollbooth/FastCarDev.TollBoothFunction/TollBooth/TollBooth.csproj

Learn more about the Advanced Security Dependency Scanning build task:
https://aka.ms/advancedsecurity/dependency-scanning/detection

Execution finished, status: 0.
Process terminating.
Finishing: AdvancedSecurityDependencyScanning

```
1. Followed by the actual **CodeAnalyzeQL** task, which brings the actual security analysis and execution results, similar to the following output:

```
Starting database finalization.
Database finalization is a time consuming process and may take a while to complete.
Finalizing csharp
Starting query analysis.
Running builtin query pack csharp-security-extended.qls for csharp
Combining all SARIF files into one SARIF file
Ensuring Advanced Security SARIF requirements
Adding VersionControlProvenance to the SARIF file
Adding automation details id and run Properties to the SARIF file
Submitting results to the AdvancedSecurity service.
Submission completed successfully.
The submission receipt from Advanced Security is 14

====================================================================================================
Analyzing CodeQL execution results.
The CodeQL analysis has successfully completed, however no violations were detected for the following language(s): 'csharp'.
If you think there should be violations, please refer to this document for troubleshooting https://aka.ms/codeQL-no-result
====================================================================================================


```

## Review

In this exercise you used GitHub Advanced Security CodeQL Code Scanning, as part of Azure DevOps Build Pipelines.




