using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MtgOffline.Mod_Framework
{
    public class RegistryEvent
    {
        private List<Deck> modDecks = new List<Deck>();
        private List<Bot> modBots = new List<Bot>();
        private List<GameMode> modGameModes = new List<GameMode>();
        private Dictionary<string, Bitmap> modImageResources = new Dictionary<string, Bitmap>();

        public void Register(Deck deck)
        {
            modDecks.Add(deck);
        }

        public void Register(Bot bot)
        {
            modBots.Add(bot);
        }

        public void Register(GameMode mode)
        {
            modGameModes.Add(mode);
        }

        public void Register(string resourceId, Bitmap image)
        {
            modImageResources.Add(resourceId, image);
        }


        public List<Deck> GetRegisteredDecks()
        {
            return modDecks;
        }

        public List<Bot> GetRegisteredBots()
        {
            return modBots;
        }

        public List<GameMode> GetRegisteredGameModes()
        {
            return modGameModes;
        }

        public Bitmap GetImageFromId(string id)
        {
            return modImageResources[id];
        }
    }
}
