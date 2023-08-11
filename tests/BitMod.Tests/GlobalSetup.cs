
using Serilog;

namespace BitMod.Tests;

public class GlobalSetup
{
	[SetUp]
	public void Init()
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
		.WriteTo.Console()
		.CreateLogger();
	}
}
