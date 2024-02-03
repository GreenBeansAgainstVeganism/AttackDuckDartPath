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
  class Bullseye : UpgradePlusPlus<DartPath>
  {
    public override int Cost => 155;
    public override int Tier => 2;
    //public override string Icon => GetTextureGUID(Name + "-Icon");

    public override string Description => "The first bloon each dart makes contact with takes damage twice.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
      CreateProjectileOnExhaustFractionModel bonushit = Game.instance.model.GetTowerFromId("BoomerangMonkey-500").GetAttackModel().GetDescendant<CreateProjectileOnExhaustFractionModel>().Duplicate();

      //ProjectileModel projectile = towerModel.GetAttackModel("AttackModel_Attack_").weapons[0].projectile;

      bonushit.projectile.id = "BullseyeBonusHit";
      bonushit.projectile.RemoveBehavior<AddBehaviorToBloonModel>();
      bonushit.projectile.RemoveBehavior<CollideOnlyWithTargetModel>();
      if (towerModel.tiers[2] >= 2)
      {
        bonushit.projectile.SetHitCamo(true);
      }

      // Must call ToList() to avoid live collection growing during iteration
      towerModel.GetDescendants<ProjectileModel>().ToList().ForEach(projectile =>
        {
          CreateProjectileOnExhaustFractionModel b = bonushit.Duplicate();

          b.projectile.AddBehavior(projectile.GetDamageModel().Duplicate());
          b.projectile.radius = projectile.radius;

          projectile.AddBehavior(b);

        });


    }
  }
}
