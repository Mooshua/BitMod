namespace BitMod.Commands.Handlers;

public class CommandArg
{
	public CommandArg(string asString)
	{
		AsString = asString;
	}

	public string AsString { get; }

	public long AsInt(long @default = -1)
	{
		if (long.TryParse(AsString, out var parsed))
			return parsed;

		return @default;
	}

	public double AsFloat(double @default = -1)
	{
		if (double.TryParse(AsString, out var parsed))
			return parsed;

		return @default;
	}
}
