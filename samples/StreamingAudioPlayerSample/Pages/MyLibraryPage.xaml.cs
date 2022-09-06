using StreamingAudioPlayerSample.ViewModels;

namespace StreamingAudioPlayerSample.Pages;

public partial class MyLibraryPage : ContentPage
{
	public MyLibraryPage(MyLibraryPageViewModel myLibraryPageViewModel)
	{
		InitializeComponent();

		BindingContext = myLibraryPageViewModel;
	}
}
