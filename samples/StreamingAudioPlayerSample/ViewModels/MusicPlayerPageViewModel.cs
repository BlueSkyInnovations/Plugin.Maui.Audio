using System.ComponentModel;
using Microsoft.Maui.Dispatching;
using Plugin.Maui.StreamingAudio;

namespace StreamingAudioPlayerSample.ViewModels;

public class MusicPlayerPageViewModel : BaseViewModel, IQueryAttributable, IDisposable
{
	readonly IStreamingAudioManager StreamingAudioManager;
	readonly IDispatcher dispatcher;
	IStreamingAudioPlayer StreamingAudioPlayer;
	TimeSpan animationProgress;
	MusicItemViewModel musicItemViewModel;
	bool isPositionChangeSystemDriven;
	bool isDisposed;

	public MusicPlayerPageViewModel(
		IStreamingAudioManager StreamingAudioManager,
		IDispatcher dispatcher)
	{
		this.StreamingAudioManager = StreamingAudioManager;
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

			StreamingAudioPlayer = StreamingAudioManager.CreatePlayerFromUri( musicItem.Source);

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

	public bool HasAudioSource => StreamingAudioPlayer is not null;

	public bool IsPlaying => StreamingAudioPlayer?.IsPlaying ?? false;

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
		get => StreamingAudioPlayer?.Volume ?? 1;
		set
		{
			if (StreamingAudioPlayer != null)
			{
				StreamingAudioPlayer.Volume = value;
			}
		}
	}

	void Play()
	{
		StreamingAudioPlayer.Play();

		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Pause()
	{
		if (StreamingAudioPlayer.IsPlaying)
		{
			StreamingAudioPlayer.Pause();
		}
		else
		{
			StreamingAudioPlayer.Play();
		}
	
		NotifyPropertyChanged(nameof(IsPlaying));
	}

	void Stop()
	{
		if (StreamingAudioPlayer.IsPlaying)
		{
			StreamingAudioPlayer.Stop();

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
