using System;

using BitMod.Configuration.Model;
using BitMod.Public;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Injects;

public class ConfigAttribute : LkTypedInjectionAttribute<IConfigObject>
{
	public ConfigAttribute(string file)
	{
		File = file;
	}

	public string File { get; set; }

	public override IConfigObject Inject(Mount context)
	{
		var config = context.Get<IConfigurationSystem>();
		return config.Get(File);
	}
}
