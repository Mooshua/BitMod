#region

using System;
using System.Net;
using System.Threading.Tasks;

using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;

using BitMod.Compatibility;

#endregion

namespace BitMod.Handler;

public abstract class BaseHandler<TPlayer, TGameServer> : IDisposable
	where TPlayer : Player<TPlayer>
	where TGameServer: GameServer<TPlayer>
{
	protected ServerListener<TPlayer, TGameServer> _server;

	public BaseHandler(ServerListener<TPlayer, TGameServer> server)
	{
		_server = server;

		_server.OnGameServerConnecting += OnGameServerConnecting;
	}

	public void Dispose()
	{

		_server.OnGameServerConnecting -= OnGameServerConnecting;
	}

	public abstract Task<bool> OnGameServerConnecting(IPAddress arg);
	
}
