using MtgOffline.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class Deck
    {
        public string deckName;
        public string author;
        public List<CardObject> cards = new List<CardObject>();

        public Deck(string name, string author)
        {
            deckName = name;
            this.author = author;
        }
        public virtual void LoadDeck()
        {

        }
        public void UnloadDeck()
        {
            cards.Clear();
        }
    }

    //Standard Decks
    public class DeckWinterWonderland : Deck
    {
        public DeckWinterWonderland():base("Winter Wonderland","Sam Kjell") {}

        public override void LoadDeck()
        {
            cards.Add(new FrostwalkBastion());
            cards.Add(new FrostwalkBastion());
            cards.Add(new FrostwalkBastion());
            cards.Add(new BlizzardStrix());
            cards.Add(new BlizzardStrix());
            cards.Add(new BlizzardStrix());
            cards.Add(new BlizzardStrix());
            cards.Add(new AbominableTreefolk());
            cards.Add(new AbominableTreefolk());
            cards.Add(new ArcumAstrolabe());
            cards.Add(new ArcumAstrolabe());
            cards.Add(new ArcumAstrolabe());
            cards.Add(new ArcumAstrolabe());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new SnowIsland());
            cards.Add(new ConiferWurm());
            cards.Add(new ConiferWurm());
            cards.Add(new ConiferWurm());
            cards.Add(new DeadOfWinter());
            cards.Add(new Chillerpillar());
            cards.Add(new Chillerpillar());
            cards.Add(new Chillerpillar());
            cards.Add(new Chillerpillar());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new SnowForest());
            cards.Add(new WintersRest());
            cards.Add(new WintersRest());
            cards.Add(new GlacialRevelation());
            cards.Add(new GlacialRevelation());
            cards.Add(new GlacialRevelation());
            cards.Add(new IcefangCoatl());
            cards.Add(new IcefangCoatl());
            cards.Add(new IcebergCancrix());
            cards.Add(new IcebergCancrix());
            cards.Add(new IcebergCancrix());
            cards.Add(new IcebergCancrix());
            cards.Add(new MaritLagesSlumber());
            cards.Add(new MaritLagesSlumber());
            cards.Add(new IcehideGolem());
            cards.Add(new IcehideGolem());
            cards.Add(new IcehideGolem());
            cards.Add(new Frostwalla());
            cards.Add(new Frostwalla());
            cards.Add(new Frostwalla());
            cards.Add(new Frostwalla());
        }
    }

    //Spellbook Decks
    public class SquirrelHorde : Deck
    {
        public SquirrelHorde(): base("Squirrel Horde (Spellbook)", "Sam Kjell"){}
        public override void LoadDeck()
        {
            cards.Add(new Forest());
            cards.Add(new Forest());
            cards.Add(new Forest());
            cards.Add(new Forest());
            cards.Add(new Forest());
            cards.Add(new Forest());
            cards.Add(new ScaleUp());
            cards.Add(new ScaleUp());
            cards.Add(new ScaleUp());
            cards.Add(new CallTheScions());
            cards.Add(new SquirrelNest());
            cards.Add(new SquirrelNest());
            cards.Add(new SquirrelNest());
            cards.Add(new DeepForestHermit());
            cards.Add(new DeepForestHermit());
        }
    }
}
