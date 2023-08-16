using BattleBitAPI.Server;

using Lilikoi.Context;

namespace BitMod.Compatibility;

public class BitServer : GameServer<BitPlayer>, IBitObject
{
	public Mount Mount { get; }


	public static implicit operator string(BitServer value)
		=> $"{value.GameIP}:{value.GamePort}";
}
