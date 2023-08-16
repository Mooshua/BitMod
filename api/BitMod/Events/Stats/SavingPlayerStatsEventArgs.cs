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
        public ulong SteamID { get; }

        /// <summary>
        /// The player's stats
        /// </summary>
        public PlayerStats PlayerStats { get; }

        internal SavingPlayerStatsEventArgs(ulong steamID, PlayerStats playerStats)
        {
            SteamID = steamID;
            PlayerStats = playerStats;
        }
    }
}
