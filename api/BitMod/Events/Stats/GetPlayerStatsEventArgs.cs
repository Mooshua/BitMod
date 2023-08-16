using BattleBitAPI.Common;
using BattleBitAPI.Common.Data;

using BitMod.Events.Base;

namespace BitMod.Events.Stats
{
    public class GetPlayerStatsEventArgs : IProducerArgs<PlayerStats>
    {
        /// <summary>
        /// The player's SteamID
        /// </summary>
        public ulong SteamID { get; }

        /// <summary>
        /// The player's stats (which you can modify)
        /// </summary>
        public PlayerStats OfficialStats { get; }

        public GetPlayerStatsEventArgs(ulong steamID, PlayerStats officialStats)
        {
            SteamID = steamID;
            OfficialStats = officialStats;
        }
    }
}
