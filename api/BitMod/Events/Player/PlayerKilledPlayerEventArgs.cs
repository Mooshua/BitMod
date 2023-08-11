using System.Numerics;

using BitMod.Compatibility;

namespace BitMod.Events.Player
{
    public class PlayerKilledPlayerEventArgs
    {
        /// <summary>
        /// The killer.
        /// </summary>
        public BitPlayer Killer { get; init; }

        /// <summary>
        /// The position of the killer.
        /// </summary>
        public Vector3 KillerPosition { get; init; }

        /// <summary>
        /// The target.
        /// </summary>
        public BitPlayer Target { get; init; }

        /// <summary>
        /// The position of the target.
        /// </summary>
        public Vector3 TargetPosition { get; init; }

        /// <summary>
        /// The tool used to kill the target.
        /// </summary>
        public string Tool { get; init; }

        internal PlayerKilledPlayerEventArgs(BitPlayer killer, Vector3 killerPosition, BitPlayer target, Vector3 targetPosition, string tool)
        {
            Killer = killer;
            KillerPosition = killerPosition;
            Target = target;
            TargetPosition = targetPosition;
            Tool = tool;
        }
    }
}
