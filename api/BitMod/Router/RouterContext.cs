using BitMod.Internal.Public;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;

using Serilog;

namespace BitMod.Router;

/// <summary>
/// A lilikoi context used to register plugins' routes
/// </summary>
public class RouterContext : Mount
{
	private string _pluginName;
	private BitMod _env;

	public RouterContext(BitMod env, string pluginName)
	{
		_env = env;
		_pluginName = pluginName;
	}

	public override T? Get<T>() where T : class
		=> _env.Get<T>();

	public override bool Has<T>()
		=> _env.Has<T>();

	public override void Store<T>(T value) where T : class
		=> _env.Store<T>(value);

	public ILogger Logger => _env.Logger;

	/// <summary>
	/// Ensure a router of the same type exists
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TSearch"></typeparam>
	/// <typeparam name="TAssembler"></typeparam>
	/// <returns></returns>
	public void Register<TResult, TSearch>( Func<IRouteAssembler<TResult, TSearch>> ctor )
		where TResult : class
		=> _env.Context.Router<TResult, TSearch>(ctor);

	/// <summary>
	///
	/// </summary>
	/// <param name="mutator"></param>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TSearch"></typeparam>
	public void Append<TResult, TSearch>(LilikoiMutator mutator)
	{
		if (!mutator.Has<RouterAssignments>())
			mutator.Store(new RouterAssignments(_pluginName));

		mutator.Get<RouterAssignments>()?.Add( _env.Context.Router<TResult, TSearch>() );
	}
}
