using BitMod.Attributes.Internal;
using BitMod.Configuration.Model;
using BitMod.Events.Config;
using BitMod.Internal;
using BitMod.Internal.Assemblers;
using BitMod.Internal.Registries;
using BitMod.Router;
using BitMod.Router.Extensions;

using Lilikoi.Attributes;
using Lilikoi.Compiler.Public;

namespace BitMod.Attributes.Targets;

public class BitConfigUpdateAttribute : LkTargetAttribute
{
	private string _configFile;

	public BitConfigUpdateAttribute(string configFile)
	{
		_configFile = configFile;
	}

	public override bool IsTargetable<TUserContext>()
		=> (typeof(RouterContext)).IsAssignableFrom(typeof(TUserContext));

	public override void Target<TUserContext>(TUserContext context, LilikoiMutator mutator)
		=> Target(context as RouterContext, mutator);

	public void Target(RouterContext router, LilikoiMutator mutator)
	{
		router.ConfigUpdated(mutator);

		//	Add wildcards for common parameters
		mutator.Wildcard<IConfigObject>(new UnpackWildcardParameterAttribute(typeof(IConfigObject)));
		mutator.Wildcard<ConfigUpdatedEventArgs>(new UnpackWildcardParameterAttribute(typeof(ConfigUpdatedEventArgs)));


		//	Proper async-await code handling
		mutator.Implicit(new AsyncAttribute());

		//	Store metadata so the command assembler can tell what kind of command we are.
		mutator.Store(new StringRouterDirectives(_configFile));
	}
}
