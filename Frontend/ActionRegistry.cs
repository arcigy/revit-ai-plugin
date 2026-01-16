using System.Collections.Generic;

namespace RevitAIPlugin
{
    public interface IRevitAction
    {
        void Execute(Dictionary<string, object> parameters);
    }

    public static class ActionRegistry
    {
        private static Dictionary<string, IRevitAction> _actions = new Dictionary<string, IRevitAction>();

        public static void Register(string name, IRevitAction action)
        {
            _actions[name] = action;
        }

        public static bool HasAction(string name) => _actions.ContainsKey(name);

        public static IRevitAction GetAction(string name) => _actions[name];
    }
}
