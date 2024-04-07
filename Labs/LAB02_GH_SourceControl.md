---
lab:
    title: 'Version Controlling with GitHub Repositories'
    
---
## Student lab manual

## Lab requirements

- This lab requires **Microsoft Edge** or an [GitHub supported browser.](https://docs.github.com/en/get-started/using-github/supported-browsers)
- If you don't have Git 2.29.2 or later installed yet, start a web browser, navigate to the [Git for Windows download page](https://gitforwindows.org/) download it, and install it.
- If you don't have Visual Studio Code installed yet, from the web browser window, navigate to the [Visual Studio Code download page](https://code.visualstudio.com/), download it, and install it.
- If you don't have Visual Studio C# extension installed yet, in the web browser window, navigate to the [C# extension installation page](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) and install it.

## Lab overview

## Objectives

After you complete this lab, you will be able to:

- Clone an existing repository.
- Save work with commits.
- Review history of changes.
- Work with branches by using Visual Studio Code.

## Instructions

### Exercise 0: Configure the lab prerequisites

#### Task 1: Configure Git and Visual Studio Code

In this task, you will install and configure Git and Visual Studio Code, including configuring the Git credential helper to securely store the Git credentials used to communicate with Azure DevOps. If you have already implemented these prerequisites, you can proceed directly to the next task.

1. On the lab computer, open **Visual Studio Code**.
1. In the Visual Studio Code interface, from the main menu, select **Terminal \| New Terminal** to open the **TERMINAL** pane.
1. Make sure that the current Terminal is running **PowerShell** by checking if the drop-down list at the top right corner of the **TERMINAL** pane shows **1: powershell**

    > **Note**: To change the current Terminal shell to **PowerShell** click the drop-down list at the top right corner of the **TERMINAL** pane and click **Select Default Shell**. At the top of the Visual Studio Code window select your preferred terminal shell **Windows PowerShell** and click the plus sign on the right-hand side of the drop-down list to open a new terminal with the selected default shell.

1. In the **TERMINAL** pane, run the following command below to configure the credential helper.

    ```git
    git config --global credential.helper wincred
    ```

1. In the **TERMINAL** pane, run the following commands to configure a user name and email for Git commits (replace the placeholders in braces with your preferred user name and email eliminating the < and > symbols):

    ```git
    git config --global user.name "<John Doe>"
    git config --global user.email <johndoe@example.com>
    ```

### Exercise 1: Clone an existing repository

In this exercise, you use Visual Studio Code to clone the Git repository you provisioned as part of the previous exercise.

#### Task 1: Clone an existing repository from ADO to GitHub

In this task, you will step through the process of cloning a Git repository by using Visual Studio Code.

1. Switch to the the web browser displaying your Azure DevOps organization with the **MyRetailBizzNizz** project you generated in the previous exercise.
1. In the vertical navigational pane of the Azure DevOps portal, select the **Repos** icon.
1. In the upper right corner of the **MyRetailBizzNizz** repository pane, click **Clone**.
1. On the **Clone Repository** panel, with the **HTTPS** Command line option selected, click the **Copy to clipboard** button next to the repo clone URL.
1. Close the **Clone Repository** panel.

1. Switch to GitHub, and **create a new Repository** called **MyNewGHRetailBizzNizz**
1. From the Main page of the new Repository, choose **...or import code from another repository**
    > Note: if the **import** option is not visible, browse to the import URL of the project: https://github.com/<yourgithubaccount>/<nameofnewrepoyoucreated>/import
1. In the **Your old repository's clone URL**, paste in the Azure DevOps Repo URL you copied earlier
1. Click **Begin Import**
1. Wait for the prompt for a **login** and **Private Access Token**
1. Switch back to Azure DevOps repos, and reopen the **clone** blade from before. Select **Generate Git Credentials**.
1. Copy the Username to the Login field in GitHub, and copy the Password to the Private Access Token field. Click **Import**.
1. Wait for the Import task in GitHub to complete.
    > Note: Notice how the full Commit history is moved over to GitHub (in a full project, it would also have any Branch from the initial project). This is also a possible way to **migrate** your Source Control process from Azure DevOps repos to GitHub Repositories.

#### Task 2: Clone a GitHub Repository to Visual Studio Code

1. Switch to **Visual Studio Code** running on your lab computer.
1. Click the **View** menu header and, in the drop-down menu, click **Command Palette**.
1. At the Command Palette prompt, run the **Git: Clone** command.
1. You need to provide a repository URL here; Find this from GitHub, by navigating to your Repository, Click **Code** , and copying the URL shown.
1. In the **Provide repository URL or pick a repository source** text box, paste the repo clone URL you copied earlier in this task and press the **Enter** key.
1. Within the **Select Folder** dialog box, navigate to the C: drive, create a new folder named **MyNewRetailBizzNizz**, select it, and then click **Select Repository Location**.
1. When prompted, log in to your GitHub account.
1. After the cloning process completes, once prompted, in the Visual Studio Code, click **Open** to open the cloned repository.

### Exercise 2: Save work with commits

In this exercise, you will step through several scenarios that involve the use of Visual Studio Code to stage and commit changes.

#### Task 1: Commit changes

In this task, you will use Visual Studio Code to commit changes.

1. In the Visual Studio Code window, at the top of the vertical toolbar, select the **EXPLORER** tab, navigate to the **/MyRetailBizzNizz/.src/MyRetailBizzNizz-Web/Program.cs** file and select it. This will automatically display its content in the details pane.
1. On the first line add the following comment:

    ```csharp
    // My first change
    ```

    > **Note**: It doesn't really matter what the comment is since the goal is just to make a change.

1. Press **Ctrl+S** to save the change.
1. In the Visual Studio Code window, select the **SOURCE CONTROL** tab to verify that Git recognized the latest change to the file residing in the local clone of the Git repository.
1. With the **SOURCE CONTROL** tab selected, at the top of the pane, in the textbox, type **My commit** as the commit message and press **Ctrl+Enter** to commit it locally.

1. If prompted whether you would like to automatically stage your changes and commit them directly, click **Always**.

    > **Note**: We will discuss **staging** later in the lab.

1. In the lower left corner of the Visual Studio Code window, to the right of the **main** label, note the **Synchronize Changes** icon of a circle with two vertical arrows pointing in the opposite directions and the number **1** next to the arrow pointing up. Click the icon and, if prompted, whether to proceed, click **OK** to push and pull commits to and from **origin/main**.

#### Task 2: Review commits

In this task, you will use the GitHub portal to review commits.

1. Switch to the web browser window displaying the GitHub interface.
1. In the details of the repository view, next to your account name,  **see the commit message** of your most recent commit. More to the right, see the **# of commits** for this repo.
1. Click the # of commits, to see the history of all commits for this repo. Verify that your commit appears at the top of list.

### Exercise 3: Review history

In this exercise, you will use the GitHub portal to review history of commits.

#### Task 1: Compare files

1. From within the **Commits** view within your Repo, select the **most recent commit** message.
1. Notice how it shows the changes made to the source file, highlighting additions in green (and any removals in red, but we don't have these...)
1. Acting as a supervisor for the change made, you can **add a comment** from the comment section below the commit change details view. 
1. Add a comment "all looks good". and Save the comment.

### Exercise 4: Work with branches

In this exercise, you will step through scenarios that involve branch management by using Visual Studio Code and the Azure DevOps portal.

#### Task 1: Create a new branch in your local repository

In this task, you will create a branch by using Visual Studio Code.

1. Switch to **Visual Studio Code** running on your lab computer.
1. With the **SOURCE CONTROL** tab selected, in the lower left corner of the Visual Studio Code window, click **main**.
1. In the pop-up window, select **+ Create new branch from...**.
1. In the **Branch name** textbox, type **dev** to specify the new branch and press **Enter**.
1. In the **Select a ref to create the 'dev' branch from** textbox, select **main** as the reference branch.
1. Provide a **commit message** and select **Publish Branch**.
1. Switch back to the GitHub Repository for this project, and refresh the page. Notice **2 branches** are visible now. Click the **2 branches** link.
1. Notice the Default Branch as **main**, as well as the **dev** Branch identified as Your Branches and Active Branches.
1. Switch back to VSCode, and validate in the **Source Control** view, or from the left down corner, you are in the **dev** branch. 
1. Make a new edit to the same file as earlier, first **remove the previous task's comment** you added, as well as now **adding a new comment** in the file.
1. **Save** the changes to the file; from the Source COntrol view, provide a commit message, and sync your changes to the Dev branch.
1. Switch back to GitHub, and **notice** a remark in a yellow bar, identifying the recent changes made **to the dev branch**. Click **Compare & Pull Request**. 
1. From the **Open a pull request** view, see how the commit message got copied to the **PR title**, and provide a description in the corresponding field.
1. Confirm by clicking **Create Pull Request**.
1. Notice how the Pull Request gets validated + how GitHub **offers to create a rule for requiring approval**. Skip this step for now (will be defined in the next task)
1. Confirm the pull request by clicking **Merge Pull Request**, followed by **Confirm Merge**.
1. Wait for the message **Pull request successfully merged and closed**. 
1. From this message, click **Delete branch**.
1. Switch back to VSCode, and notice the **dev** branch is still visible here. Try to sync, which will result in an error message **Git fatal: couldn't find remote ref dev**
1. Select **Open Git Log**; while we won't use it from here, know this could be useful for further troubleshooting source control. 

#### Task 4: Branch Policies

In this task, you will use the GitHub portal to add policies to the main branch and only allow changes using Pull Requests that comply with the defined policies. You want to ensure that changes in a branch are reviewed before they are merged.

For simplicity we will work directly on the web browser repo editor (working directly in origin), instead of using the local clone in VS code (recommended for real scenarios).

1. Switch to the web browser displaying the **Main** tab of the **Branches** pane.
1. CLick **Settings** from the GitHub Repository view. Select **Branches**.
1. CLick **Add branch protection rule**.
1. Provide a name for the **branch pattern**.
1. From the **Protect matching branches**, select **Require a pull request before merging**, as well as **Require approvals**.
    > Note: there are way more rules you can specify, but we won't go through those details in this exercise.
1. Click **Create** to confirm the branch policy.
1. If time allows, feel free to initiate a new repository change, and initate a commit. This should now be blocked, and asking for an approver.

### Exercise 5: Remove Branch Policies

When going through the different course labs in the order they are presented, the branch policy configured during this lab will block exercises in future labs. Therefore, we want you to remove the configured branch policies.

1. From the GitHub **MyNewRetailBizzNizz** Project view, navigate to **Settings** and select **Branches**. Select the **branch protection rule** you created earlier.
1. Click the **delete** button, followed by **confirming** the delete operation once more.
1. You have now disabled/removed the branch policies for the main branch.

### Exercise 6: Integrate Source Control with Azure DevOps Boards

#### Set Up GitHub Connections in Azure DevOps

1. From Azure DevOps, open your **Project Settings** and select **Github Connections**.
1. Click **Connect your GitHub account**. 
1. Click **Authenticate**, and select your **GitHub Account**. 
1. From the list of **Add GitHub repositories**, select the GitHub repository created for this exercise (MyNewRetailBizzNizz), or create a new one first if you don't have this repository.
1. Confirm the integration by clicking **Save**.

#### Set Up Azure Boards integration in GitHub

1. From GitHub, select your **user account** in the upper right corner. Click **Settings**.
1. From **Settings**, scroll down to **Integrations**; select **Applications**. 
1. Select the **Authorized OAuth Apps** tab. 
1. Select **Azure Boards**. Select **Grant** to grant access to any organizations that show as having **Access Request pending**.
1. Navigate to the **GitHub Marketplace** link for Azure Boards - https://github.com/marketplace/azure-boards
1. Select **Set Up a Plan**; choose the **GitHub organization** you want to connect to Azure Boards.
1. Next, choose the **repository/ies** you want to connect to Azure Boards. Make sure the **MyNewRetailBizzNizz** repo is selected.
1. In the next step, select the **Azure DevOps Organization** and **Azure Boards Project** (MyRetailBizzNizz) you want to connect to GitHub.com
1. Confirm the access by clicking the **Authorize Azure Boards by Azure Boards**.
1. On your board, select **New item** to enter a new work item named "Add badge to README"—Issue (Basic), User Story (Agile), or Product Backlog Item (Scrum)—depending on the process model used by your Azure Boards project. This should be Scrum if you set up the project according the exercise guidelines.
1. A work item titled Add badge to README appears on your board. Open your work item and select pull request under Add link. Select the repository and enter the pull request ID and an optional comment.
1. This linking could also be done automatically, using the **keyword** **AB#{workitemID}** in your commit messages. (Note, you can also interact with the status of the work items by using fixed, fixes or fix)
1. Switch back to the GitHub Repository, and edit the README file with some changes. 
1. When saving the changes, update the commit message to **start with AB#{ID of an existing work item}** and commit the change.
1. Open the corresponding Work Item in Azure Boards; notice how the **GitHub deeplink** to the commit change in GitHub is available.

## Review

In this lab, you used the GitHub portal to manage branches and repositories.
