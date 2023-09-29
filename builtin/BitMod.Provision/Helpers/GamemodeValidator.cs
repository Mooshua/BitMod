using Serilog;

namespace BitMod.Provision.Helpers;

public class GamemodeValidator
{
	public static IEnumerable<string> Known = new[]
	{
		//	infantry conquest
		"INFCONQ",
		//	rush (bomb defusal)
		"RUSH",
		//	conquest
		"CONQ",
		//	team deathmatch
		"TDM",
		//	domination
		"DOMI",
		"FRONTLINE",
		"Elimination",
		"CaptureTheFlag",
		"CashRun",
		"VoxelTrench",
		"VoxelFortify",

	};

	public static void ValidateGamemodes(ILogger logger, IEnumerable<string> chosen)
	{
		var notInKnown = chosen
			.Where(opt => !Known.Contains(opt));

		foreach (string s in notInKnown)
			logger.Warning("[BitMod Provision] Gamemode {@Gamemode} is not known to BitMod, it may be invalid! Check your spelling.", s);
	}
}
