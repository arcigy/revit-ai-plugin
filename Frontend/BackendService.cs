using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class BackendService
{
    private static readonly HttpClient client = new HttpClient();
    private const string backendUrl = "https://revit-ai-plugin-production.up.railway.app";

    public async Task<CommandResponse> SendRequestAsync(CommandRequest payload)
    {
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync($"{backendUrl}/api/interpret", content);
            response.EnsureSuccessStatusCode();
            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommandResponse>(responseJson);
        }
        catch (Exception ex)
        {
            // In a real application, you would log this error and show a user-friendly message.
            Console.WriteLine($"Error communicating with backend: {ex.Message}");
            return new CommandResponse { Errors = new[] { ex.Message } };
        }
    }

    public async Task SendFeedbackAsync(FeedbackRequest feedback)
    {
        var json = JsonConvert.SerializeObject(feedback);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            await client.PostAsync($"{backendUrl}/api/feedback", content);
        }
        catch (Exception ex)
        {
            // In a real application, you would log this error.
            Console.WriteLine($"Error sending feedback: {ex.Message}");
        }
    }
}
