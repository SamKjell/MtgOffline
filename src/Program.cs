using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtgOffline
{
    public static class Program
    {
        public static Window windowInstance;
        static string version = "v1.1.2";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(OnAppClose);
            Application.Run(new Setup());
        }

        public static string GetVersion()
        {
            return version;
        }


        public static string GenerateCredits()
        {
            List<string> lines = new List<string>();
            lines.Add("Credits:");
            lines.Add("Created by Sam Kjell");
            lines.Add("Cards and Game by Wizards of the Coast");

            return string.Join("\n", lines);
        }

        public static string GenerateChangelog()
        {
            List<string> lines = new List<string>();
            lines.Add("Changelog:");
            lines.Add("\n");

            //v1.1.2
            lines.Add("Version 1.1.2");
            lines.Add("\n");
            lines.Add("[Features]");
            lines.Add("-Overall minor improvements such as the support for more keywords and modding prospects.");
            lines.Add("");
            lines.Add("[Bug Fixes]");
            lines.Add("-Fixed MTGO-10: (The image gathered online was often incorrect.)");
            lines.Add("");
            lines.Add("[Technical Changes]");
            lines.Add("-Added a version requirement to mods, so that mods that are not compatible with the current game version will be ignored.");
            lines.Add("-Updated the Light Weight EnderScript system to the actual ES library. Now certain names will not break the parsing function.");

            lines.Add("\n");
            //v1.1.1
            lines.Add("Version 1.1.1");
            lines.Add("\n");
            lines.Add("[Features]");
            lines.Add("-Added modding support!");
            lines.Add("-Mod folders can be placed into the mods folder to add new cards, decks, etc. into the game.*");
            lines.Add("*Download at your own risk from trustworthy sites only.");
            lines.Add("-Documentation and modding tools will be deployed as soon as they are properly developed, but for now, official mods only.");
            lines.Add("-Added support for the Spellbook gamemode, and a spellbook deck: Squirrel Horde.");
            lines.Add("");
            lines.Add("[Bug Fixes]");
            lines.Add("-Fixed MTGO-6: (Trying to create the type planeswalker crashes the game.)");
            lines.Add("-Fixed MTGO-7: (The AI doesn't mulligan.)");
            lines.Add("-Fixed MTGO-8: (The AI doesn't discard down to hand size at the end of its turn.)");
            lines.Add("-Fixed MTGO-9: (The game crashes when a player is defeated by an attacking creature(s).)");
            lines.Add("-Fixed MTGO-4: (Card Frostwalk Bastion is able to change into a creature using mana it tapped to create and then tap on the same turn.)");
            lines.Add("");
            lines.Add("[Technical Changes]");
            lines.Add("-Added a AI behavior to cull out duplicate plays therefore reducing computations and game delay.");
            lines.Add("-Modified the mana handling system to allow the AI to float mana. It won't do this intelligently, but it will use all provided mana if possible.");
            lines.Add("-Significantly improved combat AI, and added optimizations to allow the AI to simulate its turn faster.");

            lines.Add("\n");
            //v1.1.0
            lines.Add("Version 1.1.0");
            lines.Add("\n");
            lines.Add("[Features]");
            lines.Add("-Added a basic server that will allow games to be played with others across a network connection.");
            lines.Add("(This connection is via a hosted server that will require port forwarding to play with people not connected to your network.)");
            lines.Add("-Added a basic prompt that will allow a user to specify a port and an ip address to connect to a server.");
            lines.Add("");
            lines.Add("[Bug Fixes]");
            lines.Add("-Fixed a bug with line endings for the EnderScript buffer to allow for more readable ES.");
            lines.Add("");
            lines.Add("[Technical Changes]");
            lines.Add("-Altered the EnderScript parsing system to allow for ','s to be placed within a string without breaking the parsing algorithm.");
            lines.Add("-Altered the EnderScript parsing system to allow for nested EnderScript to be stored as a string without breaking said system.");

            lines.Add("\n");
            //v1.0.1
            lines.Add("Version 1.0.1");
            lines.Add("\n");
            lines.Add("[Features]");
            lines.Add("-The setting 'light notifications' now includes whether or not the sample text is displayed in the card creator.");
            lines.Add("-Added 'Copy', 'Cut', and 'Paste' controls to allow for card duplication.");
            lines.Add("-Added the ability for the program to attempt to determine the card you are creating based on its name and then match a card image to it.");
            lines.Add("(This is toggleable via the setting 'load online card images' as this loading can sometimes be slow depending on wifi connection.)");
            lines.Add("");
            lines.Add("[Bug Fixes]");
            lines.Add("-Fixed MTGO-0: (Card Blizzard Strix was able to target itself with its ETB effect.)");
            lines.Add("-Fixed MTGO-1: (Card Abominable Treefolk's ETB didn't tap the creature it targeted.)");
            lines.Add("-Fixed MTGO-2: (The card creator would crash the game if the card was missing a subtype.)");
            lines.Add("-Fixed MTGO-3: (Multiple creatures attacking could all be blocked by a single blocker.)");
            lines.Add("");
            lines.Add("[Technical Changes]");
            lines.Add("-Added the ability for the EnderScript to update its files when it receives a new parameter.");

            lines.Add("\n");
            //v1.0.0
            lines.Add("Version 1.0.0");
            lines.Add("\n");
            lines.Add("[Features]");
            lines.Add("-First official version of Mtg Offline.");
            lines.Add("-Has one bot: Marit Lage");
            lines.Add("-Has one deck for the bot to play: 'Winter Wonderland' by Sam Kjell");
            lines.Add("-Has one game mode: 1v1. More to come! (Note only 1v1 is supported so if you try to make a custom game mode");
            lines.Add("with more people, the program will do nothing on AI turns as the AI cannot handle three people yet.)");
            lines.Add("-Has framework support for custom bots and game modes, decks, however, are not supported yet.*");
            lines.Add("*(Nobody reads footnotes so I put it below so you would read it. Did it work?) Since each card needs to be");
            lines.Add("individually programmed to make a deck, creating custom decks wouldn't be viable with the number of cards");
            lines.Add("that I have already programmed until either enough cards would be implemented, or I added modder support.");

            return string.Join("\n", lines);
        }

        public static void OnAppClose(object sender, EventArgs e)
        {
            if(Networking.Client.instance!=null)
                Networking.Client.instance.Disconnect();
        }
    }
}
