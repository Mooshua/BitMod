using CommunityServerAPI.BattleBitAPI;

using Lilikoi.Context;

namespace BitMod.Compatibility;

public class BitPlayer : Player, IBitObject
{
	public Mount Mount { get; } = new Mount();
}
