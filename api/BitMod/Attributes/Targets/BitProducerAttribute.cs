using System;

using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Events.Base;
using BitMod.Internal;
using BitMod.Router;
using BitMod.Router.Extensions;

using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitProducerAttribute : BitTargetAttribute
{
	public BitProducerAttribute(byte priority = 128)
	{
		Priority = priority;
	}

	public byte Priority { get; }

	public override string Name => "BitProducer";

	internal override bool IsValidEvent(Type arg)
	{
		var producerArgType = arg.GetInterfaces();

		foreach (Type type in producerArgType)
		{
			if (!type.IsGenericType)
				continue;

			var stripped = type.GetGenericTypeDefinition();

			//	This is a subtype of IProducerArgs<>
			if (stripped == typeof(IProducerArgs<>))
				return true;
		}

		return false;
	}


	internal override void Setup(RouterContext context, LilikoiMutator mutator)
	{
		context.Store( new EventPriority(Priority) );
		context.Producer(mutator);
	}
}
