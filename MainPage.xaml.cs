using Plugin.LocalNotification;
using StudentApp.Models;
using StudentApp.Services;
using StudentApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp
{
    public partial class MainPage : ContentPage
    {
        private List<Term> Terms;
        private List<Course> Courses;
        private List<Assessment> Assessments;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Terms = await TermService.GetTermsAsync();

            collectionView.ItemsSource = Terms;

            if (Terms.Count <= 0)
            {
                // Test Data for C6
                var testTerm = new Term();
                testTerm.Name = "Term 1";
                testTerm.Start = new DateTime(2025, 02, 01);
                testTerm.End = new DateTime(2025, 08, 31);

                var testCourse = new Course();
                testCourse.Name = "Mobile Application Development Using C#";
                testCourse.Start = new DateTime(2025, 02, 01);
                testCourse.End = new DateTime(2025, 04, 01);
                testCourse.IsStart = true;
                testCourse.IsEnd = true;
                testCourse.Status = "In Progress";
                testCourse.Note = "This is just test data for task C6";

                var testInstructor = new Instructor();
                testInstructor.Name = "Anika Patel";
                testInstructor.Phone = "555-123-4567";
                testInstructor.Email = "anika.patel@strimeuniversity.edu";

                var testOA = new Assessment();
                testOA.CourseId = testCourse.Id;
                testOA.Type = "Objective Assessment";
                testOA.Start = new DateTime(2025, 02, 15);
                testOA.End = new DateTime(2025, 03, 15);
                testOA.IsStart = true;
                testOA.IsEnd = true;

                var testPA = new Assessment();
                testPA.CourseId = testCourse.Id;
                testPA.Type = "Performance Assessment";
                testPA.Start = new DateTime(2025, 03, 16);
                testPA.End = new DateTime(2025, 03, 31);
                testPA.IsStart = true;
                testPA.IsEnd = true;

                await TermService.AddTerm(testTerm.Name, testTerm.Start, testTerm.End);
                
                Terms = await TermService.GetTermsAsync();

                collectionView.ItemsSource = Terms;

                foreach (var term in Terms)
                {
                    await CourseService.AddCourse(term.Id, testCourse.Name, testCourse.Start, testCourse.End, testCourse.IsStart, testCourse.IsEnd, testCourse.Status, testCourse.Note);
                    
                    Courses = await CourseService.GetCoursesAsync(term.Id);

                    foreach (var course in Courses)
                    {
                        await CourseService.AddInstructor(course.Id, testInstructor.Name, testInstructor.Phone, testInstructor.Email);
                        await CourseService.AddAssessment(course.Id, testOA.Type, testOA.Start, testOA.End, testOA.IsStart, testOA.IsEnd);
                        await CourseService.AddAssessment(course.Id, testPA.Type, testPA.Start, testPA.End, testPA.IsStart, testPA.IsEnd);
                    }
                }
            }

            if (Terms != null) 
            { 
                foreach (Term term in Terms)
                {
                    Courses = await CourseService.GetCoursesAsync(term.Id);
                }
            }

            if (Courses != null)
            {
                foreach (var course in Courses)
                {
                    Assessments = await CourseService.GetAssessmentsAsync(course.Id);

                    if (course.IsStart && course.Start.Date == DateTime.Today)
                    {
                        var courseStartNotificationRequest = new NotificationRequest
                        {
                            NotificationId = course.Id,
                            Title = "Reminder",
                            Description = course.Name + " starts today!" 
                        };

                        await LocalNotificationCenter.Current.Show(courseStartNotificationRequest);
                    }

                    if (course.IsEnd && course.End.Date == DateTime.Today)
                    {
                        var courseEndNotificationRequest = new NotificationRequest
                        {
                            NotificationId = course.Id,
                            Title = "Reminder",
                            Description = course.Name + " ends today!"
                        };

                        await LocalNotificationCenter.Current.Show(courseEndNotificationRequest);
                    }

                    if (Assessments != null)
                    {
                        foreach (var assessment in Assessments)
                        {
                            if (assessment.IsStart && assessment.Start.Date == DateTime.Today)
                            {
                                var assessmentStartNotificationRequest = new NotificationRequest
                                {
                                    NotificationId = assessment.Id,
                                    Title = "Reminder",
                                    Description = assessment.Type + " starts today!"
                                };

                                await LocalNotificationCenter.Current.Show(assessmentStartNotificationRequest);
                            }

                            if (assessment.IsEnd && assessment.End.Date == DateTime.Today)
                            {
                                var assessmentEndNotificationRequest = new NotificationRequest
                                {
                                    NotificationId = assessment.Id,
                                    Title = "Reminder",
                                    Description = assessment.Type + " ends today!"
                                };

                                await LocalNotificationCenter.Current.Show(assessmentEndNotificationRequest);
                            }
                        }
                    }
                }
            }
        }

        private void AddTermBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTermPage());
        }

        private async void ViewTermBtnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var term = button?.BindingContext as Term;

            if (term != null)
            {
                await Navigation.PushAsync(new ViewTermPage(term.Id));
            }
        }
    }
}
