using Lilikoi.Compiler.Public;

namespace BitMod.Router;

internal class RouterAssignments
{
	public RouterAssignments(string pluginName)
	{
		PluginName = pluginName;
	}

	public string PluginName { get; }
	public List<BaseRouter> Routers { get; } = new List<BaseRouter>();

	public void Add(BaseRouter? router)
	{
		if (router == null)
			return;

		Routers.Add(router);
	}

	public void Register(LilikoiContainer container)
	{
		foreach (BaseRouter baseRouter in Routers)
			baseRouter.Add(PluginName, container);
	}
}
