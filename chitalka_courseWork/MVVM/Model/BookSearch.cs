using Newtonsoft.Json;
using System.Net.Http;

namespace chitalka_courseWork;

public class BookSearch
{
    private static readonly string _apiKey = "";

    public static async Task<List<Book>> Search(string query)
    {
        Book book = null;
        List<Book> searchResults = [];

        string encodedTitle = Uri.EscapeDataString(query);
        string url = $"https://www.googleapis.com/books/v1/volumes?q={encodedTitle}&country=UA&key={_apiKey}";

        using (HttpClient client = new())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JsonConvert.DeserializeObject(responseBody);

                if (jsonData != null && jsonData.items != null && jsonData.items.Count > 0)
                {
                    for (int i = 0; i < 10; i ++)
                    {
                        dynamic volumeInfo = jsonData.items[i].volumeInfo;

                        string bookTitle = volumeInfo.title;
                        string bookDescription = volumeInfo.description ?? "";
                        string bookAuthor = volumeInfo.authors != null ? string.Join(", ", volumeInfo.authors) : "";
                        string coverUrl = volumeInfo.imageLinks != null ? volumeInfo.imageLinks.thumbnail : "";
                        int? pageCount = volumeInfo.pageCount;

                        if (pageCount != null)
                        {
                            book = new Book(bookTitle, bookAuthor, bookDescription, (int)pageCount, coverUrl);
                            searchResults.Add(book);
                        }
                    }
                    
                }
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
        return searchResults;
    }
}


