using System;
using Newtonsoft.Json;

namespace HackerNewsApi
{
    public class Story
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string PostedBy { get; set; }
        //public DateTime Time { get; set; }
        
        public string Time { get; set; }

        // You can create a method or a computed property to parse the Time as DateTime
        [JsonIgnore] // Json.NET should ignore this when serializing/deserializing
        public DateTime PostedTime
        {
            get
            {
                // Assuming the time is a Unix timestamp, convert it to DateTime
                if (long.TryParse(Time, out long unixTime))
                {
                    return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
                }

                // Handle the case where the time is not a valid number
                // Perhaps log the error or handle it as you see fit
                return DateTime.MinValue;
            }
        }


        public int Score { get; set; }
        public int CommentCount { get; set; }
    }
}