using AVFoundation;
using Foundation;

namespace Plugin.Maui.StreamingAudio;

partial class StreamingAudioPlayer : IStreamingAudioPlayer
{
    readonly AVPlayer player;
    bool isDisposed;

    public double Volume
    {
        get => player.Volume;
        set => player.Volume = (float)Math.Clamp(value, 0, 1);
    }

	public bool IsPlaying => player.TimeControlStatus == AVPlayerTimeControlStatus.Playing;

	internal StreamingAudioPlayer(System.Uri audioStream)
	{
		player = AVPlayer.FromUrl(new NSUrl(audioStream.AbsoluteUri))
		   ?? throw new FailedToLoadAudioException("Unable to create AVStreamingAudioPlayer from url.");

		PreparePlayer();
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
            player.Dispose();
        }

        isDisposed = true;
    }

    public void Pause() => player.Pause();

    public void Play()
    {
        if (!IsPlaying)       
        {
            player.Play();
        }
    }

    public void Stop()
    {
        player.Pause();
      
        PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }

    bool PreparePlayer()
    {
        return true;
    }
}
