---
lab:
    title: 'DevOps Project Management with Azure Boards'
    
---

## Exercise overview

In this lab, you'll learn about DevOps Project Management Azure Boards and how it can help you quickly plan, manage, and track work across your entire team. You'll explore the product backlog, sprint backlog, and task boards that can track the flow of work during an iteration. We'll also look at the enhanced tools in this release to scale for larger teams and organizations.

## Objectives

After you complete this exercise, you will be able to:

- Manage teams, areas, and iterations.
- Manage work items.
- Manage sprints and capacity.
- Customize Kanban boards.
- Define dashboards.
- Customize team process.

## Instructions

### Exercise 1: Manage Agile project

In this exercise, you will use Azure Boards to perform a number of common agile planning and portfolio management tasks, including management of teams, areas, iterations, work items, sprints and capacity, customizing Kanban boards, defining dashboards, and customizing team processes.

#### Task 1: Manage teams, areas, and iterations

In this task, you will create a new team and configure its area and iterations.

1. Verify that the web browser displays your Azure DevOps organization with the **MyRetailBizzNizz** project you generated in the previous exercise.
1. Click the cogwheel icon labeled **Project settings** located in the lower left corner of the page to open the **Project settings** page.
1. In the **General** section, select the **Teams** tab. There is already a default team in this project, **MyRetailBizzNizz Team**. Open the Team's details.
1. Click **Iterations and Area Paths** link at the top of the **MyRetailBizzNizz** page to start defining the schedule and scope of the team.
1. At the top of the **Boards** pane, select the **Iterations** tab and then click **+ Select iteration(s)**.
1. Select **MyRetailBizzNizz\Sprint 1** and click **Save and close**. Note that this first sprint will show up in the list of iterations, but the Dates are not set yet.
1. Select **Sprint 1** and click the **ellipsis (...)**. From the context menu, select **Edit**.
    > **Note**: Specify the Start Date as the first work day of last week, and count 3 full work weeks for each sprint. For example, April 8th will be the 2nd week of the sprint, making the sprint start on April 1st, and going until April 21st.
1. Repeat the previous step to add **Sprint 2** and **Sprint 3**. You could say that we are currently in the 2nd week of the first sprint.
1. Still in the **Project Settings / Boards / Team Configuration** pane, at the top of the pane, select the **Areas** tab. You will find there an automatically generated area with the name matching the name of the team.
1. Click the ellipsis symbol (...) next to the **default area** entry and, in the dropdown list, select **Include sub areas**.

    > **Note**: The default setting for all teams is to exclude sub-area paths. We will change it to include sub-areas so that the team gets visibility into all of the work items from all teams. Optionally, the management team could also choose to not include sub-areas, which automatically removes work items from their view as soon as they are assigned to one of the teams.

#### Task 2: Manage work items

In this task, you will step through common work item management tasks.

Work items play a prominent role in Azure DevOps. In this task you'll focus on using various work items to set up the plan to extend the MyRetailBizzNizz site with a product training section. 

1. In the vertical navigational pane of the Azure DevOps portal, select the **Boards** icon and, select **Work Items**.
1. On the **Work Items** window, click on **+ New Work Item > Epic**.
1. In the **Enter title** textbox, type **Product training**.
1. In the upper left corner, select the **Unassigned** entry and, start typing your name in order to assign the new work item to yourself. Use the **Search** to browse your directory of users. 
1. Next to the **Area** entry, select the **MyRetailBizzNizz** entry and, in the dropdown list, select **MyRetailBizzNizz**. This will set the **Area** to **MyRetailBizzNizz\MyRetailBizzNizz**.
1. Next to the **Iteration** entry, select the **MyRetailBizzNizz** entry and, in the dropdown list, select **Sprint 2**. This will set the **Iteration** to **MyRetailBizzNizz\Sprint 2**.
1. Click **Save** to finalize your changes. **Do not close it**.
1. In the **Related work** section on the lower right-side, select the **Add link** entry and, in the dropdown list, select **New item**.
1. On the **Add link** panel, in the **Link Type** dropdown list, select **Child**. Next, in the **Work item type** dropdown list, select **Feature**, in the **Title** textbox, type **Training dashboard** and click **OK**.
    > **Note**: On the **Training dashboard** panel, note that the assignment, **Area**, and **Iteration** are already set to the same values as the epic that the feature is based on. In addition, the feature is automatically linked to the parent item it was created from.
