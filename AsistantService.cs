using System.Text;

namespace ChatBot
{
    public static class AsistantService
    {
        private const string GptApiKey = "sk-proj-osKosxKcgWGrkVzK2upPT3BlbkFJn0wKSFF9dqoxX9SFl29M";
        private const string GptApiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";

        public static async Task<List<Question>> CreateTestForLanguages(string language, string degree)
        {
            var questions = new List<Question>();
            var client = new HttpClient();
            var apiKey = "sk-proj-osKosxKcgWGrkVzK2upPT3BlbkFJn0wKSFF9dqoxX9SFl29M";
            var url = "https://api.openai.com/v1/chat/completions";

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "user", content = $"Please generate 30 {language} questions for {degree} with 4 options and one correct answer, start the correct answer with a *, for example \"*Yes\", " +
                    $"question structure: 3+2=? find the value of?\n*5\n\n4\n\n7\n\n9, separate the questions from each other using the ~ symbol" }
                }
            };

            var jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await client.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadFromJsonAsync<Root>();
                var t = responseString.choices[0].message.content.Split("~").ToList();
                foreach (var a in t)
                {
                    var question = new Question();
                    var m = a.Split("\n").ToList();
                    m = m.Where(x => x != "").Where(x => x != " ").ToList();
                    question.Message = m[0];
                    if (m[1].Contains("*"))
                    {
                        question.RightAnswer = 0;
                    }
                    else if (m[2].Contains("*"))
                    {
                        question.RightAnswer = 1;
                    }
                    else if (m[3].Contains("*"))
                    {
                        question.RightAnswer = 2;
                    }
                    else if (m[4].Contains("*"))
                    {
                        question.RightAnswer = 3;
                    }
                    question.AnswerA = m[1].Replace("*", "");
                    question.AnswerB = m[3].Replace("*", "");
                    question.AnswerC = m[3].Replace("*", "");
                    question.AnswerD = m[4].Replace("*", "");
                    questions.Add(question);
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            return questions;
        }
    }
}
public class Choice
{
    public int index { get; set; }
    public Message message { get; set; }
    public object logprobs { get; set; }
    public string finish_reason { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}

public class Root
{
    public string id { get; set; }
    public string @object { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public List<Choice> choices { get; set; }
    public Usage usage { get; set; }
    public object system_fingerprint { get; set; }
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

