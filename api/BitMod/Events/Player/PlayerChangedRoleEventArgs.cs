using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerChangedRoleEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who changed role.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The new role of the player.
        /// </summary>
        public GameRole Role { get; init; }

        internal PlayerChangedRoleEventArgs(BitServer server, BitPlayer player, GameRole role)
        {
            Player = player;
            Role = role;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
