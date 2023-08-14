using BattleBitAPI.Common;
using BattleBitAPI.Common.Data;

using BitMod.Events.Base;

namespace BitMod.Events.Stats
{
    public class SavingPlayerStatsEventArgs : IEventArgs
    {
        /// <summary>
        /// The player's SteamID
        /// </summary>
        public ulong SteamID { get; init; }

        /// <summary>
        /// The player's stats
        /// </summary>
        public PlayerStats PlayerStats { get; init; }

        internal SavingPlayerStatsEventArgs(ulong steamID, PlayerStats playerStats)
        {
            SteamID = steamID;
            PlayerStats = playerStats;
        }
    }
}
