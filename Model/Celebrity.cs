using System;

namespace PopularCelebrities.Models
{
    public class Celebrity
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }

        public string CelebrityType { get; set; }

        public bool IsMale { get; set; }

        public string PictureUrl { get; set; }

    }
}