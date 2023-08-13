using System.Reflection;

using BitMod.Commands.Handlers;
using BitMod.Internal;
using BitMod.Internal.Public;

namespace BitMod.Commands.Extensions;

public static class PluginInvokerExtensions
{

	/// <summary>
	/// Invoke the command handler for the specified command
	/// </summary>
	/// <param name="self"></param>
	/// <param name="command"></param>
	public static void Command(this PluginInvoker self, CommandInput command)
	{
		var chain = self.Context.Get<CommandHandlerRegistry, string>(command.Command);
		chain?.Invoke( EventInput.From( command ) );
	}
}
