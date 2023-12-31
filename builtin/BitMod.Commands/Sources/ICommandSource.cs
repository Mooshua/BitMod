﻿using BattleBitAPI.Server;

using BitMod.Compatibility;

namespace BitMod.Commands.Sources;

public interface ICommandSource
{

	/// <summary>
	/// Whether or not this command is from a player
	/// or another source (eg a web panel).
	/// If this is true, then there is a BitPlayer object.
	/// </summary>
	public bool IsRemote { get; }

	/// <summary>
	/// Whether or not this source is authenticated
	/// as a Steam user. This is false if the command
	/// is from an anonymous system like RCON or a web panel.
	/// If this is true, there is a SteamID.
	/// </summary>
	public bool IsAuthenticated { get; }

	/// <summary>
	/// Whether or not the command is associated with
	/// a gameserver.
	/// If this is true, there is a BitServer object.
	/// </summary>
	public bool IsAssociatedWithGameServer { get; }

	/// <summary>
	/// If the source is authenticated, then this will be
	/// their steamid.
	/// </summary>
	public ulong Steam64 { get; }

	/// <summary>
	/// If the command is from a user in a gameserver, this will be that server.
	/// Note: Not always paired with a non-null bitplayer, depending on the source.
	/// </summary>
	public BitServer? GameServer { get; }

	/// <summary>
	/// If the command is from a user in a gameserver, this will be that user.
	/// </summary>
	public BitPlayer? Player { get; }

	/// <summary>
	/// Reply to the source with this message
	/// </summary>
	/// <param name="message"></param>
	public void Reply(string message);

}
