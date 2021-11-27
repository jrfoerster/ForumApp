namespace ForumApp.Models
{
    public class ForumListItem
    {
        public int ForumId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
    }
}
