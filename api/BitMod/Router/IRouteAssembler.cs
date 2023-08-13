using System.Collections.Generic;

using Lilikoi.Compiler.Public;

namespace BitMod.Router;

public interface IRouteAssembler<TResult, TSearch>
{

	/// <summary>
	/// When provided with the given Lilikoi container,
	/// get the TSearch value which is used to route to the container.
	/// Used to bin containers before final assembly.
	/// </summary>
	/// <param name="container"></param>
	/// <returns></returns>
	TSearch Bin(LilikoiContainer container);

	/// <summary>
	/// When provided with a list of containers (each having the same TSearch),
	/// build the final TResult object.
	/// </summary>
	/// <param name="containers"></param>
	/// <returns></returns>
	TResult Assemble(IEnumerable<LilikoiContainer> containers);

}
