using BitMod.Attributes.Internal;
using BitMod.Compatibility;
using BitMod.Extensions.Commands.Lilikoi;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

namespace BitMod.Extensions.Commands.Attributes;

public class CommandAttribute : LkTargetAttribute
{
	public CommandAttribute(string command, string help, params string[] aliases)
	{
		Command = command;
		Help = help;
		Aliases = aliases;
	}

	public string Command { get; }

	public string Help { get; }

	public string[] Aliases { get; }

	public override bool IsTargetable<TUserContext>()
		=> typeof(TUserContext) == typeof(CommandContext);

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as CommandContext, mutator);

	internal void Target(CommandContext context, LilikoiMutator mutator)
	{
		mutator.Wildcard<BitPlayer>(new ImplicitCommandSenderAttribute());

		//	Handle async result types
		if (mutator.Result.IsSubclassOf(typeof(Task)) || mutator.Result == typeof(Task))
			mutator.Implicit(new AsyncAttribute());

		mutator.Store(new CommandDescription(Command, Help, Aliases));
	}
}
