using BitMod.Configuration.Model;

namespace BitMod.Config.Extensions;

public static class ConfigSymbolExtensions
{
	public static long Int(this IConfigSymbol symbol, long fallback, long min = long.MinValue, long max = long.MaxValue)
	{
		if (symbol == null)
			return fallback;

		if (!symbol.AsInt.HasValue)
			return fallback;

		if (symbol.AsInt.Value <= min || max <= symbol.AsInt.Value)
			return fallback;

		return symbol.AsInt.Value;
	}

	public static double Float(this IConfigSymbol symbol, double fallback, double min = double.MinValue, double max = double.MaxValue)
	{
		if (symbol == null)
			return fallback;

		if (!symbol.AsFloat.HasValue)
			return fallback;

		if (symbol.AsFloat.Value <= min || max <= symbol.AsFloat.Value)
			return fallback;

		return symbol.AsFloat.Value;
	}

	public static TimeSpan AsSeconds(this IConfigSymbol symbol, double fallback, double min = double.MinValue, double max = double.MaxValue)
		=> TimeSpan.FromSeconds(Float(symbol, fallback, min, max));

	public static ushort AsPort(this IConfigSymbol symbol, ushort fallback)
		=> (ushort)symbol.Int(fallback, 0, ushort.MaxValue);
}
