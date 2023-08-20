using BitMod.Commands.Handlers;
using BitMod.Commands.Sources;
using BitMod.Internal;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Commands.Attributes;

public class BitCommandRequireAttribute : LkTypedWrapAttribute<EventInput, Task>
{
	public bool InGame { get; set; } = false;

	public bool HasServer { get; set; } = false;

	public bool IsLoggedIn { get; set; } = false;

	public BitCommandRequireAttribute()
	{
	}

	private WrapResult<Task> Reject(ICommandSource sender, string reason)
	{
		sender.Reply($"[BitMod] Cannot run command: {reason}");
		return WrapResult<Task>.Stop(Task.CompletedTask);
	}

	public override WrapResult<Task> Before(Mount mount, ref EventInput input)
	{
		var command = input.Get<CommandInput>();
		var sender = command.Sender;

		if (InGame && sender.IsRemote)
			return Reject(sender, "You must be in game to use this command!");

		if (IsLoggedIn && !sender.IsAuthenticated)
			return Reject(sender, "You must have an associated steamid to use this command!");

		if (HasServer && !sender.IsAssociatedWithGameServer)
			return Reject(sender, "You must associate this command with a gameserver!");

		return WrapResult<Task>.Continue();
	}

	public override void After(Mount mount, ref Task output)
	{

	}
}
