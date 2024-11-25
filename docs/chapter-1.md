# CHAPTER 1 - Create Blazor Webapp Application

## Create a new Blazor web app with the C# Dev Kit:

1. Bring up the Command Palette using Ctrl+Shift+P and then type ".NET".
2. Find and select the .NET: New Project command.
3. Select Blazor Web App in the dropdown list.
4. Select the folder where you want to create the new project.
5. Name the project BlazorPizzaApp and press Enter to confirm.
6. View your new Blazor app project in the Solution Explorer.

### Optional - Create a new Blazor app with the .NET CLI
`dotnet new blazor -o BlazorPizzaApp`

## Explore Project Structure:

1. *Program.cs* is the entry point for the app that starts the server and where you configure the app services and middleware.
2. *App.razor* is the root component for the app.
3. *Routes.razor* configures the Blazor router.
4. The *Components/Pages* directory contains some example web pages for the app.
5. *BlazorApp.csproj* defines the app project and its dependencies and can be viewed by double-clicking the project node in the Solution Explorer.
6. The *launchSettings.json* file inside the Properties directory defines different profile settings for the local development environment. A port number is automatically assigned at project creation and saved on this file.

## Run the app with the integrated debugger

1. In Visual Studio Code, select Run from the menu.
2. Select Start Debugging.
3. Select C# from the Select debugger drop down.
4. Select C#: BlazorApp [Default Configuration]

### Opcional: Run the app with the .NET CLI
dotnet watch

## Add Counter Component to Home page

<details>
<summary>1. In Home.razor component add the Counter component</summary>

```
@page "/"

<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.

<Counter />
```

</details>

<details>
<summary>2. Define a parameter on the Counter component to specify how much it increments with every button click.</summary>

```
[Parameter]
public int IncrementAmount { get; set; } = 1;
```

</details>

<details>
<summary>3. In Home.razor, update the <Counter /> element to add an IncrementAmount attribute that changes the increment amount to 10</summary>

`<Counter IncrementAmount="10" />`

</details