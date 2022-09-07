# Plugin.Maui.StreamingAudio

`Plugin.Maui.StreamingAudio` provides the ability to stream audio inside a .NET MAUI application from supported URIs.

## Getting Started

* Not available on NuGet (yet)

## API Usage

`Plugin.Maui.StreamingAudio` provides the `StreamingAudioManager` class that allows for the creation of `StreamingAudioPlayer`s. The `StreamingAudioManager` can be used with or without dependency injection.

### `StreamingAudioManager`

#### Dependency Injection

You will first need to register the `StreamingAudioManager` with the `MauiAppBuilder` following the same pattern that the .NET MAUI Essentials libraries follow.

```csharp
builder.Services.AddSingleton(AudioManager.Current);
```

You can then enable your classes to depend on `IAudioManager` as per the following example.

```csharp
public class StreamingAudioPlayerViewModel
{
    readonly IStreamingAudioManager audioManager;

    public StreamingAudioPlayerViewModel(IStreamingAudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    public async void PlayAudio()
    {
        var audioPlayer = audioManager.CreatePlayerFromUri(source);

        audioPlayer.Play();
    }
}
```

#### Straight usage

Alternatively if you want to skip using the dependency injection approach you can use the `AudioManager.Current` property.

```csharp
public class StreamingAudioPlayerViewModel
{
    public async void PlayAudio()
    {
        var audioPlayer = StreamingAudioManager.Current.CreatePlayerFromUri(source);

        audioPlayer.Play();
    }
}
```

### StreamingAudioPlayer

Once you have created a `StreamingAudioPlayer` you can interact with it in the following ways:

#### Events

##### `PlaybackEnded`

Raised when audio playback completes successfully.

#### Properties

##### `Balance`

Gets or sets the balance left/right: -1 is 100% left : 0% right, 1 is 100% right : 0% left, 0 is equal volume left/right.

##### `IsPlaying`

Gets a value indicating whether the currently loaded audio file is playing.

##### `Volume`

Gets or sets whether the player will continuously repeat the currently playing sound.

#### Methods

##### `Pause()`

Pause playback if playing (does not resume).

##### `Play()`

Begin playback or resume if paused.

##### `Stop()`

Stop playback and set the current position to the beginning.

# Acknowledgements

This project could not have came to be without these projects and people, thank you! <3

## SimpleAudioPlayer for Xamarin

Basically this plugin, but then for Xamarin. We have been using this in our Xamarin projects with much joy and ease, so thank you so much [Adrian](https://github.com/adrianstevens) (and contributors!) for that. Find the original project [here](https://github.com/adrianstevens/Xamarin-Plugins/tree/main/SimpleAudioPlayer) where we have based our project on and evolved it from there.
