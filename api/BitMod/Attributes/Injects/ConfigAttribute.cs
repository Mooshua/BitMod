using BitMod.Public;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class ConfigAttribute : LkInjectionAttribute
{
	public ConfigAttribute(string file)
	{
		File = file;
	}

	public string File { get; set; }

	public override bool IsInjectable<TInjectable>(Mount mount)
		=> true;


	public override TInjectable Inject<TInjectable>(Mount context)
	{
		var config = context.Get<IConfigurationSystem>();
		var model = config.Get<TInjectable>(File, () => Activator.CreateInstance(typeof(TInjectable)) as TInjectable);
		return model;
	}
}
