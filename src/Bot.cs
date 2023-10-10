using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public class Bot
    {
        public string name;
        public string resourceLocation;
        public Bitmap playerImage;
        public int weightCreaturePower;
        public int weightGraveyardSpells;

        public int aggressionLevel;
        public int favorableLands;

        public Bot(string name,string resourceLocation) {
            this.name = name;
            playerImage = Properties.Resources.DefaultPlayerIcon;
        }
    }

    public class BotSerializer
    {
        public string folderLocation;
        public string fileLocation;
        public string imageLocation;
        public bool isValid = true;
        public string invalidityReason = "";

        public BotSerializer(string botLocation)
        {
            folderLocation = botLocation;
            bool hasBot = false;
            bool hasIcon = false;
            foreach (string s in Directory.GetFiles(botLocation))
            {
                if (s.EndsWith(".es"))
                {
                    if (hasBot)
                    {
                        isValid = false;
                        invalidityReason = "More than one .es file was found at: "+ botLocation;
                        return;
                    }
                    hasBot = true;
                    fileLocation = s;
                }
                else if (s.EndsWith(".png")){
                    if (hasIcon)
                    {
                        isValid = false;
                        invalidityReason = "More than one image file was found at: " + botLocation;
                        return;
                    }
                    hasIcon = true;
                    imageLocation = s;
                }
            }
            if (!hasBot)
            {
                isValid = false;
                invalidityReason = "No .es file was found at: " + botLocation;
                return;
            }
        }


        public Bot ReadBot()
        {
            if (isValid)
            {
                //Try to get the image
                Bitmap image = null;
                if (File.Exists(imageLocation))
                    image = new Bitmap(imageLocation);

                //Try to read the EnderScrip bot file
                ESBuilder builder;
                if (File.Exists(fileLocation))
                    builder = new ESBuilder(File.ReadAllText(fileLocation));
                else
                    return null;
                string botName = builder.Get("name", "Enderbot");
                Bot bot = new Bot(botName, folderLocation);
                if (image != null)
                    bot.playerImage = image;

                //Reads weights:

                bot.weightCreaturePower = builder.Get("total_creature_power", 1);
                bot.weightGraveyardSpells = builder.Get("graveyard_spells", 1);

                //Other attributes

                bot.aggressionLevel = builder.Get("aggression_level", 0);
                bot.favorableLands = builder.Get("favorable_land_count", 3);

                return bot;
            }
            else
                throw new IOException("An invalid bot is trying to be deserialized. This shouldn't be occurring. FIX IT!!");
        }
    }
    

}
