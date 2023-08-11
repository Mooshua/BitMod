using BitMod.Events;

namespace BitMod.Internal.Routing;

public class Router<T>
	where T: new()
{
	private Dictionary<Type, T> _backend = new Dictionary<Type, T>();

	public T Get(Type index)
	{
		if (!_backend.ContainsKey(index))
			_backend[index] = new T();

		return _backend[index];
	}

	public IEnumerable<T> All()
		=> _backend.Values;
}
