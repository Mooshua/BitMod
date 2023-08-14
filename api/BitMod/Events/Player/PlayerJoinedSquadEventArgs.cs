using BattleBitAPI.Common;
using BattleBitAPI.Common.Enums;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerJoinedSquadEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who joined the squad.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The squad the player joined.
        /// </summary>
        public Squads Squads { get; }

        public PlayerJoinedSquadEventArgs(BitServer server, BitPlayer player, Squads squad)
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
