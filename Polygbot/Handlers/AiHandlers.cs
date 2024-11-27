using Mistral;

namespace Polygbot.Handlers
{
    public class AiHandlers
    {
        private static MistralClient _client = new MistralClient("r15uvSJsdSTGKygysJWuJqKRZjQGAKEm");

        public static async Task<string> UpdateAiHandler(string prompt)
        {
            var answer = await _client.CompleteAsync(
                new()
                {
                    Model = "mistral-large-latest",
                    Messages = [new() { Role = "user", Content = prompt }]
                });

            return answer.Choices[0].Message.Content;
        }
    }
}
