using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class DeepForestHermit : CreatureBase
    {
        public DeepForestHermit()
        {
            manaCost = "3,G,G";
            name = "Deep Forest Hermit";
            subTypes.Add("Elf");
            subTypes.Add("Druid");
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464110&type=card";
            power = 1;
            toughness = 1;
        }

        public override void ETB()
        {
            //Create Squirrels
            SquirrelToken squirrel = new SquirrelToken();
            squirrel.owner = controller;
            squirrel.controller = controller;
            SpawnTokenStackObject stackObj = new SpawnTokenStackObject(squirrel, 4);
            controller.TriggerAbility(stackObj);

            //Vanishing 3
            AddCounter(new TimeCounterCondition(3));

            //Add global +1/+1 to squirrels controller controlls
            TypePredicate pred = new TypePredicate(x => x.subTypes.Contains("Squirrel") && x.controller == controller,this);
            Program.windowInstance.game.AddTypeBasedCondition(pred, new StatModCondition(1, 1, "Special"));
        }

        public override void OnUpkeep(Player p)
        {
            if(p == controller)
            {
                RemoveCounter("time", 1);
                if (QuantityOfCounter("time") == 0)
                    Sacrifice();
            }
        }

        public override string GetTranscript()
        {
            return @"Card Name:
Deep Forest Hermit
Mana Cost:
3GreenGreen
Converted Mana Cost:
5
Types:
Creature — Elf Druid
Card Text:
Vanishing 3 (This creature enters the battlefield with three time counters on it. At the beginning of your upkeep, remove a time counter from it. When the last is removed, sacrifice it.)
When Deep Forest Hermit enters the battlefield, create four 1/1 green Squirrel creature tokens.
Squirrels you control get +1/+1.
P/T:
1 / 1";
        }
    }
}
