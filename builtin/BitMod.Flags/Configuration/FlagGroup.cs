namespace BitMod.Flags.Configuration;

public class FlagGroup
{
	private Dictionary<string, FlagEntry> _entries = new();

	private FlagEntry? TryGet(string key)
	{
		if (_entries.TryGetValue(key, out var match))
			return match;
		return null;
	}

	internal void Add(string key, FlagEntry entry)
	{
		_entries[key] = entry;
	}

	public bool GetBool(string key, bool fallback)
		=> TryGet(key)?.AsBool ?? fallback;

	public double GetFloat(string key, double fallback)
		=> TryGet(key)?.AsFloat ?? fallback;

	public long GetInt(string key, long fallback)
		=> TryGet(key)?.AsLong ?? fallback;

	public string GetString(string key, string fallback)
		=> TryGet(key)?.AsString ?? fallback;

	public FlagEntry? Get(string key)
		=> TryGet(key);
}
