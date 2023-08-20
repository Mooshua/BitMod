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

		/*var internalBlock = player
			.GetType()
			.GetField("mInternal",
				BindingFlags.Instance | BindingFlags.NonPublic);

		internalBlock.FieldType
			.GetField("GameServer", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			.SetValue( internalBlock.GetValue(player), server );
		*/

		return (player, server);
	}
}
