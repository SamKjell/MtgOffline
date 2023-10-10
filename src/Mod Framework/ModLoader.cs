using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MtgOffline.EnderScript;

namespace MtgOffline.Mod_Framework
{
    public class ModLoader
    {
        public static Dictionary<string, Mod> mods = new Dictionary<string, Mod>();
        public static RegistryEvent modRegistries = new RegistryEvent();

        public static void RegisterMod(string modPath, string modEsBasic, Bitmap icon)
        {
            string[] references = new string[] { "E0.E0Mod" };

            Assembly mod = Assembly.LoadFrom(modPath);

            List<Type> modTypes = mod.GetTypes().ToList();
            Type mainModFile = modTypes.Find(x=>x.GetInterface("IMod")!=null);
            Type modSettings = modTypes.Find(x => x.GetInterface("IModSettings") != null);

            if (mainModFile == null)
            {
                ErrorNotice(modPath);
                return;
            }

            IMod modInstance = (IMod)Activator.CreateInstance(mainModFile);
            IModSettings settingsInstance = modSettings == null ? null : (IModSettings)Activator.CreateInstance(modSettings);

            modInstance.RegisterModRegistries(modRegistries);

            string modId = modInstance.GetModId().ToLower();

            Mod m = new Mod(modEsBasic, modInstance, icon,modPath);
            m.modSettings = settingsInstance;

            if (references.Contains(mainModFile.FullName) && modInstance.GetMajorVersion()=="Egg")
            {//Easter Egg Mod
                ESBuffer buffer = new ESBuffer();
                buffer.AddComment("META_TAG");
                buffer.Add("is_egg", true);
                buffer.Add("link", m.link);
                m.link = buffer.GetES();
                mods.Add(modInstance.GetModId(), m);
                return;
            }

            if ("v" + modInstance.GetMajorVersion() != Program.GetVersion())
            {
                NotifyPlayer("Outdated Mod", $"{modId} is made for {modInstance.GetMajorVersion()}, not {Program.GetVersion()}. It will be ignored.");
                return;
            }
            else if (mods.ContainsKey(modId))
            {
                System.Diagnostics.Debug.WriteLine("Duplicate mod id detected. Ignoring mod.");
                return;
            }
            mods.Add(modId, m);
            EventBus.PostEvent("ModLoaded", m, null);

            System.Diagnostics.Debug.WriteLine($"The mod: {m.displayName} (modId: '{modId}') has been loaded.");
        }

        //public static void ReloadMods()
        //{
        //    mods.Clear();

        //    MethodInfo info = typeof(Setup).GetMethod("RegisterMods", BindingFlags.NonPublic | BindingFlags.Static);
        //    info.Invoke(null, new object[] {});
        //}

        private static void ErrorNotice(string path)
        {
            System.Diagnostics.Debug.WriteLine("Critical error loading a mod at" + path);
            NotifyPlayer("Mod Error", "Critical error loading mod at " + path);
        }

        private static void NotifyPlayer(string title, string desc)
        {
            Notice notice = new Notice(title, desc);
            notice.ShowDialog();
        }

    }

    public class Mod
    {
        public string displayName;
        public string author;
        public string credits;
        public string description;
        public string link;
        public string modId;
        public string majorVersion;
        public string minorVersion;

        public IMod modInstance;
        public IModSettings modSettings;
        public Bitmap icon;

        private string resourceLocation;

        public Mod(string esBasic, IMod imodInstance, Bitmap icon, string filePath)
        {
            ESBuilder builder = new ESBuilder(esBasic);
            displayName = builder.Get("name","Unnammed Mod");
            author = builder.Get("author", "");
            credits = builder.Get("credits", "");
            description = builder.Get("description","");
            link = builder.Get("weblink", null);
            modId = imodInstance.GetModId();
            majorVersion = $"v{imodInstance.GetMajorVersion()}";
            minorVersion = $"v{imodInstance.GetMinorVersion()}";

            modInstance = imodInstance;
            this.icon = icon;

            resourceLocation = filePath.Remove(filePath.LastIndexOf('\\')+1);
        }

        public string GetResourceLocation()
        {
            return resourceLocation;
        }
    }
}
