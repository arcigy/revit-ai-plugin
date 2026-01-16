using System;
using Autodesk.Revit.UI;
using System.Windows;

namespace RevitAIPlugin
{
    public static class ExternalEventWrapper
    {
        private static ExternalEvent _externalEvent;
        private static RevitExternalEventHandler _handler;

        public static void Initialize()
        {
            _handler = new RevitExternalEventHandler(null); // Initial null action
            _externalEvent = ExternalEvent.Create(_handler);
        }

        public static void Run(Action action)
        {
            if (_handler == null) Initialize();
            _handler.SetAction(action);
            _externalEvent.Raise();
        }
    }

    public class RevitExternalEventHandler : IExternalEventHandler
    {
        private Action _action;

        public RevitExternalEventHandler(Action action)
        {
            _action = action;
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void Execute(UIApplication app)
        {
            try
            {
                _action?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing action: {ex.Message}");
            }
        }

        public string GetName() => "RevitExecutionHandler";
    }
}
