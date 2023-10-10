using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public class GameMode
    {
        public string modeName;
        public string resourceLocation;
        public int numberOfPlayers;
        public int startingHealth;
        public int startingHandSize;
        public string millRule;

        public GameMode(string name, string resourceLocation)
        {
            modeName = name;
            this.resourceLocation = resourceLocation;
        }
    }

    public class GameModeSerializer
    {
        public string fileLocation;

        public GameModeSerializer(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public GameMode ReadGameMode()
        {
            if (!File.Exists(fileLocation)) return null;
            ESBuilder builder = new ESBuilder(File.ReadAllText(fileLocation));
            string gameName = builder.Get("name", null);
            if (gameName == null) return null;
            GameMode mode = new GameMode(gameName, fileLocation);
            mode.numberOfPlayers = builder.Get("player_count", 2);
            mode.startingHealth = builder.Get("starting_health", 20);
            mode.startingHandSize = builder.Get("starting_hand_size", 7);
            mode.millRule = builder.Get("mill_rule", "standard");

            return mode;
        }
    }
}
