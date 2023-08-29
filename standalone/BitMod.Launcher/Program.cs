using BitMod.Config;
using BitMod.Events.Core;
using BitMod.Logging;
using BitMod.Plugins;

public static class Program
{
	public static void Main()
	{
		var configSystem = new ConfigurationSystem();
		var logging = new LoggingSystem(configSystem);
		var pluginSystem = new PluginSystem();

		var log = logging.GetLogger();

		log.Information("Launching BitMod...");

		var bitmod = new BitMod.BitMod(log, configSystem, pluginSystem);

		bitmod.Start();

		while (true)
		{
			var command = Console.ReadLine();

			if (command != null)
				bitmod.Invoker.Event(new StandardInputEventArgs(command));
		}
	}
}
