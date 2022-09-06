using System;

namespace Plugin.Maui.StreamingAudio;

/// <summary>
/// Provides the ability to create <see cref="IStreamingAudioPlayer" /> instances.
/// </summary>
public interface IStreamingAudioManager
{
	/// <summary>
	/// ;tbd
	/// </summary>
	/// <param name="audioStream"></param>
	/// <returns></returns>
	IStreamingAudioPlayer CreatePlayerFromUri(string uri) => new StreamingAudioPlayer(new System.Uri(uri));
}
