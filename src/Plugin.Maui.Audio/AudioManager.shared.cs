namespace Plugin.Maui.Audio;

public class AudioManager : IAudioManager
{
    static IAudioManager? currentImplementation;

    public static IAudioManager Current => currentImplementation ??= new AudioManager();

	public IAudioPlayer CreatePlayer(Uri audioStream)
	{
		ArgumentNullException.ThrowIfNull(audioStream);

		return new AudioPlayer(audioStream);
	}
}
