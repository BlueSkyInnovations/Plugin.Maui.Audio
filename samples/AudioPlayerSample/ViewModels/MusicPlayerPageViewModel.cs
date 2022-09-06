using System.ComponentModel;
using Microsoft.Maui.Dispatching;
using Plugin.Maui.Audio;

namespace AudioPlayerSample.ViewModels;

public class MusicPlayerPageViewModel : BaseViewModel, IQueryAttributable, IDisposable
{
	readonly IAudioManager audioManager;
	readonly IDispatcher dispatcher;
	IAudioPlayer audioPlayer;
	TimeSpan animationProgress;
	MusicItemViewModel musicItemViewModel;
	bool isPositionChangeSystemDriven;
	bool isDisposed;

	public MusicPlayerPageViewModel(
		IAudioManager audioManager,
		IDispatcher dispatcher)
	{
		this.audioManager = audioManager;
		this.dispatcher = dispatcher;

		PlayCommand = new Command(Play);
		PauseCommand = new Command(Pause);
		StopCommand = new Command(Stop);
	}

	public async void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		if (query.TryGetValue(Routes.MusicPlayer.Arguments.Music, out object musicObject) &&
			musicObject is MusicItemViewModel musicItem)
		{
			MusicItemViewModel = musicItem;

			audioPlayer = audioManager.CreatePlayerFromUri( musicItem.Source);

			NotifyPropertyChanged(nameof(HasAudioSource));	
		}
	}

	public MusicItemViewModel MusicItemViewModel
	{
		get => musicItemViewModel;
		set
		{
			musicItemViewModel = value;
			NotifyPropertyChanged();
		}
	}

	public bool HasAudioSource => audioPlayer is not null;

	public bool IsPlaying => audioPlayer?.IsPlaying ?? false;

	public TimeSpan AnimationProgress
	{
		get => animationProgress;
		set
		{
			animationProgress = value;
			NotifyPropertyChanged();
		}
	}

	public Command PlayCommand { get; set; }
	public Command PauseCommand { get; set; }
	public Command StopCommand { get; set; }

	public double Volume
	{
		get => audioPlayer?.Volume ?? 1;
		set
		{
			if (audioPlayer != null)
			{
				audioPlayer.Volume = value;
			}
		}
	}

	void Play()
	{
		audioPlayer.Play();

		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Pause()
	{
		if (audioPlayer.IsPlaying)
		{
			audioPlayer.Pause();
		}
		else
		{
			audioPlayer.Play();
		}
	
		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Stop()
	{
		if (audioPlayer.IsPlaying)
		{
			audioPlayer.Stop();

			AnimationProgress = TimeSpan.Zero;

			NotifyPropertyChanged(nameof(IsPlaying));
		}
	}

	public void TidyUp()
	{
		//audioPlayer?.Dispose();
		//audioPlayer = null;
	}

	~MusicPlayerPageViewModel()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);

		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (isDisposed)
		{
			return;
		}

		if (disposing)
		{
			TidyUp();
		}

		isDisposed = true;
	}
}
