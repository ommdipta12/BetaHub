namespace GeminiInti.Model
{
    public class GeminiResponse
    {
        public string Res { get; set; }

        public class Candidate
        {
            public Content Content { get; set; }
            public string FinishReason { get; set; }
            public CitationMetadata CitationMetadata { get; set; }
            public double AvgLogprobs { get; set; }
        }

        public class Content
        {
            public List<Part> Parts { get; set; }
            public string Role { get; set; }
        }

        public class Part
        {
            public string Text { get; set; }
        }

        public class CitationMetadata
        {
            public List<CitationSource> CitationSources { get; set; }
        }

        public class CitationSource
        {
            public int EndIndex { get; set; }
            public string Uri { get; set; }
        }

        public List<Candidate> Candidates { get; set; }
        public UsageMetadata Usagemetadata { get; set; }
        public string ModelVersion { get; set; }

        public class UsageMetadata
        {
            public int PromptTokenCount { get; set; }
            public int CandidatesTokenCount { get; set; }
            public int TotalTokenCount { get; set; }
        }
    }
}
