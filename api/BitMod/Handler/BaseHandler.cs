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
		_server.LogLevel |= LogLevel.All;

		_server.OnGameServerConnecting += OnGameServerConnecting;
		_server.OnCreatingGameServerInstance += OnCreatingGameServerInstance;
		_server.OnCreatingPlayerInstance += OnCreatingPlayerInstance;

		_server.OnLog += OnLog;
	}

	public void Dispose()
	{
		_server.OnGameServerConnecting -= OnGameServerConnecting;
		_server.OnCreatingGameServerInstance -= OnCreatingGameServerInstance;
		_server.OnCreatingPlayerInstance -= OnCreatingPlayerInstance;

		_server.OnLog -= OnLog;
	}

	public abstract Task<bool> OnGameServerConnecting(IPAddress arg);

	public abstract TGameServer OnCreatingGameServerInstance(IPAddress address, ushort port);

	public abstract TPlayer OnCreatingPlayerInstance(ulong steamId);

	public abstract void OnLog(LogLevel name, string value, object? sender);

}
