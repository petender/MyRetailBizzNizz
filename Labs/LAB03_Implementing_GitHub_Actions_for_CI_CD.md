---
lab:
    title: 'Implementing GitHub Actions for CI/CD'
 
---

# Implementing GitHub Actions for CI/CD

## Lab overview

In this lab, you'll learn how to implement a GitHub Action workflow that deploys an Azure web app.

## Objectives

After you complete this lab, you will be able to:

- Implement a GitHub Action workflow for CI/CD.
- Explain the basic characteristics of GitHub Action workflows.

## Instructions

### Exercise 0: Import MyRetailBizzNizz to your GitHub Repository

In this exercise, you will import the existing [MyRetailBizzNizz](https://github.com/petender/MyRetailBizzNizz) repository code to your own GitHub private repo.
    
> Note: if you did the GitHub Source Control exercise, you have already imported this repo and can **skip** this step and **continue with Exercise 1**.

The repository is organized the following way:
    - **.ado** folder contains Azure DevOps YAML pipelines.
    - **.azure** folder contains Azure ARM/Bicep templates to deploy Azure Resources.
    - **.github** folder container YAML GitHub workflow definitions.
    - **.src** folder contains the .NET 8 website used on the lab scenarios.

#### Task 1: Create a public repository in GitHub and import MyRetailBizzNizz

In this task, you will create an empty public GitHub repository and import the existing [MyRetailBizzNizz](https://github.com/petender/MyRetailBizzNizz) repository.

1. From the lab computer, start a web browser, navigate to the [GitHub website](https://github.com/), sign in using your account and click on **New** to create new repository.

    ![Create Repository](images/github-new.png)

1. On the **Create a new repository** page, click on **Import a repository** link (below the page title).

    > NOTE: you can also open the import website directly at <https://github.com/new/import>

1. On the **Import your project to GitHub** page:

    | Field | Value |
    | --- | --- |
    | Your old repository’s clone URL| <https://github.com/petender/MyRetailBizzNizz> |
    | Owner | Your account alias |
    | Repository Name | MyRetailBizzNizz |
    | Privacy | **Public** |

1. Click on **Begin Import** and wait for your repository to be ready.

1. On the repository page, go to **Settings**, click on **Actions > General** and choose the option **Allow all actions and reusable workflows**. Click on **Save**.

### Exercise 1: Setup your GitHub Repository and Azure access

In this exercise, you will create an Azure Service Principal to authorize GitHub accessing your Azure subscription from GitHub Actions. You will also setup the GitHub workflow that will build, test and deploy your website to Azure.

#### Task 1: Create an Azure Service Principal and save it as GitHub secret

In this task, you will create the Azure Service Principal used by GitHub to deploy the desired resources. As an alternative, you could also use [OpenID connect in Azure](https://docs.github.com/actions/deployment/security-hardening-your-deployments/configuring-openid-connect-in-azure), as a secretless authentication mechanism.

1. On your lab computer, in a browser window, open the Azure Portal (https://portal.azure.com/).
1. In the portal, look for **Resource Groups** and click on it.
1. Click on **+ Create** to create a new Resource Group for the exercise.
1. On the **Create a resource group** tab, give the following name to your Resource Group: **rg-MyRetailBizzNizz**. Click on **Review+Create > Create**.
1. In the Azure Portal, open the **Cloud Shell** (next to the search bar).

    > NOTE: if this is the first time you open the Cloud Shell, you need to configure the [persistent storage](https://learn.microsoft.com/azure/cloud-shell/persisting-shell-storage)

1. Make sure the terminal is running in **Bash** mode and execute the following command, replacing **SUBSCRIPTION-ID** and **RESOURCE-GROUP** with your own identifiers (both can be found on the **Overview** page of the Resource Group):

    `az ad sp create-for-rbac --name GH-Action-MyRetailBizzNizz --role contributor --scopes /subscriptions/SUBSCRIPTION-ID/resourceGroups/RESOURCE-GROUP --sdk-auth`

    > NOTE: Make sure this is typed or pasted as a single line!
    > NOTE: this command will create a Service Principal with Contributor access to the Resource Group created before. This way we make sure GitHub Actions will only have the permissions needed to interact only with this Resource Group (not the rest of the subscription)

1. The command will output a JSON object, you will later use it as a GitHub secret for the workflow. Copy the JSON. The JSON contains the identifiers used to authenticate against Azure in the name of a Microsoft Entra identity (service principal).

    ```JSON
        {
            "clientId": "<GUID>",
            "clientSecret": "<GUID>",
            "subscriptionId": "<GUID>",
            "tenantId": "<GUID>",
            (...)
        }
    ```

1. You also need to run the following command to register the resource provider for the **Azure App Service** you will deploy later:

   ```bash
   az provider register --namespace Microsoft.Web
   ```

1. In a browser window, go back to your **MyRetailBizzNizz** GitHub repository.
1. On the repository page, go to **Settings**, click on **Secrets and variables > Actions**. Click on **New repository secret**
    - Name : **AZURE_CREDENTIALS**
    - Secret: **paste the previously copied  JSON object** (GitHub is able to keep multiple secrets under same name, used by  [azure/login](https://github.com/Azure/login) action )

1. Click on **Add secret**. Now GitHub Actions will be able to reference the service principal, using the repository secret.

#### Task 2: Modify and execute the GitHub workflow

In this task, you will modify the given GitHub workflow and execute it to deploy the solution in your own subscription.

1. In a browser window, go back to your **MyRetailBizzNizz** GitHub repository.
1. On the repository page, go to **Code** and open the following file: **MyRetailBizzNizz/.github/workflows/MyRetailBizzNizz-cicd.yml**. This workflow defines the CI/CD process for the given .NET 8 website code.
1. Uncomment the **on** section (delete "#"). The workflow triggers with every push to the main branch and also offers manual triggering ("workflow_dispatch").
1. In the **env** section, make the following changes:
    - Replace **NAME** in **RESOURCE-GROUP** variable. It should be the same resource group created in previous steps.
    - (Optional) You can choose your closest [azure region](https://azure.microsoft.com/explore/global-infrastructure/geographies) for **LOCATION**. For example, "eastus", "eastasia", "westus", etc.
    - Replace **YOUR-SUBS-ID** in **SUBSCRIPTION-ID**.
    - Replace **NAME** in **WEBAPP-NAME** with some unique alias. It will be used to create a globally unique website using Azure App Service.
1. Read the workflow carefully, comments are provided to help understand.

1. Click on **Start Commit** and **Commit Changes** leaving defaults (changing the main branch). The workflow will get automatically executed.

#### Task 3: Review GitHub Workflow execution

In this task, you will review the GitHub workflow execution:

1. In a browser window, go back to your **MyRetailBizzNizz** GitHub repository.
1. On the repository page, go to **Actions**, you will see the workflow setup before executing. Click on it.
1. Wait for the workflow to finish. From the **Summary** you can see the two workflow jobs, the status and Artifacts retained from the execution. You can click in each job to review logs.
1. In a browser window, go back to the Azure Portal (<https://portal.azure.com/>). Open the resource group created before. You will see that the GitHub Action, using a bicep template, has created an Azure App Service Plan + App Service. You can see the published website opening the App Service and clicking **Browse**.

### Exercise 2: Remove the Azure lab resources

In this exercise, you will use Azure Cloud Shell to remove the Azure resources provisioned in this lab to eliminate unnecessary charges.

1. In the Azure portal, open the **Bash** shell session within the **Cloud Shell** pane.
1. List all resource groups created throughout the labs of this module by running the following command:

    ```sh
    az group list --query "[?starts_with(name,'rg-MyRetailBizzNizz')].name" --output tsv
    ```

1. Delete all resource groups you created throughout the labs of this module by running the following command:

    ```sh
    az group list --query "[?starts_with(name,'rg-MyRetailBizzNizz')].[name]" --output tsv | xargs -L1 bash -c 'az group delete --name $0 --no-wait --yes'
    ```

    >**Note**: The command executes asynchronously (as determined by the --nowait parameter), so while you will be able to run another Azure CLI command immediately afterwards within the same Bash session, it will take a few minutes before the resource groups are actually removed.

## Review

In this lab, you implemented a GitHub Action workflow that deploys an Azure Web App.
