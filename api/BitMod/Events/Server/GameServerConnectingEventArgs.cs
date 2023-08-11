using System.Net;

namespace BitMod.Events.Server
{
    public class GameServerConnectingEventArgs
    {
        /// <summary>
        /// IP of incoming connection
        /// </summary>
        public IPAddress IPAddress { get; init; }

        /// <summary>
        /// Whether to allow the connection or not.
        /// </summary>
        public bool Allow { get; set; }

        public GameServerConnectingEventArgs(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
            Allow = true;
        }
    }
}
