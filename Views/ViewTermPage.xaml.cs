using Microsoft.Maui.Controls;
using StudentApp.Models;
using StudentApp.Services;
using System.Diagnostics;

namespace StudentApp.Views;

public partial class ViewTermPage : ContentPage
{
    private int _termId;
    private List<Course> Courses;

	public ViewTermPage(int termId)
	{
		InitializeComponent();

        _termId = termId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var term = await TermService.GetTerm(_termId);

        BindingContext = term;

        Courses = await CourseService.GetCoursesAsync(_termId);

        collectionView.ItemsSource = Courses;
    }

    private async void EditTermBtnClicked(object sender, EventArgs e)
    {
        if (_termId != 0)
        {
            await Navigation.PushAsync(new EditTermPage(_termId));
        }
    }

    private void AddCourseBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddCoursePage(_termId)); 
    }

    private async void ViewCourseBtnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Course;

        if (course != null)
        {
            await Navigation.PushAsync(new ViewCoursePage(course.Id));
        }
    }
}