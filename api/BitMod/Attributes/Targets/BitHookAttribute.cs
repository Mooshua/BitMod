using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Internal;
using BitMod.Internal.LilikoiRouting;

using Lilikoi.Compiler.Public;

using Serilog.Events;

namespace BitMod.Attributes.Targets;

public class BitHookAttribute : BitTargetAttribute
{
	public BitHookAttribute(byte priority = 128)
	{
		Priority = priority;
	}

	public byte Priority { get; }

	public override Type ContextType => typeof(HookRegistrationContext);

	public override string Name => "BitHook";

	internal override bool IsValidEvent(Type arg)
		=> Registry.Events[arg] == Registry.EventType.Hook;

	internal override void Setup(EventRegistrationContext context, LilikoiMutator mutator)
	{
		context.Store( new EventPriority(Priority) );
	}
}
