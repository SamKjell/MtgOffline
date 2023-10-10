using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.Mod_Framework
{
    public interface IMod
    {
        string GetModId();
        /// <summary>
        /// Should return the version of MtgOffline that this mod is made for. Ex: 1.1.2
        /// </summary>
        string GetMajorVersion();
        /// <summary>
        /// Should return the version of the mod.
        /// </summary>
        string GetMinorVersion();
        void RegisterModRegistries(RegistryEvent registryEvent);
    }
}
