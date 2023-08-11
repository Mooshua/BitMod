using BitMod.Attributes.Internal;
using BitMod.Events;
using BitMod.Internal;
using BitMod.Internal.LilikoiRouting;

using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitProducerAttribute : BitTargetAttribute
{
	public override Type ContextType => typeof(ProducerRegistrationContext);

	public override string Name => "BitProducer";

	internal override bool IsValidEvent(Type arg)
		=> Registry.Events[arg] == Registry.EventType.Value;

	internal override void Setup(EventRegistrationContext context, LilikoiMutator mutator)
	{

	}
}
