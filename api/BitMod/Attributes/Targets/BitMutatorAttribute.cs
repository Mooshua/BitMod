using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Internal;
using BitMod.Internal.LilikoiRouting;

using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitMutatorAttribute : BitTargetAttribute
{
	public override Type ContextType => typeof(MutatorRegistrationContext);

	public override string Name => "BitEvent";

	internal override bool IsValidEvent(Type arg)
		=> Registry.Events[arg] == Registry.EventType.Value;

	internal override void Setup(EventRegistrationContext context, LilikoiMutator mutator)
	{
		var arg = mutator.Parameter(0);
		var result = Registry.Producers[arg];
		mutator.Wildcard(result, new UnpackWildcardParameterAttribute());
	}
}
