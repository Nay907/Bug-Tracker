namespace capstone_backend.Models
{
    public class Comments
    {
        public int CommentId {  get; set; }
        public int BugId { get; set; }
        public string TesterName {  get; set; }
        public string DeveloperName {  get; set; }
        public string Comment { get; set; }


    }
}
