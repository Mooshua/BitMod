using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerLeftSquadEventArgs : IEventArgs, IResponsiblePlayerAccessor
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

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;
    }
}
