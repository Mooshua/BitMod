using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerTypedMessageEventArgs : IHookArgs, IResponsiblePlayerAccessor
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

        internal PlayerTypedMessageEventArgs(BitPlayer player, ChatChannel chatChannel, string message)
        {
            Player = player;
            ChatChannel = chatChannel;
            Message = message;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Player;
    }
}
