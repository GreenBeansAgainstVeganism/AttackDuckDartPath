using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
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
  class SlingShot : UpgradePlusPlus<DartPath>
  {
    public override int Cost => 390;
    public override int Tier => 3;
    //public override string Icon => GetTextureGUID(Name + "-Icon");

    public override string Description => "Dart Monkey abandons their darts in favor of a superior weapon. Attacks over obstacles and at significantly increased distance and attack rate.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
      AttackModel attack = towerModel.GetAttackModel("AttackModel_Attack_");

      attack.weapons[0].rate *= 0.7f;
      attack.attackThroughWalls = true;

      towerModel.IncreaseRange(32f);
      towerModel.ignoreBlockers = true;

      ProjectileModel projectile = attack.weapons[0].projectile;

      projectile.pierce = 1;
      projectile.maxPierce = 1;
      projectile.GetBehavior<TravelStraitModel>().speed += 100f;
      projectile.ignoreBlockers = true;

      ProjectileModel splash = projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;

      splash.radius = towerModel.tiers[0] >= 2 ? 10 : 8;
      splash.maxPierce = 0;
      splash.pierce = towerModel.tiers[0] >= 2 ? 9 : towerModel.tiers[0] >= 1 ? 7 : 5;


      projectile.ApplyDisplay<Displays.Projectiles.PebbleDisplay>();
    }
  }
}
