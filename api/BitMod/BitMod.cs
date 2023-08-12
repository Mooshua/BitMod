using BattleBitAPI.Server;

using BitMod.Compatibility;
using BitMod.Handler;
using BitMod.Internal;
using BitMod.Internal.Public;
using BitMod.Public;

using Lilikoi.Context;

using Serilog;

namespace BitMod;

public sealed class BitMod : Mount
{
	public BitMod(ILogger logger, IConfigurationSystem config, IPluginSystem plugins)
	{
		Store(logger);
		Store(plugins);
		Store(config);

		_server = new ServerListener<BitPlayer>();

		var context = new PluginContext( logger, this );
		var invoker = new PluginInvoker( context );
		var listener = new RoutingHandler( _server, invoker);

		Store(context);
		Store(invoker);
		Store(listener);

		context.Load("bitmod:internal:pluginloader", typeof(Bootstrap));

		config.Start(this);
		plugins.Start(this);

		logger.Information("[BitMod] Loaded {@PluginCount} plugins and {@ExtCount} extensions from plugin system",
			plugins.Plugins.Count,
			plugins.Extensions.Count);
	}

	private ServerListener<BitPlayer> _server;

	public RoutingHandler Handler => Get<RoutingHandler>()!;

	public PluginInvoker Invoker => Get<PluginInvoker>()!;

	public PluginContext Context => Get<PluginContext>()!;

	public ILogger Logger => Get<ILogger>()!;

	public IConfigurationSystem Config => Get<IConfigurationSystem>()!;

	public IPluginSystem Plugins => Get<IPluginSystem>()!;

	/// <summary>
	/// Start the BitMod system
	/// </summary>
	public void Start()
	{
		var network = Config.Get("core", () => BitModConfig.Default()).Listener;
		Logger.Information("[BitMod] Starting server on {@IP}:{@Port}", network.PublicIP, network.Port);
		_server.Start(network.GetAddress(), network.Port);
	}

	public void Stop()
	{

	}

}
