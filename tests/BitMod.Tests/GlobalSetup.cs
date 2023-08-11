
using Serilog;

namespace BitMod.Tests;

public class GlobalSetup
{
	[SetUp]
	public void Init()
	{
		Environment.DoNotCatchEventExceptions = true;

		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
		.WriteTo.Console()
		.CreateLogger();
	}
}
