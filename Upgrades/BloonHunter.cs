using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppSystem.IO;
using PathsPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttackDuckDartPath.Upgrades
{
  class BloonHunter : UpgradePlusPlus<DartPath>
  {
    public override int Cost => 2400;
    public override int Tier => 4;
    //public override string Icon => GetTextureGUID(Name + "-Icon");

    public override string Description => "Hurls bolases which grapple and weaken smaller bloons/Moabs, slowing them and causing them to take double damage from all sources for a brief period.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
      SlowForBloonModel bolas = new SlowForBloonModel("SlowForBloonModel_Bolas", 0.5f, 3.0f, "AttackDuckDartPath_Bolas", 9999999, "SpiritWalkerHaunt", true, false, "Bfb,Ddt,Zomg,Bad,Boss", "", true, null, false, false, false, 0);

      SlowModifierForTagModel bolasmoabmod = new SlowModifierForTagModel("SlowModifierForTagModel_BolasMoab", "Moab", "AttackDuckDartPath_Bolas", 1.5f, false, false, 0.0f, false);

      AttackModel attack = towerModel.GetAttackModel("AttackModel_Attack_");

      towerModel.IncreaseRange(8f);

      ProjectileModel projectile = attack.weapons[0].projectile;

      projectile.AddBehavior(new RotateModel("RotateModel", 1440f));
      projectile.radius += 2;

      ProjectileModel splash = projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;

      splash.radius += towerModel.tiers[0] >= 2 ? 6 : 4;
      splash.pierce += towerModel.tiers[0] >= 2 ? 11 : towerModel.tiers[0] >= 1 ? 9 : 7;

      splash.AddBehavior(bolas);
      splash.AddBehavior(bolasmoabmod);

      projectile.ApplyDisplay<Displays.Projectiles.BolasDisplay>();

    }
  }
}
