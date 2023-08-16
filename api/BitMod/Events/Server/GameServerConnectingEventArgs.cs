using System.Net;

using BitMod.Events.Base;

namespace BitMod.Events.Server
{
    public class GameServerConnectingEventArgs : IHookArgs
    {
        /// <summary>
        /// IP of incoming connection
        /// </summary>
        public IPAddress IPAddress { get; }

        public GameServerConnectingEventArgs(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }
    }
}
