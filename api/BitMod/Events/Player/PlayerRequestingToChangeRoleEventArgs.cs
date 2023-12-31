﻿using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerRequestingToChangeRoleEventArgs : IHookArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player requesting.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The role the player asking to change.
        /// </summary>
        public GameRole Role { get; }

        public PlayerRequestingToChangeRoleEventArgs(BitServer server, BitPlayer player, GameRole role)
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
