using BattleBitAPI.Common;
using BattleBitAPI.Common.Enums;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerTypedMessageEventArgs : IHookArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who typed the message.
        /// </summary>
        public BitPlayer Player { get; init; }

        /// <summary>
        /// The channel the message was sent in.
        /// </summary>
        public ChatChannel ChatChannel { get; init; }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; init; }

        internal PlayerTypedMessageEventArgs(BitServer server, BitPlayer player, ChatChannel chatChannel, string message)
        {
            Server = server;
            Player = player;
            ChatChannel = chatChannel;
            Message = message;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
