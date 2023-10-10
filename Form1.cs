using MtgOffline.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public partial class Window : Form
    {
        public Game game;
        public List<Object> gameData = new List<Object>();
        public Object selectedObject = null;
        public CardObject clipboard = null;
        public bool offlineMode = false;
        public bool connectedToServer = false;
        public bool inCombat = false;

        bool declaringAttacks = false;
        
        List<CardData> permanentData = new List<CardData>();
        CardData permanentSelectedObject;

        public Window()
        {
            game = Setup.gameInstance;
            game.Start();
            InitializeComponent();
            UpdateGameConsole();
            offlineMode = !Settings.loadImages;
        }

        public Window(bool server)
        {
            game = Setup.gameInstance;
            game.Start();
            foreach (Player p in game.players)
            {
                if (p is Networking.ServerPlayer && (p as Networking.ServerPlayer).id == Setup.startingTurn)
                {
                    game.turn = game.players.IndexOf(p);
                    break;
                }
            }
            InitializeComponent();
            revealToolStripMenuItem.Enabled = true;
            revealToolStripMenuItem.Visible = true;
            UpdateGameConsole();
        }

        

        private void battlefieldToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void GameConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameConsole.SelectedIndex >= gameData.Count || GameConsole.SelectedIndex < 0)
                return;
            StackConsole.ClearSelected();
            selectedObject = gameData[GameConsole.SelectedIndex];
            UpdateInspector();
        }
        private void StackConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StackConsole.SelectedIndex >= game.stack.Count || StackConsole.SelectedIndex < 0)
                return;
            GameConsole.ClearSelected();
            selectedObject = game.stack[StackConsole.SelectedIndex];
            UpdateInspector();
        }
        public void UpdateInspector()
        {
            if (selectedObject == null)
                return;
            else if (selectedObject is CardObject)
            {
                CardObject c = (CardObject)selectedObject;
                Bitmap bitmap = new Bitmap(MtgOffline.Properties.Resources.DefaultCardIcon);
                if (c.tempImage == null)
                {
                    try
                    {
                        if(c.imageURL!=null && c.imageURL[0] == '\\')
                        {
                            string id = c.imageURL.Substring(1);
                            bitmap = Mod_Framework.ModLoader.modRegistries.GetImageFromId(id);
                            c.tempImage = bitmap;
                        }
                        else if (!offlineMode)
                        {
                            WebClient web = new WebClient();
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            Stream stream = web.OpenRead(c.imageURL);
                            bitmap = new Bitmap(stream);
                            c.tempImage = bitmap;
                            stream.Flush();
                            stream.Close();
                            web.Dispose();
                        }   
                    }
                    catch (WebException e)
                    {
                        offlineMode = true;
                    }
                    catch (ArgumentException e) { }

                }
                else
                    bitmap = c.tempImage;
                if (bitmap != null)
                    CardImage.Image = bitmap;

                if (c.isPermanent)
                {
                    SetInspectorStatePermanent();
                    PermanentInspectorTitle.Text = c.name + ":";
                    PermanentInspectorSubtitle.Text = ListToString(c.superTypes) + " " + ListToString(c.cardTypes) + ":";
                    PermanentInspectorTypeTitle.Text = ListToString(c.subTypes);
                    PermanentDataViewer.Items.Clear();
                    PermanentDataViewer.ClearSelected();
                    permanentData.Clear();
                    foreach (string staticA in c.staticAbilities)
                    {
                        PermanentDataViewer.Items.Add(staticA);
                        permanentData.Add(new CardData("Static Ability", "Indefinitely",staticA));
                    }
                    foreach (Condition condition in c.conditions)
                    {
                        PermanentDataViewer.Items.Add(condition.description);
                        permanentData.Add(new CardData("Condition", condition.conclusionTime, condition));
                    }
                }
            }
            else if (selectedObject is Player)
            {
                Player p = (Player)selectedObject;
                if (p.icon != null)
                    CardImage.Image = p.icon;
                SetInspectorStatePlayer();
                PlayerInspectorTitle.Text = p.name + ": " + p.health + " HP";
            }
            else if(selectedObject is StackObject)
            {
                StackObject s = (StackObject)selectedObject;
                CardStackObject cso = null;
                SpellStackObject sso = null;
                ImageStackObject iso = null;
                if (s is CardStackObject)
                    cso = s as CardStackObject;
                else if (s is SpellStackObject)
                    sso = s as SpellStackObject;
                CardImage.Image = null;
                SetInspectorStateStack();
                StackInspectorName.Text = s.name;
                StackInspectorTypeInfo.Text = "?Activated/Triggered Ability?";
                StackInspectorSubtypeInfo.Text = "";
                StackInspectorTargets.BeginUpdate();
                StackInspectorTargets.Items.Clear();
                if (s.targetCards != null)
                {
                    foreach (CardObject c in s.targetCards)
                    {
                        StackInspectorTargets.Items.Add(c.controller.name + "'s " + c.name);
                    }
                }
                if (s.targetPlayers != null)
                {
                    foreach (Player p in s.targetPlayers)
                    {
                        StackInspectorTargets.Items.Add(p.name);
                    }
                }
                if (s.targetStackObjects != null)
                {
                    foreach (StackObject stackObj in s.targetStackObjects)
                    {
                        StackInspectorTargets.Items.Add(stackObj.name);
                    }
                }
                StackInspectorTargets.EndUpdate();
                if (cso != null||sso!=null)
                {
                    CardObject card = (cso == null) ? sso.card : cso.card;
                    StackInspectorTypeInfo.Text = ListToString(card.superTypes) + " " + ListToString(card.cardTypes) + ":";
                    StackInspectorSubtypeInfo.Text = ListToString(card.subTypes);
                    Bitmap bitmap = new Bitmap(MtgOffline.Properties.Resources.DefaultCardIcon);
                    if (card.tempImage == null)
                    {
                        try
                        {
                            if (card.imageURL[0] == '\\')
                            {
                                string id = card.imageURL.Substring(1);
                                bitmap = Mod_Framework.ModLoader.modRegistries.GetImageFromId(id);
                                card.tempImage = bitmap;
                            }
                            else if (!offlineMode)
                            {
                                WebClient web = new WebClient();
                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                Stream stream = web.OpenRead(card.imageURL);
                                bitmap = new Bitmap(stream);
                                card.tempImage = bitmap;
                                stream.Flush();
                                stream.Close();
                                web.Dispose();
                            }
                        }
                        catch (WebException e)
                        {
                            offlineMode = true;
                        }
                        catch (ArgumentException e) { }

                    }
                    else
                        bitmap = card.tempImage;
                    if (bitmap != null)
                        CardImage.Image = bitmap;
                }
            }

        }

        public void UpdateInspector(CardObject c)
        {
            if (c == null)
            {
                CardImage.Image = null;
                SetInspectorStateEmpty();
                return;
            }
            Bitmap bitmap = new Bitmap(MtgOffline.Properties.Resources.DefaultCardIcon);
            SetInspectorStateEmpty();
            if (c.tempImage == null)
            {
                try
                {
                    if (c.imageURL!=null && c.imageURL[0] == '\\')
                    {
                        string id = c.imageURL.Substring(1);
                        bitmap = Mod_Framework.ModLoader.modRegistries.GetImageFromId(id);
                        c.tempImage = bitmap;
                    }
                    else if (!offlineMode)
                    {
                        WebClient web = new WebClient();
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        Stream stream = web.OpenRead(c.imageURL);
                        bitmap = new Bitmap(stream);
                        c.tempImage = bitmap;
                        stream.Flush();
                        stream.Close();
                        web.Dispose();
                    }
                }
                catch (WebException e)
                {
                    offlineMode = true;
                }
                catch (ArgumentException e) { }

            }
            else
                bitmap = c.tempImage;
            if (bitmap != null)
                CardImage.Image = bitmap;

            if (c.isPermanent)
            {
                PermanentInspectorTitle.Text = c.name + ":";
                PermanentInspectorSubtitle.Text = ListToString(c.superTypes) + " " + ListToString(c.cardTypes) + ":";
                PermanentInspectorTypeTitle.Text = ListToString(c.subTypes);
                PermanentDataViewer.Items.Clear();
                PermanentDataViewer.ClearSelected();
                permanentData.Clear();
                foreach (string staticA in c.GetStaticAbilities())
                {
                    PermanentDataViewer.Items.Add(staticA);
                    permanentData.Add(new CardData("Static Ability", "Indefinitely", staticA));
                }
                foreach (Condition condition in c.conditions)
                {
                    PermanentDataViewer.Items.Add(condition.description);
                    permanentData.Add(new CardData("Condition", condition.conclusionTime, condition));
                }
            }
        }

        public void UpdateStackConsole()
        {
            StackConsole.BeginUpdate();

            StackConsole.Items.Clear();
            foreach (StackObject s in game.stack)
            {
                string stackName = "";
                int i = game.GetStackObjectIndex(s);
                if (game.replacementActions.ContainsKey(i))
                {
                    foreach (StackReplacementAction a in game.replacementActions[i])
                    {
                        stackName += ("(" + a.name + ")");
                    }
                }
                stackName += s.name;
                StackConsole.Items.Add(stackName);
            }
            if (game.stack.Count == 0)
            {
                StackConsole.Items.Add("EMPTY");
                StackConsole.SelectionMode = SelectionMode.None;
            }
            else
                StackConsole.SelectionMode = SelectionMode.One;
            StackConsole.EndUpdate();
        }

        public void UpdateGameConsole()
        {
            game.CheckIfTypeBasedConditionsStillApply();

            GameConsole.BeginUpdate();
            GameConsole.Items.Clear();
            gameData.Clear();
            foreach (Player p in game.players)
            {
                GameConsole.Items.Add(p.name + ": " + p.health + " HP");
                gameData.Add(p);

                List<CardObject> bf = new List<CardObject>();
                foreach (CardObject c in p.battlefield)
                {
                    if(!c.isAttatched)
                        bf.Add(c);
                }
                bf.Sort((x, y) => x.CompareTo(y));
                foreach (CardObject c in bf)
                {
                    String types = WriteTypes(c);
                    String combatStats = "";
                    if(c is CreatureBase)
                    {
                        CreatureBase cr = c as CreatureBase;
                        combatStats = "    " + cr.GetPower() + "/" + cr.GetToughness();
                    }
                    GameConsole.Items.Add("    " + (c.isTapped ? "(T) " : "") + c.name + "    " + types + combatStats);
                    gameData.Add(c);

                    //Handle attatched Cards
                    c.auras.Sort((x, y) => x.CompareTo(y));
                    foreach (CardObject aura in c.auras)
                    {
                        String t = WriteTypes(aura);
                        GameConsole.Items.Add("        " + aura.name + "    " + t);
                        gameData.Add(aura);
                    }
                }
            }
            GameConsole.EndUpdate();
        }

        String WriteTypes(CardObject c)
        {
            String types = "";
            foreach (String type in c.superTypes)
            {
                types += type + " ";
            }
            foreach (String type in c.cardTypes)
            {
                types += type + " ";
            }
            types += "- ";
            foreach (String type in c.subTypes)
            {
                types += type + " ";
            }
            return types;
        }

        private void plainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating plains land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject plains = new Plains();
            plains.owner = p;
            plains.controller = p;
            LandETB(plains);
            //p.EnterTheBattlefield(plains);
        }

        private void islandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating island land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject island = new Island();
            island.owner = p;
            island.controller = p;
            LandETB(island);
            //p.EnterTheBattlefield(island);
        }

        private void swampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating swamp land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject swamp = new Swamp();
            swamp.controller = p;
            swamp.owner = p;
            LandETB(swamp);
            //p.EnterTheBattlefield(swamp);
        }

        private void mountainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating mountain land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject mountain = new Mountain();
            mountain.owner = p;
            mountain.controller = p;
            LandETB(mountain);
            //p.EnterTheBattlefield(mountain);
        }
        
        private void forestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating forest land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject forest = new Forest();
            forest.owner = p;
            forest.controller = p;
            LandETB(forest);
            //p.EnterTheBattlefield(forest);
        }

        private void wastesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating wastes land
            if (!CanAct()) return;
            Player p = game.players[game.turn];
            CardObject wastes = new Wastes();
            wastes.owner = p;
            wastes.controller = p;
            LandETB(wastes);
            //p.EnterTheBattlefield(wastes);
        }

        private void passTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanAct()||game.stack.Count!=0) return;
            GetLocalPlayer().PlayerEndstep();
            if (!connectedToServer)
                game.NextTurn();
            else
                Networking.ClientSend.Trigger("nextturn");
        }

        bool CanAct()
        {
            if (game.turn == game.players.Count - 1) return true;
            return false;
        }
        public Player GetLocalPlayer()
        {
            return game.players[game.players.Count - 1];
        }

        private void PlayerInspectorLose_Click(object sender, EventArgs e)
        {
            Player p = IndexToPlayer();
            if (p == null) return;
            if (p == GetLocalPlayer() && p.health == 13 && p.battlefield.Find(x => x.imageURL == "Cheat Sheet" && x.name == "Cheat Sheet") != null)
            {
                E0 binaryForm = new E0();
                binaryForm.Show();
                return;
            }
            SpellCaster caster = new SpellCaster("Loss", new LoseStackObject(p));
            caster.ShowDialog();
            //game.RemovePlayer(p.name + " has lost", p);
        }
        Player IndexToPlayer()
        {
            if (GameConsole.SelectedIndex < 0 || GameConsole.SelectedIndex >= GameConsole.Items.Count) return null;
            return gameData[GameConsole.SelectedIndex] as Player;
        }

        void SetInspectorStatePlayer()
        {
            PlayerInspector.Visible = true;
            PlayerInspector.Enabled = true;
            PermanentInspector.Visible = false;
            PermanentInspector.Enabled = false;
            StackInspector.Visible = false;
            StackInspector.Enabled = false;
            playerToolStripMenuItem.Visible = true;
            playerToolStripMenuItem.Enabled = true;
        }
        void SetInspectorStatePermanent()
        {
            PermanentInspector.Visible = true;
            PermanentInspector.Enabled = true;
            PlayerInspector.Visible = false;
            PlayerInspector.Enabled = false;
            StackInspector.Visible = false;
            StackInspector.Enabled = false;
            playerToolStripMenuItem.Visible = false;
            playerToolStripMenuItem.Enabled = false;

        }

        void SetInspectorStateStack()
        {
            PermanentInspector.Visible = false;
            PermanentInspector.Enabled = false;
            PlayerInspector.Visible = false;
            PlayerInspector.Enabled = false;
            StackInspector.Visible = true;
            StackInspector.Enabled = true;
            playerToolStripMenuItem.Visible = false;
            playerToolStripMenuItem.Enabled = false;
        }

        void SetInspectorStateEmpty()
        {
            PermanentInspector.Visible = false;
            PermanentInspector.Enabled = false;
            PlayerInspector.Visible = false;
            PlayerInspector.Enabled = false;
            StackInspector.Visible = false;
            StackInspector.Enabled = false;
            playerToolStripMenuItem.Visible = false;
            playerToolStripMenuItem.Enabled = false;
            CardImage.Image = null;
        }

        private void StackConsoleResolve_Click(object sender, EventArgs e)
        {
            if (!connectedToServer)
                game.ResolveStack();
            else
                Networking.ClientSend.Trigger("update resolve");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PlayerInspectorTitle_Click(object sender, EventArgs e)
        {

        }

        String ListToString(List<String> list)
        {
            String s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += list[i];
                if (i != list.Count - 1)
                    s += " ";
            }
            return s;
        }


        private void PermanentInspector_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PermanentInspectorDestroy_Click(object sender, EventArgs e)
        {
            StackObject stackObj = new DestoryStackObject(selectedObject as CardObject);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            SetInspectorStateEmpty();
        }

        private void creatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CardCreator creator = new CardCreator();
            creator.ShowDialog();
            if (creator.DialogResult == DialogResult.Cancel)
                return;
            CardObject result = creator.result;
            result.owner = GetLocalPlayer();
            result.controller = result.owner;
            if (!(result is LandBase))
            {
                CardStackObject stackObj = new CardStackObject(result);
                SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
                caster.ShowDialog();
            }
            else
                LandETB(result);
        }

        private void LandETB(CardObject card)
        {
            if (card == null || !(card is LandBase)) return;
            //Handle land
            if (connectedToServer)
                Networking.ClientSend.CardPacket(card);
            else
                card.controller.EnterTheBattlefield(card);
        }

        private void editSelectedCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(selectedObject != null && selectedObject is CardObject)
            {
                CardObject start = selectedObject as CardObject;
                CardCreator creator = new CardCreator(start);
                creator.ShowDialog();
                if (creator.DialogResult == DialogResult.Cancel)
                    return;
                CardObject result = creator.result;
                result.owner = start.owner;
                result.controller = start.controller;
                result.conditions = start.conditions;
                result.id = start.id;
                if (!connectedToServer)
                {
                    start.controller.battlefield[start.controller.battlefield.IndexOf(start)] = result;
                    UpdateGameConsole();
                    UpdateInspector();
                }
                else
                {
                    ESBuffer buffer = new ESBuffer();
                    buffer.Add("cmd_identifier", "editcard");
                    buffer.Add("card_id", result.id);
                    buffer.Add("card_es_basic", Networking.NetworkHandler.CardObjectToStringBasic(result));
                    Networking.ClientSend.TriggerES(buffer.GetES());
                }
            }
        }

        private void PermanentInspectorExile_Click(object sender, EventArgs e)
        {
            StackObject stackObj = new ExileStackObject(selectedObject as CardObject);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            SetInspectorStateEmpty();
        }

        private void PermanentInspectorDamage_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is CreatureBase)) return;
            NumberPrompt prompt = new NumberPrompt("Damage", "How much damage is this spell/ability doing?");
            prompt.ShowDialog();
            int damage = prompt.returnValue;
            StackObject stackObj = new DamageTargetStackObject(new List<CardObject>() { selectedObject as CardObject }, new List<Player>(), damage);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
        }

        private void PlayerInspectorDamageHeal_Click(object sender, EventArgs e)
        {
            NumberPrompt prompt = new NumberPrompt("Damage/Heal", "How much damage is this spell/ability doing? Use negative numbers to heal the player.", -100000, 100000);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = null;
            if (value >= 0) //Damage
            {
                stackObj = new DamageTargetStackObject(new List<CardObject>(), new List<Player>() { selectedObject as Player }, value);
            }
            else //Healing
            {
                stackObj = new HealingStackObject(new List<Player>() { selectedObject as Player }, -value); 
            }
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
        }

        private void PermanentInspectorToggleTap_Click(object sender, EventArgs e)
        {
            CardObject card = selectedObject as CardObject;
            if(card.controller == GetLocalPlayer())
            {
                if (!connectedToServer)
                {
                    if (card.isTapped)
                        (selectedObject as CardObject).Untap();
                    else if (!card.isTapped)
                        (selectedObject as CardObject).Tap();
                    UpdateGameConsole();
                }
                else
                {
                    if (card.isTapped)
                        Networking.ClientSend.Trigger($"untap {card.id}");
                    else
                        Networking.ClientSend.Trigger($"tap {card.id}");
                }
            }
            else
            {
                StackObject stackObj = null;
                if (card.isTapped)
                    stackObj = new UntapStackObject(new List<CardObject> { selectedObject as CardObject });
                else if (!card.isTapped)
                    stackObj = new TapStackObject(new List<CardObject> { selectedObject as CardObject });

                if (stackObj != null)
                {
                    SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
                    caster.ShowDialog();
                }
            }
        }

        private void PermanentInspectorToHand_Click(object sender, EventArgs e)
        {
            StackObject stackObj = new ReturnToHandStackObject(new List<CardObject> { selectedObject as CardObject});
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            SetInspectorStateEmpty();
        }

        private void PermanentInspectorToLibrary_Click(object sender, EventArgs e)
        {
            CardObject card = selectedObject as CardObject;
            int librarySize = (card.owner.library.Count - 1);
            NumberPrompt prompt = new NumberPrompt("Card to Library","What place, from the top, does the card go? 0 is the top, and negative numbers place it that many numbers on the bottom of the library. (-1 is the bottom, -2 is one card up from the bottom, etc.)",-(librarySize+1),librarySize);
            prompt.ShowDialog();
            int value = prompt.returnValue;
            StackObject stackObj = new ReturnToLibraryStackObject(selectedObject as CardObject, value);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj);
            caster.ShowDialog();
            SetInspectorStateEmpty();
        }

        private void PermanentDataLabel_Click(object sender, EventArgs e)
        {

        }

        private void PermanentDataViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = PermanentDataViewer.SelectedIndex;
            if (i >= permanentData.Count || i < 0) return;
            permanentSelectedObject = permanentData[PermanentDataViewer.SelectedIndex];
            PermanentDataType.Text = permanentSelectedObject.type.ToUpper();
            PermanentDataDuration.Text = permanentSelectedObject.duration.ToUpper();
        }

        private void PermanentDataRemove_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is CardObject) || permanentSelectedObject == null || (permanentSelectedObject.associatedObject is String)) return;
            (selectedObject as CardObject).RemoveCondition(permanentSelectedObject.associatedObject as Condition);
            PermanentDataDuration.Text = "";
            PermanentDataType.Text = "";
            UpdateGameConsole();
            UpdateInspector();
        }

        private void viewTranscriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedObject == null) return;
            CardObject c = null;
            if (selectedObject is CardObject)
                c = selectedObject as CardObject;
            else if (selectedObject is CardStackObject)
                c = (selectedObject as CardStackObject).card;
            else if (selectedObject is SpellStackObject)
                c = (selectedObject as SpellStackObject).card;
            else
                return;
            game.LargeNotifyPlayer("Transcript: "+ c.name, c.GetTranscript());
        }

        private void PlayerInspectorViewHand_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is Player) || selectedObject as Player == GetLocalPlayer()) return;
            CardPoolViewer viewer = new CardPoolViewer((selectedObject as Player).hand, CardPoolType.HAND,selectedObject as Player);
            viewer.ShowDialog();
        }

        private void PlayerInspectorViewGraveyard_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is Player) || selectedObject as Player == GetLocalPlayer()) return;
            CardPoolViewer viewer = new CardPoolViewer((selectedObject as Player).graveyard, CardPoolType.GRAVEYARD, selectedObject as Player,false);
            viewer.ShowDialog();
        }

        private void PlayerInspectorViewExile_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is Player) || selectedObject as Player == GetLocalPlayer()) return;
            CardPoolViewer viewer = new CardPoolViewer((selectedObject as Player).exile, CardPoolType.EXILE, selectedObject as Player, false);
            viewer.ShowDialog();
        }

        private void viewLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is Player)||selectedObject as Player == GetLocalPlayer()) return;
            CardPoolViewer viewer = new CardPoolViewer((selectedObject as Player).library, CardPoolType.LIBRARY, selectedObject as Player);
            viewer.ShowDialog();
        }

        private void StackInspectorTypeInfo_Click(object sender, EventArgs e)
        {

        }

        private void StackInspectorToHand_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is StackObject)) return;
            StackObject stackObj = new ApplyReplacementActionStackObject("To hand", selectedObject as StackObject, StackReplacementAction.TO_HAND);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj, false);
            caster.ShowDialog();
            selectedObject = null;
            SetInspectorStateEmpty();
        }

        private void StackInspectorToLibrary_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is StackObject)) return;
            StackObject stackObj = new ApplyReplacementActionStackObject("To libary", selectedObject as StackObject, StackReplacementAction.TO_LIBRARY);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj,false);
            caster.ShowDialog();
            selectedObject = null;
            SetInspectorStateEmpty();
        }

        private void StackInspectorCounter_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is StackObject)) return;
            StackObject stackObj = new ApplyReplacementActionStackObject("Countering", selectedObject as StackObject, StackReplacementAction.COUNTERED);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj, false);
            caster.ShowDialog();
            selectedObject = null;
            SetInspectorStateEmpty();
        }

        private void StackInspectorExile_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is StackObject)) return;
            StackObject stackObj = new ApplyReplacementActionStackObject("Exiling", selectedObject as StackObject, StackReplacementAction.EXILED);
            SpellCaster caster = new SpellCaster(stackObj.name, stackObj, false);
            caster.ShowDialog();
            selectedObject = null;
            SetInspectorStateEmpty();
        }

        private void declareAttackersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //This will need to be reworked for games with more than one opponent
            if(!declaringAttacks)
            {
                declaringAttacks = true;
                GameConsole.SelectionMode = SelectionMode.MultiExtended;
                if (!Settings.lightNotifications)
                    game.NotifyPlayer("Declare Attackers", "Select your attackers. Then, press 'Declare Attacks' again to move into combat.");
            }
            else
            {
                declaringAttacks = false;
                inCombat = true;
                List<object> selectedObjects = new List<object>();
                foreach (int i in GameConsole.SelectedIndices)
                {
                    if(i<gameData.Count)
                        selectedObjects.Add(gameData[i]);
                }
                List<AttackData> attacks = new List<AttackData>();
                foreach (object item in selectedObjects)
                {
                    if (item is CreatureBase && (item as CreatureBase).controller == GetLocalPlayer())
                    {
                        CreatureBase c = item as CreatureBase;
                        bool isTapped = c.isTapped;
                        c.Attack();
                        if (connectedToServer && isTapped != c.isTapped)
                            Networking.ClientSend.Trigger($"cmd tap {c.id}");
                        AttackData data = new AttackData(c, game.players[0]);
                        attacks.Add(data);
                    }                     
                }
                GameConsole.SelectionMode = SelectionMode.One;
                game.DeclareAttacks(attacks);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is CardObject)) return;
            clipboard = selectedObject as CardObject;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(selectedObject is CardObject)) return;
            clipboard = selectedObject as CardObject;
            clipboard.controller.battlefield.Remove(selectedObject as CardObject);
            selectedObject = null;
            UpdateGameConsole();
        }

        private void viewClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clipboard == null) return;
            selectedObject = clipboard;
            UpdateInspector();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clipboard == null) return;
            Player owner = GetLocalPlayer();
            if (selectedObject is Player)
                owner = selectedObject as Player;

            CardObject template;
            if (clipboard is CreatureBase)
            {
                CreatureBase t = new CreatureBase();
                t.power = (clipboard as CreatureBase).power;
                t.toughness = (clipboard as CreatureBase).toughness;
                template = t;
            }
            else if (clipboard is ArtifactBase)
                template = new ArtifactBase();
            else if (clipboard is EnchantmentBase)
                template = new EnchantmentBase();
            else if (clipboard is LandBase)
                template = new LandBase();
            else return;
            template.name = clipboard.name;
            template.superTypes = clipboard.superTypes;
            template.cardTypes = clipboard.cardTypes;
            template.subTypes = clipboard.subTypes;
            template.tempImage = clipboard.tempImage;
            template.manaCost = clipboard.manaCost;
            template.conditions = clipboard.conditions;
            template.staticAbilities = clipboard.staticAbilities;
            template.abilities = clipboard.abilities;
            template.owner = owner;
            template.controller = owner;
            if (!(template is LandBase))
            {
                CardStackObject stackObj = new CardStackObject(template);
                SpellCaster caster = new SpellCaster("Pasting " + stackObj.name, stackObj);
                caster.ShowDialog();
            }
            else
                template.controller.EnterTheBattlefield(template);

        }

        private void Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Networking.Client.instance !=null && Networking.Client.instance.IsConnected())
            {
                connectedToServer = false;
                Networking.Client.instance.Disconnect();
            }
            Setup.serverPlayers.Clear();
        }

        private void cancelAttackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!declaringAttacks) return;
            declaringAttacks = false;
            GameConsole.SelectionMode = SelectionMode.One;
            GameConsole.ClearSelected();
        }

        private void revealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!connectedToServer || !(selectedObject is CardObject)) return;
        }

        private void Window_Load(object sender, EventArgs e)
        {
            Mod_Framework.EventBus.PostEvent("Load", sender, e);
        }
    }

    class CardData
    {
        public string type;
        public string duration;
        public object associatedObject;
        public CardData(string type, string duration, object associatedObject)
        {
            this.type = type;
            this.duration = duration;
            this.associatedObject = associatedObject;
        }
    }
}
