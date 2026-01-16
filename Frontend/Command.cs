using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAIPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            // Initialize External Events if not already done (double safety)
            ExternalEventWrapper.Initialize();

            // Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            // Show Command Window
            // We pass the UIDocument to the window so it can access selection/active view
            CommandWindow window = new CommandWindow(uidoc);
            window.ShowDialog();

            return Result.Succeeded;
        }
    }
}
