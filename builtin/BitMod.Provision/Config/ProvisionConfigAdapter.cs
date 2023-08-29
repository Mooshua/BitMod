using BitMod.Compatibility;
using BitMod.Configuration.Model;

namespace BitMod.Provision.Config;

public class ProvisionConfigAdapter
{
	private IConfigObject _configObject;

	public ProvisionConfigAdapter(IConfigObject configObject)
	{
		_configObject = configObject;
	}

	public ProvisionServerAdapter? GetServer(string server)
	{
		var child = _configObject.Get<IConfigObject>(server);

		if (child == null)
			return null;

		return new ProvisionServerAdapter(child);
	}
}