1. Click **Add link** to save the Child item. On the (New Feature) **Training dashboard** panel, click **Save & Close**.
1. In the vertical navigation pane of the Azure DevOps portal, in the list of the **Boards** items, select **Boards**.
1. On the **Boards** panel, select the **MyRetailBizzNizz boards** entry. This will open the board for that particular team.
1. On the **Boards** panel, in the upper right corner, select the **Backlog items** entry and, in the dropdown list, select **Features**.
1. Hover with the mouse pointer over the rectangle representing the **Training dashboard** feature. This will reveal the ellipsis symbol (...) in its upper right corner.
1. Click the ellipsis (...) icon and, in the dropdown list, select **Add Product Backlog Item**.
1. In the textbox of the new product backlog item, type **As a customer, I want to view new tutorials** and press the **Enter** key to save the entry.
1. Repeat the previous step to add two more PBIs designed to enable the customer to see their recently viewed tutorials and to request new tutorials named, respectively, **As a customer, I want to see tutorials I recently viewed** and **As a customer, I want to request new tutorials**.
1. On the **Boards** panel, in the upper right corner, select the **Features** entry and, in the dropdown list, select **Backlog items**.
    > **Note**: Backlog items have a state that defines where they are relative to being completed. While you could open and edit the work item using the form, it's easier to just drag cards on the board.
1. On the **Board** tab of the **MyRetailBizzNizz** panel, drag the first work item named **As a customer, I want to view new tutorials** from the **New** to **Approved** stage.
1. Hover with the mouse pointer over the rectangle representing the work item you moved to the **Approved** stage. This will reveal the down facing caret symbol.
1. Click the down facing caret symbol to expand the work item card, select the **Unassigned** entry, and in the list of user accounts, select your account to assign the moved PBI to yourself.
1. On the **Board** tab of the **MyRetailBizzNizz** panel, drag the second work item named **As a customer, I want to see tutorials I recently viewed** from the **New** to the **Committed** stage.
1. On the **Board** tab of the **MyRetailBizzNizz** panel, drag the third work item named **As a customer,  I want to request new tutorials** from the **New** to the **Done** stage.
1. On the **Board** tab of the **MyRetailBizzNizz** pane, at the top of the pane, click **View as Backlog** to display the tabular form.
1. On the **Backlog** tab of the **MyRetailBizzNizz** pane, in the upper left corner of the pane, click the second plus sign from the top, the one next to the first work item. This will display the **NEW TASK** panel.
1. At the top of the **NEW TASK** panel, in the **Enter title** textbox, type **Add page for most recent tutorials**.
1. On the **NEW TASK** panel, in the **Remaining Work** textbox, type **5**.
1. On the **NEW TASK** panel, in the **Activity** dropdown list, select **Development**.
1. On the **NEW TASK** panel, click **Save & Close**.

#### Task 3: Manage sprints and capacity

In this task, you will step through common sprint and capacity management tasks.

Teams build the sprint backlog during the sprint planning meeting, typically held on the first day of the sprint. The sprint backlog should contain all the information the team needs to successfully plan and complete work within the time allotted without having to rush at the end. 

1. In the vertical navigational pane of the Azure DevOps portal, select the **Boards** icon and, in the list of the **Boards** items, select **Sprints**.
1. On the **Taskboard** tab of the **Sprints** view, in the toolbar, on the right hand side, select the **View options** symbol (directly to the left of the funnel icon) and, in the **View options** dropdown list, select the **Work details** entry. Select **Sprint 2** as filter.
1. Within the **ToDo** Column, notice the Task Item **Add page for most recent tutorials**, click the **Unassigned** entry and, in the list of user accounts, select your account to assign the task to yourself.
1. Select the **Capacity** tab of the **Sprints** view.
1. On the **Capacity** tab of the **Sprints** view, click **+Add User** and select your user account. For this user, set the **Activity** field to **Development** and, in the **Capacity per day** textbox, type **1**. This represents 1 hour of work.
1. On the **Capacity** tab of the **Sprints** view, directly next to the entry representing your user account, in the **Days off** column, click the **0 days** entry. This will display a panel where you can set your days off.
1. In the displayed panel, use the calendar view to set your vacation to span five work days during the current sprint (within the next three weeks) and, once completed, click **OK**.
1. Back on the **Capacity** tab of the **Sprints** view, click **Save**.
1. Select the **Taskboard** tab of the **Sprints** view.
1. On the **Taskboard** tab of the **Sprints** view, in the square box representing the **Add page for most recent tutorials**, set the estimated number of hours to **14**, to match your total capacity for this sprint, which you identified in the previous step.
1. On the **Taskboard** tab of the **Sprints** view, in the toolbar, on the right hand side, select the **View options** symbol (directly to the left of the funnel icon) and, in the **View options** dropdown list, select the **Assigned To=** entry.
1. Click the **Configure team settings** cogwheel icon (directly to the right of the funnel icon).
1. On the **Settings** panel, select the **Styles** tab, click **+ Styling rule**, under the **Rule name** label, in the **Name** textbox, type **Development**, and, in the **Card color** dropdown list, select the green rectangle.
1. In the **Rule criteria** section, in the **Field** dropdown list, select **Activity**, in the **Operator** dropdown list, select **=**, and, in the **Value** dropdown list, select **Development**.
1. On the **Settings** panel, select the **Backlogs** tab.
1. On the **Settings** panel, select the **Working days** tab.
1. On the **Settings** panel, select the **Working with bugs** tab.
1. On the **Settings** panel, click **Save and close** to save the styling rule.

