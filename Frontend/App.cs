using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace RevitAIPlugin
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Initialize External Events
            ExternalEventWrapper.Initialize();

            // Create Ribbon Panel
            string tabName = "RevitAI";
            application.CreateRibbonTab(tabName);
            RibbonPanel panel = application.CreateRibbonPanel(tabName, "AI Tools");

            // Create Button
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData(
                "cmdRevitAI",
                "Execute AI\nCommand",
                assemblyPath,
                "RevitAIPlugin.Command"
            );

            // Add Tooltip
            buttonData.ToolTip = "Open the AI Command Interface";

            // Add Icon (Placeholder)
            // Uri uriImage = new Uri("pack://application:,,,/RevitAIPlugin;component/Resources/execute.png");
            // BitmapImage largeImage = new BitmapImage(uriImage);
            // buttonData.LargeImage = largeImage;

            panel.AddItem(buttonData);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
