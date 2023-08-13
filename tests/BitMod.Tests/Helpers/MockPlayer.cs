using System.Net;
using System.Reflection;

using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Tests.Helpers;

public static class MockPlayer
{
	public static (BitPlayer player, BitServer server) New()
	{
		var server = new BitServer();
		var player = new BitPlayer();

		player.GetType().GetProperty(nameof(player.GameServer), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			.SetValue(player, server);

		return (player, server);
	}
}
