using System.Windows;

namespace RevitAIPlugin
{
    public static class ExecutionEngine
    {
        public static void ExecuteWorkflow(Workflow workflow)
        {
            foreach (var step in workflow.Steps)
            {
                if (!ActionRegistry.HasAction(step.Command))
                {
                    MessageBox.Show($"Unknown action: {step.Command}");
                    continue;
                }

                var action = ActionRegistry.GetAction(step.Command);
                ExternalEventWrapper.Run(() => action.Execute(step.Parameters));
            }
        }
    }
}
