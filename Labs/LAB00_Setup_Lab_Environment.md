---
lab:
    title: 'Setup Microsoft DevOps Solutions - lab environment'
    
---

# Validate lab environment

## Student lab manual

## Instructions to create an Azure DevOps Organization (you only have to do this once)

1. You need an **Azure Pass promocode** from the presenter before continuiing with this exercise.
1. Use a private browser session to get a new **personal Microsoft Account (MSA)** at [https://account.microsoft.com](https://account.microsoft.com).
1. Using the same browser session, go to [https://www.microsoftazurepass.com](https://www.microsoftazurepass.com) to redeem your Azure Pass using your Microsoft Account (MSA). For details, see [Redeem a Microsoft Azure Pass](https://www.microsoftazurepass.com/Home/HowTo?Length=5). Follow the instructions for redemption.
1. When you are asked for personal account details, make sure the address and phone nr do exist, as it is getting validated. The phone nr is preferably your mobile phone, as it could be used for additional account verification during the lifespan of the Microsoft Account.

> Note: The Azure Pass times out after 30 days from activation, or when you consumed the $100 value linked to it on Azure Resources.

### Once you have your Azure Subscription ready

1. Open a browser and navigate to [https://portal.azure.com](https://portal.azure.com), then search at the top of the Azure portal screen for **Azure DevOps**. In the resulting page, click **Azure DevOps organizations**.
1. Next, click on the link labelled **My Azure DevOps Organizations** or navigate directly to [https://aex.dev.azure.com](https://aex.dev.azure.com).
1. On the **We need a few more details** page, select **Continue**.
1. In the drop-down box on the left, choose **Default Directory**, instead of “Microsoft Account”.
1. If prompted (*"We need a few more details"*), provide your name, e-mail address, and location and click **Continue**.
1. Back at [https://aex.dev.azure.com](https://aex.dev.azure.com) with **Default Directory** selected click the blue button **Create new organization**.
1. Accept the *Terms of Service* by clicking **Continue**.
1. If prompted (*"Almost done"*), leave the name for the Azure DevOps organization at default (it needs to be a globally unique name) and pick a hosting location close to you from the list.
1. Once the newly created organization opens in **Azure DevOps**, click **Organization settings** in the bottom left corner.
1. At the **Organization settings** screen click **Billing** (opening this screen takes a few seconds).
1. Click **Setup billing** and on the right-hand side of the screen select the **Azure Pass - Sponsorship** subscription and click **Save** to link the subscription with the organization.
1. Once the screen shows the linked Azure Subscription ID at the top, change the number of **Paid parallel jobs** for **MS Hosted CI/CD** from 0 to **1**. Then click the **SAVE** button at the bottom.
1. In **Organization Settings**, go to section **Pipelines** and click **Settings**.
1. Toggle the switch to **Off** for **Disable creation of classic build pipelines** and **Disable creation of classic release pipelines**
    > Note: The **Disable creation of classic release pipelines** switch sets to **On** hides classic release pipeline creation options such as the **Release** menu in the **Pipeline** section of DevOps projects. While we won't immediately use classic release pipelines in an exercise anymore, the presenter might use this for some demos on Release Quality Gates. 


## Instructions to create the sample Azure DevOps Project (you only have to do this once)

### Exercise 0: Configure the lab prerequisites

> **Note**: make sure you completed the steps to create your Azure DevOps Organization before continuing with these steps.

In this exercise, you will set up the prerequisites for the lab, which consist of a new Azure DevOps project with a repository based on the [PDT-MyRetailBizznizz](https://github.com/petender/MyRetailBizzNizz).

#### Task 1:  Create and configure the team project

In this task, you will create an **MyRetailBizzNizz** Azure DevOps project to be used by several labs.

1. On your lab computer, in a browser window open your Azure DevOps organization. Click on **New Project**. Give your project the following settings:
    - name: **MyRetailBizzNizz**
    - visibility: **Private**
    - Advanced: Version Control: **Git**
    - Advanced: Work Item Process: **Scrum**

1. Click **Create**.

#### Task 2:  Import MyRetailBizzNizz Git Repository

In this task you will import the MyRetailBizzNizz Git repository that will be used by several labs.

1. On your lab computer, in a browser window open your Azure DevOps organization and the previously created **MyRetailBizzNizz** project. Click on **Repos>Files** , **Import a Repository**. Select **Import**. On the **Import a Git Repository** window, paste the following URL https://github.com/petender/MyRetailBizzNizz.git and click **Import**:

1. The repository is organized the following way:
    - **.ado** folder contains Azure DevOps YAML pipelines.
    - **.azure** folder contains Azure Deployment Templates using ARM or Bicep.
    - **.github** folder container YAML GitHub workflow definitions.
    - **.src** folder contains the .NET 8 Retail website (.NET Team EShopOnWeb) and Tollbooth (Azure Serverless Architecture) used as sample applications for scenarios.

You have now completed the necessary prerequisite steps to continue with the actual workshop exercises.
