using BattleBitAPI.Common;
using BattleBitAPI.Common.Arguments;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;
using BitMod.Events.Result;

namespace BitMod.Events.Player
{
    public class PlayerSpawningEventArgs : IProducerArgs<SpawnRequest>, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who is spawning.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The spawn request (which you can modify).
        /// </summary>
        public OnPlayerSpawnArguments Request { get; set; }

        internal PlayerSpawningEventArgs(BitServer server, BitPlayer player, OnPlayerSpawnArguments request)
        {
            Player = player;
            Request = request;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
