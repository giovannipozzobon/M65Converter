﻿using M65Converter.Sources.Data.Intermediate.Containers;
using M65Converter.Sources.Data.Intermediate.Helpers;
using M65Converter.Sources.Data.Providers;

using UnitTests.Models;

namespace UnitTests.Creators.Outputs;

public class OutputScreenDataCreator : BaseOutputDataCreator
{
	protected override IStreamProvider? OnGetActualStream(DataContainer.OutputStreams outputs)
	{
		// We only produce single screen data in unit tests, except when no screen output is expected.
		return IsScreensRunnerEnabled
			? outputs.ScreenScreenDataStreams[0]
			: MemoryStreamProvider.Empty("export-screen.bin");
	}

	protected override IStreamProvider? OnGetExpectedStream()
	{
		return new MemoryStreamProvider
		{
			Data = (ColourMode, IsCharsInputUsed, IsScreensInputUsed, IsRRBEnabled) switch
			{
				(CharColourMode.NCM, false, true, false) => Resources.export_ncm_screens_screen,
				(CharColourMode.NCM, false, true, true) => Resources.export_ncm_screens_rrb_screen,
				(CharColourMode.NCM, true, true, false) => Resources.export_ncm_base_screens_screen,
				(CharColourMode.NCM, true, true, true) => Resources.export_ncm_base_screens_rrb_screen,

				(CharColourMode.FCM, false, true, false) => Resources.export_fcm_screens_screen,
				(CharColourMode.FCM, false, true, true) => Resources.export_fcm_screens_rrb_screen,
				(CharColourMode.FCM, true, true, false) => Resources.export_fcm_base_screens_screen,
				(CharColourMode.FCM, true, true, true) => Resources.export_fcm_base_screens_rrb_screen,

				_ => Array.Empty<byte>()
			},

			Filename = "export-screen.bin"
		};
	}
}
