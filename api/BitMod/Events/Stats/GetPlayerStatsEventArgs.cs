using CommunityServerAPI.BattleBitAPI.Common.Data;

namespace BitMod.Events.Stats
{
    public class GetPlayerStatsEventArgs
    {
        /// <summary>
        /// The player's SteamID
        /// </summary>
        public ulong SteamID { get; init; }

        /// <summary>
        /// The player's stats (which you can modify)
        /// </summary>
        public PlayerStats OfficialStats { get; set; }

        internal GetPlayerStatsEventArgs(ulong steamID, PlayerStats officialStats)
        {
            SteamID = steamID;
            OfficialStats = officialStats;
        }
    }
}
