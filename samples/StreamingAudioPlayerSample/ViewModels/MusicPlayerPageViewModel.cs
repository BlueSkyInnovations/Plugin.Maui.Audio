using System.ComponentModel;
using Microsoft.Maui.Dispatching;
using Plugin.Maui.StreamingAudio;

namespace StreamingAudioPlayerSample.ViewModels;

public class MusicPlayerPageViewModel : BaseViewModel, IQueryAttributable, IDisposable
{
	readonly IStreamingAudioManager streamingAudioManager;
	readonly IDispatcher dispatcher;
	IStreamingAudioPlayer streamingAudioPlayer;
	TimeSpan animationProgress;
	MusicItemViewModel musicItemViewModel;
	bool isPositionChangeSystemDriven;
	bool isDisposed;

	public MusicPlayerPageViewModel(
		IStreamingAudioManager streamingAudioManager,
		IDispatcher dispatcher)
	{
		this.streamingAudioManager = streamingAudioManager;
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

			streamingAudioPlayer = streamingAudioManager.CreatePlayerFromUri( musicItem.Source);

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

	public bool HasAudioSource => streamingAudioPlayer is not null;

	public bool IsPlaying => streamingAudioPlayer?.IsPlaying ?? false;

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
		get => streamingAudioPlayer?.Volume ?? 1;
		set
		{
			if (streamingAudioPlayer != null)
			{
				streamingAudioPlayer.Volume = value;
			}
		}
	}

	void Play()
	{
		streamingAudioPlayer.Play();

		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Pause()
	{
		if (streamingAudioPlayer.IsPlaying)
		{
			streamingAudioPlayer.Pause();
		}
		else
		{
			streamingAudioPlayer.Play();
		}
	
		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Stop()
	{
		if (streamingAudioPlayer.IsPlaying)
		{
			streamingAudioPlayer.Stop();

			AnimationProgress = TimeSpan.Zero;

			NotifyPropertyChanged(nameof(IsPlaying));
		}
	}

	public void TidyUp()
	{
		//StreamingAudioPlayer?.Dispose();
		//StreamingAudioPlayer = null;
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
