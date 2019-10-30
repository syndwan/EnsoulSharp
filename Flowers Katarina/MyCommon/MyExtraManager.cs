namespace Flowers_Katarina.MyCommon
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SharpDX;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI.Values;
    using EnsoulSharp.SDK.Utility;

    using Flowers_Katarina.MyBase;

    #endregion

    public static class MyExtraManager
    {
        public static float GetComboDamage(AIHeroClient target)
        {
            if (target == null || target.IsDead || !target.IsValidTarget())
            {
                return 0;
            }

            if (target.IsUnKillable())
            {
                return 0;
            }

            var damage = 0d;

            if (MyLogic.IgniteSlot != SpellSlot.Unknown && MyLogic.Ignite.IsReady())
            {
                damage += ObjectManager.Player.GetIgniteDamage(target);
            }

            if (MyLogic.Q.IsReady())
            {
                damage += GetDamage(MyLogic.Q, target);
            }

            if (MyLogic.W.IsReady())
            {
                damage += GetDamage(MyLogic.W, target);
            }

            if (MyLogic.E.IsReady())
            {
                damage += GetDamage(MyLogic.E, target);
            }

            if (MyLogic.R.IsReady())
            {
                damage += GetDamage(MyLogic.R, target);
            }

            if (ObjectManager.Player.CharacterName == "Katarina")
            {
                var targetDagger =
                    ObjectManager.Get<AIMinionClient>()
                        .Where(
                            x =>
                                x.CharacterName == "testcuberender" && x.Health > 1 && x.IsValid &&
                                x.Distance(target) <= 340).ToArray();

                if (targetDagger.Any())
                {
                    damage += GetKataPassiveDamage(target) * targetDagger.Count();
                }
            }

            if (ObjectManager.Player.HasBuff("SummonerExhaust"))
            {
                damage = damage * 0.6f;
            }

            if (target.CharacterName == "Morderkaiser")
            {
                damage -= target.Mana;
            }

            if (target.HasBuff("GarenW"))
            {
                damage = damage * 0.7f;
            }

            if (target.HasBuff("ferocioushowl"))
            {
                damage = damage * 0.7f;
            }

            if (target.HasBuff("BlitzcrankManaBarrierCD") && target.HasBuff("ManaBarrier"))
            {
                damage -= target.Mana / 2f;
            }

            return (float)damage;
        }

        public static double GetDamage(this Spell spell, AIBaseClient target)
        {
            if (!spell.IsReady())
            {
                return 0;
            }

            double dmg = 0d;

            switch (spell.Slot)
            {
                case SpellSlot.Q:
                    dmg =
                    new double[] { 75, 105, 135, 165, 195 }[ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level - 1
                    ] + 0.3f * ObjectManager.Player.TotalMagicalDamage;
                    break;
                case SpellSlot.W:
                    dmg = 0d;
                    break;
                case SpellSlot.E:
                    dmg =
                        new double[] {30, 45, 60, 75, 90}[ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level - 1
                        ] +
                        0.25f * ObjectManager.Player.TotalMagicalDamage +
                        0.65f * ObjectManager.Player.TotalAttackDamage;
                    break;
                case SpellSlot.R:
                    dmg =
                        new[] {375, 562.5, 750}[ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level - 1
                        ] +
                        2.85f * ObjectManager.Player.TotalMagicalDamage +
                        3.30f * ObjectManager.Player.TotalAttackDamage;
                    break;
            }

            if (dmg > 0)
            {
                return ObjectManager.Player.CalculateDamage(target, DamageType.Magical, dmg);
            }

            return 0d;
        }


        public static float GetKataPassiveDamage(AIBaseClient target)
        {
            var hant = ObjectManager.Player.Level < 6
                ? 0
                : (ObjectManager.Player.Level < 11
                    ? 1
                    : (ObjectManager.Player.Level < 16 ? 2 : 3));
            var damage = new double[]
                             {75, 78, 83, 88, 95, 103, 112, 122, 133, 145, 159, 173, 189, 206, 224, 243, 264, 245}[
                             ObjectManager.Player.Level - 1]
                         + ObjectManager.Player.FlatPhysicalDamageMod
                         + new[] { 0.55, 0.70, 0.85, 1 }[hant] * ObjectManager.Player.TotalMagicalDamage;

            return (float)ObjectManager.Player.CalculateDamage(target, DamageType.Magical, damage);
        }

        public static bool CanMoveMent(this AIBaseClient target)
        {
            return !(target.MoveSpeed < 50) && !target.HasBuffOfType(BuffType.Stun) &&
                   !target.HasBuffOfType(BuffType.Fear) && !target.HasBuffOfType(BuffType.Snare) &&
                   !target.HasBuffOfType(BuffType.Knockup) && !target.HasBuff("recall") &&
                   !target.HasBuffOfType(BuffType.Knockback)
                   && !target.HasBuffOfType(BuffType.Charm) && !target.HasBuffOfType(BuffType.Taunt) &&
                   !target.HasBuffOfType(BuffType.Suppression) &&
                   !target.HasBuff("zhonyasringshield") && !target.HasBuff("bardrstasis");
        }

        public static bool IsUnKillable(this AIBaseClient target)
        {
            if (target == null || target.IsDead || target.Health <= 0)
            {
                return true;
            }

            if (target.HasBuff("KindredRNoDeathBuff"))
            {
                return true;
            }

            if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3f &&
                target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            if (target.HasBuff("JudicatorIntervention"))
            {
                return true;
            }

            if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3f &&
                target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            if (target.HasBuff("VladimirSanguinePool"))
            {
                return true;
            }

            if (target.HasBuff("ShroudofDarkness"))
            {
                return true;
            }

            if (target.HasBuff("SivirShield"))
            {
                return true;
            }

            if (target.HasBuff("itemmagekillerveil"))
            {
                return true;
            }

            return target.HasBuff("FioraW");
        }

        public static double GetIgniteDamage(this AIHeroClient source, AIHeroClient target)
        {
            return 50 + 20 * source.Level - target.HPRegenRate / 5 * 3;
        }

        public static double GetRealDamage(this Spell spell, AIBaseClient target, bool havetoler = false, float tolerDMG = 0)
        {
            if (target != null && !target.IsDead && target.Buffs.Any(a => a.Name.ToLower().Contains("kalistaexpungemarker")))
            {
                if (target.HasBuff("KindredRNoDeathBuff"))
                {
                    return 0;
                }

                if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3f)
                {
                    return 0;
                }

                if (target.HasBuff("JudicatorIntervention"))
                {
                    return 0;
                }

                if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3f)
                {
                    return 0;
                }

                if (target.HasBuff("FioraW"))
                {
                    return 0;
                }

                if (target.HasBuff("ShroudofDarkness"))
                {
                    return 0;
                }

                if (target.HasBuff("SivirShield"))
                {
                    return 0;
                }

                var damage = 0d;

                damage += spell.IsReady()
                    ? ObjectManager.Player.GetSpellDamage(target, spell.Slot)
                    : 0d + (havetoler ? tolerDMG : 0) - target.HPRegenRate;

                if (target.CharacterName == "Morderkaiser")
                {
                    damage -= target.Mana;
                }

                if (ObjectManager.Player.HasBuff("SummonerExhaust"))
                {
                    damage = damage * 0.6f;
                }

                if (target.HasBuff("BlitzcrankManaBarrierCD") && target.HasBuff("ManaBarrier"))
                {
                    damage -= target.Mana / 2f;
                }

                if (target.HasBuff("GarenW"))
                {
                    damage = damage * 0.7f;
                }

                if (target.HasBuff("ferocioushowl"))
                {
                    damage = damage * 0.7f;
                }

                return damage;
            }

            return 0d;
        }

        public static double GetRealDamage(this AIBaseClient target, double DMG)
        {
            if (target != null && !target.IsDead && target.Buffs.Any(a => a.Name.ToLower().Contains("kalistaexpungemarker")))
            {
                if (target.HasBuff("KindredRNoDeathBuff"))
                {
                    return 0;
                }

                if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3f)
                {
                    return 0;
                }

                if (target.HasBuff("JudicatorIntervention"))
                {
                    return 0;
                }

                if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3f)
                {
                    return 0;
                }

                if (target.HasBuff("FioraW"))
                {
                    return 0;
                }

                if (target.HasBuff("ShroudofDarkness"))
                {
                    return 0;
                }

                if (target.HasBuff("SivirShield"))
                {
                    return 0;
                }

                var damage = 0d;

                damage += DMG - target.HPRegenRate;

                if (target.CharacterName == "Morderkaiser")
                {
                    damage -= target.Mana;
                }

                if (ObjectManager.Player.HasBuff("SummonerExhaust"))
                {
                    damage = damage * 0.6f;
                }

                if (target.HasBuff("BlitzcrankManaBarrierCD") && target.HasBuff("ManaBarrier"))
                {
                    damage -= target.Mana / 2f;
                }

                if (target.HasBuff("GarenW"))
                {
                    damage = damage * 0.7f;
                }

                if (target.HasBuff("ferocioushowl"))
                {
                    damage = damage * 0.7f;
                }

                return damage;
            }

            return 0d;
        }

        public static void QEWLogic(AIHeroClient target, bool useQ, bool useW, bool useE)
        {
            if (target == null || !target.IsValidTarget())
            {
                target = TargetSelector.GetTarget(MyLogic.E.Range + 300f);
            }

            if (target == null || !target.IsValidTarget() || !target.IsValidTarget())
            {
                return;
            }

            if (useQ && MyLogic.Q.IsReady())
            {
                if (target.IsValidTarget(MyLogic.Q.Range))
                {
                    if (!(MyLogic.W.IsReady() && MyLogic.E.IsReady()) && target.DistanceToPlayer() <= 300 ||
                        !target.IsFacing(ObjectManager.Player) && target.DistanceToPlayer() > 300)
                    {
                        MyLogic.Q.CastOnUnit(target);
                    }
                }
                else if (target.IsValidTarget(MyLogic.E.Range) &&
                         (Orbwalker.ActiveMode == OrbwalkerMode.Combo &&
                          MyLogic.ComboMenu["FlowersKatarina.ComboMenu.QOnMinion"].GetValue<MenuBool>().Enabled ||
                          Orbwalker.ActiveMode == OrbwalkerMode.Harass &&
                          MyLogic.HarassMenu["FlowersKatarina.HarassMenu.QOnMinion"].GetValue<MenuBool>()))
                {
                    var extraDagger = target.PreviousPosition.Extend(ObjectManager.Player.PreviousPosition, 350f);
                    var min = ObjectManager.Get<AIBaseClient>().Aggregate((x, y) => x.Distance(extraDagger) < y.Distance(extraDagger) ? x : y);

                    if (min.Distance(extraDagger) < 130)
                    {
                        MyLogic.Q.CastOnUnit(min);
                    }
                }
            }

            if (useE && MyLogic.E.IsReady() && target.IsValidTarget(MyLogic.E.Range + 300f) && !MyLogic.Q.IsReady())
            {
                var ePos = GetEPosition(target);

                if (ePos != Vector3.Zero && ePos.DistanceToPlayer() <= MyLogic.E.Range && CanCastE(ePos, target))
                {
                    if (MyLogic.MiscMenu["FlowersKatarina.MiscMenu.EHumanizer"].GetValue<MenuSliderButton>().Enabled)
                    {
                        DelayAction.Add(
                            MyLogic.MiscMenu["FlowersKatarina.MiscMenu.EHumanizer"].GetValue<MenuSliderButton>().Value,
                            () => MyLogic.E.Cast(ePos));
                    }
                    else
                    {
                        MyLogic.E.Cast(ePos);
                    }
                }
            }

            if (useW && MyLogic.W.IsReady() && target.IsValidTarget(MyLogic.W.Range))
            {
                MyLogic.W.Cast();
            }
        }

        public static void EQWLogic(AIHeroClient target, bool useQ, bool useW, bool useE)
        {
            if (target == null || !target.IsValidTarget())
            {
                target = TargetSelector.GetTarget(MyLogic.E.Range + 300f);
            }

            if (target == null || !target.IsValidTarget() || !target.IsValidTarget())
            {
                return;
            }

            if (useE && MyLogic.E.IsReady() && target.IsValidTarget(MyLogic.E.Range + 300f))
            {
                var ePos = GetEPosition(target);

                if (ePos != Vector3.Zero && ePos.DistanceToPlayer() <= MyLogic.E.Range && CanCastE(ePos, target))
                {
                    if (MyLogic.MiscMenu["FlowersKatarina.MiscMenu.EHumanizer"].GetValue<MenuSliderButton>().Enabled)
                    {
                        DelayAction.Add(
                            MyLogic.MiscMenu["FlowersKatarina.MiscMenu.EHumanizer"].GetValue<MenuSliderButton>().Value,
                            () => MyLogic.E.Cast(ePos));
                    }
                    else
                    {
                        MyLogic.E.Cast(ePos);
                    }
                }
            }

            if (useQ && MyLogic.Q.IsReady() && target.IsValidTarget(MyLogic.Q.Range) && !MyLogic.E.IsReady())
            {
                if (target.IsValidTarget(MyLogic.Q.Range))
                {
                    if (!(MyLogic.W.IsReady() && MyLogic.E.IsReady()) && target.DistanceToPlayer() <= 300 ||
                        !target.IsFacing(ObjectManager.Player) && target.DistanceToPlayer() > 300)
                    {
                        MyLogic.Q.CastOnUnit(target);
                    }
                }
                else if (target.IsValidTarget(MyLogic.E.Range) &&
                         (Orbwalker.ActiveMode == OrbwalkerMode.Combo &&
                          MyLogic.ComboMenu["FlowersKatarina.ComboMenu.QOnMinion"].GetValue<MenuBool>().Enabled ||
                          Orbwalker.ActiveMode == OrbwalkerMode.Harass &&
                          MyLogic.HarassMenu["FlowersKatarina.HarassMenu.QOnMinion"].GetValue<MenuBool>()))
                {
                    var extraDagger = target.PreviousPosition.Extend(ObjectManager.Player.PreviousPosition, 350f);
                    var min = ObjectManager.Get<AIBaseClient>().Aggregate((x, y) => x.Distance(extraDagger) < y.Distance(extraDagger) ? x : y);

                    if (min.Distance(extraDagger) < 130)
                    {
                        MyLogic.Q.CastOnUnit(min);
                    }
                }
            }

            if (useW && MyLogic.W.IsReady() && target.IsValidTarget(MyLogic.W.Range))
            {
                MyLogic.W.Cast();
            }
        }

        public static Vector3 GetEPosition(AIHeroClient target)
        {
            var pos = Vector3.Zero;

            if (!target.IsValidTarget(MyLogic.E.Range + MyLogic.PassiveRange))
            {
                pos = Vector3.Zero;
            }
            else
            {
                if (MyLogic.Daggers.Any(
                    x =>
                        GameObjects.EnemyHeroes.Any(a => a.Distance(x.Position) <= MyLogic.PassiveRange) &&
                        x.Position.DistanceToPlayer() <= MyLogic.E.Range))
                {
                    foreach (
                        var obj in
                        MyLogic.Daggers.Where(x => x.Position.Distance(target.PreviousPosition) <= MyLogic.PassiveRange)
                            .OrderByDescending(x => x.Position.Distance(target.PreviousPosition)))
                    {
                        if (obj.Dagger != null && obj.Dagger.IsValid && obj.Position.DistanceToPlayer() <= MyLogic.E.Range)
                        {
                            pos = obj.Position;
                        }
                    }
                }
                else if (
                 MyLogic.Daggers.Any(
                        x =>
                            GameObjects.EnemyHeroes.Any(a => a.Distance(x.Position) <= MyLogic.E.Range) &&
                            x.Position.DistanceToPlayer() <= MyLogic.E.Range))
                {
                    foreach (
                        var obj in
                      MyLogic.Daggers.Where(x => x.Position.Distance(target.PreviousPosition) <= MyLogic.E.Range)
                            .OrderBy(x => x.Position.Distance(target.PreviousPosition)))
                    {
                        if (obj.Dagger != null && obj.Dagger.IsValid && obj.Position.DistanceToPlayer() <= MyLogic.E.Range)
                        {
                            pos = obj.Position;
                        }
                    }
                }
                else if (target.DistanceToPlayer() <= MyLogic.E.Range - 130)
                {
                    pos = ObjectManager.Player.PreviousPosition.Extend(target.PreviousPosition, target.DistanceToPlayer() + 130);
                }
                else if (target.IsValidTarget(MyLogic.E.Range))
                {
                    pos = target.PreviousPosition;
                }
                else
                {
                    pos = Vector3.Zero;
                }
            }

            return pos;
        }

        public static bool CanCastE(Vector3 pos, AIHeroClient target)
        {
            if (pos == Vector3.Zero || target == null || target.IsDead)
            {
                return false;
            }

            if (MyLogic.MiscMenu["FlowersKatarina.MiscMenu.ETurret"].GetValue<MenuList>().Index == 0 && pos.IsUnderEnemyTurret())
            {
                return false;
            }

            if (MyLogic.MiscMenu["FlowersKatarina.MiscMenu.ETurret"].GetValue<MenuList>().Index == 1 && pos.IsUnderEnemyTurret())
            {
                if (ObjectManager.Player.HealthPercent <=
                    MyLogic.MiscMenu["FlowersKatarina.MiscMenu.ETurretHP"].GetValue<MenuSlider>().Value &&
                    target.Health > GetComboDamage(target) * 0.85)
                {
                    return false;
                }
            }

            if (MyLogic.ComboMenu["FlowersKatarina.ComboMenu.EKillAble"].GetValue<MenuKeyBind>().Active)
            {
                if (GameObjects.EnemyHeroes.Count(x => x.Distance(pos) <= MyLogic.R.Range) >= 3)
                {
                    if (GameObjects.EnemyHeroes.Count(x => x.Distance(pos) <= MyLogic.R.Range) == 3)
                    {
                        if (GameObjects.EnemyHeroes.Count(x => x.Health < GetComboDamage(target) * 1.45) <= 2)
                        {
                            return false;
                        }
                    }
                    else if (GameObjects.EnemyHeroes.Count(x => x.Distance(pos) <= MyLogic.R.Range) == 4)
                    {
                        if (GameObjects.EnemyHeroes.Count(x => x.Health < GetComboDamage(target) * 1.45) < 2)
                        {
                            return false;
                        }
                    }
                    else if (GameObjects.EnemyHeroes.Count(x => x.Distance(pos) <= MyLogic.R.Range) == 5)
                    {
                        if (GameObjects.EnemyHeroes.Count(x => x.Health < GetComboDamage(target) * 1.45) < 3)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (Orbwalker.ActiveMode == OrbwalkerMode.Combo)
                    {
                        if (target.Health >
                            (GameObjects.AllyHeroes.Any(x => x.DistanceToPlayer() <= MyLogic.E.Range)
                                ? GetComboDamage(target) + ObjectManager.Player.Level * 45
                                : GetComboDamage(target)))
                        {
                            return false;
                        }
                    }
                    else if (Orbwalker.ActiveMode == OrbwalkerMode.Harass)
                    {
                        if (pos.IsUnderEnemyTurret())
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static void CancelUlt(bool ignoreCheck = false)
        {
            if (!ignoreCheck && GameObjects.Heroes.Any(x => !x.IsDead && x.DistanceToPlayer() <= MyLogic.R.Range))
            {
                return;
            }

            if (Variables.GameTimeTickCount - MyLogic.lastCancelTime > 5000)
            {
                ObjectManager.Player
                    .IssueOrder(GameObjectOrder.MoveTo,
                        ObjectManager.Player.PreviousPosition.Extend(Game.CursorPos, 100));
                MyLogic.lastCancelTime = Variables.GameTimeTickCount;
            }
        }

        public static SpellSlot GetItemSlot(this AIHeroClient source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return SpellSlot.Unknown;
            }

            var slot =
                source.InventoryItems.FirstOrDefault(
                    x => string.Equals(itemName, x.SpellName, StringComparison.CurrentCultureIgnoreCase));
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return slot.SpellSlot;
            }

            return SpellSlot.Unknown;
        }

        public static bool CanUseItem(this AIHeroClient source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return false;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown)
            {
                return source.Spellbook.GetSpell(slot).State == SpellState.Ready;
            }

            return false;
        }

        public static void UseItem(this AIHeroClient source, AIHeroClient target, string itemName)
        {
            if (source == null || target == null || !target.IsValidTarget() || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.Spellbook.CastSpell(slot, target);
            }
        }

        public static void UseItem(this AIHeroClient source, Vector3 position, string itemName)
        {
            if (source == null || position == Vector3.Zero || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.Spellbook.CastSpell(slot, position);
            }
        }

        public static void UseItem(this AIHeroClient source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.Spellbook.CastSpell(slot);
            }
        }

        public static void GetNames(this AIHeroClient source)
        {
            foreach (var slot in source.InventoryItems)
            {
                if (!slot.SpellName.ToLower().Contains("no script"))
                {
                    Console.WriteLine(slot.SpellName + " - " + slot.ItemID);
                }
            }
        }

        public static IEnumerable<GameObject> badaoFleeLogic
        {
            get
            {
                var Vinasun = new List<GameObject>();
                Vinasun.AddRange(GameObjects.Minions.Where(x => x.IsValidTarget(MyLogic.E.Range) && (x.IsMinion() || x.GetJungleType() != JungleType.Unknown)).ToArray());
                Vinasun.AddRange(GameObjects.Heroes.Where(x => x != null && x.IsTargetable && x.IsValidTarget(MyLogic.E.Range)).ToArray());
                Vinasun.AddRange(MyLogic.Daggers.Where(x => Variables.GameTimeTickCount - x.CreateTime < 3850).Select(x => x.Dagger).ToArray());
                return Vinasun;
            }
        }

        public static bool HaveShiled(this AIBaseClient target)
        {
            if (target == null || target.IsDead || target.Health <= 0)
            {
                return false;
            }

            if (target.HasBuff("BlackShield"))
            {
                return true;
            }

            if (target.HasBuff("bansheesveil"))
            {
                return true;
            }

            if (target.HasBuff("SivirE"))
            {
                return true;
            }

            if (target.HasBuff("NocturneShroudofDarkness"))
            {
                return true;
            }

            if (target.HasBuff("itemmagekillerveil"))
            {
                return true;
            }

            if (target.HasBuffOfType(BuffType.SpellShield))
            {
                return true;
            }

            return false;
        }
    }
}
