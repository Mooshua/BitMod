﻿using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerChangedTeamEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who joined a team.
        /// </summary>
        public BitPlayer Player { get; }

        /// <summary>
        /// The new team which the player joined.
        /// </summary>
        public Team Team { get; }

        internal PlayerChangedTeamEventArgs(BitServer server, BitPlayer player, Team team)
        {
            Player = player;
            Team = team;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
