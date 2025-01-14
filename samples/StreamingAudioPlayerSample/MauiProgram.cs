﻿using StreamingAudioPlayerSample.Pages;
using StreamingAudioPlayerSample.ViewModels;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Maui.StreamingAudio;
using SkiaSharp.Views.Maui.Controls.Hosting;

#if IOS
using AVFoundation;
#endif

namespace StreamingAudioPlayerSample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureLifecycleEvents(events =>
			{
#if IOS
				events.AddiOS(ios => ios
					.FinishedLaunching((app, options) =>
					{
						AVAudioSession session = AVAudioSession.SharedInstance();
						session.SetCategory(AVAudioSessionCategory.Playback);
						session.SetActive(true);

						return true;
					}));
#endif
			})
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
			


		builder.Services.AddTransient<MyLibraryPage>();
		builder.Services.AddTransient<MyLibraryPageViewModel>();

		builder.Services.AddTransient<MusicPlayerPage>();
		builder.Services.AddTransient<MusicPlayerPageViewModel>();

		Routing.RegisterRoute(Routes.MusicPlayer.RouteName, typeof(MusicPlayerPage));

		builder.Services.AddSingleton(StreamingAudioManager.Current);

		return builder.Build();
	}
}