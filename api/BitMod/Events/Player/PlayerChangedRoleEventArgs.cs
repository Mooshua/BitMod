using BattleBitAPI.Common;

using BitMod.Compatibility;

namespace BitMod.Events.Player
{
    public class PlayerChangedRoleEventArgs
    {
        /// <summary>
        /// The player who changed role.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The new role of the player.
        /// </summary>
        public GameRole Role { get; init; }

        internal PlayerChangedRoleEventArgs(BitPlayer player, GameRole role)
        {
            Player = player;
            Role = role;
        }
    }
}
