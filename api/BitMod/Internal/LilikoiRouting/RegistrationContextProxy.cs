namespace BitMod.Internal.LilikoiRouting;

internal class RegistrationContextProxy : EventRegistrationContext
{
	private EventRegistrationContext _parent;

	public RegistrationContextProxy(EventRegistrationContext parent)
	{
		_parent = parent;
	}

	public override Type Event { get => _parent.Event; internal set => _parent.Event = value; }
}
