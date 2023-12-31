﻿using BitMod.Plugins.Extensions;

using Lilikoi.Context;

using Serilog;

namespace SamplePlugin.SampleExtension;

public class HelloExtension : IExtension
{
	public string Name => "Hello!";

	public void Register(Mount mount)
	{
		mount.Get<ILogger>().Warning("Hello from extension! Even Newer Version!");
	}

	public void Unregister(Mount mount)
	{
		mount.Get<ILogger>().Warning("Goodbye!");
	}
}
