using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MtgOffline.Networking
{
    public class Client
    {
        public static Client instance;
        public static int dataBufferSize = 4094;

        public string ip = "127.0.0.1";
        public int port = 1313;
        public int myId = 0;
        public Tcp tcp;

        private bool isConnected = false;
        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        public Client(int port, string ip)
        {
            this.port = port;
            this.ip = ip;
        }

        public void Start()
        {
            tcp = new Tcp();
            
        }

        public void ConnectToServer()
        {
            isConnected = true;
            InitializeClientData();
            tcp.Connect();
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                if (Program.windowInstance!=null && Program.windowInstance.connectedToServer)
                {
                    Program.windowInstance.Close();
                    Program.windowInstance = null;
                    Notice notice = new Notice("Disconnected", "You have lost your connection to the server.");
                    notice.ShowDialog();
                }
                if (Setup.networkNotice != null)
                    Setup.networkNotice.Close();
                isConnected = false;
                if(tcp.socket!=null)
                    tcp.socket.Close();
            }
        }

        public bool IsConnected() { return isConnected; }

        public class Tcp
        {
            public TcpClient socket;

            private NetworkStream stream;
            private Packet receiveData;
            private byte[] receiveBuffer;

            public void Connect()
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };
                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
            }

            private void Disconnect()
            {
                stream = null;
                receiveData = null;
                receiveBuffer = null;
                socket = null;
            }

            private void ConnectCallback(IAsyncResult result)
            {
                try
                {
                    socket.EndConnect(result);
                }
                catch (SocketException e)
                {
                    //No server:
                    NetworkHandler.NoServer();
                    return;
                }

                //socket.EndConnect(result);

                if (!socket.Connected) return;

                stream = socket.GetStream();

                receiveData = new Packet();

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            public void SendData(Packet packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error sending data to server: {e}");
                }
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    int byteLength = stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        instance.Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    receiveData.Reset(HandleData(data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch
                {
                    Disconnect();
                }
            }

            private bool HandleData(byte[] data)
            {
                int packetLength = 0;

                receiveData.SetBytes(data);

                if(receiveData.UnreadLength() >= 4)
                {
                    packetLength = receiveData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }
                while(packetLength > 0 && packetLength <= receiveData.UnreadLength())
                {
                    byte[] packetBytes = receiveData.ReadBytes(packetLength);
                    using(Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet);

                    }

                    packetLength = 0;
                    if (receiveData.UnreadLength() >= 4)
                    {
                        packetLength = receiveData.ReadInt();
                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (packetLength <= 1) return true;
                return false;
            }
        }

        private void InitializeClientData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>();
            packetHandlers.Add((int)ServerPackets.welcome, ClientReceive.Welcome);
            packetHandlers.Add((int)ServerPackets.playerBasic, ClientReceive.StartingPlayerData);
            packetHandlers.Add((int)ServerPackets.trigger, ClientReceive.Trigger);
            packetHandlers.Add((int)ServerPackets.stackObject, ClientReceive.StackObject);
            packetHandlers.Add((int)ServerPackets.esTrigger, ClientReceive.ESTrigger);
            packetHandlers.Add((int)ServerPackets.cardStackObject, ClientReceive.CardStackObject);
        }
    }
}
