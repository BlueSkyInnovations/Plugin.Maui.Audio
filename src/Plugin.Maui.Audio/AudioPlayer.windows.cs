using Windows.Media.Core;
using Windows.Media.Playback;

namespace Plugin.Maui.Audio;

partial class AudioPlayer : IAudioPlayer
{
	bool isDisposed = false;
	readonly MediaPlayer player;

	public double Volume
	{
		get => player.Volume;
		set => player.Volume = Math.Clamp(value, 0, 1);
	}


	public bool IsPlaying =>
		player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing; //might need to expand


	public AudioPlayer(System.Uri audioStream)
	{
		player = CreatePlayer();

		if (player is null)
		{
			throw new FailedToLoadAudioException($"Failed to create {nameof(MediaPlayer)} instance. Reason unknown.");
		}

		player.Source = MediaSource.CreateFromUri(new Uri(audioStream.AbsoluteUri ));
		player.AudioCategory = MediaPlayerAudioCategory.Media;
		player.MediaEnded += OnPlaybackEnded;
	}

	void OnPlaybackEnded(MediaPlayer sender, object args)
	{
		PlaybackEnded?.Invoke(sender, EventArgs.Empty);
	}

	public void Play()
	{
		if (player.Source is null)
		{
			return;
		}

		if (player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
		{
			Pause();		
		}

		player.Play();
	}

	public void Pause()
	{
		player.Pause();
	}

	public void Stop()
	{
		Pause();		
		PlaybackEnded?.Invoke(this, EventArgs.Empty);
	}

	MediaPlayer CreatePlayer()
	{
		return new MediaPlayer() { AutoPlay = false, IsLoopingEnabled = false };
	}

	protected virtual void Dispose(bool disposing)
	{
		if (isDisposed)
		{
			return;
		}

		if (disposing)
		{
			Stop();

			player.MediaEnded -= OnPlaybackEnded;
			player.Dispose();
		}

		isDisposed = true;
	}
}
