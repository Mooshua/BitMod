using System.Net;

using BitMod.Configuration.Model;

namespace BitMod.Config.Implementation;

public class ConfigSymbol : IConfigSymbol
{
	public ConfigSymbol(string symbol)
	{
		Symbol = symbol;

		if (IPAddress.TryParse(symbol, out var ip))
			AsAddress = ip;

		if (float.TryParse(symbol, out var floating))
			AsFloat = floating;

		if (long.TryParse(symbol, out var integer))
			AsInt = integer;

		if (bool.TryParse(symbol, out var boolean))
			AsBoolean = boolean;
	}

	public string Symbol { get; private set; }

	public IPAddress? AsAddress { get; private set; }

	public long? AsInt { get; private set; }

	public double? AsFloat { get; private set; }

	public bool? AsBoolean { get; private set; }

	public override string ToString()
		=> Symbol;
}
