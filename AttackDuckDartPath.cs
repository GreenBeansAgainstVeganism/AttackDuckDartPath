using MelonLoader;
using BTD_Mod_Helper;
using AttackDuckDartPath;
using PathsPlusPlus;
using Il2CppAssets.Scripts.Models.Towers;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers;

[assembly: MelonInfo(typeof(AttackDuckDartPath.AttackDuckDartPath), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AttackDuckDartPath
{
  public class AttackDuckDartPath : BloonsTD6Mod
  {
    public override void OnApplicationStart()
    {
      ModHelper.Msg<AttackDuckDartPath>("AttackDuckDartPath loaded!");
    }
  }

  public class DartPath : PathPlusPlus
  {
    public override string Tower => TowerType.DartMonkey;

    //public override int UpgradeCount => 5;
  }


  // Damage multiplier effect
  [HarmonyPatch(typeof(Bloon), nameof (Bloon.ExecuteDamageTask))]
  internal class Bloon_ExecuteDamageTask
  {
    [HarmonyPrefix]
    internal static bool Prefix(Bloon __instance, ref float totalAmount)
    {
      if(__instance.GetMutatorById("AttackDuckDartPath_MoabSnare") != null)
      {
        totalAmount *= 3;
      }
      else if(__instance.GetMutatorById("AttackDuckDartPath_Bolas") != null)
      {
        totalAmount *= 2;
      }
      return true;
    }
  }
}