using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerLeftSquadEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who left the squad.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The squad the player left.
        /// </summary>
        public Squads Squads { get; }

        internal PlayerLeftSquadEventArgs(BitServer server, BitPlayer player, Squads squad)
        {
            Player = player;
            Squads = squad;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
