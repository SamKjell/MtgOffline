using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtgOffline.EnderScript;

namespace MtgOffline.Networking
{
    public class NetworkHandler
    {
        public static bool isConnected = false;
        public static void Join(int port, string ip)
        {
            Client.instance = new Client(port,ip);
            Client.instance.Start();
            Client.instance.ConnectToServer();
            isConnected = true;
        }

        public static void NoServer()
        {
            isConnected = false;
            Action action = new Action(() =>
            {
                Setup.networkNotice.SetTitle("Server Not Found");
                Setup.networkNotice.SetDesc("No server was found.");
                Setup.networkNotice.SetButtonText("Okay");
            });
            Setup.networkNotice.Invoke(action);
        }

        //Utilities
        public static ServerPlayer GetPlayerFromId(int id)
        {
            foreach (Player p in Program.windowInstance.game.players)
            {
                if (p is ServerPlayer && (p as ServerPlayer).id == id)
                    return p as ServerPlayer; 
            }
            return null;
        }

        public static CardObject GetCardFromId(int id)
        {
            foreach (Player p in Program.windowInstance.game.players)
            {
                foreach (CardObject c in p.battlefield)
                {
                    if (c.id == id)
                        return c;
                }
            }
            return null;
        }

        public static string ConcatIds(List<Player> players)
        {
            string s = "";
            bool f = false;
            foreach (Player p in players)
            {
                if(p is ServerPlayer)
                {
                    if (f)
                        s += ",";
                    else
                        f = true;
                    s += (p as ServerPlayer).id;
                }
            }
            return s;
        }

        public static string ConcatIds(List<CardObject> cards)
        {
            string s = "";
            bool f = false;
            foreach (CardObject c in cards)
            {
                if (f)
                    s += ",";
                else
                    f = true;
                s += c.id;
            }
            return s;
        }

        public static string ConcatIds(List<CreatureBase> cards)
        {
            List<CardObject> cObjects = new List<CardObject>();
            foreach (CreatureBase c in cards)
                cObjects.Add(c);
            return ConcatIds(cObjects);
        }

        //Data conversion methods
        public static string AttackDataToStringBasic(AttackData data)
        {
            if (data == null || data.creature == null || data.target == null) return null;
            ESBuffer buffer = new ESBuffer();
            buffer.Add("attacker", data.creature.id);
            buffer.Add("blockers", $"{ConcatIds(data.blockers)}");
            buffer.Add("target", (data.target as ServerPlayer).id);
            return buffer.GetES();
        }

        public static string CardObjectToStringBasic(CardObject card)
        {
            if (card == null) return null;
            ESBuffer buffer = new ESBuffer();
            buffer.Add("name", card.name);
            buffer.Add("card_types", string.Join(" ", card.cardTypes));
            buffer.Add("super_types", string.Join(" ", card.superTypes));
            buffer.Add("sub_types", string.Join(" ", card.subTypes));
            buffer.Add("static_abilities", string.Join(" ", card.staticAbilities));
            buffer.Add("link", card.imageURL);
            buffer.Add("card_id", card.id);
            buffer.Add("controller_id", (card.controller as ServerPlayer).id);
            buffer.Add("owner_id", (card.owner as ServerPlayer).id);
            if(card is CreatureBase)
            {
                buffer.Add("power", (card as CreatureBase).power);
                buffer.Add("toughness", (card as CreatureBase).toughness);
            }

            return buffer.GetES();
        }

        public static string StackObjectToStringBasic(StackObject stackObject)
        {
            if (stackObject == null) return null;
            ESBuffer buffer = new ESBuffer();
            buffer.Add("name", stackObject.name);
            buffer.Add("description", stackObject.description);
            List<int> ids = new List<int>();
            if (stackObject.targetPlayers != null)
            {
                foreach (Player target in stackObject.targetPlayers)
                    ids.Add((target as ServerPlayer).id);
            }
            buffer.Add("target_players", string.Join(",", ids));
            ids.Clear();
            if (stackObject.targetCards != null)
            {
                foreach (CardObject c in stackObject.targetCards)
                    ids.Add(c.id);
            }
            buffer.Add("target_cards", string.Join(",", ids));
            if(stackObject.controller!=null)
                buffer.Add("controller",(stackObject.controller as ServerPlayer).id);
            if (stackObject.owner != null)
                buffer.Add("owner", (stackObject.controller as ServerPlayer).id);
            buffer.Add("cmd_syntax", stackObject.GetCMDSyntax());

            return buffer.GetES();
        }

