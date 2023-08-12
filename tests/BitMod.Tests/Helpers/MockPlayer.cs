using System.Net;
using System.Reflection;

using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Tests.Helpers;

public static class MockPlayer
{
	public static BitPlayer New()
	{
		//	God has abandoned us
		var server = new GameServer(null, default, (gameServer, resources, arg3) => Task.CompletedTask, IPAddress.Any,
			default, default, default, default, default, default, default, default,
			default, default, default, default);
		var player = new BitPlayer();

		player.GetType().GetProperty(nameof(player.GameServer), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			.SetValue(player, server);

		return player;
	}
}
