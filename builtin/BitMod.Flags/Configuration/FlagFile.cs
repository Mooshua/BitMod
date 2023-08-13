using Serilog;

namespace BitMod.Flags.Configuration;

public class FlagFile
{
	public Dictionary<string, FlagGroup> Groups { get; } = new ();

	private ILogger _logger;

	public FlagFile(Dictionary<string, FlagFileEntry> file, ILogger logger)
	{
		_logger = logger;
		foreach (var (key, _) in file)
			Groups[key] = DoGroup(file, key);
	}

	private void Dive(Dictionary<string, FlagFileEntry> file, string[] visited, FlagFileEntry current, FlagGroup applyTo)
	{
		if (current.Inherit != null)
		{
			foreach (string inheritFrom in current.Inherit)
			{
				if (visited.Contains(inheritFrom))
					continue;

				if (!file.ContainsKey(inheritFrom))
				{
					_logger.Warning("Warning: Group {@GroupName} inherits from {@InheritName} which does not exist.", visited[visited.Length - 1], inheritFrom);
					continue;
				}

				var newVisited = visited.Append(inheritFrom).ToArray();
				Dive(file, newVisited, file[inheritFrom], applyTo);
			}
		}

		if (current.Set != null)
		{
			foreach (var (key, value) in current.Set)
			{
				applyTo.Add(key, new FlagEntry(value));
			}
		}
	}

	private FlagGroup DoGroup(Dictionary<string, FlagFileEntry> file, string group)
	{
		var entry = file[group];
		FlagGroup result = new FlagGroup();

		//	Apply default values first
		if (file.ContainsKey("#default"))
			Dive(file, new[] { group, "#default"}, file["#default"], result );

		Dive(file, new [] {group}, entry, result);

		return result;
	}

	public FlagGroup Get(string key)
	{
		if (Groups.TryGetValue(key, out var result))
			return result;

		if (Groups.TryGetValue("#default", out var def))
			return def;

		return new FlagGroup();
	}
}