        public static string PlayerToStringBasic(Player player)
        {
            ESBuffer buffer = new ESBuffer();
            buffer.Add("nickname", player.name);
            buffer.Add("health", player.health);
            return buffer.GetES();
        }

        public static AttackData StringToAttackData(string esBasic)
        {
            ESBuilder builder = new ESBuilder(esBasic);
            ServerPlayer target = GetPlayerFromId(builder.Get("target", 0));
            CreatureBase attacker = GetCardFromId(builder.Get("attacker", -1)) as CreatureBase;
            List<CreatureBase> blockers = new List<CreatureBase>();
            string[] ids = builder.Get("blockers", "").Split(',');
            foreach (string id in ids)
            {
                if (id != "" && id != null && int.TryParse(id, out int i))
                    blockers.Add(GetCardFromId(i) as CreatureBase);
            }

            AttackData data = new AttackData(attacker,target);
            data.blockers = blockers;
            return data;
        }

        public static CardObject StringToCardObject(string esBasic)
        {
            ESBuilder builder = new ESBuilder(esBasic);
            string name = builder.Get("name", "Unnamed Card");
            string[] superTypes = builder.Get("super_types", "").Split(' ');
            string[] cardTypes = builder.Get("card_types", "").Split(' ');
            string[] subTypes = builder.Get("sub_types", "").Split(' ');
            string[] staticAbilities = builder.Get("static_abilities", "").Split(' ');
            string link = builder.Get("link", "");
            int cardId = builder.Get("card_id", -1);
            ServerPlayer controller = GetPlayerFromId(builder.Get("controller_id", 0));
            ServerPlayer owner = GetPlayerFromId(builder.Get("owner_id", 0));
            CardObject card = null;
            List<string> cTypes = cardTypes.ToList();
            if (cTypes.Contains("Creature"))
            {
                CreatureBase template = new CreatureBase();
                int power = builder.Get("power", 0);
                int toughness = builder.Get("toughness", 0);
                template.power = power;
                template.toughness = toughness;
                template.Heal();
                card = template;
            }
            else if (cTypes.Contains("Artifact"))
                card = new ArtifactBase();
            else if (cTypes.Contains("Enchantment"))
                card = new EnchantmentBase();
            else if (cTypes.Contains("Land"))
                card = new LandBase();
            else
                card = new CardObject();

            card.name = name;
            card.superTypes = superTypes.ToList();
            card.cardTypes = cTypes;
            card.subTypes = subTypes.ToList();
            card.staticAbilities = staticAbilities.ToList();
            card.imageURL = link;
            card.controller = controller;
            card.owner = owner;
            card.id = cardId;

            return card;
        }

        public static CMDStackObject StringToStackObject(string esBasic)
        {
            ESBuilder builder = new ESBuilder(esBasic);
            string name = builder.Get("name", "Unnamed StackObject");
            string desc = builder.Get("description", "THIS SHOULDN'T BE HAPPENING!!");
            string listOfPlayerIds = builder.Get("target_players", "");
            string listOfCardIds = builder.Get("target_cards","");
            int controllerId = builder.Get("controller", -1);
            int ownerId = builder.Get("owner", -1);
            string cmdSyntax = builder.Get("cmd_syntax", "");

            CMDStackObject stackObj = new CMDStackObject(cmdSyntax);
            stackObj.name = name;
            stackObj.description = desc;
            stackObj.targetPlayers = new List<Player>();
            foreach (string id in listOfPlayerIds.Split(','))
            {
                if (id == "") continue;
                ServerPlayer p = GetPlayerFromId(int.Parse(id));
                if (p != null)
                    stackObj.targetPlayers.Add(p);
            }
            stackObj.targetCards = new List<CardObject>();
            foreach (string id in listOfCardIds.Split(','))
            {
                if (id == "") continue;
                CardObject c = GetCardFromId(int.Parse(id));
                if (c != null)
                    stackObj.targetCards.Add(c);
            }
            ServerPlayer controller = GetPlayerFromId(controllerId);
            stackObj.controller = controller;
            ServerPlayer owner = GetPlayerFromId(ownerId);
            stackObj.owner = owner;

            return stackObj;
        }

        public static ServerPlayer StringToPlayer(string esBasic)
        {
            ESBuilder builder= new ESBuilder(esBasic);
            string nickname = builder.Get("nickname", "Player 1");
            int health = builder.Get("health", 0);
            ServerPlayer p = new ServerPlayer();
            p.name = nickname;
            p.health = health;
            return p;
        }
    }
}
