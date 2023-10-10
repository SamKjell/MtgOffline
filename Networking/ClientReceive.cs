using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MtgOffline.EnderScript.LW;

namespace MtgOffline.Networking
{
    public class ClientReceive
    {
        public static void Welcome(Packet packet)
        {
            string msg = packet.ReadString();
            int myId = packet.ReadInt();

            Client.instance.myId = myId;
            ClientSend.WelcomeReceived();
           
            //Let the player know they have connected to the server
            Action action = new Action(() =>
            {
                Setup.networkNotice.SetTitle("You are in!");
                Setup.networkNotice.SetDesc("Your game will begin when the host starts the game.");
            });
            Setup.networkNotice.Invoke(action);
        }

        public static void StartingPlayerData(Packet packet)
        {
            string playerEsBasic = packet.ReadString();
            int id = packet.ReadInt();
            Bitmap icon = packet.ReadBitmap();

            ServerPlayer p = NetworkHandler.StringToPlayer(playerEsBasic);
            p.id = id;
            p.startingHealth = p.health;
            p.icon = icon;

            if (Client.instance.myId != id)
                Setup.serverPlayers.Insert(0, p);
            else
                Setup.serverPlayers.Add(p);
        }

        public static void StackObject(Packet packet)
        {
            string esBasic = packet.ReadString();

            Action action = new Action(() => {
                CMDStackObject stackObj = NetworkHandler.StringToStackObject(esBasic);
                Program.windowInstance.game.stack.Add(stackObj);
                Program.windowInstance.UpdateStackConsole();
            });
            Program.windowInstance.Invoke(action);
        }

        public static void CardStackObject(Packet packet)
        {
            string esBasic = packet.ReadString();

            Action action = new Action(() =>
            {
                CardStackObject stackObj = new CardStackObject(NetworkHandler.StringToCardObject(esBasic));
                Program.windowInstance.game.stack.Add(stackObj);
                Program.windowInstance.UpdateStackConsole();
            });
            Program.windowInstance.Invoke(action);
        }

        public static void ESTrigger(Packet packet)
        {
            string esBasic = packet.ReadString();
            string[] esValues = LWEnderScriptUtils.GetValues(esBasic);

            string cmd = LWEnderScriptUtils.FindString(esValues, "cmd_identifier", null);
            if (cmd == null) return;
            else if (cmd == "lose")
                Lose(esValues);
            else if (cmd == "createcard")
                CreateCard(esBasic);
            else if (cmd == "getblocks")
                GetBlocks(esValues);
            else if (cmd == "setblocks")
                SetBlocks(esValues);
            else if (cmd == "editcard")
                EditCard(esValues);
        }

        public static void Trigger(Packet packet)
        {
            string msg = packet.ReadString();
            string[] syntax = msg.Split(' ');

            if (msg == "stackclear")
                StackClear();
            else if (msg == "nextturn")
                NextTurn();
            else if (syntax[0] == "startgame")
                StartGame(int.Parse(syntax[1]));
            else if (syntax[0] == "heal")
                Heal(int.Parse(syntax[1]), int.Parse(syntax[2]));
            else if (syntax[0] == "damageplayer")
                DamagePlayer(int.Parse(syntax[1]), int.Parse(syntax[2]));
            else if (syntax[0] == "damagecard")
                DamageCard(int.Parse(syntax[1]), int.Parse(syntax[2]),syntax.Length==4?bool.Parse(syntax[3]):false);
            else if (syntax[0] == "stackremove")
                StackRemove(int.Parse(syntax[1]));
            else if (syntax[0] == "destroy")
                DestoryCard(int.Parse(syntax[1]));
            else if (syntax[0] == "exile")
                ExileCard(int.Parse(syntax[1]));
            else if (syntax[0] == "tohand")
                ReturnCardToHand(int.Parse(syntax[1]));
            else if (syntax[0] == "tolibrary")
                ReturnCardToLibrary(int.Parse(syntax[1]), int.Parse(syntax[2]));
            else if (syntax[0] == "tap")
                SetTappedCard(int.Parse(syntax[1]), true);
            else if (syntax[0] == "untap")
                SetTappedCard(int.Parse(syntax[1]), false);
            else if (syntax[0] == "setturn")
                SetTurn(int.Parse(syntax[1]));

        }
        //Trigger Methods:
        private static void EditCard(string[] esValues)
        {
            Action action = new Action(() =>
            {
                int id = LWEnderScriptUtils.FindInt(esValues, "card_id", -1);
                CardObject card = NetworkHandler.StringToCardObject(LWEnderScriptUtils.FindString(esValues, "card_es_basic", null));
                foreach(Player player in Program.windowInstance.game.players)
                {
                    foreach (CardObject c in player.battlefield)
                    {
                        if (c.id == id)
                        {
                            player.battlefield[player.battlefield.IndexOf(c)] = card;
                            Program.windowInstance.UpdateGameConsole();
                            Program.windowInstance.UpdateInspector();
                            return;
                        }
                    }
                }
            });
            Program.windowInstance.Invoke(action);
        }

