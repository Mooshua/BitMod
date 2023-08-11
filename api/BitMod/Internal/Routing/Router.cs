using BitMod.Events;

namespace BitMod.Internal.Routing;

public class Router<T>
{
	private Func<T> _make;

	public Router(Func<T> make)
	{
		_make = make;
	}

	private Dictionary<Type, T> _backend = new Dictionary<Type, T>();

	public T Get(Type index)
	{
		if (!_backend.ContainsKey(index))
			_backend[index] = _make();

		return _backend[index];
	}

	public IEnumerable<T> All()
		=> _backend.Values;
}
