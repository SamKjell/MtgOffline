using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtgOffline.EnderScript;
using MtgOffline.EnderScript.LW;

namespace MtgOffline.Networking
{
    public class ServerPlayer : Player
    {
        public int id;

        public ServerPlayer()
        {
            isPlayer = true;
        }

        public void RequestBlocks(string[] esValues)
        {
            string[] esBasics = LWEnderScriptUtils.FindString(esValues, "es_basic", null).Split('~');
            List<AttackData> attacks = new List<AttackData>();
            foreach (string esBasic in esBasics)
                attacks.Add(NetworkHandler.StringToAttackData(esBasic));

            //Generic display code:
            List<CreatureBase> creatures = new List<CreatureBase>();
            foreach (CreatureBase c in GetCreaturesInLocation(battlefield))
                creatures.Add(c);
            CombatPrompt prompt = new CombatPrompt(attacks, creatures, false);
            prompt.ShowDialog();

            List<AttackData> blocks = prompt.returnData;
            List<string> blockStrings = new List<string>();
            foreach (AttackData a in blocks)
                blockStrings.Add(NetworkHandler.AttackDataToStringBasic(a));

            LWEnderScriptUtils.OverwriteString(esValues, "es_basic", string.Join("~", blockStrings));
            LWEnderScriptUtils.OverwriteString(esValues, "cmd_identifier", "setblocks");

            ClientSend.TriggerES(LWEnderScriptUtils.GetES(esValues));
        }

        public void AddToBlocks(string[] esValues)
        {
            string[] esBasics = LWEnderScriptUtils.FindString(esValues, "es_basic", null).Split('~');
            foreach (string esBasic in esBasics)
                AddBlock(NetworkHandler.StringToAttackData(esBasic));
        }
    }
}
