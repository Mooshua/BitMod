using BattleBitAPI.Server;

using BitMod.Compatibility;

using Serilog;

namespace BitMod.Commands.Sources.Internal;

public class InGameSource : ICommandSource
{
	private ILogger _logger;
	private BitPlayer _player;
	private BitServer _server;

	public InGameSource(BitServer server, BitPlayer player, ILogger logger)
	{
		_player = player;
		_logger = logger;
		_server = server;
	}

	public bool IsRemote => false;

	public bool IsAuthenticated => true;

	public bool IsAssociatedWithGameServer => true;

	public ulong Steam64 => _player.SteamID;

	public BitServer? GameServer => _server;

	public BitPlayer? Player => _player;

	public void Reply(string message)
	{
		_logger.Information("Replying to {@Player} ({@SteamID}) with message {@Message}",
			_player.Name,
			_player.SteamID,
			message);

		_player.Message(message);
	}
}
