using BattleBitAPI.Common;

using BitMod.Compatibility;
using BitMod.Events.Accessors;
using BitMod.Events.Base;

namespace BitMod.Events.Player
{
    public class PlayerReportedEventArgs : IEventArgs, IResponsiblePlayerEvent
    {
        /// <summary>
        /// The player who made the report.
        /// </summary>
        public BitPlayer Reporter { get; }

        /// <summary>
        /// The player being reported.
        /// </summary>
        public BitPlayer Reported { get; }

        /// <summary>
        /// The report reason.
        /// </summary>
        public ReportReason Reason { get; }

        /// <summary>
        /// Additional details about the report.
        /// </summary>
        public string Detail { get; }

        internal PlayerReportedEventArgs(BitServer server, BitPlayer reporter, BitPlayer reported, ReportReason reason, string detail)
        {
            Reporter = reporter;
            Reported = reported;
            Reason = reason;
            Detail = detail;
            Server = server;
        }

        /// <inheritdoc />
        public BitPlayer ResponsiblePlayer => Reporter;

        /// <inheritdoc />
        public BitServer Server { get; }
    }
}
