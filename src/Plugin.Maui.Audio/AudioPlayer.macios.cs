using System;
using AVFoundation;
using Foundation;
using GameKit;
using Microsoft.Maui.Storage;

namespace Plugin.Maui.Audio;

partial class AudioPlayer : IAudioPlayer
{
    readonly AVPlayer player;
    bool isDisposed;

    public double Volume
    {
        get => player.Volume;
        set => player.Volume = (float)Math.Clamp(value, 0, 1);
    }

	public bool IsPlaying => player.TimeControlStatus == AVPlayerTimeControlStatus.Playing;

	internal AudioPlayer(System.Uri audioStream)
	{
		player = AVPlayer.FromUrl(new NSUrl(audioStream.AbsoluteUri))
		   ?? throw new FailedToLoadAudioException("Unable to create AVAudioPlayer from url.");

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
