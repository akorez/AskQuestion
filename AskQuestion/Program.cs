using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;


var Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>() 
    .Build();
var APIkey = Configuration["APIkey"];


while (true)
{
    Console.WriteLine("Lutfen soru sorunuz!");
    string question = Console.ReadLine();

    if (question.Contains("q") || question.ToLower().Contains("quit"))
        break;

    if (question.Length > 0)
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Add("authorization", $"Bearer {APIkey}");

        var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + question + "\",\"temperature\": 1,\"max_tokens\": 500}",
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

        string responseString = await response.Content.ReadAsStringAsync();

        try
        {
            var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);

            string guess = GuessCommand(dyData!.choices[0].text);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"---> ChatGPT answer is: {guess}");
            Console.ResetColor();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not deserialize the output JSON: {ex.Message}");
        }

    }
    else
    {
        Console.WriteLine("---> You need to ask a question");
    }

    static string GuessCommand(string raw)
    {
        var lastIndex = raw.LastIndexOf('\n');
        string guess = raw.Substring(lastIndex + 1);

        return guess;
    }
}