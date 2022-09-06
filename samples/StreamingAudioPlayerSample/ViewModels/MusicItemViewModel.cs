using System;
namespace StreamingAudioPlayerSample.ViewModels;

public class MusicItemViewModel : BaseViewModel
{
	public MusicItemViewModel(string title, string artist, string source)
	{
		Title = title;
		Artist = artist;
		Source = source;
	}

	public string Title { get; }
	public string Artist { get; }
	public string Source { get; }
}
