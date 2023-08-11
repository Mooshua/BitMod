using BitMod.Compatibility;

using CommunityServerAPI.BattleBitAPI.Common.Enums;

namespace BitMod.Events.Player
{
    public class PlayerLeftSquadEventArgs
    {
        /// <summary>
        /// The player who left the squad.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The squad the player left.
        /// </summary>
        public Squads Squads { get; }

        internal PlayerLeftSquadEventArgs(BitPlayer player, Squads squad)
        {
            Player = player;
            Squads = squad;
        }
    }
}
