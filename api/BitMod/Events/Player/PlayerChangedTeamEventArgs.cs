using BitMod.Compatibility;

using CommunityServerAPI.BattleBitAPI.Common.Enums;

namespace BitMod.Events.Player
{
    public class PlayerChangedTeamEventArgs
    {
        /// <summary>
        /// The player who joined a team.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The new team which the player joined.
        /// </summary>
        public Team Team { get; init; }

        internal PlayerChangedTeamEventArgs(BitPlayer player, Team team)
        {
            Player = player;
            Team = team;
        }
    }
}
