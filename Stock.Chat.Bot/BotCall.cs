using System.Net.Http.Headers;

namespace Stock.Chat.CrossCutting
{
    public class BotCall : IDisposable
    {
        private const string PREFIX = "https://stooq.com/q/l/?s=";
        private const string URL = ".us&f=sd2t2ohlcv&h&e=csv";
        private string quote = string.Empty;

        public string CallServiceStock(string keyWord)
        {
            var url = $"{PREFIX}{keyWord}{URL}";
            quote = GetStockInformation(PREFIX,url).Split(',')[13];
            return VerifyResponse() ? $"{keyWord} quote is ${quote} per share" : $"Stock code \"{keyWord}\" not found";
        }

        public bool VerifyResponse() => !quote.Contains("N/D");

        public void Dispose() => GC.SuppressFinalize(this);

        public static string GetStockInformation(string prefix, string url)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(prefix);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result = Task.Run(async () => await client.GetAsync(url)).Result;
            return Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
        }

        public static bool IsStockCall(string receivedMessage) => string.Compare(receivedMessage, 0, "/stock=", 0, 7) == 0;
    }
}
