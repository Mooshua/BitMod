using System.Numerics;

using BattleBitAPI.Common;

namespace BitMod.Events.Result;

public class SpawnRequest
{
	public SpawnRequest(OnPlayerSpawnArguments request)
	{
		_request = request;
	}

	private OnPlayerSpawnArguments _request;

	public PlayerSpawningPosition RequestedPoint
	{
		get => _request.RequestedPoint;
		set => _request.RequestedPoint = value;
	}

	public PlayerLoadout Loadout
	{
		get => _request.Loadout;
		set => _request.Loadout = value;
	}

	public PlayerWearings Wearings
	{
		get => _request.Wearings;
		set => _request.Wearings = value;
	}

	public Vector3 SpawnPosition
	{
		get => _request.SpawnPosition;
		set => _request.SpawnPosition = value;
	}

	public Vector3 LookDirection
	{
		get => _request.LookDirection;
		set => _request.LookDirection = value;
	}

	public PlayerStand SpawnStand
	{
		get => _request.SpawnStand;
		set => _request.SpawnStand = value;
	}

	public float SpawnProtection
	{
		get => _request.SpawnProtection;
		set => _request.SpawnProtection = value;
	}

	public static implicit operator OnPlayerSpawnArguments(SpawnRequest req) => req._request;

}
