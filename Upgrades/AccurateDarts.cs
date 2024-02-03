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
  class AccurateDarts : UpgradePlusPlus<DartPath>
  {
    public override int Cost => 95;
    public override int Tier => 1;
    //public override string Icon => GetTextureGUID(Name + "-Icon");

    public override string Description => "Darts fly further and curve slightly towards their target.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
      TrackTargetModel seeking = new TrackTargetModel("TrackTargetModel_", 80.0f, true, false, 90.0f, false, 270f, false, false);

      //ProjectileModel projectile = towerModel.GetAttackModel("AttackModel_Attack_").weapons[0].projectile;

      towerModel.GetDescendants<ProjectileModel>().ForEach(projectile =>
      {
        TravelStraitModel travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
          travel.Lifespan *= 1.4f;
          travel.Speed *= 1.3f;
        }

        projectile.AddBehavior(seeking.Duplicate());

      });

    }
  }
}
