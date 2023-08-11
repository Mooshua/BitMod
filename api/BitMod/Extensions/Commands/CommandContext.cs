using System.Reflection;
using System.Text.RegularExpressions;

using BitMod.Compatibility;
using BitMod.Extensions.Commands.Lilikoi;
using BitMod.Plugins.Events;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Scan;

using Serilog;

namespace BitMod.Extensions.Commands;

public class CommandContext : Mount
{
	private Mount _global;

	private ILogger _logger;

	private Dictionary<string, List<LilikoiContainer>> _containersByPlugin = new ();

	private Dictionary<string, List<Func<CommandInput, Task<Completion>>>> _containersByCommand = new ();

	public CommandContext(Mount global, ILogger logger)
	{
		_logger = logger;
		_global = global;
	}

	private void AppendCommand(string cmd, LilikoiContainer container)
	{
		if (!_containersByCommand.ContainsKey(cmd))
			_containersByCommand[cmd] = new List<Func<CommandInput, Task<Completion>>>();

		_containersByCommand[cmd].Add(container.Compile<CommandInput, Task<Completion>>());
	}

	private void Rebuild()
	{
		_containersByCommand = new Dictionary<string, List<Func<CommandInput, Task<Completion>>>>();
		foreach (var (key, value) in _containersByPlugin)
		{
			foreach (LilikoiContainer commandContainer in value)
			{
				var desc = commandContainer.Get<CommandDescription>()!;
				AppendCommand(desc.Name, commandContainer);

				foreach (string descAlias in desc.Aliases)
					AppendCommand(descAlias, commandContainer);
			}
		}
	}

	public void PluginLoadMethod(string name, MethodInfo method)
	{
		_containersByPlugin.Add(name, Scanner.Scan<CommandContext, CommandInput, Task<Completion>>(this, method, () => _global));
		Rebuild();
	}

	public void PluginLoad(string name, Assembly assembly)
	{
		_containersByPlugin.Add(name, Scanner.Scan<CommandContext, CommandInput, Task<Completion>>(this, assembly, _global));
		Rebuild();
	}

	public void PluginUnload(string name)
	{
		_containersByPlugin.Remove(name);
		Rebuild();
	}

	public string[] ParseArguments(string message)
	{
		//	https://stackoverflow.com/questions/14655023/split-a-string-that-has-white-spaces-unless-they-are-enclosed-within-quotes/14655145#14655145

		return Regex.Matches(message, @"[\""].+?[\""]|[^ ]+")
			.Cast<Match>()
			.Select(m => m.Value)
			.ToArray();
	}

	public void Invoke(BitPlayer player, string message)
	{
		var args = ParseArguments(message);
		if (args.Length == 0)
			throw new InvalidOperationException("CommandContext.Invoke must receive full command string--Instead got empty!");

		var cmd = args[0].Substring(1);
		if (_containersByCommand.ContainsKey(cmd))
		{
			player.Message($"[BitMod] Unable to find command '{cmd}'!");
			return;
		}

		var input = new CommandInput(player, args);
		var handlers = _containersByCommand[cmd];

		foreach (Func<CommandInput,Task<Completion>> handler in handlers)
		{
			try
			{
				var task = handler(input);
				var result = task.GetAwaiter().GetResult();

				if (result == Completion.Completed)
					return;
			}
			catch (Exception ex)
			{
				_logger.Warning(ex, "Command handler failed during execution of {@Cmd}! Params: {@Params}", cmd, args);

				if (Environment.DoNotCatchEventExceptions)
					throw;
			}
		}
	}

}
