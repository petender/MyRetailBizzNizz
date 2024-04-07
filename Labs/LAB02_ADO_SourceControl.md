---
lab:
    title: 'Version Controlling with Git in Azure Repos'
    
---

# Version Controlling with Git in Azure Repos

## Student lab manual

## Lab requirements

- This lab requires **Microsoft Edge** or an [Azure DevOps supported browser.](https://docs.microsoft.com/azure/devops/server/compatibility)
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

#### Task 1: Clone an existing repository

In this task, you will step through the process of cloning a Git repository by using Visual Studio Code.

1. Switch to the the web browser displaying your Azure DevOps organization with the **MyRetailBizzNizz** project you generated in the previous exercise.
1. In the vertical navigational pane of the Azure DevOps portal, select the **Repos** icon.

1. In the upper right corner of the **MyRetailBizzNizz** repository pane, click **Clone**.

    > **Note**: Getting a local copy of a Git repo is called *cloning*. Every mainstream development tool supports this and will be able to connect to Azure Repos to pull down the latest source to work with.

1. On the **Clone Repository** panel, with the **HTTPS** Command line option selected, click the **Copy to clipboard** button next to the repo clone URL.

    > **Note**: You can use this URL with any Git-compatible tool to get a copy of the codebase.

1. Close the **Clone Repository** panel.
1. Switch to **Visual Studio Code** running on your lab computer.
1. Click the **View** menu header and, in the drop-down menu, click **Command Palette**.
1. At the Command Palette prompt, run the **Git: Clone** command.
1. In the **Provide repository URL or pick a repository source** text box, paste the repo clone URL you copied earlier in this task and press the **Enter** key.
1. Within the **Select Folder** dialog box, navigate to the C: drive, create a new folder named **Git**, select it, and then click **Select Repository Location**.
1. When prompted, log in to your Azure DevOps account.
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

In this task, you will use the Azure DevOps portal to review commits.

1. Switch to the web browser window displaying the Azure DevOps interface.
1. In the vertical navigational pane of the Azure DevOps portal, in the **Repos** section, select **Commits**.
1. Verify that your commit appears at the top of list.

### Exercise 3: Review history

In this exercise, you will use the Azure DevOps portal to review history of commits.

#### Task 1: Compare files

In this task, you will step through commit history by using the Azure DevOps portal.

1. With the **SOURCE CONTROL** tab of the Visual Studio Code window open, select **Constants.cs** representing the non-staged version of the file.
    > **Note**: A comparison view is opened to enable you to easily locate the changes you've made. In this case, it's just the one comment.
1. Switch to the web browser window displaying the **Commits** pane of the **Azure DevOps** portal to review the source branches and merges. These provide a convenient way to visualize when and how changes were made to the source.
1. Scroll down to the **My commit** entry (pushed before) and hover the mouse pointer over it to reveal the ellipsis symbol on the right side.
1. Click the ellipsis, in the dropdown menu, select **Browse Files**, and review the results.

### Exercise 4: Work with branches

In this exercise, you will step through scenarios that involve branch management by using Visual Studio Code and the Azure DevOps portal.

#### Task 1: Create a new branch in your local repository

In this task, you will create a branch by using Visual Studio Code.

1. Switch to **Visual Studio Code** running on your lab computer.
1. With the **SOURCE CONTROL** tab selected, in the lower left corner of the Visual Studio Code window, click **main**.
1. In the pop-up window, select **+ Create new branch from...**.
1. In the **Branch name** textbox, type **dev** to specify the new branch and press **Enter**.
1. In the **Select a ref to create the 'dev' branch from** textbox, select **main** as the reference branch.

#### Task 2: Delete a branch

In this task, you will use the Visual Studio Code to work with a branch created in the previous task.

1. In the **Visual Studio Code** window, with the **SOURCE CONTROL** tab selected, in the lower left corner of the Visual Studio Code window, click the **Publish changes** icon (directly to the right of the **dev** label representing your newly created branch).
1. Switch to the web browser window displaying the **Commits** pane of the **Azure DevOps** portal and select **Branches**.
1. On the **Mine** tab of the **Branches** pane, verify that the list of branches includes **dev**.
1. Hover the mouse pointer over the **dev** branch entry to reveal the ellipsis symbol on the right side.
1. Click the ellipsis, in the pop-up menu, select **Delete branch**, and, when prompted for confirmation, click **Delete**.
1. Switch back to the **Visual Studio Code** window and, with the **SOURCE CONTROL** tab selected, in the lower left corner of the Visual Studio Code window, click the **dev** entry. This will display the existing branches in the upper portion of the Visual Studio Code window.
1. Verify that now there are two **dev** branches listed.
1. Go to the web browser displaying the **Mine** tab of the **Branches**
1. On the **Mine** tab of the **Branches** pane, select the **All** tab.
1. On the **All** tab of the **Branches** pane, in the **Search branch name** text box, type **dev**.
1. Review the **Deleted branches** section containing the entry representing the newly deleted branch.
1. In the **Deleted branches** section, hover the mouse pointer over the **dev** branch entry to reveal the ellipsis symbol on the right side.
1. Click the ellipsis, in the pop-up menu and select **Restore branch**.
    > **Note**: You can use this functionality to restore a deleted branch as long as you know its exact name.

#### Task 4: Branch Policies

In this task, you will use the Azure DevOps portal to add policies to the main branch and only allow changes using Pull Requests that comply with the defined policies. You want to ensure that changes in a branch are reviewed before they are merged.

For simplicity we will work directly on the web browser repo editor (working directly in origin), instead of using the local clone in VS code (recommended for real scenarios).

1. Switch to the web browser displaying the **Mine** tab of the **Branches** pane in the Azure DevOps portal.
1. On the **Mine** tab of the **Branches** pane, hover the mouse pointer over the **main** branch entry to reveal the ellipsis symbol on the right side.
1. Click the ellipsis and, in the pop-up menu, select **Branch Policies**.
1. On the **main** tab of the repository settings, enable the option for **Require minimum number of reviewers**. Add **1** reviewer and check the box **Allow requestors to approve their own changes**(as you are the only user in your project for the lab)
1. On the **main** tab of the repository settings, enable the option for **Check for linked work items** and leave it with **Required** option.

#### Task 5: Testing branch policy

In this task, you will use the Azure DevOps portal to test the policy and create your first Pull Request.

1. In the vertical navigational pane of the of the Azure DevOps portal, in the **Repos>Files**, make sure the **main** branch is selected (dropdown above shown content).
1. To make sure policies are working, try making a change and committing it on the **main** branch, navigate to the **/MyRetailBizzNizz/src/MyRetailBizzNizz-Web/Program.cs** file and select it. This will automatically display its content in the details pane.
1. On the first line add the following comment:

    ```csharp
    // Testing main branch policy
    ```

1. Click on **Commit > Commit**. You will see a warning: changes to the main branch can only be done using a Pull Request.

1. Click on **Cancel** to skip the commit.

#### Task 6: Working with Pull Requests

In this task, you will use the Azure DevOps portal to create a Pull Request, using the **dev** branch to merge a change into the protected **main** branch. An Azure DevOps work item with be linked to the changes to be able to trace pending work with code activity.

1. In the vertical navigational pane of the of the Azure DevOps portal, in the **Boards** section, select **Work Items**.
1. Click on **+ New Work Item > Product Backlog Item**. In title field, write **Testing my first PR** and click on **Save**.
1. Now go back to the vertical navigational pane of the of the Azure DevOps portal, in the **Repos>Files**, make sure the **dev** branch is selected.
1. Navigate to the **/MyRetailBizzNizz/src/Web/Program.cs** file and make the following change on the first line:

    ```csharp
    // Testing my first PR
    ```

1. Click on **Commit > Commit** (leave default commit message). This time the commit works, **dev** branch has no policies.
1. A message will pop-up, proposing to create a Pull Request (as you **dev** branch is now ahead in changes, compared to **main**). Click on **Create a Pull Request**.
1. In the **New pull request** tab, leave defaults and click on **Create**.
1. The Pull Request will show some failed/pending requirements, based on the policies applied to our target **main** branch.
    - Proposed changes should have a work item linked
    - At least 1 user should review and approve the changes.

1. On the right side options, click on the **+** button next to **Work Items**. Link the previously created work item to the Pull Request by clicking on it. You will see one of the requirements changes  status.
1. Next,  open the **Files** tab to review the proposed changes. In a more complete Pull Request,  you would be able to review files one by one (marked as reviewed) and open comments for lines that may not be clear (hovering the mouse over the line number gives you an option to post a comment).
1. Go back to the **Overview** tab, and on the top-right click on **Approve**. All the requirements will change to green. Now you can click on **Complete**.
1. On the **Complete Pull Request** tab, multiple options will be given before completing the merge:
    - **Merge Type**: 4 merge types are offered, you can review them [here](https://learn.microsoft.com/azure/devops/repos/git/complete-pull-requests?view=azure-devops&tabs=browser#complete-a-pull-request) or observing the given animations. Choose **Merge (no fast forward)**.
    - **Post-complete options**:
        - Check **Complete associated work item...**. It will move associated PBI to **Done** state.

1. Click on **Complete Merge**

### Exercise 5: Remove Branch Policies

When going through the different course labs in the order they are presented, the branch policy configured during this lab will block exercises in future labs. Therefore, we want you to remove the configured branch policies.

1. From the Azure DevOps **MyRetailBizzNizz** Project view, navigate to **Repos** and select **Branches**. Select the **Mine** tab of the **Branches** pane.
1. On the **Mine** tab of the **Branches** pane, hover the mouse pointer over the **main** branch entry to reveal the ellipsis symbol (the ...) on the right side.
1. Click the ellipsis and, in the pop-up menu, select **Branch Policies**.

    ![Policy Settings](images/policy-settings.png)

1. On the **main** tab of the repository settings, disable the option for **Require minimum number of reviewers**.
1. On the **main** tab of the repository settings, disable the option for **Check for linked work items**.

    ![Branch Policies](images/branch-policies.png)

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

In this lab, you used the Azure DevOps portal to manage branches and repositories.
