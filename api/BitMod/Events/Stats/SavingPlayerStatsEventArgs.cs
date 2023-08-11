using CommunityServerAPI.BattleBitAPI.Common.Data;

namespace BitMod.Events.Stats
{
    public class SavingPlayerStatsEventArgs
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