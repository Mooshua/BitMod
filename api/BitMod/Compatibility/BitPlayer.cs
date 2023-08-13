using BattleBitAPI;

using CommunityServerAPI.BattleBitAPI;

using Lilikoi.Context;

namespace BitMod.Compatibility;

public class BitPlayer : Player<BitPlayer>, IBitObject
{
	public Mount Mount { get; } = new Mount();
}
