---
lab:
    title: 'Implementing Azure DevOps MultiStage CI-CD'
 
---

## Lab overview

In this lab, you'll learn how to implement a multi-stage Azure DevOps Pipeline, running a code build process (CI), as well as deploying different Azure Resources, followed by publishing (CD) the build artifact to Azure Functions and EventGrid.

## Objectives

After you complete this lab, you will be able to:

- Implement an Azure DevOps Pipeline for CI/CD.
- Explain the basic characteristics of multi-stage Azure DevOps Pipelines.

## Instructions

### Exercise 1: Configure CI/CD Pipelines as Code with YAML in Azure DevOps

In this exercise, you will configure CI/CD Pipelines as code with YAML in Azure DevOps.

#### Task 1: Add a YAML build definition

In this task, you will add a YAML build definition to the existing project.

1. Navigate to the **Pipelines** pane in the **Pipelines** hub.
1. In the **Create your first Pipeline** window, click **Create pipeline**.

   > **Note**: We will use the wizard to create a new YAML Pipeline definition based on our project.

1. On the **Where is your code?** pane, click **Azure Repos Git (YAML)** option.
1. On the **Select a repository** pane, click **MyRetailBizzNizz**.
1. On the **Configure your pipeline** pane, scroll down and select **Starter Pipeline**.
1. This will provide you with a **sample YAML** pipeline. You will build up the Azure Infrastructure stage, CI-stage, and necessary CD-stages as part of this exercise.
1. From the **Save and Run** button, CLick **Save** to Save the pipeline as-is. 
1. From the **Pipeline** view, click the ellipsis (...) and select **Rename/Move**. Replace the default name of **MyRetailBizzNizz** with **TollboothCICD**. CLick **Save** to confirm.
1. Open the **TollboothCICD** Pipeline by clicking the **Edit** button.

1. Delete the full sample YAML, and gradually build up the new YAML pipeline structure, by copying in different code snippets sections:

```
name: $(BuildDefinitionName)_$(date:yyyyMMdd)$(rev:.r)-$(RunName)

trigger: none

pr: none
```

This section creates a dynamic **name** for the Pipeline run; we set the **trigger:none** and **pr:none**, to only allow a manual start of the job.

1. Next, below the pr:none line, paste in the following **parameters** section:

```
parameters:
  - name: Alias
    type: string
    default: petender
  - name: azureSubscription
    type: string
    default: azureSubscription
  - name: Location
    type: string
    default: eastus

```

1. Followed by leaving an empty line for the next **variables** section:

```
variables:
  - name: RunName
    value: "${{parameters.Alias}}"
  - name: RGName 
    value: "RG-TOLLBOOTH"
  - name: dotnetFunctionZipPath
    value: $(Build.ArtifactStagingDirectory)/dotnet
  - name: nodeFunctionZipPath
    value: $(Build.ArtifactStagingDirectory)/node
  - name: pipelineName
    value: $(Build.DefinitionName)
  - name: recipientEmail
    value: "${{parameters.Email}}" 
```
1. Notice the use of Azure DevOps variables and parameters, using the $() and ${{}} syntax.

1. With variables and parameters defined, leave another empty line for the first **stage**, which will deploy the Azure Infrastructure from a Bicep file.

```
stages:
  - stage: Infra
    jobs:
      - job: Bicep
        steps:
          - checkout: self

          - task: PowerShell@2
            displayName: 'Generate Random String for Naming Resources'
            inputs:
              targetType: 'inline'
              script: |
                # Your PowerShell script here
                
                #generate random string for naming resources using lowercase and digits only
                do {
                $randomName = -join ((97..122) + (48..57) | Get-Random -Count 12 | % {[char]$_})
                } while ($randomName -notmatch '^[a-zA-Z][a-zA-Z0-9-]{6,50}[a-zA-Z0-9]$')

                Write-Host "##vso[task.setvariable variable=genrandom;]$randomName"
                Write-Host $genrandom


          - task: AzureCLI@2
            name: deployBicep
            inputs:
              azureSubscription: ${{ parameters.azureSubscription }}
              scriptType: "pscore"
              scriptLocation: "inlineScript"
              inlineScript: |
                az group create --location ${{parameters.Location}} --name $(RGName)
                $out = $(az deployment group create -f ./infra/tollbooth/main.bicep -g $(RGName) --parameters namingConvention=tb$(genrandom) location=${{parameters.Location}} -o json --query properties.outputs) | ConvertFrom-Json
                $out.PSObject.Properties | ForEach-Object {
                  $keyname = $_.Name
                  $value = $_.Value.value
                  echo "##vso[task.setvariable variable=$keyname;isOutput=true]$value"
                }
```
1. This stage has **2 tasks**; in the first task, we run a **PowerShell script** to create a **random string of 12 characters**, using a **Regular Expression** for **lowercase/uppercase and numeric characters**. Using the line **Write-Host #vso[task.setvariable...]**, we specify a new variable called **genrandom**, which corresponds to the other variable $randomName value. This genrandom-variable will be reused in the next task. The second task runs **Azure CLI**, and deploys a **main.bicep** Azure Bicep template, which deploys the necessary Azure Infrastructure (Storage Account, Functions, CosmosDB, EventGrid, KeyVault,...). Notice how the **namingConvention parameter** reuses the **genrandom** variable from the previous task. 

