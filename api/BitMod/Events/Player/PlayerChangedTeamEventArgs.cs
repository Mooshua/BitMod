using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerChangedTeamEventArgs : IEventArgs, IResponsiblePlayerAccessor
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

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;
    }
}
