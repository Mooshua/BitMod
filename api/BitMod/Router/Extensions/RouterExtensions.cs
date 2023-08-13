using System;

using BitMod.Internal.Assemblers;
using BitMod.Internal.Registries;

using Lilikoi.Compiler.Public;

namespace BitMod.Router.Extensions;

public static class RouterExtensions
{

	/// <summary>
	/// Register this mutator as a hook
	/// </summary>
	/// <param name="context"></param>
	/// <param name="self"></param>
	public static void Hook(this RouterContext context, LilikoiMutator self)
	{
		context.Register( () => new HookAssembler(context.Logger) );
		context.Append<HookEventRegistry, Type>(self);
	}

	/// <summary>
	/// Register this mutator as an event
	/// </summary>
	/// <param name="context"></param>
	/// <param name="self"></param>
	public static void Event(this RouterContext context, LilikoiMutator self)
	{
		context.Register( () => new EventAssembler(context.Logger) );
		context.Append<SimpleEventRegistry, Type>(self);
	}

	/// <summary>
	/// Register this mutator as an event
	/// </summary>
	/// <param name="context"></param>
	/// <param name="self"></param>
	public static void Producer(this RouterContext context, LilikoiMutator self)
	{
		context.Register( () => new ProducerAssembler(context.Logger) );
		context.Append<ProducerEventRegistry, Type>(self);
	}

	/// <summary>
	/// Register this mutator as an event
	/// </summary>
	/// <param name="context"></param>
	/// <param name="self"></param>
	public static void Mutator(this RouterContext context, LilikoiMutator self)
	{
		context.Register( () => new MutatorAssembler(context.Logger) );
		context.Append<MutatorEventRegistry, Type>(self);
	}
}
