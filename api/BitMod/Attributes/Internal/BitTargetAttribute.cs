using System.ComponentModel;

using BitMod.Attributes.Internal;
using BitMod.Internal;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;


[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class BitTargetAttribute : LkTargetAttribute
{
	public override bool IsTargetable<TUserContext>()
		=> typeof(TUserContext) == typeof(EventRegistrationContext);

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as EventRegistrationContext, mutator);

	internal void Target(EventRegistrationContext context, LilikoiMutator mutator)
	{
		if (mutator.Parameters == 0)
			throw new InvalidOperationException("BitMod targets require at least one argument in the handler function!");

		if (!IsValidEvent(mutator.Parameter(0)))
			throw new InvalidOperationException($"Type {mutator.Parameter(0).FullName} is not a valid for BitMod target {Name}!");

		context.Event = mutator.Parameter(0);
		mutator.Wildcard<UnpackWildcardParameterAttribute>(new UnpackWildcardParameterAttribute(context.Event));

		//	Handle async result types
		if (mutator.Result.IsSubclassOf(typeof(Type)))
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
