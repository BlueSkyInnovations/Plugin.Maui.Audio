namespace Plugin.Maui.StreamingAudio;

public class StreamingAudioManager : IStreamingAudioManager
{
    static IStreamingAudioManager? currentImplementation;

    public static IStreamingAudioManager Current => currentImplementation ??= new StreamingAudioManager();

	public IStreamingAudioPlayer CreatePlayer(Uri audioStream)
	{
		ArgumentNullException.ThrowIfNull(audioStream);

		return new StreamingAudioPlayer(audioStream);
	}
}
