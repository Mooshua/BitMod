using System.ComponentModel;

using BitMod.Attributes.Internal;
using BitMod.Internal;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

using Serilog;

namespace BitMod.Attributes.Targets;


[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class BitTargetAttribute : LkTargetAttribute
{
	public abstract Type ContextType { get; }

	public override bool IsTargetable<TUserContext>()
		=> typeof(TUserContext) == ContextType;

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as EventRegistrationContext, mutator);

	internal void Target(EventRegistrationContext context, LilikoiMutator mutator)
	{
		if (mutator.Parameters == 0)
			throw new InvalidOperationException("BitMod targets require at least one argument in the handler function!");

		var first = mutator.Parameter(0);

		if (!IsValidEvent(first))
			throw new InvalidOperationException($"Type {mutator.Parameter(0).FullName} is not a valid for BitMod target {Name}!");

		context.Event = first;
		mutator.Wildcard(new UnpackWildcardParameterAttribute(first), first);

		//	Handle async result types
		if (mutator.Result.IsSubclassOf(typeof(Task)) || mutator.Result == typeof(Task))
			mutator.Implicit(new AsyncAttribute());

		Setup(context, mutator);
	}

	public abstract string Name { get; }

	/// <summary>
	/// Given the provided type, determine if it a valid event type.
	/// </summary>
	/// <param name="arg"></param>
	/// <returns></returns>
	internal abstract bool IsValidEvent(Type arg);

	/// <summary>
	/// Set up any required wildcards or wraps
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mutator"></param>
	internal abstract void Setup(EventRegistrationContext context, LilikoiMutator mutator);
}
