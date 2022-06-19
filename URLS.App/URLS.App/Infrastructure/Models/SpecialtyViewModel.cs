namespace URLS.App.Infrastructure.Models
{
    public class SpecialtyViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public FacultyViewModel Faculty { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
}
