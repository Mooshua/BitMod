// See https://aka.ms/new-console-template for more information


using BitMod.Config;
using BitMod.Logging;
using BitMod.Plugins;

var configSystem = new ConfigurationSystem();
var logging = new LoggingSystem(configSystem);
var pluginSystem = new PluginSystem();

var log = logging.GetLogger();

var bitmod = new BitMod.BitMod(log, configSystem, pluginSystem);

bitmod.Start();

ManualResetEvent close = new ManualResetEvent(false);

close.WaitOne();
