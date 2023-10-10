using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtgOffline
{
    public partial class CardPoolViewer : Form
    {
        List<CardObject> cardData;
        Window w;
        CardObject selectedCard;
        bool hiddenCards;
        CardPoolType type;
        Player source;
        public CardPoolViewer(List<CardObject> cardPool, CardPoolType type,Player source, bool hideCards = true)
        {
            this.source = source;
            this.type = type;
            InitializeComponent();
            string l = type.ToString().ToLower().Substring(1); 
            Text = "Viewing " + source.name + "'s " + type.ToString()[0] + l;
            cardData = cardPool;
            w = Program.windowInstance;
            ManageMenuStrip();
            hiddenCards = hideCards;
            UpdateCardViewer();
        }
        public CardPoolViewer(List<CardObject> cardPool,string deckName,CardPoolType type = CardPoolType.DECK)
        {
            cardPool.Sort((x,y)=>x.name.CompareTo(y.name));
            this.type = type;
            InitializeComponent();
            Text = "Viewing the Deck: " + deckName;
            cardData = cardPool;
            ManageMenuStrip();
            UpdateCardViewer();
            hiddenCards = false;
            cardPoolToolStripMenuItem.Enabled = false;
            cardPoolToolStripMenuItem.Visible = false;
        }

        void ManageMenuStrip()
        {
            switch ((int)type)
            {
                case 0:
                    {
                        ManageCardActions("deck");
                        return;
                    }
                case 1:
                    {
                        handToolStripMenuItem.Enabled = true;
                        handToolStripMenuItem.Visible = true;
                        ManageCardActions("hand");
                        break;
                    }
                case 2:
                    {
                        libraryToolStripMenuItem.Enabled = true;
                        libraryToolStripMenuItem.Visible = true;
                        ManageCardActions("library");
                        break;
                    }
                case 3:
                    {
                        graveyardToolStripMenuItem.Enabled = true;
                        graveyardToolStripMenuItem.Visible = true;
                        ManageCardActions("graveyard");
                        break;
                    }
                case 4:
                    {
                        exileToolStripMenuItem.Enabled = true;
                        exileToolStripMenuItem.Visible = true;
                        ManageCardActions("exile");
                        break;
                    }
                case 5:
                    {
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void CardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardList.SelectedIndex < 0 || CardList.SelectedIndex >= CardList.Items.Count) return;
            selectedCard = cardData[CardList.SelectedIndex];
            if(!hiddenCards && type!=CardPoolType.DECK)
                w.UpdateInspector(selectedCard);
        }

        void UpdateCardViewer()
        {
            CardList.Update();
            CardList.Items.Clear();
            int index = 0;
            foreach (CardObject c in cardData)
            {
                string name = (hiddenCards) ? "Card Number: " + index++ : c.name;
                CardList.Items.Add(name);
            }
            CardList.EndUpdate();
        }

        private void toggleHiddenCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hiddenCards = !hiddenCards;
            UpdateCardViewer();
        }

        private void viewTranscriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hiddenCards || selectedCard == null) return;
            if(type == CardPoolType.DECK)
            {
                LargeNotice notice = new LargeNotice("Transcript: " + selectedCard.name, selectedCard.GetTranscript());
                notice.ShowDialog();
            }
            else
                w.game.LargeNotifyPlayer("Transcript: " + selectedCard.name, selectedCard.GetTranscript());
        }

        void ManageCardActions(string ignore)
        {
            if (ignore == "deck") return;
            if (ignore != "hand")
            {
                toHandToolStripMenuItem.Enabled = true;
                toHandToolStripMenuItem.Visible = true;
            }
            else
            {
                discardToolStripMenuItem1.Enabled = true;
                discardToolStripMenuItem1.Visible = true;
            }
            if (ignore != "library")
            {
                toLibraryToolStripMenuItem.Enabled = true;
                toLibraryToolStripMenuItem.Visible = true;
            }
            if (ignore != "graveyard")
            {
                toGraveyardToolStripMenuItem.Enabled = true;
                toGraveyardToolStripMenuItem.Visible = true;
            }
            if (ignore != "exile")
            {
                toExileToolStripMenuItem.Enabled = true;
                toExileToolStripMenuItem.Visible = true;
            }
        }

        private void discardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;
            Dictionary<Player, List<CardObject>> targets = new Dictionary<Player, List<CardObject>>();
            targets.Add(selectedCard.owner, new List<CardObject>() { selectedCard });
            DiscardStackObject stackObj = new DiscardStackObject(targets);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        void RefreshCardPool()
        {
            switch((int)type)
            {
                case 0:
                    {
                        cardData = source.deck.cards;
                        break;
                    }
                case 1:
                    {
                        cardData = source.hand;
                        break;
                    }
                case 2:
                    {
                        cardData = source.library;
                        break;
                    }
                case 3:
                    {
                        cardData = source.graveyard;
                        break;
                    }
                case 4:
                    {
                        cardData = source.exile;
                        break;
                    }
                case 5:
                    {
                        return;
                    }
            }
            UpdateCardViewer();
        }

        private void toLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;
            int librarySize = (selectedCard.owner.library.Count - 1);
            NumberPrompt prompt = new NumberPrompt("Card to Library", "What place, from the top, does the card go? 0 is the top, and negative numbers place it that many numbers on the bottom of the library. (-1 is the bottom, -2 is one card up from the bottom, etc.)", -(librarySize + 1), librarySize);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = new ReturnToLibraryStackObject(selectedCard, value, type.ToString().ToLower());
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void toExileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;
            StackObject stackObj = new ToExileStackObject(selectedCard, type.ToString().ToLower());
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void toHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;
            StackObject stackObj = new ToHandStackObject(selectedCard, type.ToString().ToLower());
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void toGraveyardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;
            StackObject stackObj = new ToGraveyardStackObject(selectedCard, type.ToString().ToLower());
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void CardPoolViewer_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }

        private void discardBulkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int handSize = source.hand.Count;
            NumberPrompt prompt = new NumberPrompt("Discarding Cards", "How many cards are being discarded?", 1, handSize);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = new RandomDiscardStackObject(source, value);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void shuffleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            source.ShuffleLibrary();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void millToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int libSize = source.library.Count();
            NumberPrompt prompt = new NumberPrompt("Milling", "How many cards are being milled?", 1, libSize);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = new MillStackObject(new List<Player>() { source }, value, null, null);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector(null);
            RefreshCardPool();
        }

        private void topCardsOnBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int libSize = source.library.Count();
            NumberPrompt prompt = new NumberPrompt("Cards to Bottom", "How many cards are being put on the bottom of the library?",1,libSize);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = new ToBottomOfLibStackObject(source, value);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            selectedCard = null;
            w.UpdateInspector();
            RefreshCardPool();
        }

        private void viewCardOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hiddenCards || selectedCard == null) return;
            if (selectedCard.imageURL == null || selectedCard.imageURL[0] == '\\')
            {
                Notice notice = new Notice("No Link", "This card has not been given a weblink. This could be because it is modded or does not have a picture online.");
                notice.ShowDialog();
            }
            else
                System.Diagnostics.Process.Start(selectedCard.imageURL);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
    public enum CardPoolType
    {
        DECK,
        HAND,
        LIBRARY,
        GRAVEYARD,
        EXILE,
        MISC
    }
}
