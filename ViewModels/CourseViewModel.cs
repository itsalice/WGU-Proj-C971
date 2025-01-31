using StudentApp.Models;

namespace StudentApp.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public Assessment Assessment { get; set; }

    }
}
