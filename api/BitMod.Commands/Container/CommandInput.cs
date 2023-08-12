using BattleBitAPI.Server;

using BitMod.Commands.Sources;
using BitMod.Compatibility;
using BitMod.Events.Accessors;

using Lilikoi.Context;

namespace BitMod.Commands.Handlers;

public class CommandInput : Mount, IResponsiblePlayerAccessor, IRelevantGameserverAccessor
{
	public CommandInput(ICommandSource sender, string[] arguments)
	{
		Sender = sender;
		Arguments = arguments;
	}

	/// <summary>
	/// The source that sent this command
	/// </summary>
	public ICommandSource Sender { get; }

	public string[] Arguments { get; }

	public string Command { get; }

	/// <inheritdoc />
	public BitPlayer? ResponsiblePlayer => Sender.Player;

	/// <inheritdoc />
	public GameServer? RelevantGameserver => Sender.GameServer;
}
