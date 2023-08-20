using System.Threading.Tasks;

using BattleBitAPI.Common;

using BitMod.Attributes.Targets;
using BitMod.Compatibility;
using BitMod.Events.Player;
using BitMod.Gamemode.GunGame.State;

namespace BitMod.Gamemode.GunGame.Controllers;

public class GunGameController
{
	[BitEvent]
	public async Task OnPlayerKilled(PlayerKilledPlayerEventArgs ev)
	{
		var serverState = ev.Server.Mount.Get<GunGameState>();
		var playerState = ev.Killer.Mount.Get<GunGamePlayerState>();

		//	Upgrade the killer
	}

	private void UpgradePlayer(BitPlayer player, GunGameState serverState, GunGamePlayerState playerState)
	{
		if (serverState.Progression.Count <= playerState.Progression)
		{
		}
	}
}
