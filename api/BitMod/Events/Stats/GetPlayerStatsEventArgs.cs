using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Stats
{
    public class GetPlayerStatsEventArgs : IProducerArgs<PlayerStats>, IGameserverEvent
    {
        /// <summary>
        /// The player's SteamID
        /// </summary>
        public ulong SteamID { get; }

        /// <summary>
        /// The player's stats (which you can modify)
        /// </summary>
        public PlayerStats OfficialStats { get; }

        public GetPlayerStatsEventArgs(BitServer server, ulong steamID, PlayerStats officialStats)
        {
            SteamID = steamID;
            OfficialStats = officialStats;
            Server = server;
        }

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
