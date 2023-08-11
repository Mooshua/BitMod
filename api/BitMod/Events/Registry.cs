using BattleBitAPI.Common;

using BitMod.Events.Meta;
using BitMod.Events.Player;
using BitMod.Events.Server;
using BitMod.Events.Stats;

namespace BitMod.Events;

public static class Registry
{
	public enum EventType
	{
		/// <summary>
		/// No response type
		/// </summary>
		None,
		/// <summary>
		/// Plugins decide whether the result is "true" or "false".
		/// </summary>
		Hook,
		/// <summary>
		/// Plugins decide which value is used for the return
		/// </summary>
		Value,
	}

	public static Dictionary<Type, EventType> Events = new Dictionary<Type, EventType>()
	{
		//	Player
		{ typeof(PlayerChangedRoleEventArgs), EventType.None },
		{ typeof(PlayerChangedTeamEventArgs), EventType.None },
		{ typeof(PlayerConnectedEventArgs), EventType.None },
		{ typeof(PlayerDiedEventArgs), EventType.None },
		{ typeof(PlayerDisconnectedEventArgs), EventType.None },
		{ typeof(PlayerJoinedSquadEventArgs), EventType.None },
		{ typeof(PlayerKilledPlayerEventArgs), EventType.None },
		{ typeof(PlayerLeftSquadEventArgs), EventType.None },
		{ typeof(PlayerReportedEventArgs), EventType.None },
		{ typeof(PlayerSpawnedEventArgs), EventType.None },
		{ typeof(PlayerSpawningEventArgs), EventType.None },

		{ typeof(PlayerRequestingToChangeRoleEventArgs), EventType.Hook },
		{ typeof(PlayerTypedMessageEventArgs), EventType.Hook },

		//	Gameserver
		{ typeof(GameServerConnectedEventArgs), EventType.None },
		{ typeof(GameServerDisconnectedEventArgs), EventType.None },
		{ typeof(GameServerReconnectedEventArgs), EventType.None },
		{ typeof(GameServerTickEventArgs), EventType.None },

		{ typeof(GameServerConnectingEventArgs), EventType.Hook },

		//	Stats
		{ typeof(GetPlayerStatsEventArgs), EventType.Value },
		{ typeof(SavingPlayerStatsEventArgs), EventType.None },

		//	Meta
		{ typeof(PluginLoadEvent), EventType.None },
		{ typeof(PluginUnloadEvent), EventType.None },
	};

	public static Dictionary<Type, Type> Producers = new Dictionary<Type, Type>()
	{
		{ typeof(GetPlayerStatsEventArgs), typeof(PlayerStats) }
	};

	/// <summary>
	/// Deterministally ordered list of events
	/// </summary>
	public static List<Type> EventTypes = Events.Keys
		.OrderBy(type => type.FullName)
		.ToList();

	public static int EventCount => EventTypes.Count;

	public static int IndexOf(Type ev)
		=> EventTypes.IndexOf(ev);
}
