﻿namespace URLS.App.Infrastructure.Models
{
    public class UniversityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string ShortName { get; set; }
        public string ShortNameEng { get; set; }
        public List<FacultyViewModel> Faculties { get; set; }
    }
}
