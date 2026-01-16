using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace RevitAIPlugin
{
    public static class Logger
    {
        public static void LogAction(string actionName, Dictionary<string, object> parameters)
        {
            // Example: write to file or console
            Debug.WriteLine($"Action: {actionName}, Params: {JsonConvert.SerializeObject(parameters)}");
        }

        public static void LogError(string actionName, string error)
        {
            Debug.WriteLine($"Error in {actionName}: {error}");
        }
    }
}
