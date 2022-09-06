using System;

namespace Plugin.Maui.Audio;

/// <summary>
/// Provides the ability to create <see cref="IAudioPlayer" /> instances.
/// </summary>
public interface IAudioManager
{
	/// <summary>
	/// ;tbd
	/// </summary>
	/// <param name="audioStream"></param>
	/// <returns></returns>
	IAudioPlayer CreatePlayerFromUri(string uri) => new AudioPlayer(new System.Uri(uri));
}
