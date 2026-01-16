using System.Collections.Generic;

namespace RevitAIPlugin
{
    public class PayloadBuilder
    {
        public CommandRequest BuildPayload(string command, string activeView, List<SelectedElement> selection, ImageContext image)
        {
            return new CommandRequest
            {
                CommandText = command,
                Context = new Context
                {
                    ActiveView = activeView,
                    SelectedElements = selection
                },
                ImageContext = image
            };
        }
    }
}
