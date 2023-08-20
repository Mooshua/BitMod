using Lilikoi.Attributes;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class MetaAttribute : LkTypedInjectionAttribute<Mount>
{
	public override Mount Inject(Mount context)
		=> context;
}
