# CHAPTER 5: Deploy to Azure

## Prerequisites and code changes

1. Activate the sandbox azure subscription through this link:
https://learn.microsoft.com/en-us/training/modules/publish-azure-web-app-with-visual-studio/5-exercise-publish-an-asp.net-app-from-visual-studio

2. Create app service in portal.azure.com using the previous subscription

3. Install Azure App Service extension in Visual Studio Code
https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice

4. Ensure Database Migrations on Startup the application:
```
//Program.cs
//Run migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    db.Database.Migrate();
}
app.Run();
```
5. Publish the application using cli command dotnet publish in release configuration
`dotnet publish -c Release`
6. In Explorer in Visual Studio navigate to where the files where published and right click > Deploy Web App.
7. Navegate to Azure app service url to see the app.