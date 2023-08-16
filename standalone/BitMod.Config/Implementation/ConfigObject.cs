using System.Collections;

using BitMod.Configuration.Model;

using ValveKeyValue;

namespace BitMod.Config.Implementation;

public class ConfigObject : IConfigObject
{
	private KVObject _kvObject;
	private Dictionary<string, ConfigSymbol> _childSymbols = new();
	private Dictionary<string, ConfigObject> _childObjects = new();
	private List<KeyValuePair<string, IConfigModel>> _enumerable = new();

	public ConfigObject(KVObject underlying)
	{
		_kvObject = underlying;

		//	Generate children
		foreach (KVObject kvObjectChild in _kvObject.Children)
		{
			if (kvObjectChild.Value.ValueType == KVValueType.Collection)
				//	This is another collection. Add it to the collection cache
				_childObjects.Add(kvObjectChild.Name, new ConfigObject(kvObjectChild));
			else
				_childSymbols.Add(kvObjectChild.Name, new ConfigSymbol(kvObjectChild.Value.ToString()));
		}

		_enumerable = _childObjects
			.Select(kv =>
				new KeyValuePair<string, IConfigModel>(kv.Key, kv.Value))
			.Concat(
				_childSymbols.Select(kv =>
					new KeyValuePair<string, IConfigModel>(kv.Key, kv.Value)))
			.ToList();
	}

	private ConfigObject? GetObject(string key)
	{
		if (_childObjects.TryGetValue(key, out var obj))
			return obj;
		return null;
	}

	private ConfigSymbol? GetSymbol(string key)
	{
		if (_childSymbols.TryGetValue(key, out var sym))
			return sym;
		return null;
	}

	public IEnumerator<KeyValuePair<IConfigSymbol, IConfigModel>> GetEnumerator()
		=> _enumerable
			.Select(kv => new KeyValuePair<IConfigSymbol,IConfigModel>(
				new ConfigSymbol(kv.Key),
				kv.Value
			)).GetEnumerator();

	public IEnumerable<IConfigModel> AsList()
		=> _childObjects.Values.Concat<IConfigModel>(_childSymbols.Values);

	IEnumerator IEnumerable.GetEnumerator()
		=> _enumerable.GetEnumerator();

	public IConfigModel? Get(string key)
		=> Get<IConfigModel>(key);

	public TModel? Get<TModel>(string key)
		where TModel : class, IConfigModel
	{
		//	Little criss-cross here.
		//	This means if we pass a model that isn't a
		//	config symbol (eg IConfigModel) then we will
		//	search both objects and symbols.
		if (typeof(TModel) != typeof(IConfigSymbol))
			return GetObject(key) as TModel;

		if (typeof(TModel) != typeof(IConfigObject))
			return GetSymbol(key) as TModel;

		return null;
	}

	public IConfigSymbol? GetSymbol(string key, string? fallback = null)
		=> Get<IConfigSymbol>(key) ?? new ConfigSymbol(fallback);

	public int Count => _enumerable.Count;
}
