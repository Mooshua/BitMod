using BitMod.Compatibility;

using Serilog;

namespace BitMod.Commands.Sources.Internal;

public class StandardInputSource : ICommandSource
{
	private readonly ILogger _logger;

	public StandardInputSource(ILogger logger)
	{
		_logger = logger;
	}

	public bool IsRemote => true;

	public bool IsAuthenticated => false;

	public bool IsAssociatedWithGameServer => false;

	public ulong Steam64 => ulong.MaxValue;

	public BitServer? GameServer => null;

	public BitPlayer? Player => null;

	public void Reply(string message)
	{
		_logger.Information("[BitMod Commands] {@Msg}", message);
	}
}
