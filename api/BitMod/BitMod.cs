using BitMod.Compatibility;
using BitMod.Handler;
using BitMod.Internal;
using BitMod.Internal.Public;
using BitMod.Public;

using CommunityServerAPI.BattleBitAPI.Server;

using Lilikoi.Context;

using Serilog;

namespace BitMod;

public sealed class BitMod : Mount
{
	public BitMod(ILogger logger, IConfigurationSystem config, IPluginSystem plugins)
	{
		_server = new ServerListener<BitPlayer>();

		var context = new PluginContext( logger, this );
		var invoker = new PluginInvoker( context );
		var listener = new RoutingHandler( _server, invoker);

		Store(context);
		Store(invoker);
		Store(listener);

		Store(logger);
		Store(plugins);
		Store(config);
	}

	private ServerListener<BitPlayer> _server;

	public ILogger Logger => Get<ILogger>();

	public IConfigurationSystem Config => Get<IConfigurationSystem>()!;

	public IPluginSystem Plugins => Get<IPluginSystem>()!;

	/// <summary>
	/// Start the BitMod system
	/// </summary>
	public void Start()
	{
		var network = Config.Get<ListenerConfig>(() => new ListenerConfig());
		_server.Start(network.Address, network.Port);
	}

	public void Stop()
	{

	}

}
