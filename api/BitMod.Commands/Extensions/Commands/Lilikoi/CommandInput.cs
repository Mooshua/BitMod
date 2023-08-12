using BitMod.Compatibility;

namespace BitMod.Extensions.Commands.Lilikoi;

public class CommandInput
{
	public CommandInput(BitPlayer sender, string[] args)
	{
		Sender = sender;
		Args = args;
	}

	public BitPlayer Sender { get; }

	public string[] Args { get; }
}
