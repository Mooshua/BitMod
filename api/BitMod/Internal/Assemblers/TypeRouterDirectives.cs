using System;

namespace BitMod.Internal.Assemblers;

public class TypeRouterDirectives
{
	public TypeRouterDirectives(Type route)
	{
		Route = route;
	}

	public Type Route { get; }
}
