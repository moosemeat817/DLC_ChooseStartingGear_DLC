using MelonLoader;
using UnityEngine;

namespace DLC_ChooseStartingGear_DLC
{
    public class Implementation : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
        }
    }
}
