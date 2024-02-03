using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
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
  class MoabWrangler : UpgradePlusPlus<DartPath>
  {
    public override int Cost => 15600;
    public override int Tier => 5;
    //public override string Icon => GetTextureGUID(Name + "-Icon");

    public override string Description => "Throws multiple bolases at once and excels at trapping large swathes of bloons. Moab Snare Ability: Lays a trap which, when triggered, traps even the biggest bloons around it, slowing them and tripling all damage dealt to them for a short window of time.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
      AttackModel attack = towerModel.GetAttackModel("AttackModel_Attack_");

      attack.weapons[0].SetEmission(new RandomEmissionModel("RandomEmissionModel", 2, 75f, 10f, null, false, 0f, 0f, 0f, false));
      attack.weapons[0].rate *= 0.33f;
      towerModel.IncreaseRange(8f);

      ProjectileModel projectile = attack.weapons[0].projectile;

      projectile.GetDamageModel().damage += 1;

      ProjectileModel splash = projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;

      splash.radius += 8f;
      splash.GetDamageModel().damage += 1;
      //splash.pierce += towerModel.tiers[0] >= 2 ? 20 : towerModel.tiers[0] >= 1 ? 16 : 12;
      splash.GetBehavior<SlowModifierForTagModel>().slowMultiplier = 1.0f;

      AbilityModel ability = Game.instance.model.GetTowerFromId("ObynGreenfoot 10").GetAbility(1).Duplicate();

      ability.Cooldown = 30f;
      ability.displayName = "Moab Snare";
      ability.name = "AbilityModel_MoabSnare";
      ability.icon = GetSpriteReference("SnareDisplay");

      ProjectileModel snare = projectile.Duplicate();

      snare.RemoveBehavior<TrackTargetModel>();
      snare.RemoveBehavior<TravelStraitModel>();
      snare.RemoveBehavior<RotateModel>();
      snare.AddBehavior(new AgeModel("AgeModel", 9999999f, 3, false, null));
      snare.AddBehavior(new InstantModel("InstantModel",false));
      snare.radius = 8f;

      ProjectileModel snaresplash = snare.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;

      snaresplash.radius = 32f;
      snaresplash.pierce = 999f;
      snaresplash.GetDamageModel().damage = 50f;

      snaresplash.GetBehavior<SlowForBloonModel>().bloonId = "";
      snaresplash.GetBehavior<SlowForBloonModel>().mutationId = "AttackDuckDartPath_MoabSnare";
      snaresplash.GetBehavior<SlowForBloonModel>().Lifespan = 5f;
      snaresplash.GetBehavior<SlowModifierForTagModel>().tag = "Bad";
      snaresplash.GetBehavior<SlowModifierForTagModel>().slowMultiplier = 2.0f;

      //snaresplash.RemoveBehavior<DisplayModel>();
      //snaresplash.AddBehavior(Game.instance.model.GetTowerFromId("HeliPilot-030").GetAttackModel("AttackModel_Downdraft_Downdraft").weapons[0].projectile.GetBehavior<DisplayModel>().Duplicate());
      //snaresplash.display = snaresplash.GetBehavior<DisplayModel>().display;
      //snaresplash.scale = 2.0f;

      ability.GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile = snare;
      ability.GetBehavior<ActivateAttackModel>().attacks[0].range = towerModel.range;


      towerModel.AddBehavior(ability);
      towerModel.towerSelectionMenuThemeId = "SelectPointInput";

      snare.ApplyDisplay<Displays.Projectiles.SnareDisplay>();
    }
  }
}
