namespace GeminiInti.Model
{
    public class GeminiContents
    {
        public List<GeminiParts> contents { get; set; }
    }

    public class GeminiParts
    {
        public List<GeminiTexts> parts { get; set; }
    }

    public class GeminiTexts
    {
        public string text { get; set; }
    }
}
