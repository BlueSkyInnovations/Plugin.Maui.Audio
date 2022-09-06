namespace Plugin.Maui.StreamingAudio;

/// <summary>
/// Provides the ability to play audio.
/// </summary>
public interface IStreamingAudioPlayer : IDisposable
{
	///<Summary>
	/// Raised when audio playback completes successfully.
	///</Summary>
	event EventHandler PlaybackEnded;
	
	///<Summary>
	/// Gets or sets the playback volume 0 to 1 where 0 is no-sound and 1 is full volume.
	///</Summary>
	double Volume { get; set; }

	///<Summary>
	/// Gets a value indicating whether the currently loaded audio file is playing.
	///</Summary>
	bool IsPlaying { get; }

	///<Summary>
	/// Begin playback or resume if paused.
	///</Summary>
	void Play();

	///<Summary>
	/// Pause playback if playing (does not resume).
	///</Summary>
	void Pause();

	///<Summary>
	/// Stop playback and set the current position to the beginning.
	///</Summary>
	void Stop();
}