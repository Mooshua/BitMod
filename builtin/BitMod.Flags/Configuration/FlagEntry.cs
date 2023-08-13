namespace BitMod.Flags.Configuration;

public class FlagEntry
{
	public FlagEntry( string value)
	{
		AsString = value;

		if (Boolean.TryParse(value, out var boolValue))
			AsBool = boolValue;

		if (double.TryParse(value, out var floatValue))
			AsFloat = floatValue;

		if (long.TryParse(value, out var intValue))
			AsLong = intValue;
	}

	public bool AsBool { get; }

	public string AsString { get; }

	public long AsLong { get; }

	public double AsFloat { get; }
}
