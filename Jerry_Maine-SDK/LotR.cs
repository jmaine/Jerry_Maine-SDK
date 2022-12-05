using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Text.Json;
using System.Web;
using System.Linq;

namespace Jerry.Maine.SDK
{
    public class LotR
    {
        private const string BaseURL = "https://the-one-api.dev/v2/";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IApiKey apiKey;
        private readonly JsonSerializerOptions? options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public LotR(IHttpClientFactory httpClientFactory, IApiKey apiKey)
        {
            this.httpClientFactory = httpClientFactory;
            this.apiKey = apiKey;
        }

        private async Task<T?> GetObjects<T>(string url, string? param = null)
        {
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            var uriBuilder = new UriBuilder(BaseURL) { Query = param };
            uriBuilder.Path = $"{uriBuilder.Path}/{url}";
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey.Key);
            httpRequestMessage.RequestUri = uriBuilder.Uri ;
            using var message = await httpClientFactory.CreateClient().SendAsync(httpRequestMessage);
            using var content = message.Content;
            using var stream = await content.ReadAsStreamAsync();
            if (message.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, options);
            }
            else
            {
                throw new Exception();
            }
        }

        private string NameValuePair(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Uri.EscapeDataString(name);
            }
            else
            {
                return Uri.EscapeDataString(name) + "=" + Uri.EscapeDataString( value);
            }
        }

        public async Task<ListResult<Book>?> Books(List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort? sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            if (sort?.Field != null)
            {
                paramz.Add(NameValuePair("sort", $"{sort.Field}:{sort.Direction.ToString().ToLowerInvariant()}" ));
            }

            return await GetObjects<ListResult<Book> ? >("book", string.Join("&", paramz));
        }

       

        public async Task<Book?> Book(string id)
        {
            return await GetObjects<Book?>($"book/{Uri.EscapeUriString(id)}");
        }
        public async Task<ListResult<Chapter>?> BookChapters(string id, List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort? sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            if (sort?.Field != null)
            {
                paramz.Add(NameValuePair("sort", $"{sort.Field}:{sort.Direction.ToString().ToLowerInvariant()}"));
            }

            return await GetObjects<ListResult<Chapter>?>($"book/{Uri.EscapeUriString(id)}/chapter", string.Join("&", paramz));
        }

        public async Task<ListResult<Movie>?> Movies(List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort? sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            if (sort?.Field != null)
            {
                paramz.Add(NameValuePair("sort", $"{sort.Field}:{sort.Direction.ToString().ToLowerInvariant()}"));
            }

            return await GetObjects<ListResult<Movie>?>("movie", string.Join("&", paramz));
        }

        public async Task<Movie?> Movie(string id)
        {
            return await GetObjects<Movie?>($"movie/{Uri.EscapeUriString(id)}");

        }

        public async Task<ListResult<Quote>?> MovieQuote(string id, List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            if (sort != null)
            {
                paramz.Add(NameValuePair("sort", $"{sort.Field}:{sort.Direction.ToString().ToLowerInvariant()}"));
            }

            return await GetObjects<ListResult<Quote>?>($"movie/{Uri.EscapeUriString(id)}/quote", string.Join("&", paramz));
        }

        public async Task<ListResult<Character>?> Characters(List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            return await GetObjects<ListResult<Character>?>("character", string.Join("&", paramz));
        }

        public async Task<Character?> Character(string id)
        {
            return await GetObjects<Character?>($"character/{Uri.EscapeUriString(id)}");
        }

        public async Task<ListResult<Quote>?> CharacterQuote(string id, List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            return await GetObjects<ListResult<Quote>?>($"character/{Uri.EscapeUriString(id)}/quote", string.Join("&", paramz));
        }

        public async Task<ListResult<Quote>?> Quotes(List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            return await GetObjects<ListResult<Quote>?>("character", string.Join("&", paramz));
        }

        public async Task<Quote?> Quote(string id)
        {
            return await GetObjects<Quote?>($"quote/{Uri.EscapeUriString(id)}");
        }

        public async Task<ListResult<Chapter>?> Chapters(List<IFilter>? filters = null, int? limit = null, int? page = null, int? offset = null, Sort sort = null)
        {
            var paramz = filters?.Where(x => !(x.Name == "limit" || x.Name == "page" || x.Name == "offset"))?.Select(x => NameValuePair(x.Name, x.Value))?.ToList() ?? new List<string>();

            if (limit != null)
            {
                paramz.Add(NameValuePair("limit", limit.ToString()));
            }
            if (page != null)
            {
                paramz.Add(NameValuePair("page", page.ToString()));
            }
            if (offset != null)
            {
                paramz.Add(NameValuePair("offset", offset.ToString()));
            }

            return await GetObjects<ListResult<Chapter>?>("chapter", string.Join("&", paramz));
        }

        public async Task<Book? > Chapter(string id)
        {
            return await GetObjects<Book?>($"quote/{Uri.EscapeUriString(id)}");
        }
    }
}
