using BitMod.Plugins.Extensions;
using BitMod.Public;

using Serilog;

namespace BitMod;

public static class BitMock
{
	public class MockFigSystem : IConfigurationSystem
	{
		private Dictionary<Type, object> _configs;

		public MockFigSystem(Dictionary<Type, object> configs)
		{
			_configs = configs;
		}

		public void Start(BitMod env)
		{

		}

		public T Get<T>(string name, Func<T> makeDefault)
		{
			throw new NotImplementedException();
		}
	}

	public class MockInSystem : IPluginSystem
	{
		private BitMod _env;

		public void Start(BitMod env)
		{
			_env = env;
		}

		private List<IExtension> _extensions = new ();

		private List<IPluginSystem.IPlugin> _plugins = new ();


		public IReadOnlyList<IPluginSystem.IPlugin> Plugins => _plugins;

		public IReadOnlyList<IExtension> Extensions => _extensions;

		public void Load(IExtension extension)
		{
			extension.Register(_env);
			_extensions.Add(extension);
		}

		public void Unload(IExtension extension)
		{
			extension.Unregister(_env);
			_extensions.Remove(extension);
		}

		public void Stop()
		{
		}
	}

	public static BitMod Mock( Dictionary<Type, object> configs = null )
	{
		var logger = Log.Logger;

		var config = new MockFigSystem(configs ?? new());
		var plugins = new MockInSystem();

		var mod = new BitMod(logger, config, plugins);

		return mod;
	}
}
