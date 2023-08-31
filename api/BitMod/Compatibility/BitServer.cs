using BattleBitAPI.Server;

using Lilikoi.Context;

namespace BitMod.Compatibility;

public class BitServer : GameServer<BitPlayer>, IBitObject, IMount
{
	public Mount Mount { get; }

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

	public override string ToString()
		=> $"{this.GameIP}:{this.GamePort}";

	public static implicit operator string(BitServer value)
		=> value.ToString();
}
