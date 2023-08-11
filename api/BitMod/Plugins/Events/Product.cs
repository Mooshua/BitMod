using Lilikoi.Context;

namespace BitMod.Plugins.Events;

public class Product
{
	private Product(bool exists)
	{
		Exists = exists;
	}

	public bool Exists { get; }

	private Mount _storage = new Mount();

	public bool Is<T>()
		=> _storage.Has<T>();

	public T? As<T>()
		where T: class
		=> _storage.Get<T>();

	/// <summary>
	/// Do not produce a value, instead delegating responsibility
	/// for creating the value to another entry in the handler chain
	/// </summary>
	/// <returns></returns>
	public static Product None()
		=> new Product(false);

	/// <summary>
	/// Produce an item, immediately ending the handler chain
	/// and passing this value through mutators
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static Product Produce<T>(T value)
		where T: class
	{
		var product = new Product(true);
		product._storage.Store(value);

		return product;
	}
}
