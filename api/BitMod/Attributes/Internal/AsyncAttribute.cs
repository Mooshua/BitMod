using System.Threading.Tasks;

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

namespace BitMod.Attributes.Internal;

/// <summary>
/// Prevent further "After" wraps from being executed before the provided async task
/// has completed.
/// </summary>
public class AsyncAttribute : LkTypedWrapAttribute<object, Task>
{
	public override WrapResult<Task> Before(Mount mount, ref object input)
		=> WrapResult<Task>.Continue();

	public override void After(Mount mount, ref Task output)
		=> output.GetAwaiter().GetResult();
}
