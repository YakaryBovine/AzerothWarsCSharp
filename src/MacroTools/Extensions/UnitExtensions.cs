using System;
using System.Collections.Generic;
using MacroTools.LegendSystem;
using MacroTools.Libraries;
using WCSharp.Shared.Data;
using static War3Api.Common;
using static MacroTools.Libraries.GeneralHelpers;

namespace MacroTools.Extensions
{
  /// <summary>
  /// Provides a series of useful extensions to the unit class.
  /// </summary>
  public static class UnitExtensions
  {
    private const float HeroDropDist = 50; //The radius in which heroes spread out items when they drop them

    /// <summary>
    /// Sets the unit's level to a particular value.
    /// </summary>
    public static unit SetLevel(this unit whichUnit, int newLevel, bool showEyeCandy = true)
    {
      var oldLevel = GetHeroLevel(whichUnit);
      if (newLevel > oldLevel)
        SetHeroLevel(whichUnit, newLevel, showEyeCandy);
      else if (newLevel < oldLevel)
        UnitStripHeroLevel(whichUnit, oldLevel - newLevel);

      return whichUnit;
    }

    /// <summary>
    /// Determines whether or not the unit's attack can be seen in the UI window.
    /// </summary>
    public static unit ShowAttackUi(this unit whichUnit, bool show, int weaponSlot = 0)
    {
      BlzSetUnitWeaponBooleanField(whichUnit, UNIT_WEAPON_BF_ATTACK_SHOW_UI, weaponSlot, show);
      return whichUnit;
    }

    public static unit SetUnitLevel(this unit whichUnit, int level)
    {
      BlzSetUnitIntegerField(whichUnit, UNIT_IF_LEVEL, level);
      return whichUnit;
    }

    /// <summary>
    /// Sets a unit's armor.
    /// </summary>
    public static unit SetArmor(this unit whichUnit, int armor)
    {
      BlzSetUnitArmor(whichUnit, armor);
      return whichUnit;
    }

    /// <summary>
    /// Returns the unit's maximum hit points.
    /// </summary>
    public static int GetMaximumHitPoints(this unit whichUnit) => BlzGetUnitMaxHP(whichUnit);

    /// <summary>
    /// Returns the unit's current hit points.
    /// </summary>
    public static float GetHitPoints(this unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_LIFE);

