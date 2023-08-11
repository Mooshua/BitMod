using BenchmarkDotNet.Running;

public static class Program
{
	private static void Main(string[] args)
	{
		BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
	}
}
