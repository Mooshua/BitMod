using System;
using System.IO;
using System.Linq;
using System.Reflection;

using BitMod.Public;

using McMaster.NETCore.Plugins;

using Serilog;

namespace BitMod.Plugins;

public class Plugin : IPluginSystem.IPlugin
{

	private string _pluginName;
	private ILogger _logger;
	private PluginSystem _parent;

	private FileSystemWatcher _watcher;

	public Plugin(string folder, ILogger logger, PluginSystem parent)
	{
		_pluginName = folder;
		_logger = logger;
		_parent = parent;

		_watcher = new FileSystemWatcher();
		_watcher.Path = Path.Join(System.Environment.CurrentDirectory, PluginWatcher.PLUGIN_PATH, _pluginName);
		_watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.CreationTime;
		_watcher.Filter = "*.dll";
		_watcher.Created += OnChange;
		_watcher.Deleted += OnChange;
		_watcher.Renamed += OnChange;
		_watcher.Changed += OnChange;
		_watcher.Error += OnError;
		_watcher.EnableRaisingEvents = true;

		LoadPlugin();
	}

	private void OnError(object sender, ErrorEventArgs errorEventArgs)
	{
		_logger.Warning(errorEventArgs.GetException(), "[BitMod Plugins] Error in {@PluginName}'s filesystem watcher:", _pluginName);
	}

	private void OnChange(object source, FileSystemEventArgs ev)
	{
		_logger.Information("[BitMod Plugins] Hot reloading plugin {@PluginName} due to changed file {@File}", _pluginName, ev.Name);
		UnloadPlugin();
		LoadPlugin();


	}



	private string? GetPluginFile()
	{
		var path = Path.Join(System.Environment.CurrentDirectory, PluginWatcher.PLUGIN_PATH, _pluginName);
		var matches = Directory.GetFiles(path, "*.dll");

		if (matches.Length == 1)
			return matches[0];

		if (matches.Length == 0)
			_logger.Error("[BitMod Plugins] Error loading plugin {@Name}: No .dll files in directory!", _pluginName);
		else
		{
			if (matches.Select(match => Path.GetFileName(match)).Contains($"{_pluginName}.dll"))
				return Path.Join(path, $"{_pluginName}.dll");

			_logger.Error("[BitMod Plugins] Error loading plugin {@Name}: Multiple .dll files found--but did not find {@Directory}.dll. Instead found: {@Matches}", _pluginName, _pluginName, matches);
		}

		return null;
	}

	public void LoadPlugin()
	{
		try
		{
			var file = GetPluginFile();
			if (file == null)
				return;

			Loader = PluginLoader.CreateFromAssemblyFile(file, (config) =>
			{
				config.IsUnloadable = true;
				config.LoadInMemory = true;
				config.PreferSharedTypes = true;
			});

			Assembly = Loader.LoadDefaultAssembly();

			_parent.Load(this);
		}
		catch (Exception e)
		{
			_logger.Error(e, "[BitMod Plugins] Error loading plugin {@Name}", _pluginName);
		}

	}

	public void UnloadPlugin()
	{
		try
		{
			_parent.Unload(this);
			Loader.Dispose();
		}
		catch (Exception e)
		{
			_logger.Error(e, "[BitMod Plugins] Error unloading plugin {@Name}", _pluginName);
		}
	}

	public string Name => _pluginName;

	public Assembly Assembly { get; private set; }

	public PluginLoader Loader { get; private set; }
}
