## BinaryPlate

A Clean Architecture and Multi-Tenant Starter Template for Building SaaS Applications using Blazor WASM and .NET with Full Source Code.

### Getting Started

*   Open the **BinaryPlate** project in **Visual Studio**.
*   Right-click on the root **Solution** node and select Set Startup Projects.. from the context menu.
*   Click on the **Multiple startup projects** radio button and choose to run the **BlazorPlate**, **WebApi**, and **HostApp** projects as startup projects.
*   Hit **F5** to run all the startup projects in order to generate the **.vs** folder.
*   Hit **Shift+F5** to stop running all the startup projects.
*   Right-click on the solution and select **Open Folder in File Explorer** from the context menu, then close **Visual Studio**.
*   Navigate to **.vs\\BinaryPlate\\config** and open the **applicationhost.config** file in any code editor.
*   Press **CTRL+F** and search for \[**name="BinaryPlate.BlazorPlate**\] (without the brackets) until you reach the **site** tag for the **BinaryPlate.BlazorPlate** project.
*   Remove the **localhost** portion from the **binding** tag as shown in the following screenshot.

![](https://blazorplate.net/assets/img/code-screenshots/applicationhost.png)

The site block should look like the following screenshot after editing.

![](https://blazorplate.net/assets/img/code-screenshots/applicationhost-without-localhost.png)

*   Open **SSMS** and create a new database, and name it **Hangfire**.
*   Run Visual Studio as **Administrator**.
*   Hit **F5** to run the startup projects.
