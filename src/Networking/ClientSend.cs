using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Networking
{
    public class ClientSend
    {

        public static void WelcomeReceived()
        {
            using(Packet packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                packet.Write(Client.instance.myId);
                packet.Write(Settings.nickname);
                packet.Write(Settings.playerIcon==null? Properties.Resources.DefaultPlayerIcon:Settings.playerIcon);
                packet.Write(Program.GetVersion());

                SendTCPData(packet);
            }
        }

        public static void StackPacket(StackObject stackObj)
        {
            if(stackObj is CardStackObject)
            {
                CardPacket((stackObj as CardStackObject).card);
                return;
            }
            using(Packet packet = new Packet((int)ClientPackets.stackPacket))
            {
                string esBasic = NetworkHandler.StackObjectToStringBasic(stackObj);
                if (esBasic == null) return;

                packet.Write(esBasic);
                SendTCPData(packet);
            }
        }

        public static void Trigger(string msg)
        {
            using(Packet packet = new Packet((int)ClientPackets.trigger))
            {
                packet.Write(msg);
                SendTCPData(packet);
            }
        }

        public static void TriggerES(string esBasic)
        {
            using(Packet packet = new Packet((int)ClientPackets.esTrigger))
            {
                packet.Write(esBasic);
                SendTCPData(packet);
            }
        }

        public static void CardPacket(CardObject card)
        {
            using(Packet packet = new Packet((int)ClientPackets.cardPacket))
            {
                string esBasic = NetworkHandler.CardObjectToStringBasic(card);
                if (esBasic == null) return;

                packet.Write(esBasic);
                SendTCPData(packet);
            }
        }

        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.tcp.SendData(packet);
        }
    }
}
