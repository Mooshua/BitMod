using BitMod.Compatibility;

using CommunityServerAPI.BattleBitAPI.Common.Enums;

namespace BitMod.Events.Player
{
    public class PlayerJoinedSquadEventArgs
    {
        /// <summary>
        /// The player who joined the squad.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The squad the player joined.
        /// </summary>
        public Squads Squads { get; }

        internal PlayerJoinedSquadEventArgs(BitPlayer player, Squads squad)
        {
            Player = player;
            Squads = squad;
        }
    }
}