1. Next, we specify the **Build Stage (CI)** for the **Dotnet Functions** which are part of this project:

```
  - stage: BuildDotNet
    dependsOn:
    variables:
      dotnetSdkVersion: "6.0.x"
      buildConfiguration: "Release"
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
1. In short, this stage connects to the **src** source folder, and looks for any **dotnet csharp project** in the **src/MyRetailBizzNizz** folder, and runs a **build** and **publish** task.

1. In the next stage, we run the **CI-Build process** for the **Node Functions** as part of this Azure Architecture:

```
  - stage: BuildNode
    dependsOn:
    variables:
      nodePath: "./src/MyRetailBizzNizz-Tollbooth/FastCarDev.SavePlateDataFunction"
      nodeZipName: "savePlateDataFunction.zip"
    jobs:
      - job: build_node
        displayName: build the node functions
        pool:
          vmImage: "ubuntu-latest"
          demands:
            - npm
        steps:
          - checkout: self

          - task: Npm@1
            displayName: "Run npm install"
            inputs:
              workingDir: $(nodePath)
              verbose: false

          - task: ArchiveFiles@2
            displayName: "Archive files"
            inputs:
              rootFolderOrFile: "$(System.DefaultWorkingDirectory)/$(nodePath)"
              includeRootFolder: false
              archiveType: zip
              archiveFile: $(nodeFunctionZipPath)/$(nodeZipName)
              replaceExistingArchive: true

          - upload: $(nodeFunctionZipPath)/$(nodeZipName)
            artifact: node
```

1. Which gets followed by the next **stage**, **DeployCode**, which picks up the **CI-Build Artifacts** from the **Build drop folders**, and deploys to Azure; this is using the **built-in Azure DevOps tasks** for **Azure WebApps** and **Azure Functions**.
> Note: there has been a recent issue since mid March, where these prebuilt tasks don't always work, providing an error message "resource WebApp xyz123 not found". Resource must exist before running the deployment. If you face this issue, try replacing the prebuilt tasks for WebApps and Functions with their corresponding Azure CLI alternative. (Hint: Microsoft Copilot knowns how to do this...)

```
  - stage: DeployCode
    displayName: Deploy code
    dependsOn: [Infra, BuildDotNet, BuildNode]
    condition: succeeded()
    variables:
      processImageFnName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.ProcessImageFnName']]
      savePlateFnName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.SavePlateFnName']]
      imageUploadAppName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.UploadImageWebAppName']]
    jobs:
      - deployment: Deploy
        environment: Demo
        displayName: Deploy
        strategy:
          runOnce:
            deploy:
              steps:
                - task: AzureFunctionApp@2
                  displayName: "Azure Function Deploy: ProcessImage"
                  inputs:
                    connectedServiceNameARM: ${{ parameters.azureSubscription }}
                    appType: functionApp
                    appName: $(processImageFnName)
                    package: "$(Pipeline.Workspace)/dotnet/TollBooth.zip"

                - task: AzureWebApp@1
                  displayName: "Azure Web App: Upload Image"
                  inputs:
                    azureSubscription: ${{ parameters.azureSubscription }}
                    appType: webAppLinux
                    appName: $(imageUploadAppName)
                    package: "$(Pipeline.Workspace)/dotnet/FastCarDev.UploadImage.zip"

                - task: AzureFunctionApp@2
                  displayName: "Azure Function Deploy: SavePlateData"
                  inputs:
                    connectedServiceNameARM: ${{ parameters.azureSubscription }}
                    appType: functionApp
                    appName: $(savePlateFnName)
                    package: "$(Pipeline.Workspace)/node/*.zip"
```

1. Which brings us to the last **Stage**, deploying the **Event Code** to Azure Resources:

```
  - stage: EventSubs
    dependsOn: [Infra, DeployCode]
    jobs:
      - job: Bicep
        variables:
          processImageFnName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.ProcessImageFnName']]
          savePlateFnName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.SavePlateFnName']]
          eventGridTopicName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.EventGridTopicName']]
          dataLakeAccountName: $[stageDependencies.Infra.Bicep.outputs['deployBicep.StorageAccName']]
        steps:
          - checkout: self
          - task: AzureCLI@2
            name: deployBicep
            inputs:
              azureSubscription: ${{ parameters.azureSubscription }}
              scriptType: "pscore"
              scriptLocation: "inlineScript"
              inlineScript: |
                echo $(processImageFnName)
                $key = az functionapp keys list -g $(RGName) -n $(processImageFnName) --query systemKeys.blobs_extension --output tsv
                az deployment group create -f ./infra/tollbooth/eventgridsub.bicep -g $(RGName) `
                  --parameters dataLakeAccountName=$(dataLakeAccountName) processImageFnName=$(processImageFnName) `
                    eventGridTopicName=$(eventGridTopicName) savePlateFnName=$(savePlateFnName) `
                    blobExtensionKey=$key
```
1. With all code snippets entered, click **Validate and Save**. This should throw a few error messages, related to **azureSubscription** and **connectedServiceNameARM**. This will be fixed in the next task.
1. For now, click **Save Anyway** to save the pipeline code. Confirm by clicking **Save** again.

