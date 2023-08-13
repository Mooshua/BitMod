using Lilikoi.Context;

namespace BitMod.Compatibility;

public interface IBitObject
{
	public Mount Mount { get; }

	/// <summary>
	/// Fetch the specified type from this player's TypeDictionary.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? Get<T>()
		where T : class
		=> Mount.Get<T>();

	/// <summary>
	/// Determine if the specified type is in the player's TypeDictionary
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public bool Has<T>()
		=> Mount.Has<T>();

	/// <summary>
	/// Create a new type in the specified type dictionary
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void Store<T>(T value)
		where T : class
		=> Mount.Store(value);
}
