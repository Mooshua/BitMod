using System.Numerics;

using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerKilledPlayerEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The killer.
        /// </summary>
        public BitPlayer Killer { get; }

        /// <summary>
        /// The position of the killer.
        /// </summary>
        public Vector3 KillerPosition { get; }

        /// <summary>
        /// The target.
        /// </summary>
        public BitPlayer Target { get; }

        /// <summary>
        /// The position of the target.
        /// </summary>
        public Vector3 TargetPosition { get; }

        /// <summary>
        /// The tool used to kill the target.
        /// </summary>
        public string Tool { get; }

        public PlayerBody BodyPart { get; }

        public ReasonOfDamage Source { get; }

        internal PlayerKilledPlayerEventArgs(BitServer server, BitPlayer killer, Vector3 killerPosition, BitPlayer target, Vector3 targetPosition, string tool, PlayerBody bodyPart, ReasonOfDamage source)
        {
            Killer = killer;
            KillerPosition = killerPosition;
            Target = target;
            TargetPosition = targetPosition;
            Tool = tool;
            BodyPart = bodyPart;
            Source = source;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Killer;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
