namespace capstone_backend.Models
{
    public class Bugs
    {
        public int BugId { get; set; }
        public int ProjectId {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string StepsToReproduce { get; set; }
        public string Status {  get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string FilePath { get; set; }
        public string TesterName { get; set; }
        public string DeveloperName { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string CompletedAt { get; set; }
    }
}
