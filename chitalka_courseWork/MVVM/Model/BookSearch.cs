using Google.Apis.Books.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Google.Apis.Services;
using Google.Apis.Books.v1.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using System.Net;

namespace chitalka_courseWork
{
    public class BookSearch
    {
        private static readonly string apiKey = "AIzaSyCEMRkC0TfWFk9pgfy1wkwEflW2k73BTT0";

        public static async Task<List<Book>> Search(string title)
        {
            Book book = null;
            List<Book> searchResults = new List<Book>();


            string encodedTitle = Uri.EscapeDataString(title);
            string url = $"https://www.googleapis.com/books/v1/volumes?q={encodedTitle}&country=UA&key={apiKey}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonData = JsonConvert.DeserializeObject(responseBody);

                    if (jsonData != null && jsonData.items != null && jsonData.items.Count > 0)
                    {
                        
                        for (int i = 0; i < 8; i++)
                        {
                            dynamic volumeInfo = jsonData.items[i].volumeInfo;

                            string bookTitle = volumeInfo.title;
                            string bookDescription = volumeInfo.description ?? "";
                            string bookAuthor = volumeInfo.authors != null ? string.Join(", ", volumeInfo.authors) : "";
                            int pageCount = volumeInfo.pageCount;
                            book = new Book(bookTitle, bookAuthor, bookDescription, pageCount);

                            searchResults.Add(book);
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

    
}
