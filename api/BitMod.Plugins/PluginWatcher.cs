using System.Reflection;

using McMaster.NETCore.Plugins;

using Serilog;

namespace BitMod.Plugins;

public class PluginWatcher
{
	private ILogger _logger;
	private PluginSystem _parent;
	private FileSystemWatcher _watcher;

	public const string PLUGIN_PATH = "plugins";

	public PluginWatcher(PluginSystem parent, ILogger logger)
	{
		_parent = parent;
		_logger = logger;

		_watcher = new FileSystemWatcher();
		_watcher.Path = Path.Join(System.Environment.CurrentDirectory, PLUGIN_PATH);
		_watcher.NotifyFilter = NotifyFilters.DirectoryName;
		_watcher.Filter = "*.*";
		_watcher.Created += OnNewPlugin;
		_watcher.Deleted += OnRemovePlugin;
		_watcher.EnableRaisingEvents = true;

		_logger.Information("[BitMod Plugin Watcher] Started watching directory {@Path}", _watcher.Path);

		foreach (string directory in Directory.GetDirectories(PLUGIN_PATH))
			LoadPlugin( Path.GetFileName( directory ) );
	}

	public void OnRemovePlugin(object source, FileSystemEventArgs ev)
	{
		_logger.Warning("[BitMod Plugin Watcher] Unloading removed plugin {@Name}", ev.Name);
	}

	private string? GetPluginFile(string directory)
	{
		var path = Path.Join(System.Environment.CurrentDirectory, PLUGIN_PATH, directory);
		var matches = Directory.GetFiles(path, "*.dll");

		if (matches.Length == 1)
			return matches[0];

		if (matches.Length == 0)
			_logger.Error("[BitMod Plugin] Error loading plugin {@Name}: No .dll files in directory!", directory);
		else
		{
			if (matches.Contains($"{directory}.dll"))
				return Path.Join(path, $"{directory}.dll");

			_logger.Error("[BitMod Plugin] Error loading plugin {@Name}: Multiple .dll files found--but did not find {@Directory}.dll. Instead found: {@Matches}", directory, directory, matches);
		}

		return null;
	}

	public void OnNewPlugin(object source, FileSystemEventArgs ev)
	{
		_logger.Information("[BitMod Plugin Watcher] Loading new plugin {@Name}", ev.Name);

		LoadPlugin(ev.Name);
	}

	public void LoadPlugin(string directory)
	{
		var file = GetPluginFile(directory);
		if (file == null)
			return;

		_logger.Information("{@File}", file);

		var loader = PluginLoader.CreateFromAssemblyFile(file, (config) =>
		{
			config.EnableHotReload = true;
			//config.IsUnloadable = true;
			//config.LoadInMemory = true;
			//config.PreferSharedTypes = true;
		});

		loader.Reloaded += (sender, args) =>
		{
			_logger.Information("Hot Reloading {@Name}", directory);
			_parent.Unload(directory);
			_parent.Load(directory, args.Loader.LoadDefaultAssembly(), args.Loader);
		};

		var assembly = loader.LoadDefaultAssembly();

		_parent.Load(directory, assembly, loader);
	}
}
