using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerJoinedSquadEventArgs : IEventArgs, IResponsiblePlayerAccessor
    {
        /// <summary>
        /// The player who joined the squad.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The squad the player joined.
        /// </summary>
        public Squads Squads { get; }

        public PlayerJoinedSquadEventArgs(BitPlayer player, Squads squad)
        {
            Player = player;
            Squads = squad;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;
    }
}
