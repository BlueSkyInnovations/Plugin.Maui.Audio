namespace Plugin.Maui.StreamingAudio;

public partial class StreamingAudioPlayer : IStreamingAudioPlayer
{
    public event EventHandler? PlaybackEnded;

    ~StreamingAudioPlayer()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }
}