        private static void SetBlocks(string[] esValues)
        {
            Action action = new Action(() =>
            {
                (Program.windowInstance.GetLocalPlayer() as ServerPlayer).AddToBlocks(esValues);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void GetBlocks(string[] esValues)
        {
            Action action = new Action(() =>
            {
                (Program.windowInstance.GetLocalPlayer() as ServerPlayer).RequestBlocks(esValues);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void NextTurn()
        {
            Action action = new Action(() =>
            {
                Program.windowInstance.game.NextTurn();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void SetTurn(int turn)
        {
            Action action = new Action(() =>
            {
                Program.windowInstance.game.turn = turn;
                Program.windowInstance.game.players[turn].PlayerUntapStep();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void SetTappedCard(int id, bool tap)
        {
            Action action = new Action(() =>
            {
                if (tap)
                    NetworkHandler.GetCardFromId(id).Tap();
                else
                    NetworkHandler.GetCardFromId(id).Untap();
                Program.windowInstance.UpdateGameConsole();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void ReturnCardToLibrary(int id, int valueFromTop)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetCardFromId(id).ReturnToOwnerLibrary(valueFromTop);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void ReturnCardToHand(int id)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetCardFromId(id).ReturnToOwnerHand();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void ExileCard(int id)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetCardFromId(id).Exile();
            });
            Program.windowInstance.Invoke(action);
        }
        private static void DestoryCard(int id)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetCardFromId(id).Destroy();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void StartGame(int startingTurn)
        {
            Setup.startingTurn = startingTurn;
            Setup.networkNotice.Close();
        }

        private static void Heal(int id,int amount)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetPlayerFromId(id).Heal(amount);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void StackRemove(int id)
        {
            Action action = new Action(() =>
            {
                Program.windowInstance.game.stack.RemoveAll(x => x.id == id);
                Program.windowInstance.UpdateStackConsole();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void StackClear()
        {
            Action action = new Action(() =>
            {
                Program.windowInstance.game.stack.Clear();
                Program.windowInstance.UpdateStackConsole();
            });
            Program.windowInstance.Invoke(action);
        }

        private static void DamagePlayer(int id, int amount)
        {
            Action action = new Action(() =>
            {
                NetworkHandler.GetPlayerFromId(id).TakeDamage(amount);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void DamageCard(int id, int amount, bool deathtouch = false)
        {
            Action action = new Action(() =>
            {
                CardObject c = NetworkHandler.GetCardFromId(id);
                if (c is CreatureBase)
                    (c as CreatureBase).Damage(amount,deathtouch);
            });
            Program.windowInstance.Invoke(action);
        }

        private static void Lose(string[] esValues)
        {
            int id = LWEnderScriptUtils.FindInt(esValues, "id", 0);
            Player p = NetworkHandler.GetPlayerFromId(id);
            string reason = LWEnderScriptUtils.FindString(esValues, "reason", null);
            if (reason != null)
                reason = $"{p.name} " + reason;
            else
                reason = p.name + " Has lost.";

            p.Lose(reason);
        }

        private static void CreateCard(string esBasic)
        {
            Action action = new Action(() =>
            {
                CardObject c = NetworkHandler.StringToCardObject(esBasic);
                c.controller.EnterTheBattlefield(c);
            });
            Program.windowInstance.Invoke(action);
        }
    }
}
