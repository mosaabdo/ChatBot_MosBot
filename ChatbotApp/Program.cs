using System.Text;
using System.Text.Json;

namespace ChatbotApp
{
    public class Program
    {
        private static readonly string apiKey = "api key";
        private static List<object> chatHistory = new List<object>();
        static async Task Main(string[] args)
        {
            chatHistory.Add(new
            {
                role = "system",
                content = "You are an intelligent assistant. Please provide very accurate and extremely concise answers to save tokens.\r\n" +
                ""
            });


            Console.WriteLine(@"
=================================================
 __  __           ____        _                 =
|  \/  | ___  ___| __ )  ___ | |_               =
| |\/| |/ _ \/ __|  _ \ / _ \| __|              =
| |  | | (_) \__ \ |_) | (_) | |_               =
|_|  |_|\___/|___/____/ \___/ \__|              =
                                                =
=================================================
if you need end chat write exit and click enter =
=================================================
if you need genreat image, start input: image   =
=================================================

        ");


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Input: ");
                string userInput = Console.ReadLine();
                Console.ResetColor();
                if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Bay.");
                    Console.ResetColor();
                    break;
                }
                else if (userInput.StartsWith("image ", StringComparison.OrdinalIgnoreCase))
                {
                    string imagePrompt = userInput.Substring(7);
                    await GenerateImageAsync(imagePrompt);
                }
                else
                {
                    await GenerateTextAsync(userInput);
                }
            }
            Console.WriteLine();
        }
        static async Task GenerateTextAsync(string uinput)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Thinking...");
            chatHistory.Add(new { role = "user", content = uinput });
            if (chatHistory.Count > 7)
            {
                chatHistory.RemoveRange(1, 2);
            }
            var requestBody = new
            {
                model = "gpt-5-mini",
                input = chatHistory,
                max_output_tokens = 450,
                temperature = 1
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/responses", content);
                    string responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        using (JsonDocument doc = JsonDocument.Parse(responseString))
                        {
                            var output = doc.RootElement.GetProperty("output");

                            foreach (var item in output.EnumerateArray())
                            {
                                if (item.GetProperty("type").GetString() == "message")
                                {
                                    var contentArray = item.GetProperty("content");

                                    foreach (var contents in contentArray.EnumerateArray())
                                    {
                                        if (contents.GetProperty("type").GetString() == "output_text")
                                        {
                                            var botReply = contents.GetProperty("text").GetString();

                                            Console.WriteLine($"Bot: {botReply}\n");
                                            chatHistory.Add(new { role = "assistant", content = botReply });
                                            return;
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("No message content found:");
                            Console.WriteLine(responseString);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {responseString}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            Console.ResetColor();

        }
        static async Task GenerateImageAsync(string input)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Generating Image...");

            var requestBody = new
            {
                model = "gpt-image-1-mini",
                prompt = input,
                size = "1024x1024"
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response =
                        await client.PostAsync("https://api.openai.com/v1/images/generations", content);

                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(responseString);
                        return;
                    }

                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        var root = doc.RootElement;

                        string? base64 = null;

                        if (root.TryGetProperty("data", out var data))
                        {
                            if (data[0].TryGetProperty("b64_json", out var b64))
                                base64 = b64.GetString();
                        }

                        if (root.TryGetProperty("output", out var output))
                        {
                            foreach (var item in output.EnumerateArray())
                            {
                                if (item.TryGetProperty("b64_json", out var b64))
                                    base64 = b64.GetString();
                            }
                        }

                        if (string.IsNullOrEmpty(base64))
                        {
                            Console.WriteLine("No image found");
                            return;
                        }

                        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        string folderPath = Path.Combine(projectRoot, "images");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string fileName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                        string filePath = Path.Combine(folderPath, fileName);

                        byte[] imageBytes = Convert.FromBase64String(base64);
                        File.WriteAllBytes(filePath, imageBytes);

                        Console.WriteLine("Success ✔️");
                        Console.WriteLine($"Image saved in: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            Console.ResetColor();
        }
    }
}
