using BitMod.Compatibility;

using CommunityServerAPI.BattleBitAPI.Common.Enums;

namespace BitMod.Events.Player
{
    public class PlayerRequestingToChangeRoleEventArgs
    {
        /// <summary>
        /// The player requesting.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The role the player asking to change.
        /// </summary>
        public GameRole Role { get; init; }

        internal PlayerRequestingToChangeRoleEventArgs(BitPlayer player, GameRole role)
        {
            Player = player;
            Role = role;
        }
    }
}
