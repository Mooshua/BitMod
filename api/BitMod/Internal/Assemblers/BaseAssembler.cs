using System;
using System.Collections.Generic;

using BitMod.Router;

using Lilikoi.Compiler.Public;

namespace BitMod.Internal.Assemblers;

public abstract class BaseAssembler<TRegistry> : IRouteAssembler<TRegistry, Type>
{
	public Type Bin(LilikoiContainer container)
		=> container.Get<TypeRouterDirectives>().Route;

	public abstract TRegistry Assemble(IEnumerable<LilikoiContainer> containers);
}
