using BitMod.Commands.Handlers;
using BitMod.Commands.Router;
using BitMod.Router;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

namespace BitMod.Commands.Attributes;

public class BitCommandAttribute : LkTargetAttribute
{
	public BitCommandAttribute(string command, string help)
	{
		Command = command;
		Help = help;
	}

	public string Command { get; }

	public string Help { get; }

	public override bool IsTargetable<TUserContext>()
		=> typeof(TUserContext) == typeof(RouterContext);

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as RouterContext, mutator);

	public void Target(RouterContext context, LilikoiMutator mutator)
	{
		context.Register<CommandHandlerRegistry, string>(() => new CommandAssembler());
		context.Append<CommandHandlerRegistry, string>(mutator);

		mutator.Store(new CommandMetadata(Command));
	}
}
