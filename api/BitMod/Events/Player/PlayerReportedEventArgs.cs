using BattleBitAPI.Common;

using BitMod.Compatibility;

namespace BitMod.Events.Player
{
    public class PlayerReportedEventArgs
    {
        /// <summary>
        /// The player who made the report.
        /// </summary>
        public BitPlayer Reporter { get; init; }

        /// <summary>
        /// The player being reported.
        /// </summary>
        public BitPlayer Reported { get; init; }

        /// <summary>
        /// The report reason.
        /// </summary>
        public ReportReason Reason { get; init; }

        /// <summary>
        /// Additional details about the report.
        /// </summary>
        public string Detail { get; init; }

        internal PlayerReportedEventArgs(BitPlayer reporter, BitPlayer reported, ReportReason reason, string detail)
        {
            Reporter = reporter;
            Reported = reported;
            Reason = reason;
            Detail = detail;
        }
    }
}
