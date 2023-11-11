# Hacker News API Consumer

## Description
This project is an ASP.NET Core Web API designed to fetch and display the top n stories from Hacker News. It leverages the Hacker News API to retrieve story details and ranks them based on their scores.

## Features
- Retrieve top n stories from Hacker News.
- Efficient handling of HTTP requests to avoid overloading the Hacker News API.
- Caching implemented for improved performance.
- Utilizes `IHttpClientFactory` for optimal HttpClient usage.

## Getting Started

### Prerequisites
- .NET 5.0 SDK or later
- An IDE (like Visual Studio, VS Code, or JetBrains Rider)

## Using the API
- Get Top Stories: GET /BestStories/{n} where n is the number of top stories to retrieve.

## Configuration
Cache Duration: The cache duration can be configured in appsettings.json under the CacheSettings section.