### Exercise 2: Configure Azure DevOps Service Connection for Azure Deployments

In this exercise, you will configure CI/CD Pipelines as code with YAML in Azure DevOps.

#### Task 1: Create an Azure Service Principal and save it as ADO Service Connection

In this task, you will create the Azure Service Principal used by Azure DevOps to deploy the desired resources. 

1. On your lab computer, in a browser window, open the Azure Portal (https://portal.azure.com/).
1. In the Azure Portal, open the **Cloud Shell** (next to the search bar).

    > NOTE: if this is the first time you open the Cloud Shell, you need to configure the [persistent storage](https://learn.microsoft.com/azure/cloud-shell/persisting-shell-storage)

1. Make sure the terminal is running in **Bash** mode and execute the following command, replacing **SUBSCRIPTION-ID** and **RESOURCE-GROUP** with your own identifiers (both can be found on the **Overview** page of the Resource Group):

    `az ad sp create-for-rbac --name GH-Action-MyRetailBizzNizz --role contributor --scopes /subscriptions/SUBSCRIPTION-ID/ --sdk-auth`

    > NOTE: Make sure this is typed or pasted as a single line!
    > NOTE: this command will create a Service Principal with Contributor access to the Azure Subscription. In real-life scenarios, you might consider using the **Least Privilege** concept, by providing permissions to a Resource Group only, if applicable in your setup.

1. The command will output a JSON object, you will later use the credentials for the ADO Service Connection. Copy the JSON. The JSON contains the identifiers used to authenticate against Azure in the name of a Microsoft Entra identity (service principal).

    ```JSON
        {
            "clientId": "<GUID>",
            "clientSecret": "<GUID>",
            "subscriptionId": "<GUID>",
            "tenantId": "<GUID>",
            (...)
        }
    ```
1. Apart from the Service Principal credentials, also **copy** the **subscription Id** aside; this can be found from the **Azure Portal / Search / Subscriptions**.
1. You also need to run the following command to register the resource provider for the **Azure App Service** you will deploy later:

   ```bash
   az provider register --namespace Microsoft.Web
   ```
1. With the necessary **Service Principal** created, head back to your **Azure DevOps project**, and navigate to **Project Settings**. 
1. From the Pipelines submenu, select **Service Connections**, **Create Service Connection**.
1. From the list of options, select **Azure Resource Manager**.
1. In the **New Azure Service Connection**, select **Service Principal (manual).**
1. **Copy over** the 4 credentials from the JSON output, as well as the Azure Subscription Name and Id in the corresponding fields.
1. In the **Service Connection Name** field, enter **azureSubscription** (which is the name of the parameter in the YAML Pipeline used)
1. Under **Security**, check the setting **Grant access permission to all pipelines**.
1. Click **Verify and Save**. 

### Exercise 3: Manually Trigger the Tollbooth CI/CD MultiStage Pipeline

In this exercise, you will manually trigger the MultiStage CI/CD Pipeline as code with YAML in Azure DevOps.

#### Task 1: Trigger the Tollbooth pipeline

1. From the ADO Project, navigate to **Pipelines**. Select the **TollboothCICD** Pipeline. 
1. Click **Run pipeline**.
1. From the **Run pipeline** window, confirm the different parameters:
- Alias: your alias of choice
- azureSubscription: azureSubscription (or the name of the Service Connection if you did not used the name azureSubscription)
- Location: Azure region of your choice - using the shortregion naming convention e.g. eastus, centralus, westeurope,...
1. Click **Run** to start the pipeline.
1. Notice the graphical view of the different **Stages**.
1. Wait for the different Stages to kick-off, to follow the details within each **Job**.
1. Once the **Bicep** stage kicks off, you can see it **connects to Azure** and creates the **Resource Group**; further details from ARM are not visible in ADO, but can be viewed from **Azure DevOps / Resource Group / Deployments**. If the deployment should fail, inspect the Azure error messages. If you want to trigger a new Run, delete the Resource Group first if the error is related to a Region quota issue.

> Note: full CI/CD pipeline deployment should take about 5-7min on average.



## Review

In this lab, you implemented a Multi-Stage Azure DevOps YAML Pipeline for CI/CD, that deploys different Azure Resources using a Bicep template, followed by the necessary Build-CI stages for Dotnet and Node applications. Closed by running a CD-Release pipeline stage to publish the actual Functions and WebApp code into the Azure Resources.
