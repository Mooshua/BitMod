using BitMod.Events;

namespace BitMod.Internal;

internal class EventRegistrationContext
{
	public Type Event { get; internal set; }

	public int EventId => Registry.IndexOf(Event);
}
