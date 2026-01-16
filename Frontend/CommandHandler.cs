using System;
using System.Collections.Generic;
using System.Windows;
using Autodesk.Revit.DB;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace RevitAIPlugin
{
    public class CommandHandler
    {
        private readonly string _backendUrl = "https://revit-ai-plugin-production.up.railway.app";
        private readonly HttpClient _httpClient = new HttpClient();

        public bool ValidateInput(string command, List<Element> selectedElements, ImageContext image)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                MessageBox.Show("Command cannot be empty.");
                return false;
            }

            // Placeholder for specific command logic validation
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

        public async Task<CommandResponse> SendRequestAsync(CommandRequest payload)
        {
            string json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(_backendUrl + "/api/interpret", content);
                response.EnsureSuccessStatusCode();
                string responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResponse>(responseJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error communicating with backend: {ex.Message}");
                return null;
            }
        }

        public void HandleResponse(CommandResponse response)
        {
            if (response == null)
            {
                MessageBox.Show("No response from AI backend.");
                return;
            }

            if (response.Errors != null && response.Errors.Any())
            {
                MessageBox.Show("AI workflow errors: " + string.Join(", ", response.Errors));
                return;
            }

            if (response.Workflow != null)
            {
                ExecutionEngine.ExecuteWorkflow(response.Workflow);
            }
        }

        public async Task SendFeedbackAsync(string command, bool success, string note)
        {
            var feedback = new FeedbackRequest
            {
                CommandText = command,
                Success = success,
                Note = note
            };

            string json = JsonConvert.SerializeObject(feedback);
            await _httpClient.PostAsync(_backendUrl + "/api/feedback", new StringContent(json, Encoding.UTF8, "application/json"));
        }
    }
}
