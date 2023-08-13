using System;
using System.ComponentModel;
using System.Threading.Tasks;

using BattleBitAPI.Server;

using BitMod.Attributes.Injects;
using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Internal;
using BitMod.Internal.Assemblers;
using BitMod.Router;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Internal;


[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class BitTargetAttribute : LkTargetAttribute
{
	public override bool IsTargetable<TUserContext>()
		=> typeof(TUserContext) == typeof(RouterContext);

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as RouterContext, mutator);

	internal void Target(RouterContext context, LilikoiMutator mutator)
	{
		if (mutator.Parameters == 0)
			throw new InvalidOperationException("BitMod targets require at least one argument in the handler function!");

		var first = mutator.Parameter(0);

		if (!IsValidEvent(first))
			throw new InvalidOperationException($"Type {mutator.Parameter(0).FullName} is not a valid for BitMod target {Name}!");

		mutator.Wildcard(new UnpackWildcardParameterAttribute(first), first);

		//	Handle async result types
		if (mutator.Result.IsSubclassOf(typeof(Task)) || mutator.Result == typeof(Task))
			mutator.Implicit(new AsyncAttribute());

		if (typeof(IResponsiblePlayerEvent).IsAssignableFrom(first))
			mutator.Wildcard<BitPlayer>(new ResponsiblePlayerAttribute());

		if (typeof(IGameserverEvent).IsAssignableFrom(first))
			mutator.Wildcard<BitServer>(new RelevantGameserverAttribute());

		mutator.Store(new TypeRouterDirectives(first));
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
	internal abstract void Setup(RouterContext context, LilikoiMutator mutator);
}
