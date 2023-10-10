using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class GameRuleHandler
    {
        public static bool HandleGameRuleMill(Player player)
        {
            string millRule = Program.windowInstance.game.gameMode.millRule;
            switch (millRule)
            {
                case "standard":
                    return true;
                case "recycle":
                    if (player.graveyard.Count > 0)
                    {
                        List<CardObject> gy = Simulation.CloneList(player.graveyard);
                        foreach (CardObject card in gy)
                            card.ReturnToOwnerLibrary(0, "graveyard");
                        player.ShuffleLibrary();
                        return false;
                    }
                    return true;
                default:
                    GameRuleEventArgs args = new GameRuleEventArgs();
                    Mod_Framework.EventBus.PostEvent("gamerule_onMill", player,args);
                    return !args.cancelEvent;
            }
        }
    }

    public class GameRuleEventArgs : EventArgs
    {
        public bool cancelEvent;
    }
}
