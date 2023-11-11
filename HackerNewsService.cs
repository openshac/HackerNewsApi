using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HackerNewsApi
{
    public class HackerNewsService
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
        private CacheSettings _cacheSettings;

        public HackerNewsService(IMemoryCache cache, IOptions<CacheSettings> cacheSettings, HttpClient httpClient)
        {
            _cache = cache;
            _httpClient = httpClient;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<IEnumerable<int>> GetBestStoriesIdsAsync()
        {

            return await _cache.GetOrCreateAsync("best_stories_ids", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                string bestStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
                var response = await _httpClient.GetAsync(bestStoriesUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<int>>();
            });
            
            //string bestStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
            //var response = await _httpClient.GetAsync(bestStoriesUrl);
            //response.EnsureSuccessStatusCode();
            //var ids = await response.Content.ReadAsAsync<IEnumerable<int>>();
            //return ids;
        }

        public async Task<Story> GetStoryDetailsAsync(int id)
        {

            return await _cache.GetOrCreateAsync("stories_details", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheSettings.DurationInSeconds);

                string storyUrl = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";
                var response = await _httpClient.GetAsync(storyUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Story>();

            });

            //    string storyUrl = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";
            //var response = await _httpClient.GetAsync(storyUrl);
            //response.EnsureSuccessStatusCode();
            //var story = await response.Content.ReadAsAsync<Story>();
            //return story;
        }
    }
}
