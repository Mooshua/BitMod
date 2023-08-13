using System;

using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Events.Base;
using BitMod.Internal;
using BitMod.Router;
using BitMod.Router.Extensions;

using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitEventAttribute : BitTargetAttribute
{
	public override string Name => "BitEvent";

	internal override bool IsValidEvent(Type arg)
		=> arg.IsAssignableTo(typeof(IEventArgs));

	internal override void Setup(RouterContext context, LilikoiMutator mutator)
	{
		context.Event(mutator);
	}
}
