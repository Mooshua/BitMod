using Serilog;

namespace BitMod.Provision.Helpers;

public class MapcycleValidator
{
	public static IEnumerable<string> Known = new[]
	{

		"Azagor",
		"Basra",
		"Construction",
		"District",
		"Dustydew",
		"Eduardovo",
		"Frugis",
		"Isle",
		"Lonovo",
		"MultiIslands",
		"Namak",
		"OilDunes",
		"River",
		"Salhan",
		"SandySunset",
		"TensaTown",
		"Valley",
		"Wakistan",
		"WineParadise",

		//	old versions of maps
		"Old_District",
		"Old_Eduardovo",
		"Old_MultuIslands",
		"Old_Namak",
		"Old_OilDunes",

		//	not visible in polls
		"EventMap"
	};

	public static void ValidateMaps(ILogger logger, IEnumerable<string> chosen)
	{
		var notInKnown = chosen
			.Where(opt => !Known.Contains(opt));

		foreach (string s in notInKnown)
			logger.Warning("[BitMod Provision] Map {@Map} is not known to BitMod, it may be invalid! Check your spelling.", s);
	}
}
