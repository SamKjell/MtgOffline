using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using MtgOffline.Networking;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public partial class Setup : Form
    {
        public List<Deck> deckOptions = new List<Deck>();
        public List<Bot> bots = new List<Bot>();
        public List<GameMode> modes = new List<GameMode>();

        public static string appDataPath = "";//(File.Exists("settings.es")) ? "":Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\EnderTek\MtgOffline\";

        public static Game gameInstance;
        public Setup()
        {
            InitializeComponent();
            Text = "Mtg Offline " + Program.GetVersion();
            SetPlayerThreeMode(false);
            SetPlayerFourMode(false);
            RegisterMods();
            RegisterDecks();
            RegisterBots();
            RegisterGameModes();
            InitializeLists();
            Settings.ReadSettings();
            PlayerOneTitle.Text = Settings.nickname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Start the game
            int i = GameModeList.SelectedIndex;
            if (i>=modes.Count || i < 0)
            {
                Notice notice = new Notice("No Game Mode Selected","Please select a game mode!");
                notice.ShowDialog();
                return;
            }
            GameMode currentMode = modes[i];
            List<Player> players = new List<Player>();
            for (int j = 0; j < currentMode.numberOfPlayers; j++)
            {
                if (j == 0) //Create default player
                {
                    Player p1 = new Player(Settings.nickname, currentMode.startingHealth, null, true);
                    p1.icon = Settings.playerIcon;
                    players.Add(p1);
                }
                else if(j== 1)
                {
                    int b = PlayerTwoBots.SelectedIndex;
                    int d = PlayerTwoDecks.SelectedIndex;
                    if (b >= bots.Count || b < 0)
                    {
                        Notice notice = new Notice("Select a Bot", "You have not selected a bot for Player 2.");
                        notice.ShowDialog();
                        return;
                    }
                    if (d >= deckOptions.Count || d < 0)
                    {
                        Notice notice = new Notice("No Deck Selected", "You have not selected a deck for Player 2.");
                        notice.ShowDialog();
                        return;
                    }
                    Player p2 = new Player(bots[b].name, currentMode.startingHealth, deckOptions[d], false, bots[b]);
                    p2.icon = bots[b].playerImage;
                    players.Add(p2);
                }
                else if (j == 2)
                {
                    int b = PlayerThreeBots.SelectedIndex;
                    int d = PlayerThreeDecks.SelectedIndex;
                    if (b >= bots.Count || b < 0)
                    {
                        Notice notice = new Notice("Select a Bot", "You have not selected a bot for Player 3.");
                        notice.ShowDialog();
                        return;
                    }
                    if (d >= deckOptions.Count || d < 0)
                    {
                        Notice notice = new Notice("No Deck Selected", "You have not selected a deck for Player 3.");
                        notice.ShowDialog();
                        return;
                    }
                    Player p3 = new Player(bots[b].name, currentMode.startingHealth, deckOptions[d], false, bots[b]);
                    p3.icon = bots[b].playerImage;
                    players.Add(p3);
                }
                else if (j == 3)
                {
                    int b = PlayerFourBots.SelectedIndex;
                    int d = PlayerFourDecks.SelectedIndex;
                    if (b >= bots.Count || b < 0)
                    {
                        Notice notice = new Notice("Select a Bot", "You have not selected a bot for Player 4.");
                        notice.ShowDialog();
                        return;
                    }
                    if (d >= deckOptions.Count || d < 0)
                    {
                        Notice notice = new Notice("No Deck Selected", "You have not selected a deck for Player 4.");
                        notice.ShowDialog();
                        return;
                    }
                    Player p4 = new Player(bots[b].name, currentMode.startingHealth, deckOptions[d], false, bots[b]);
                    p4.icon = bots[b].playerImage;
                    players.Add(p4);
                }
            }
            players.Reverse();
            if (!ValidateDeckSizes(players, currentMode)) { return; }
            gameInstance = new Game(players, currentMode);
            Window w = new Window();
            Program.windowInstance = w;
            w.Show();
        }

        private bool ValidateDeckSizes(List<Player>players, GameMode mode)
        {
            foreach (Player p in players)
            {
                if (p.isPlayer) continue;
                p.deck.LoadDeck();
                if(p.deck.cards.Count < mode.startingHandSize)
                {
                    Notice notice = new Notice("Deck Too Small",$"The deck: {p.deck.deckName} is too small for the gamemode: {mode.modeName}.");
                    notice.ShowDialog();
                    p.deck.cards.Clear();
                    return false;
                }
                p.deck.cards.Clear();
            }
            return true;
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
        

        void RegisterDecks()
        {
            deckOptions.Add(new DeckWinterWonderland());
            deckOptions.Add(new SquirrelHorde());
            //Register modded decks
            deckOptions.AddRange(Mod_Framework.ModLoader.modRegistries.GetRegisteredDecks());
        }
        void RegisterBots()
        {
           List<BotSerializer> botSerializers = new List<BotSerializer>();
            if (!Directory.Exists(appDataPath + "bots"))
            {
                Notice notice = new Notice("Missing Folder", $"The folder 'bots' is missing at {appDataPath}");
                notice.ShowDialog();
                return;
            }
                
           foreach(string dir in Directory.GetDirectories(appDataPath + "bots"))
            {
                BotSerializer b = new BotSerializer(dir);
                if (b.isValid)
                    botSerializers.Add(b);
                else
                {
                    Notice notice = new Notice("Invalid Bot", b.invalidityReason);
                    notice.ShowDialog();
                }
            }
            foreach (BotSerializer serializer in botSerializers)
            {
                Bot bot = serializer.ReadBot();
                if (bot == null)
                {
                    Notice notice = new Notice("File not Found", "This file was unable to be found at: " + serializer.folderLocation);
                    notice.ShowDialog();
                }
                else
                    bots.Add(bot);
            }
            bots.AddRange(Mod_Framework.ModLoader.modRegistries.GetRegisteredBots());
        }
        void InitializeLists()
        {
            //Bots
            PlayerTwoBots.BeginUpdate();
            PlayerThreeBots.BeginUpdate();
            PlayerFourBots.BeginUpdate();
            PlayerTwoBots.Items.Clear();
            PlayerThreeBots.Items.Clear();
            PlayerFourBots.Items.Clear();
            foreach (Bot bot in bots)
            {
                PlayerTwoBots.Items.Add(bot.name);
                PlayerThreeBots.Items.Add(bot.name);
                PlayerFourBots.Items.Add(bot.name);
            }
            PlayerTwoBots.EndUpdate();
            PlayerThreeBots.EndUpdate();
            PlayerFourBots.EndUpdate();

            //Decks
            PlayerTwoDecks.BeginUpdate();
            PlayerThreeDecks.BeginUpdate();
            PlayerFourDecks.BeginUpdate();
            DeckCollection.BeginUpdate();

            PlayerTwoDecks.Items.Clear();
            PlayerThreeDecks.Items.Clear();
            PlayerFourDecks.Items.Clear();
            DeckCollection.Items.Clear();
            foreach (Deck deck in deckOptions)
            {
                PlayerTwoDecks.Items.Add(deck.deckName);
                PlayerThreeDecks.Items.Add(deck.deckName);
                PlayerFourDecks.Items.Add(deck.deckName);
                DeckCollection.Items.Add(deck.deckName);
            }
            PlayerTwoDecks.EndUpdate();
            PlayerThreeDecks.EndUpdate();
            PlayerFourDecks.EndUpdate();
            DeckCollection.EndUpdate();

            //Game Modes
            GameModeList.BeginUpdate();
            GameModeList.Items.Clear();
            foreach (GameMode gm in modes)
            {
                GameModeList.Items.Add(gm.modeName);
            }
            GameModeList.EndUpdate();
        }

        void RegisterGameModes()
        {
            List<GameModeSerializer> modeSerializers = new List<GameModeSerializer>();
            if (!Directory.Exists(appDataPath + "gamemodes"))
            {
                Notice notice = new Notice("Missing Folder", $"The folder 'gamemodes' is missing at {appDataPath}");
                notice.ShowDialog();
            }
            foreach (string file in Directory.GetFiles(appDataPath + "gamemodes"))
            {
                if (!file.EndsWith(".es")) continue;
                GameModeSerializer g = new GameModeSerializer(file);
                modeSerializers.Add(g);
            }
            foreach (GameModeSerializer serializer in modeSerializers)
            {
                GameMode mode = serializer.ReadGameMode();
                if (mode == null)
                {
                    Notice notice = new Notice("IO Error", "Either this file doesn't exist or no name was present in the .es file at: " + serializer.fileLocation);
                    notice.ShowDialog();
                }
                else
                    modes.Add(mode);
            }

            modes.AddRange(Mod_Framework.ModLoader.modRegistries.GetRegisteredGameModes());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GameModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int g = GameModeList.SelectedIndex;
            if (g >= modes.Count || g < 0) return;
            GameMode mode = modes[g];

            if (mode.numberOfPlayers < 2) mode.numberOfPlayers = 2;
            else if (mode.numberOfPlayers > 4) mode.numberOfPlayers = 4;

            int pCount = mode.numberOfPlayers;

            SetPlayerThreeMode(pCount > 2);
            SetPlayerFourMode(pCount > 3);
        }

        void SetPlayerThreeMode(bool active)
        {
            PlayerThreeBotLabel.Enabled = active;
            PlayerThreeBotLabel.Visible = active;
            PlayerThreeBots.Enabled = active;
            PlayerThreeBots.Visible = active;
            PlayerThreeDeckLabel.Enabled = active;
            PlayerThreeDeckLabel.Visible = active;
            PlayerThreeDecks.Enabled = active;
            PlayerThreeDecks.Visible = active;
        }
        void SetPlayerFourMode(bool active)
        {
            PlayerFourBotLabel.Enabled = active;
            PlayerFourBotLabel.Visible = active;
            PlayerFourBots.Enabled = active;
            PlayerFourBots.Visible = active;
            PlayerFourDeckLabel.Enabled = active;
            PlayerFourDeckLabel.Visible = active;
            PlayerFourDecks.Enabled = active;
            PlayerFourDecks.Visible = active;
        }

        void RefreshSettings()
        {
            Settings.ReadSettings();
            PlayerOneTitle.Text = Settings.nickname;
        }

        private void DecksButton_Click(object sender, EventArgs e)
        {
            bool isVisible = !DeckCollection.Visible;
            DeckCollection.Visible = isVisible;
            DeckCollection.Enabled = isVisible;
        }

        private void DeckCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = DeckCollection.SelectedIndex;
            if (i >= deckOptions.Count || i < 0) return;
            Deck d = deckOptions[i];
            d.LoadDeck();
            CardPoolViewer viewer = new CardPoolViewer(d.cards,d.deckName+$" ({d.cards.Count} Cards)"+" by " + d.author);
            viewer.ShowDialog();
            d.UnloadDeck();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.ShowDialog();
            RefreshSettings();
        }

        public static Notice networkNotice;
        public static List<Player> serverPlayers = new List<Player>();
        public static int startingTurn;

        private void JoinGame_Click(object sender, EventArgs e)
        {
            ServerPrompt prompt = new ServerPrompt();
            prompt.ShowDialog();
            if (prompt.DialogResult == DialogResult.Cancel) return;

            NetworkHandler.Join(prompt.port,prompt.address);
            if (!NetworkHandler.isConnected) return;
            networkNotice = new Notice("Connecting", "Attempting to make a connection with the host.", "Cancel", new Notice.CloseEvent(() =>
            {
                Client.instance.Disconnect();
            }));
            networkNotice.ShowDialog();
            if (!Client.instance.IsConnected()) return;

            //Start game
            gameInstance = new Game(serverPlayers, null);
            Window w = new Window(true);
            w.connectedToServer = true;
            Program.windowInstance = w;
            w.Show();
        }

        private void Setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Client.instance!=null && Client.instance.IsConnected())
            {
                if (Program.windowInstance != null)
                    Program.windowInstance.connectedToServer = false;
                Client.instance.Disconnect();
            }
        }

        private static void RegisterMods()
        {
            if (!Directory.Exists(appDataPath+"mods"))
            {
                Notice notice = new Notice("Missing Folder", $"The folder 'mods' is missing at {appDataPath}");
                notice.ShowDialog();
                return;
            }

            //Easter Eggs
            if (File.Exists(appDataPath+"settings.es")) {
                ESBuilder builder = new ESBuilder(File.ReadAllText(appDataPath + "settings.es"));
                bool brotherBeagle = builder.Get("brother_beagle", "") == "426561676c65";
                if (brotherBeagle && !Directory.Exists(appDataPath + "mods/E0"))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(appDataPath + "mods/E0");
                    dir.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden;
                    File.WriteAllBytes(appDataPath + "mods/E0/E0.dll", Properties.Resources.EggLibrary0);
                    File.WriteAllText(appDataPath + "mods/E0/mod.es", Properties.Resources.EggES0);
                    Properties.Resources.EggIcon0.Save(appDataPath + "mods/E0/icon.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            foreach (string folder in Directory.GetDirectories(appDataPath + "mods"))
            {
                List<string> files = Directory.GetFiles(folder).ToList();
                string modPath = null;
                Bitmap image = null;
                string esBasic = null;
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                        modPath = file;
                    else if (file.EndsWith("icon.png"))
                        image = new Bitmap(file);
                    else if (file.EndsWith("mod.es"))
                        esBasic = File.ReadAllText(file);
                }

                if (esBasic == null || modPath == null) continue;

                Mod_Framework.ModLoader.RegisterMod(modPath, esBasic, image);

                //if (!file.EndsWith(".dll")) continue;
                //Mod_Framework.ModLoader.RegisterMod(file);

            }
        }

        private void ViewMods_Click(object sender, EventArgs e)
        {
            ModBrowser browser = new ModBrowser();
            browser.ShowDialog();
        }
    }
}
