using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Cards
{
    public class SquirrelNest : AuraBase
    {
        public SquirrelNest()
        {
            manaCost = "1,G,G";
            name = "Squirrel Nest";
            imageURL = "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=464131&type=card";
            targetPred = x => x.CardContainsType("Land")&& x.controller == controller;
            negativeEffects = false;
            SetToRandom();
            effects = new AbilityCondition(new SquirrelNestSpawnAbility(), "T: Create a 1/1 green Squirrel creature token.");
        }

        public override string GetTranscript()
        {
            return @"Card Name:
Squirrel Nest
Mana Cost:
1GreenGreen
Converted Mana Cost:
3
Types:
Enchantment — Aura
Card Text:
Enchant land
Enchanted land has 'Tap: Create a 1 / 1 green Squirrel creature token.'";
        }
    }

    public class SquirrelNestSpawnAbility : Ability
    {
        public SquirrelNestSpawnAbility()
        {
            cost = "T";
        }
        public override void AdditionalActions()
        {
            SquirrelToken squirrel = new SquirrelToken();
            squirrel.owner = source.controller;
            squirrel.controller = source.controller;
            effect = new SpawnTokenStackObject(squirrel);
        }
    }
}
