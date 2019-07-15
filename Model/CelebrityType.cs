namespace PopularCelebrities.Models
{
    public class CelebrityType
    {
        public const string ACTOR = "Actor";
        public const string PRODUCER = "Producer";
        public const string WRITER = "Writer";
        public const string MUSIC_DEPARTMENT = "Music_department";

        public string GetType(string description)
        {
            switch (description.Trim())
            {
                case PRODUCER:
                    return PRODUCER;
                case WRITER:
                    return WRITER;
                case MUSIC_DEPARTMENT:
                    return MUSIC_DEPARTMENT;
                default:
                    return ACTOR;

            }
        }
    }
}