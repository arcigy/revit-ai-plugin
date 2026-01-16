using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAIPlugin
{
    public partial class CommandWindow : Window
    {
        private ImageContext _currentImage;
        private List<SelectedElement> _selectedElements;
        private readonly UIDocument _uidoc;

        public CommandWindow(UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg;*.jpeg;*.png";
            if (dlg.ShowDialog() == true)
            {
                string path = dlg.FileName;
                byte[] imageBytes = File.ReadAllBytes(path);
                string base64 = Convert.ToBase64String(imageBytes);

                _currentImage = new ImageContext
                {
                    Filename = Path.GetFileName(path),
                    Base64 = base64
                };

                lblImageName.Content = Path.GetFileName(path);
            }
        }

        private void BtnUseSelected_Click(object sender, RoutedEventArgs e)
        {
            ICollection<ElementId> ids = _uidoc.Selection.GetElementIds();

            var selectedElements = ids.Select(id => {
                Element ele = _uidoc.Document.GetElement(id);
                return new SelectedElement {
                    Id = id.IntegerValue,
                    Category = ele.Category?.Name,
                    Name = ele.Name
                };
            }).ToList();

            _selectedElements = selectedElements;
            lblSelectionCount.Content = $"{selectedElements.Count} elements selected";
        }

        private async void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            var payload = new CommandRequest
            {
                CommandText = txtCommand.Text,
                Context = new Context {
                    ActiveView = _uidoc.ActiveView.Name,
                    SelectedElements = _selectedElements
                },
                ImageContext = _currentImage
            };

            var backendService = new BackendService();
            var response = await backendService.SendRequestAsync(payload);
            
            // Assuming FeedbackDialog.Show handles the response or we process it here
            // FeedbackDialog.Show(response); 
            // For now, let's just execute the workflow if valid
            
            if (response != null && response.Workflow != null)
            {
                 // Execution logic would go here, likely delegating to ExecutionEngine
                 // ExecutionEngine.ExecuteWorkflow(response.Workflow);
                 MessageBox.Show("Workflow executed (placeholder).");
            }
            else if (response != null && response.Errors != null)
            {
                MessageBox.Show("Errors: " + string.Join(", ", response.Errors));
            }
        }
    }
}
