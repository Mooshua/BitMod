using System;

using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Events.Base;
using BitMod.Internal;
using BitMod.Router;
using BitMod.Router.Extensions;

using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Attributes.Targets;

public class BitMutatorAttribute : BitTargetAttribute
{

	public override string Name => "BitMutator";

	internal override bool IsValidEvent(Type arg)
	{
		var producerArgType = arg.GetInterfaces();

		foreach (Type type in producerArgType)
		{
			var stripped = type.GetGenericTypeDefinition();

			//	This is a subtype of IProducerArgs<>
			if (stripped == typeof(IProducerArgs<>))
				return true;
		}

		return false;
	}



	internal override void Setup(RouterContext context, LilikoiMutator mutator)
	{
		context.Mutator(mutator);
		var producerArgType = mutator.Parameter(0).GetInterfaces();

		foreach (Type type in producerArgType)
		{
			if (!type.IsGenericType)
				continue;

			var stripped = type.GetGenericTypeDefinition();

			//	This is a subtype of IProducerArgs<>
			if (stripped == typeof(IProducerArgs<>))
			{
				var genericArgs = type.GetGenericArguments();

				foreach (Type genericArg in genericArgs)
					mutator.Wildcard(genericArg, new UnpackWildcardParameterAttribute());
			}
		}

	}
}
