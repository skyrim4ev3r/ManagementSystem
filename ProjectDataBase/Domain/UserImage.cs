namespace Project.ProjectDataBase.Domain
{
    public class UserImage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string FileName { get; set; }
        public bool IsMainPhoto { get; set; }
    }
}
