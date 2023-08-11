using BitMod.Internal;

using Lilikoi.Attributes;
using Lilikoi.Context;

namespace BitMod.Attributes.Internal;

/// <summary>
/// To avoid generic propagation, we'll use a mount to store the actual event information.
/// </summary>
internal class UnpackWildcardParameterAttribute : LkParameterAttribute
{
	public UnpackWildcardParameterAttribute(Type @event)
	{
		Event = @event;
	}

	public Type Event { get; }

	public override bool IsInjectable<TParameter, TInput>(Mount mount)
		=>    typeof(TParameter) == Event
		   && typeof(TInput) == typeof(EventInput);

	public override TParameter Inject<TParameter, TInput>(Mount context, TInput input)
		=> (input as EventInput)!.Get<TParameter>()!;
}
