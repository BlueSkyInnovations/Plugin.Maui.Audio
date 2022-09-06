using Android.Content;
using Android.Content.Res;
using Android.Media;
using Stream = System.IO.Stream;
using Uri = Android.Net.Uri;
using AndroidNet = Android.Net;

namespace Plugin.Maui.Audio;

partial class AudioPlayer : IAudioPlayer
{
    readonly MediaPlayer player;
    static int index = 0;
    double volume = 0.5;
    double balance = 0;
    string path = string.Empty;
    bool isDisposed = false;

    public double Volume
    {
        get => volume;
        set => player.SetVolume((float)Math.Clamp(volume, 0, 1), (float)Math.Clamp(volume, 0, 1));
    }

    public bool IsPlaying => player.IsPlaying;

	internal AudioPlayer(System.Uri uri)
	{
		player = new MediaPlayer();
		player.Completion += OnPlaybackEnded;
				
		player.SetDataSource(Android.App.Application.Context, Uri.Parse(uri.AbsoluteUri));

		player.Prepare();
	}

    public void Play()
    {
        if (IsPlaying)
        {
            Pause();          
        }

        player.Start();
    }

    public void Stop()
    {
        if (!IsPlaying)
        {
            return;
        }

        Pause();
		
        PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }

    public void Pause()
    {
        player.Pause();
    }

    void OnPlaybackEnded(object? sender, EventArgs e)
    {
        PlaybackEnded?.Invoke(this, e);

        //this improves stability on older devices but has minor performance impact
        // We need to check whether the player is null or not as the user might have dipsosed it in an event handler to PlaybackEnded above.
        if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.M)
        {           
            player.Stop();
            player.Prepare();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
        {
            return;
        }

        if (disposing)
        {
            player.Completion -= OnPlaybackEnded;
            player.Release();
            player.Dispose();            
        }

        isDisposed = true;
    }
}
