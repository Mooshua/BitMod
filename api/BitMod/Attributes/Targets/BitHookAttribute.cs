using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Events.Base;
using BitMod.Internal;
using BitMod.Router;
using BitMod.Router.Extensions;

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

	public override string Name => "BitHook";

	internal override bool IsValidEvent(Type arg)
		=> arg.IsAssignableTo(typeof(IHookArgs));

	internal override void Setup(RouterContext context, LilikoiMutator mutator)
	{
		context.Store( new EventPriority(Priority) );
		context.Hook( mutator );
	}
}
