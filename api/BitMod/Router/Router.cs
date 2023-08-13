using System;
using System.Collections.Generic;

using Lilikoi.Compiler.Public;

namespace BitMod.Router;

/// <summary>
/// A router is a utility class which manages
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TSearch"></typeparam>
/// <typeparam name="TRouter"></typeparam>
public class Router<TResult, TSearch> : BaseRouter
	where TResult: class
{
	private IRouteAssembler<TResult, TSearch> _router;

	private Dictionary<string, List<LilikoiContainer>> _containers = new();

	private Dictionary<TSearch, TResult> _routes = new();

	private bool _pendingAssembly = false;

	public Router(IRouteAssembler<TResult, TSearch> router)
	{
		_router = router;
	}

	public override Type Search => typeof(TSearch);

	public override Type Result => typeof(TResult);

	internal override void Add(string name, IEnumerable<LilikoiContainer> containers)
	{
		_pendingAssembly = true;
		if (_containers.TryGetValue(name, out var container))
			container.AddRange(containers);
		else
			_containers[name] = new List<LilikoiContainer>(containers);
	}

	internal override void Remove(string name)
	{
		_pendingAssembly = true;
		if (_containers.TryGetValue(name, out var container))
			container.Clear();
	}

	private void Rebuild()
	{
		var bin = new Dictionary<TSearch, List<LilikoiContainer>>();

		foreach (var (plugin, value) in _containers)
		{
			foreach (LilikoiContainer lilikoiContainer in value)
			{
				var key = _router.Bin(lilikoiContainer);

				if (bin.TryGetValue(key, out var keyBin))
					keyBin.Add(lilikoiContainer);
				else
					bin[key] = new List<LilikoiContainer>() { lilikoiContainer };
			}
		}

		//	Ensure no reads are made during this time period as the router is in
		//	an undefined state.
		lock (this)
		{
			_routes.Clear();
			foreach (var (search, containers) in bin)
			{
				var final = _router.Assemble(containers);
				_routes.Add(search, final);
			}
			_pendingAssembly = false;
		}
	}

	public TResult? Find(TSearch search)
	{
		if (_pendingAssembly)
			Rebuild();

		lock (this)
		{
			if (_routes.TryGetValue(search, out var result))
				return result;

			return null;
		}
	}
}
