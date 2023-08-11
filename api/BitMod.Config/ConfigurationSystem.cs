using BitMod.Public;

using Tomlyn;

namespace BitMod.Config;

public class ConfigurationSystem : IConfigurationSystem
{
	public static string CONFIG_PATH = "configs";
	public static string CONFIG_EXT = ".toml";

	public TomlModelOptions Options = new TomlModelOptions()
	{

	};

	public void Start(BitMod env)
	{

	}

	public T Get<T>(string name, Func<T> makeDefault)
		where T : class, new()
	{
		var path = Path.Join(System.Environment.CurrentDirectory, CONFIG_PATH, name + CONFIG_EXT);

		if (!File.Exists(path))
		{
			var def = makeDefault();
			var contents = Toml.FromModel(def, Options);

			File.WriteAllText(path, contents);
		}

		var configContents = File.ReadAllText(path);
		return Toml.ToModel<T>(configContents, path, Options);
	}
}
