using BitMod.Commands.Sources;
using BitMod.Compatibility;
using BitMod.Internal.Public;
using BitMod.Moderation.Events;

namespace BitMod.Moderation.Extensions;

public static class BitPlayerExtensions
{
	public static void Ban(this BitPlayer player, PluginInvoker invoker, ICommandSource sender, string reason, TimeSpan duration)
		=> invoker.Event(new PlayerBannedEventArgs(player.SteamID, player.Name, duration, sender, reason));
}
