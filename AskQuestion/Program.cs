using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;


var Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>() 
    .Build();
var APIkey = Configuration["APIkey"];


while (true)
{
    Console.WriteLine("Please ask your question!");
    string question = Console.ReadLine();

    if (question.Trim().ToLower() =="quit" || question.Trim().ToLower() == "q")
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"---> ChatGPT answer is: {dyData!.choices[0].text}");
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
}
