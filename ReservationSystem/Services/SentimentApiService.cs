// Services/SentimentApiService.cs
using System.Net.Http;
using System.Threading.Tasks; // Asenkron işlemler için
using System.Text;
using Newtonsoft.Json; // JSON serileştirme/deserileştirme için

public class SentimentApiService
{
    // Python API'nin yerel adresi
    // Python terminalinde yazan adresle (http://0.0.0.0:8000 veya localhost) aynı olması şarttır.
    private const string ApiUrl = "http://localhost:8000/api/sentiment/analyze";
    private readonly HttpClient _httpClient = new HttpClient();

    // Gelen yorumu Python'a gönderir ve analiz sonucunu alır.
    public async Task<string> GetSentimentAsync(string commentText)
    {
        // İsteği Python'ın beklediği JSON formatına dönüştürme
        var payload = new { text = commentText };
        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            // 2. Python Servisine ASENKRON POST isteği gönderme
            HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, content);

            // 3. Yanıtı kontrol etme
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // 4. Gelen JSON yanıtını (örnek: {"sentiment": "Pozitif"}) ayrıştırma
                dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                // Sentiment alanını çek
                return (string)result.sentiment;
            }
            else
            {
                // API hata verirse, durumu loglayıp Nötr döndür
                return $"API Hata: {response.StatusCode}";
            }
        }
        catch (HttpRequestException)
        {
            // Servis çalışmıyorsa veya bağlantı yoksa
            return "API Servis Bağlantı Hatası";
        }
    }
}