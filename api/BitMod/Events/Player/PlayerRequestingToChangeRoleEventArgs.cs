using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerRequestingToChangeRoleEventArgs : IHookArgs, IResponsiblePlayerAccessor
    {
        /// <summary>
        /// The player requesting.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The role the player asking to change.
        /// </summary>
        public GameRole Role { get; init; }

        public PlayerRequestingToChangeRoleEventArgs(BitPlayer player, GameRole role)
        {
            Player = player;
            Role = role;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;
    }
}