#### Task 4: Customize Kanban boards

In this task, you will step through the process of customizing Kanban boards.

1. In the vertical navigational pane of the Azure DevOps portal, in the list of the **Boards** items, select **Boards**.
1. On the **Boards** panel, click the **Configure board settings** cogwheel icon (directly to the right of the funnel icon).
1. On the **Settings** panel, select the **Tag colors** tab, click **+ Tag color**, in the **Tag** textbox, type **data** and leave the default color in place.
1. From the **Configure board settings** (cogwheel) On the **Settings** panel, select the **Annotations** tab.
1. On the **Settings** panel, select the **Tests** tab.
1. On the **Settings** panel, click **Save and close** to save the styling rule.
1. From the **Board** tab of the **MyRetailBizzNizz** panel, open the Work Item representing the **As a customer, I want to view new tutorials** backlog item.
1. From the detailed item view, at the top of the panel, to the right of the **0 comments** entry, click **Add tag**.
1. In the resulting textbox, type **data** and press the **Enter** key.
1. Repeat the previous step to add the **ux** tag.
1. Save these edits by clicking **Save & Close**.
1. On the **Boards** panel, click the **Configure board settings** cogwheel icon (directly to the right of the funnel icon).
1. On the **Settings** panel, select the **Columns** tab.
1. Click **+ Column**, under the **Column name** label, in the **Name** textbox, type **QA Approved** and, in the **WIP limit** textbox, type **1**
1. On the **Settings** panel, select the **Columns** tab again. Notice the ellipsis next to the **QA Approved** column you created. Select **Move right** twice, so that the QA Approved column gets positioned in-between **Committed** and **Done**.
1. On the **Settings** panel, click **Save**.
1. **Refresh** the **Boards portal**, so the **QA Approved** column is visible in the Kanban board view now.
1. Drag the **As a customer, I want to see tutorials I recently viewed** work item from the **Committed** stage into the **QA Approved** stage.
1. Drag the **As a customer, I want to view new tutorials** work item from the **Approved** stage into the **QA Approved** stage.

    > **Note**: The stage now exceeds its **WIP** limit and is colored red as a warning.

1. Move the **As a customer, I want to see tutorials I recently viewed** backlog item back to **Committed**.
1. On the **Boards** panel, click the **Configure board settings** cogwheel icon (directly to the right of the funnel icon).
1. On the **Settings** panel, return to the **Columns** tab and select the **QA Approved** tab.
1. On the **QA Approved** tab, enable the **Split column into doing and done** checkbox to create two separate columns.

    > **Note**: As your team updates the status of work as it progresses from one stage to the next, it helps that they agree on what **done** means. By specifying the **Definition of done** criteria for each Kanban column, you help share the essential tasks to complete before moving an item into a downstream stage.

1. On the **QA Approved** tab, at the bottom of the panel, in the **Definition of done** textbox, type **Passes \*\*all\*\* tests**.
1. On the **Settings** panel, click **Save and close**.
1. On the **Boards** panel, click the **Configure boards settings** cogwheel icon (directly to the right of the funnel icon).
1. On the **Settings** panel, select the **Swimlanes** tab.
1. On the **Swimlanes** tab, click **+ Swimlane**, directly under the **Swimlane name** label, in the **Name** textbox, type **Expedite**.
1. On the **Settings** panel, click **Save**.
1. Back on the **Board** tab of the **Boards** panel, drag and drop the **Committed** work item onto the **QA Approved \| Doing** stage of the **Expedite** swimlane so that it gets recognized as having priority when QA bandwidth becomes available.

## Review

In this exercise you used Azure Boards to perform a number of common agile planning and portfolio management tasks, including management of teams, areas, iterations, work items, sprints and capacity, customizing Kanban boards, defining dashboards, and customizing team processes.
