using Serilog;

namespace BitMod.Logging;

public class LoggingConfiguration
{
	public RollingInterval FileInterval { get; set; } = RollingInterval.Day;
}
