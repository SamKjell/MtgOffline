using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class MaritLagesSlumber : EnchantmentBase
    {
        public MaritLagesSlumber()
        {
            manaCost = "1,U";
            name = "Marit Lage's Slumber";
            superTypes.Add("Legendary");
            superTypes.Add("Snow");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464005&type=card";
        }
        public override void CardEntersTheBattlefield(CardObject c)
        {
            if(c.superTypes.Contains("Snow") && c.controller == controller)
            {
                ScryStackObject trigger = new ScryStackObject(controller, 1);
                controller.TriggerAbility(trigger);
            }
        }
        public override void OnUpkeep(Player p)
        {
            if (p == controller && Player.CountBySuperType("Snow", controller.battlefield) >= 10)
            {
                //Sacrifice and create 20/20
                CreatureBase card = new CreatureBase();
                card.name = "Marit Lage";
                card.superTypes.Add("Legenedary");
                card.cardTypes.Insert(0, "Token");
                card.subTypes.Add("Avatar");
                card.power = 20;
                card.toughness = 20;
                card.imageURL = "https://6d4be195623157e28848-7697ece4918e0a73861de0eb37d08968.ssl.cf1.rackcdn.com/191607_200w.jpg";
                card.controller = controller;
                card.owner = controller;
                card.staticAbilities.Add("Flying");
                card.staticAbilities.Add("Indestructible");

                MaritLageTrigger trigger = new MaritLageTrigger(card, this);
                controller.TriggerAbility(trigger);
            }
        }
        public override string GetTranscript()
        {
            return @"
Card Name:Marit Lage's Slumber
Mana Cost:1Blue
Converted Mana Cost:2
Types:Legendary Snow Enchantment
Card Text:
Whenever Marit Lage's Slumber or another snow permanent enters the battlefield under your control, scry 1.
At the beginning of your upkeep, if you control ten or more snow permanents, sacrifice Marit Lage's Slumber. If you do, create Marit Lage, a legendary 20/20 black Avatar creature token with flying and indestructible.";
        }
    }
    public class MaritLageTrigger : CardStackObject
    {
        public CardObject maritLage;
        public MaritLageTrigger(CardObject card, CardObject source) : base(card)
        {
            maritLage = source;
            name = "Marit Lage's Awakening";
        }
        public override void Resolve()
        {
            if (Player.CountBySuperType("Snow", controller.battlefield) >= 10 && controller.IsCardOnBattleField(maritLage))
            {
                maritLage.Sacrifice();
                base.Resolve();
            }
        }
    }
}
