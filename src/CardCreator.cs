using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public partial class CardCreator : Form
    {
        public CardObject result;
        public string image;
        public CardCreator()
        {
            InitializeComponent();  
            UpdateInformation();
        }

        public void UpdateImage()
        {
            Bitmap image = (MtgOffline.Properties.Resources.FloppyIcon);
            for (int x = 0; x < image.Height; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    if (pixel.Name!="0")
                    {
                        image.SetPixel(x, y, CreateCard.ForeColor);
                    }
                }
            }

            Download.Image = image;
        }

        public CardCreator(CardObject card)
        {
            InitializeComponent();
            UpdateInformation();
            NameBox.Text = card.name;
            String types = "";
            for (int i = 0; i < card.superTypes.Count; i++)
            {
                types += card.superTypes[i];
                if (i < card.superTypes.Count - 1)
                    types += " ";
            }
            types += ", ";
            for (int i = 0; i < card.cardTypes.Count; i++)
            {
                types += card.cardTypes[i]+" ";       
            }
            types += "- ";
            for (int i = 0; i < card.subTypes.Count; i++)
            {
                types += card.subTypes[i];
                if (i < card.subTypes.Count - 1)
                    types += " ";
            }
            Types.Text = types;
            String staticAbilities = "";
            for (int i = 0; i < card.staticAbilities.Count; i++)
            {
                staticAbilities += card.staticAbilities[i];
                if (i < card.staticAbilities.Count - 1)
                    staticAbilities += "\n";
            }
            StaticAbilities.Text = staticAbilities;
            Cost.Text = card.manaCost;
            if(card is CreatureBase)
            {
                CreatureBase c = card as CreatureBase;
                Power.Text = c.power.ToString();
                Toughness.Text = c.toughness.ToString();
            }
            result = card;
        }

        void UpdateInformation()
        {
            UpdateImage();
            if (Settings.lightNotifications)
            {
                NameBox.Text = "";
                Types.Text = "";
                StaticAbilities.Text = "";
                Power.Text = "";
                Toughness.Text = "";
                Cost.Text = "";
            }
        }

        private void CardCreator_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }

        private void Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateCard_Click(object sender, EventArgs e)
        {
            List<String> superTypes = new List<String>();
            List<String> cardTypes = new List<String>();
            List<String> subTypes = new List<String>();
            List<String> staticAbilities;
            String[] temp = Types.Text.Split('-');
            subTypes = ((temp.Length>1)?temp[1].Trim().Split(' ').ToList():new List<string>());
            String[] temp2 = temp[0].Split(',');
            if (temp2.Length > 1)
            {
                superTypes = temp2[0].Split(' ').ToList();
                cardTypes = temp2[1].Trim().Split(' ').ToList();
            }
            else
            {
                superTypes = new List<String>();
                cardTypes = temp2[0].Trim().Split(' ').ToList();
            }
            staticAbilities = StaticAbilities.Text.Split('\n').ToList();
            CardObject precursor = result;
            if (cardTypes.Contains("Creature"))
            {
                CreatureBase template = new CreatureBase();
                int.TryParse(Power.Text, out template.power);
                int.TryParse(Toughness.Text, out template.toughness);
                template.currentHealth = template.GetToughness();
                result = template;
            }
            else if (cardTypes.Contains("Artifact"))
            {
                result = new ArtifactBase();
            }
            else if (cardTypes.Contains("Enchantment"))
            {
                result = new EnchantmentBase();
            }
            else if (cardTypes.Contains("Land"))
                result = new LandBase();
            else if (cardTypes.Contains("Planeswalker"))
            {
                //Currently unsupported card type
                result = new CardObject();
                result.isPermanent = true;
            }
            else return;
            result.superTypes = superTypes;
            result.cardTypes = cardTypes;
            result.subTypes = subTypes;
            result.name = NameBox.Text;
            result.manaCost = Cost.Text;
            //result.tempImage = Properties.Resources.DefaultCardIcon;
            if(image != null)
            {
                result.imageURL = image;
            }
            else if (Settings.loadImages)
            {
                string[] searchTerms = NameBox.Text.Split(' ');
                for (int i = 0; i < searchTerms.Length; i++)
                    searchTerms[i] = "+[" + searchTerms[i] + "]";
                string concatST = string.Join("", searchTerms);
                string link = new WebUtils("https://gatherer.wizards.com/Pages/Search/Default.aspx?name=" + concatST).GetCardImageLink(result.name);
                if (link != null)
                    result.imageURL = link;
            }
            result.staticAbilities = staticAbilities;
            if (precursor != null)
                result.conditions = precursor.conditions;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Download_Click(object sender, EventArgs e)
        {
            if (NameBox.Text == "Cheat Sheet")
            {
                DepreciatedE0();
                Close();
                return;
            }
            string[] searchTerms = NameBox.Text.Split(' ');
            for (int i = 0; i < searchTerms.Length; i++)
                searchTerms[i] = "+[" + searchTerms[i] + "]";
            string concatST = string.Join("", searchTerms);
            string link = new WebUtils("https://gatherer.wizards.com/Pages/Search/Default.aspx?name=" + concatST).GetCardLink(NameBox.Text,out string id);

            if (link != null)
                image = link;

            if (id!=null)
            {
                link = "https://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid=" + id;
                string cardEs = new WebUtils(link).GetCardES();
                ESBuilder builder = new ESBuilder(cardEs);
                Types.Text = builder.Get("cardTypes", "<CARD TYPE ERROR>");
                string[] pt = builder.Get("pt", "0/0").Split('/');
                Power.Text = pt[0].Trim();
                Toughness.Text = pt[1].Trim();
            }
        }

        private void DepreciatedE0()
        {
            CardObject card = new CardObject();
            card.name = "Cheat Sheet";
            card.tempImage = Properties.Resources.E0;
            card.superTypes.Add("Legendary");
            card.cardTypes.Add("Artifact");
            card.manaCost = "13";

            card.controller = Program.windowInstance.GetLocalPlayer();
            card.owner = card.controller;
            card.imageURL = "Cheat Sheet";

            card.controller.EnterTheBattlefield(card);
        }
    }
}