    /// <summary>
    /// Sets the unit's scaling value.
    /// </summary>
    public static unit SetScale(this unit whichUnit, float scale)
    {
      SetUnitScale(whichUnit, scale, scale, scale);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's maximum hit points.
    /// </summary>
    public static unit SetMaximumHitpoints(this unit whichUnit, int value)
    {
      BlzSetUnitMaxHP(whichUnit, value);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's current hit points.
    /// </summary>
    public static unit SetCurrentHitpoints(this unit whichUnit, int value)
    {
      SetUnitState(whichUnit, UNIT_STATE_LIFE, value);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's base damage.
    /// </summary>
    public static unit SetDamageBase(this unit whichUnit, int value, int weaponSlot = 0)
    {
      BlzSetUnitBaseDamage(whichUnit, value, weaponSlot);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's number of damage dice.
    /// </summary>
    public static unit SetDamageDiceNumber(this unit whichUnit, int value, int weaponSlot = 0)
    {
      BlzSetUnitDiceNumber(whichUnit, value, weaponSlot);
      return whichUnit;
    }

    /// <summary>
    /// Sets the number of sides on the unit's damage dice.
    /// </summary>
    public static unit SetDamageDiceSides(this unit whichUnit, int value, int weaponSlot = 0)
    {
      BlzSetUnitDiceSides(whichUnit, value, weaponSlot);
      return whichUnit;
    }

    /// <summary>
    /// Changes the unit's skin to match that of another unit type.
    /// </summary>
    public static unit SetSkin(this unit whichUnit, int skinUnitTypeId)
    {
      BlzSetUnitSkin(whichUnit, skinUnitTypeId);
      return whichUnit;
    }

    /// <summary>
    /// Changes the unit's name.
    /// </summary>
    public static unit SetName(this unit whichUnit, string name)
    {
      BlzSetUnitName(whichUnit, name);
      return whichUnit;
    }

    /// <summary>
    /// Returns true if the unit is an illusion.
    /// </summary>
    public static bool IsIllusion(this unit whichUnit) => IsUnitIllusion(whichUnit);

    /// <summary>
    /// Returns whether or not the unit is of the specified type.
    /// </summary>
    public static bool IsType(this unit whichUnit, unittype unitType) => IsUnitType(whichUnit, unitType);

    /// <summary>
    /// Forces the unit to face a particular direction.
    /// </summary>
    /// <returns>The same unit that was passed in.</returns>
    public static unit SetFacingEx(this unit whichUnit, float facing)
    {
      BlzSetUnitFacingEx(whichUnit, facing);
      return whichUnit;
    }

    /// <summary>
    /// Determines whether or not the unit explodes on death.
    /// </summary>
    public static unit SetExplodeOnDeath(this unit whichUnit, bool flag)
    {
      SetUnitExploded(whichUnit, flag);
      return whichUnit;
    }

    /// <summary>
    /// Gets the unit's unit level if it's a unit, or hero level if it's a hero.
    /// </summary>
    public static int GetLevel(this unit whichUnit) =>
      IsUnitType(whichUnit, UNIT_TYPE_HERO) ? GetHeroLevel(whichUnit) : GetUnitLevel(whichUnit);

    /// <summary>
    /// Changes the unit's colour to the specified values.
    /// <para>255 is full, 0 is empty.</para>
    /// </summary>
    /// <returns>The same unit that was passed in.</returns>
    public static unit SetColor(this unit whichUnit, int red, int green, int blue, int alpha)
    {
      SetUnitVertexColor(whichUnit, red, green, blue, alpha);
      return whichUnit;
    }

    /// <summary>
    /// Causes the unit to die after the specified duration, like a summoned unit.
    /// </summary>
    /// <param name="whichUnit">The unit to affect.</param>
    /// <param name="duration">How long the unit should last.</param>
    /// <param name="buffId">This buff's name is placed on the unit's timed life progress bar.</param>
    /// <returns></returns>
    public static unit SetTimedLife(this unit whichUnit, float duration, int buffId = 0)
    {
      if (duration < 1)
        throw new ArgumentException($"Cannot apply a timed life with a {nameof(duration)} less than 1.");

      UnitApplyTimedLife(whichUnit, buffId, duration);
      return whichUnit;
    }

    /// <summary>
    /// Gets the unit's type ID as shown in the object editor.
    /// </summary>
    public static int GetTypeId(this unit whichUnit) => GetUnitTypeId(whichUnit);

    /// <summary>
    /// Sets the unit's animation speed.
    /// </summary>
    /// <param name="whichUnit">The unit to set animation speed for.</param>
    /// <param name="speed">The animation speed. 0 is completely paused, 1 is normal.</param>
    /// <returns>The same unit that was passed in.</returns>
    public static unit SetAnimationSpeed(this unit whichUnit, float speed)
    {
      SetUnitTimeScale(whichUnit, speed);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's animation speed.
    /// </summary>
    /// <param name="whichUnit">The unit to set animation speed for.</param>
    /// <param name="animation">The name of the animation to play, e.g. "birth".</param>
    /// <returns>The same unit that was passed in.</returns>
    public static unit SetAnimation(this unit whichUnit, string animation)
    {
      SetUnitAnimation(whichUnit, animation);
      return whichUnit;
    }

    /// <summary>
    /// Gets the units proper name. If it doesn't have one, get their unit name instead.
    /// </summary>
    public static string GetProperName(this unit whichUnit) =>
      IsUnitType(whichUnit, UNIT_TYPE_HERO) ? GetHeroProperName(whichUnit) : GetUnitName(whichUnit);

    /// <summary>
    /// Returns the units name.
    /// </summary>
    public static string GetName(this unit whichUnit) => GetUnitName(whichUnit);

    /// <summary>
    /// Drops the item on the ground.
    /// </summary>
    public static unit DropItem(this unit whichUnit, item whichItem)
    {
      UnitRemoveItem(whichUnit, whichItem);
      return whichUnit;
    }

    /// <summary>
    /// Determines whether or not the unit exists in the game world.
    /// </summary>
    public static unit Show(this unit whichUnit, bool show)
    {
      ShowUnit(whichUnit, show);
      return whichUnit;
    }

    /// <summary>
    /// Kill the unit instantly.
    /// </summary>
    public static unit Kill(this unit whichUnit)
    {
      KillUnit(whichUnit);
      return whichUnit;
    }

    /// <summary>
    /// Pings the unit on the minimap.
    /// </summary>
    /// <param name="whichUnit">The unit to ping.</param>
    /// <param name="duration">How long the ping should last.</param>
    public static void Ping(this unit whichUnit, float duration) =>
      PingMinimap(GetUnitX(whichUnit), GetUnitY(whichUnit), duration);

    /// <summary>
    /// If true, prevents the unit from moving or taking actions.
    /// </summary>
    public static unit PauseEx(this unit unit, bool value)
    {
      BlzPauseUnitEx(unit, value);
      return unit;
    }

    /// <summary>
    /// If true, the unit cannot be targeted by attacks or hostile abilities and cannot be damaged.
    /// </summary>
    public static unit SetInvulnerable(this unit unit, bool value)
    {
      SetUnitInvulnerable(unit, value);
      return unit;
    }

    /// <summary>
    /// Removes the unit from the game permanently.
    /// </summary>
    public static void Remove(this unit unit)
    {
      RemoveUnit(unit);
    }

    /// <summary>
    /// Orders a unit to perform a specified order at a specified <see cref="Point"/>.
    /// </summary>
    public static unit IssueOrder(this unit unit, string order, Point target)
    {
      IssuePointOrder(unit, order, target.X, target.Y);
      return unit;
    }

    /// <summary>
    /// Orders a unit to perform a specified order on the specified target.
    /// </summary>
    public static unit IssueOrder(this unit unit, string order, widget target)
    {
      IssueTargetOrder(unit, order, target);
      return unit;
    }

    /// <summary>
    /// Orders a unit to perform the specified targetless order.
    /// </summary>
    public static unit IssueOrder(this unit unit, string order)
    {
      IssueImmediateOrder(unit, order);
      return unit;
    }
    
    /// <summary>
    /// Orders a unit to perform the specified targetless order.
    /// </summary>
    public static unit IssueOrder(this unit unit, int orderId)
    {
      IssueImmediateOrderById(unit, orderId);
      return unit;
    }

    /// <summary>
    /// Moves the unit to a specified <see cref="Point"/>.
    /// </summary>
    public static unit SetPosition(this unit unit, Point where, bool considerPathability = false)
    {
      if (!considerPathability)
      {
        SetUnitX(unit, where.X);
        SetUnitY(unit, where.Y);
      }
      else
        SetUnitPosition(unit, where.X, where.Y);

      return unit;
    }

    /// <summary>
    /// Returns the position of the unit.
    /// </summary>
    public static Point GetPosition(this unit unit) => new(GetUnitX(unit), GetUnitY(unit));

    /// <summary>
    /// Changess the unit's owner to the specified player.
    /// </summary>
    public static unit SetOwner(this unit unit, player whichPlayer, bool changeColor = true)
    {
      SetUnitOwner(unit, whichPlayer, changeColor);
      return unit;
    }

    /// <summary>
    /// Returns the current owner of the specified unit/
    /// </summary>
    public static player OwningPlayer(this unit unit)
    {
      return GetOwningPlayer(unit);
    }

    /// <summary>
    /// Determines whether or not the waygate is active.
    /// </summary>
    public static unit SetWaygateActive(this unit waygate, bool flag)
    {
      WaygateActivate(waygate, flag);
      return waygate;
    }

    /// <summary>
    ///   Sets the Waygate's destination to the target point.
    ///   Blindly assumes that the unit is a Waygate.
    /// </summary>
    public static unit SetWaygateDestination(this unit waygate, Point destination)
    {
      WaygateActivate(waygate, true);
      WaygateSetDestination(waygate, destination.X, destination.Y);
      return waygate;
    }

    /// <summary>
    ///   If the unit has the given item type, returns the item slot the item is in.
    ///   Otherwise, returns 0.
    ///   This function is 1-indexed.
    /// </summary>
    public static int GetInventoryIndexOfItemType(this unit whichUnit, int itemTypeId)
    {
      var index = 0;
      while (true)
      {
        var indexItem = UnitItemInSlot(whichUnit, index);
        if (indexItem != null && GetItemTypeId(indexItem) == itemTypeId) return index + 1;

        index += 1;
        if (index >= 6) break;
      }

      return 0;
    }

    /// <summary>
    /// Sets the units hit points to a specified percentage value.
    /// </summary>
    public static unit SetLifePercent(this unit whichUnit, float percent)
    {
      SetUnitState(whichUnit, UNIT_STATE_LIFE,
        GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE) * MathEx.Max(0, percent) * 0.01f);
      return whichUnit;
    }

    /// <summary>
    /// Returns the percentage hit points the unit has remaining.
    /// </summary>
    public static float GetLifePercent(this unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_LIFE) /
      GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE) * 100;

    /// <summary>
    ///   Resurrects the specified unit.
    /// </summary>
    public static void Resurrect(this unit whichUnit)
    {
      if (UnitAlive(whichUnit)) throw new ArgumentException("Tried to resurrect a unit that is already alive.");

      var x = GetUnitX(whichUnit);
      var y = GetUnitY(whichUnit);
      var unitType = GetUnitTypeId(whichUnit);
      var face = GetUnitFacing(whichUnit);
      DestroyEffect(AddSpecialEffect(@"Abilities\Spells\Human\Resurrect\ResurrectTarget.mdl", x, y));
      RemoveUnit(whichUnit);
      CreateUnit(OwningPlayer(whichUnit), unitType, x, y, face);
    }

    /// <summary>
    /// Damages the specified unit by the specified amount.
    /// </summary>
    /// <param name="unit">The unit to damage.</param>
    /// <param name="damager">The unit dealing the damage.</param>
    /// <param name="amount">The amount of damage to deal.</param>
    /// <param name="attack">Whether or not the damage instance is an attack.</param>
    /// <param name="ranged">Whether or not the damage instance is considered ranged.</param>
    /// <param name="attackType">The attack type to use.</param>
    /// <param name="damageType">The damage type to use.</param>
    /// <param name="weaponType">The weapon type to use.</param>
    public static void TakeDamage(this unit unit, unit damager, float amount, bool attack = false, bool ranged = true,
      attacktype? attackType = null, damagetype? damageType = null, weapontype? weaponType = null)
    {
      UnitDamageTarget(damager, unit, amount, attack, ranged, attackType ?? ATTACK_TYPE_NORMAL,
        damageType ?? DAMAGE_TYPE_MAGIC, weaponType ?? WEAPON_TYPE_WHOKNOWS);
    }

    /// <summary>
    /// Restore mana to the unit.
    /// </summary>
    /// <param name="whichUnit">The unit to restore mana to.</param>
    /// <param name="amount">How much mana to restore.</param>
    public static void RestoreMana(this unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MANA,
      whichUnit.GetMana() + amount);

    /// <summary>
    /// Returns the current mana value of the unit.
    /// </summary>
    public static float GetMana(this unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_MANA);

    /// <summary>
    /// Heals the specified unit by the specified amount.
    /// </summary>
    /// <param name="unit">The unit to heal.</param>
    /// <param name="amount">The amount of damage to heal.</param>
    public static void Heal(this unit unit, float amount)
    {
      SetUnitState(unit, UNIT_STATE_LIFE, GetUnitState(unit, UNIT_STATE_LIFE) + amount);
    }

    /// <summary>
    ///   Reveals the unit, makes it vulnerable, and transfers its ownership to the specified player.
    /// </summary>
    public static void Rescue(this unit whichUnit, player whichPlayer)
    {
      //If the unit costs 10 food, that means it should be owned by neutral passive instead of the rescuing player.
      whichUnit
        .SetOwner(GetUnitFoodUsed(whichUnit) == 10 ? Player(PLAYER_NEUTRAL_PASSIVE) : whichPlayer)
        .Show(true)
        .PauseEx(false);

      var asCapital = CapitalManager.GetFromUnit(whichUnit);
      if (asCapital == null || asCapital.ProtectorCount == 0)
        whichUnit.SetInvulnerable(false);
    }

    /// <summary>
    /// The amount of damage the unit deals on average when it uses its basic attack.
    /// </summary>
    /// <param name="whichUnit">The unit to get the average damage for.</param>
    /// <param name="weaponIndex">Which weapon to return information about; can be 1 or 2.</param>
    public static int GetAverageDamage(this unit whichUnit, int weaponIndex)
    {
      float baseDamage = BlzGetUnitBaseDamage(whichUnit, weaponIndex);
      float numberOfDice = BlzGetUnitDiceNumber(whichUnit, weaponIndex);
      float sidesPerDie = BlzGetUnitDiceSides(whichUnit, weaponIndex);
      return R2I(baseDamage + (numberOfDice + sidesPerDie * numberOfDice) / 2);
    }

    /// <summary>
    ///   Gets a units physical damage reduction as a percentage. Only takes armor into account.
    /// </summary>
    public static float GetDamageReduction(this unit whichUnit)
    {
      var armor = BlzGetUnitArmor(whichUnit);
      return armor * 006 / ((1 + 006) * armor);
    }

    /// <summary>
    ///   Increases the unit's Strength, Agility, or Intelligence.
    ///   Displays a special effect depending on which attributes are increased.
    /// </summary>
    public static unit AddHeroAttributes(this unit whichUnit, int str, int agi, int intelligence)
    {
      SetHeroStr(whichUnit, GetHeroStr(whichUnit, false) + str, true);
      SetHeroAgi(whichUnit, GetHeroAgi(whichUnit, false) + agi, true);
      SetHeroInt(whichUnit, GetHeroInt(whichUnit, false) + intelligence, true);

      string sfx;
      if (str > 0 && agi == 0 && intelligence == 0)
        sfx = "Abilities\\Spells\\Items\\AIsm\\AIsmTarget.mdl";
      else if (str == 0 && agi > 0 && intelligence == 0)
        sfx = "Abilities\\Spells\\Items\\AIam\\AIamTarget.mdl";
      else if (str == 0 && agi == 0 && intelligence > 0)
        sfx = "Abilities\\Spells\\Items\\AIim\\AIimTarget.mdl";
      else
        sfx = "Abilities\\Spells\\Items\\AIlm\\AIlmTarget.mdl";

      DestroyEffect(AddSpecialEffect(sfx, GetUnitX(whichUnit), GetUnitY(whichUnit)));
      return whichUnit;
    }

    /// <summary>
    /// Adds an amount of experience to the hero.
    /// </summary>
    public static unit AddExperience(this unit whichUnit, int amount)
    {
      AddHeroXP(whichUnit, amount, true);
      return whichUnit;
    }

    /// <summary>
    ///   Drops a units entire inventory on the ground.
    /// </summary>
    public static unit DropAllItems(this unit whichUnit)
    {
      if (IsUnitType(whichUnit, UNIT_TYPE_SUMMONED))
        throw new InvalidOperationException($"Tried to call {nameof(DropAllItems)} on a summoned hero.");

      var unitX = GetUnitX(whichUnit);
      var unitY = GetUnitY(whichUnit);
      float angInRadians = 0;

      for (var i = 0; i < 6; i++)
      {
        var x = unitX + HeroDropDist * Cos(angInRadians);
        var y = unitY + HeroDropDist * Sin(angInRadians);
        angInRadians += 360 * MathEx.DegToRad / 6;
        var itemToDrop = UnitItemInSlot(whichUnit, i);
        if (!BlzGetItemBooleanField(itemToDrop, ITEM_BF_DROPPED_WHEN_CARRIER_DIES) &&
            !BlzGetItemBooleanField(itemToDrop, ITEM_BF_CAN_BE_DROPPED)) continue;
        whichUnit.DropItem(itemToDrop);
        itemToDrop.SetPositionSafe(new Point(x, y));
      }

      return whichUnit;
    }

    /// <summary>
    ///   Transfers all of this unit's items to another unit.
    /// </summary>
    public static void TransferItems(this unit sender, unit receiver)
    {
      for (var i = 0; i < 6; i++) UnitAddItem(receiver, UnitItemInSlot(sender, i));
    }

    /// <summary>
    ///   Add an item to a unit. If the unit's inventory is full, drop it on the ground near them instead.
    /// </summary>
    public static unit AddItemSafe(this unit whichUnit, item whichItem)
    {
      SetItemPosition(whichItem, GetUnitX(whichUnit), GetUnitY(whichUnit));
      UnitAddItem(whichUnit, whichItem);
      return whichUnit;
    }

    /// <summary>
    /// Multiplities the specified unit's attack damage by the specified amount.
    /// </summary>
    /// <param name="whichUnit">The unit to affect.</param>
    /// <param name="multiplier">The amount to multiply attack damage by.</param>
    /// <param name="weaponIndex">Which weapon to return information about; can be 1 or 2.</param>
    public static unit MultiplyBaseDamage(this unit whichUnit, float multiplier, int weaponIndex)
    {
      BlzSetUnitBaseDamage(whichUnit, R2I(I2R(BlzGetUnitBaseDamage(whichUnit, weaponIndex)) * multiplier), weaponIndex);
      return whichUnit;
    }

    /// <summary>
    /// Multiplities the specified unit's hit points by the specified amount.
    /// </summary>
    /// <param name="whichUnit">The unit to affect.</param>
    /// <param name="multiplier">The amount to multiply hit points by.</param>
    public static unit MultiplyMaxHitpoints(this unit whichUnit, float multiplier)
    {
      var percentageHitpoints = whichUnit.GetLifePercent();
      BlzSetUnitMaxHP(whichUnit, R2I(I2R(BlzGetUnitMaxHP(whichUnit)) * multiplier));
      whichUnit.SetLifePercent(percentageHitpoints);
      return whichUnit;
    }

    /// <summary>
    /// Multiplities the specified unit's mana by the specified amount.
    /// </summary>
    /// <param name="whichUnit">The unit to affect.</param>
    /// <param name="multiplier">The amount to multiply hit points by.</param>
    public static unit MultiplyMaxMana(this unit whichUnit, float multiplier)
    {
      var percentageHitpoints = whichUnit.GetManaPercent();
      BlzSetUnitMaxMana(whichUnit, R2I(I2R(BlzGetUnitMaxMana(whichUnit)) * multiplier));
      whichUnit.SetManaPercent(percentageHitpoints);
      return whichUnit;
    }

    /// <summary>
    /// Gets the percentage of mana that a unit has remaining.
    /// </summary>
    public static float GetManaPercent(this unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_MANA) /
      GetUnitState(whichUnit, UNIT_STATE_MAX_MANA) * 100;

    /// <summary>
    /// Sets the percentage of mana a unit has remaining.
    /// </summary>
    public static unit SetManaPercent(this unit whichUnit, float percent)
    {
      SetUnitState(whichUnit, UNIT_STATE_MANA,
        GetUnitState(whichUnit, UNIT_STATE_MAX_MANA) * MathEx.Max(0, percent) * 0.01f);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's maximum mana.
    /// </summary>
    /// <returns>The same unit that was provided.</returns>
    public static unit SetMaximumMana(this unit whichUnit, int maximumMana)
    {
      BlzSetUnitMaxMana(whichUnit, maximumMana);
      return whichUnit;
    }

    /// <summary>
    /// Sets the unit's current mana.
    /// </summary>
    /// <returns>The same unit that was provided.</returns>
    public static unit SetMana(this unit whichUnit, int value)
    {
      SetUnitState(whichUnit, UNIT_STATE_MANA, value);
      return whichUnit;
    }

    /// <summary>
    /// Returns the unit's facing angle.
    /// </summary>
    public static float GetFacing(this unit whichUnit) => GetUnitFacing(whichUnit);

    /// <summary>
    /// Returns the unit's active rally point.
    /// </summary>
    public static Point GetRallyPoint(this unit whichUnit)
    {
      var rallyLocation = GetUnitRallyPoint(whichUnit);
      var rallyPoint = new Point(GetLocationX(rallyLocation), GetLocationY(rallyLocation));
      return rallyPoint;
    }

    /// <summary>
    /// Adds an ability to the unit.
    /// </summary>
    public static unit AddAbility(this unit whichUnit, int abilityTypeId)
    {
      UnitAddAbility(whichUnit, abilityTypeId);
      UnitMakeAbilityPermanent(whichUnit, true, abilityTypeId);
      return whichUnit;
    }

    /// <summary>
    /// Sets a specific ability of a unit to the specified level.
    /// </summary>
    /// <returns></returns>
    public static unit SetAbilityLevel(this unit whichUnit, int abilityTypeId, int level)
    {
      SetUnitAbilityLevel(whichUnit, abilityTypeId, level);
      return whichUnit;
    }

    /// <summary>
    /// Removes an ability from a unit.
    /// </summary>
    public static unit RemoveAbility(this unit whichUnit, int abilityTypeId)
    {
      UnitRemoveAbility(whichUnit, abilityTypeId);
      return whichUnit;
    }

    /// <summary>
    /// Returns true if the unit is alive.
    /// </summary>
    public static bool IsAlive(this unit whichUnit) => UnitAlive(whichUnit);

    /// <summary>
    /// Changes a unit's attack type as an integer.
    /// </summary>
    public static unit SetAttackType(this unit whichUnit, int attackType)
    {
      BlzSetUnitWeaponIntegerField(whichUnit, UNIT_WEAPON_IF_ATTACK_ATTACK_TYPE, 0, attackType);
      return whichUnit;
    }

    /// <summary>
    /// Returns the unit's attack type as an integer.
    /// </summary>
    public static int GetAttackType(this unit whichUnit) =>
      BlzGetUnitWeaponIntegerField(whichUnit, UNIT_WEAPON_IF_ATTACK_ATTACK_TYPE, 0);

    /// <summary>
    /// Changes a unit's armor type.
    /// </summary>
    public static unit SetArmorType(this unit whichUnit, int armorType)
    {
      BlzSetUnitIntegerField(whichUnit, UNIT_IF_DEFENSE_TYPE, armorType);
      return whichUnit;
    }

    /// <summary>
    /// Adds an additional unit type to the unit.
    /// </summary>
    /// <returns>The same unit that was passed in.</returns>
    public static unit AddType(this unit whichUnit, unittype whichUnitType)
    {
      UnitAddType(whichUnit, whichUnitType);
      return whichUnit;
    }

    /// <summary>
    /// Causes the specified unit to become capturable,
    /// such that it changes ownership to the attacker when reduced below 0 hit points.
    /// </summary>
    public static unit MakeCapturable(this unit whichUnit)
    {
      CreateTrigger()
        .RegisterUnitEvent(whichUnit, EVENT_UNIT_DAMAGED)
        .AddAction(() =>
        {
          if (!(GetEventDamage() + 1 >= GetUnitState(whichUnit, UNIT_STATE_LIFE))) return;
          SetUnitOwner(whichUnit, GetOwningPlayer(GetEventDamageSource()), true);
          BlzSetEventDamage(0);
          SetUnitState(whichUnit, UNIT_STATE_LIFE, GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE));
        });
      return whichUnit;
    }

    /// <summary>
    /// Activates an ability's full cooldown for a unit.
    /// </summary>
    public static unit StartUnitAbilityCooldownFull(this unit whichUnit, int abilCode)
    {
      var fullCooldown = BlzGetUnitAbilityCooldown(whichUnit, abilCode, 0);
      BlzStartUnitAbilityCooldown(whichUnit, abilCode, fullCooldown);
      return whichUnit;
    }

    /// <summary>
    /// Turns the unit to face a particular position.
    /// </summary>
    public static unit FacePosition(this unit whichUnit, Point targetPoint)
    {
      var unitPosition = whichUnit.GetPosition();
      var facing = WCSharp.Shared.Util.AngleBetweenPoints(unitPosition.X, unitPosition.Y, targetPoint.X, targetPoint.Y);
      BlzSetUnitFacingEx(whichUnit, facing);
      return whichUnit;
    }

    /// <summary>
    /// Returns true if the unit is a hero, has Resistant Skin, or is a creep with a level of 6 or higher.
    /// </summary>
    public static bool IsResistant(this unit whichUnit)
    {
      return whichUnit.IsType(UNIT_TYPE_RESISTANT) || whichUnit.IsType(UNIT_TYPE_HERO) ||
             (whichUnit.OwningPlayer() == Player(PLAYER_NEUTRAL_AGGRESSIVE) && whichUnit.GetLevel() >= 6);
    }

    /// <summary>
    /// Removes All abilities not in the ignore list from a unit
    /// </summary>
    /// <param name="whichUnit">The unit to remove abilities from</param>
    /// <param name="ignoredAbilityId">List of ability Ids to not be removed. </param>
    /// <returns>A List of abilityids for a given unit.</returns>
    public static unit RemoveAllAbilities(this unit whichUnit, List<int> ignoredAbilityId)
    {
      
      var abilities = GetUnitAbilities(whichUnit);

      foreach (var ability in abilities)
      {
        var abilityid = BlzGetAbilityId(ability);
        if (!ignoredAbilityId.Contains(abilityid))
        {
          RemoveAbility(whichUnit, abilityid);
        }
      }

      return whichUnit;
    }
    
    /// <summary>
    /// Gets all the abilities for a unit
    /// </summary>
    /// <param name="whichUnit">The unit to get the abilities of.</param>
    /// <returns>A List of ability objects for a given unit.</returns>
    public static List<ability> GetUnitAbilities(this unit whichUnit)
    {
      
      var abilities = new List<ability>();
      var index = 0;

      while(true)
      {
        var ability = BlzGetUnitAbilityByIndex(whichUnit, index);

        if (ability is null)
          break;
        
        abilities.Add(ability);
        index++;
      }

      return abilities;
    }
  }
}