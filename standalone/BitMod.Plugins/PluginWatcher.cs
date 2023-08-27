using System.IO;
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
			new Plugin( Path.GetFileName( directory ), _logger, _parent );
	}

	public void OnRemovePlugin(object source, FileSystemEventArgs ev)
	{
		_logger.Warning("[BitMod Plugin Watcher] Unloading removed plugin {@Name}", ev.Name);
		_parent.Deleted(ev.Name);
	}

	public void OnNewPlugin(object source, FileSystemEventArgs ev)
	{
		_logger.Information("[BitMod Plugin Watcher] Loading new plugin {@Name}", ev.Name);

		new Plugin(ev.Name, _logger, _parent);
	}


}
