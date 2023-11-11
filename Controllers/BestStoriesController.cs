using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        public BestStoriesController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("{n}")]
        public async Task<IActionResult> GetBestStories(int n)
        {
            var ids = await _hackerNewsService.GetBestStoriesIdsAsync();
            var tasks = ids.Take(n).Select(id => _hackerNewsService.GetStoryDetailsAsync(id));
            var stories = await Task.WhenAll(tasks);

            return Ok(stories.OrderByDescending(s => s.Score));
        }
    }
}
