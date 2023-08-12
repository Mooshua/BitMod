using System.Net;


using Serilog;

namespace BitMod.Whitelist.Configuration;

public class WhitelistFile
{
	public List<string> AllowedIPs { get; set; } = new List<string>();

	public IEnumerable<IPAddress> Parse(ILogger? logger)
	{
		foreach (string allowedIP in AllowedIPs)
		{
			if (IPAddress.TryParse(allowedIP, out var ip))
				yield return ip;
			else
				logger?.Warning("[BitMod Whitelist] Failed to parse {@IP} into IPAddress", allowedIP);
		}
	}
}
