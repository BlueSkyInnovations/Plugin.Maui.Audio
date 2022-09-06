using System.Collections.ObjectModel;

namespace StreamingAudioPlayerSample.ViewModels;

public class MyLibraryPageViewModel : BaseViewModel
{
	MusicItemViewModel selectedMusicItem;

	public MyLibraryPageViewModel()
	{
		Music = new ObservableCollection<MusicItemViewModel>
		{
			new MusicItemViewModel("SlayRadio", "Stream", "http://relay1.slayradio.org:8000")		
		};
	}

	public ObservableCollection<MusicItemViewModel> Music { get; }

	public MusicItemViewModel SelectedMusicItem
	{
		get => selectedMusicItem;
		set
		{
			selectedMusicItem = value;
			NotifyPropertyChanged();

			OnMusicItemSelected();
		}
	}

	async void OnMusicItemSelected()
	{
		await Shell.Current.GoToAsync(
			Routes.MusicPlayer.RouteName,
			new Dictionary<string, object>
			{
				[Routes.MusicPlayer.Arguments.Music] = SelectedMusicItem
			});
	}
}
