using BitMod.Configuration.Model;

namespace BitMod.Storage.Local.Config;

public class StorageConfigAdapter
{
	private const string PATH = "path";

	private IConfigObject _configObject;

	public StorageConfigAdapter(IConfigObject configObject)
	{
		_configObject = configObject;
	}

	public bool HasPath()
		=> _configObject.Get<IConfigSymbol>(PATH) != null;

	public string? Path => _configObject.Get<IConfigSymbol>(PATH)?.Symbol;

}
