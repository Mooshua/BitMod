using BitMod.Public;

using ValveKeyValue;

namespace BitMod.Config;

public class ConfigurationSystem : IConfigurationSystem
{
	public static string CONFIG_PATH = "configs";
	public static string CONFIG_EXT = ".kv";

	public KVSerializer Serializer => KVSerializer.Create(KVSerializationFormat.KeyValues1Text);

	public KVSerializerOptions Options => KVSerializerOptions.DefaultOptions;

	public void Start(BitMod env)
	{

	}


	public T Get<T>(string name, Func<T> makeDefault)
	{
		var path = Path.Join(System.Environment.CurrentDirectory, CONFIG_PATH, name + CONFIG_EXT);

		if (!File.Exists(path))
		{
			var def = makeDefault();

			using (var stream = File.OpenWrite(path))
				Serializer.Serialize(stream, def, name, Options);
		}

		using (var stream = File.OpenRead(path))
			return Serializer.Deserialize<T>(stream, Options);
	}
}
