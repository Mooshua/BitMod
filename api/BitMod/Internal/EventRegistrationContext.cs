using BitMod.Events;

using Lilikoi.Context;

namespace BitMod.Internal;

internal class EventRegistrationContext : Mount
{
	public virtual Type Event { get; internal set; }

	public int EventId => Registry.IndexOf(Event);
}
