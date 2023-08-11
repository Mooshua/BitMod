using BattleBitAPI.Common;

using BitMod.Compatibility;

namespace BitMod.Events.Player
{
    public class PlayerSpawningEventArgs
    {
        /// <summary>
        /// The player who is spawning.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The spawn request (which you can modify).
        /// </summary>
        public PlayerSpawnRequest Request { get; set; }

        internal PlayerSpawningEventArgs(BitPlayer player, PlayerSpawnRequest request)
        {
            Player = player;
            Request = request;
        }
    }
}
