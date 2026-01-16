using System.Windows;
using System.Collections.Generic;

namespace RevitAIPlugin
{
    public class CommandValidator
    {
        public bool ValidateInput(string command, List<SelectedElement> selectedElements, ImageContext image)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                MessageBox.Show("Command cannot be empty.");
                return false;
            }

            // Placeholder for selection validation
            // if (commandRequiresSelection(command) && (selectedElements == null || selectedElements.Count == 0))
            // {
            //     MessageBox.Show("You must select elements for this command.");
            //     return false;
            // }

            // Placeholder for image validation
            // if (image != null && !IsValidImage(image))
            // {
            //     MessageBox.Show("Uploaded image is invalid or too large.");
            //     return false;
            // }

            return true;
        }

        private bool IsValidImage(ImageContext image)
        {
             // Placeholder logic
             return !string.IsNullOrEmpty(image.Base64);
        }
    }
}
