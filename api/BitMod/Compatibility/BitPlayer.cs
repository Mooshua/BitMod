using BattleBitAPI;

using Lilikoi.Context;

namespace BitMod.Compatibility;

public class BitPlayer : Player<BitPlayer>, IBitObject, IMount
{
	public Mount Mount { get; } = new Mount();

	public void Store<T>(T value) where T : class
		=> Mount.Store(value);

	public T? Get<T>() where T : class
		=> Mount.Get<T>();

	public T? Super<T>(Type super) where T : class
		=> Mount.Super<T>(super);

	public bool Has<T>()
		=> Mount.Has<T>();

	public bool Has(Type t)
		=> Mount.Has(t);
}
