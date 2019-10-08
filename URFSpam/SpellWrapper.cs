namespace URFSpam
{
    using System;
    using System.Linq;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI.Values;
    using EnsoulSharp.SDK.Prediction;

    internal class SpellWrapper
    {
        public static Spell QNormal;
        public static Spell QCircle;
        public static Spell QLinear;

        public static Spell WNormal;
        public static Spell WCircle;
        public static Spell WLinear;

        public static Spell ENormal;
        public static Spell ECircle;
        public static Spell ELinear;

        public static Spell RNormal;
        public static Spell RCircle;
        public static Spell RLinear;

        public static void Initializer()
        {
            QNormal = new Spell(SpellSlot.Q);
            QCircle = new Spell(SpellSlot.Q);
            QLinear = new Spell(SpellSlot.Q);

            WNormal = new Spell(SpellSlot.W);
            WCircle = new Spell(SpellSlot.W);
            WLinear = new Spell(SpellSlot.W);

            ENormal = new Spell(SpellSlot.E);
            ECircle = new Spell(SpellSlot.E);
            ELinear = new Spell(SpellSlot.E);

            RNormal = new Spell(SpellSlot.R);
            RCircle = new Spell(SpellSlot.R);
            RLinear = new Spell(SpellSlot.R);
        }

        public static void SetSkillshot()
        {
            //QQQ
            QNormal.Range = MenuWrapper.Q.Range.Value;
            QCircle.Range = MenuWrapper.Q.Range.Value;
            QLinear.Range = MenuWrapper.Q.Range.Value;

            QCircle.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
            QLinear.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);

            QLinear.Collision = MenuWrapper.Q.Type.Index == 4;

            QNormal.MinHitChance = (HitChance) (4 - MenuWrapper.Q.Hitchance.Index);
            QCircle.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
            QLinear.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);

            //WWW
            WNormal.Range = MenuWrapper.W.Range.Value;
            WCircle.Range = MenuWrapper.W.Range.Value;
            WLinear.Range = MenuWrapper.W.Range.Value;

            WCircle.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
            WLinear.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);

            WLinear.Collision = MenuWrapper.W.Type.Index == 4;

            WNormal.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
            WCircle.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
            WLinear.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);

            //EEE
            ENormal.Range = MenuWrapper.E.Range.Value;
            ECircle.Range = MenuWrapper.E.Range.Value;
            ELinear.Range = MenuWrapper.E.Range.Value;

            ECircle.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
            ELinear.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);

            ELinear.Collision = MenuWrapper.E.Type.Index == 4;

            ENormal.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
            ECircle.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
            ELinear.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);

            //RRR
            RNormal.Range = MenuWrapper.R.Range.Value;
            RCircle.Range = MenuWrapper.R.Range.Value;
            RLinear.Range = MenuWrapper.R.Range.Value;

            RCircle.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
            RLinear.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);

            RLinear.Collision = MenuWrapper.R.Type.Index == 4;

            RNormal.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
            RCircle.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
            RLinear.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);

            //Set Range
            MenuWrapper.Q.Range.ValueChanged += QRangeValueChanged;
            MenuWrapper.W.Range.ValueChanged += WRangeValueChanged;
            MenuWrapper.E.Range.ValueChanged += ERangeValueChanged;
            MenuWrapper.R.Range.ValueChanged += RRangeValueChanged;

            //Set Hitchange
            MenuWrapper.Q.Hitchance.ValueChanged += QHitchanceValueChanged;
            MenuWrapper.W.Hitchance.ValueChanged += WHitchanceValueChanged;
            MenuWrapper.E.Hitchance.ValueChanged += EHitchanceValueChanged;
            MenuWrapper.R.Hitchance.ValueChanged += RHitchanceValueChanged;

            //Set Collision
            MenuWrapper.Q.Type.ValueChanged += QCollisionValueChanged;
            MenuWrapper.W.Type.ValueChanged += WCollisionValueChanged;
            MenuWrapper.E.Type.ValueChanged += ECollisionValueChanged;
            MenuWrapper.R.Type.ValueChanged += RCollisionValueChanged;
        }

        public static SpellSlot GetPrioritySpell()
        {
            var QPriority = MenuWrapper.Priority.QPriority.Value;
            var WPriority = MenuWrapper.Priority.WPriority.Value;
            var EPriority = MenuWrapper.Priority.EPriority.Value;
            var RPriority = MenuWrapper.Priority.RPriority.Value;

            var arry = new[] {QPriority, WPriority, EPriority, RPriority};
            Array.Sort(arry);

            var result = SpellSlot.Unknown;
            var level = 0;
            foreach (var a in arry)
            {
                if (a == QPriority)
                {
                    if (a > level)
                    {
                        result = SpellSlot.Q;
                        level = a;
                    }
                }

                if (a == WPriority)
                {
                    if (a > level)
                    {
                        result = SpellSlot.W;
                        level = a;
                    }
                }

                if (a == EPriority)
                {
                    if (a > level)
                    {
                        result = SpellSlot.E;
                        level = a;
                    }
                }

                if (a == RPriority)
                {
                    if (a > level)
                    {
                        result = SpellSlot.R;
                        level = a;
                    }
                }
            }

            return result;
        }

        public static void CastSpell(SpellSlot slot, OrbwalkerMode mode)
        {
            if (slot == SpellSlot.Unknown || ObjectManager.Player.Spellbook.CanUseSpell(slot) != SpellState.Ready)
            {
                return;
            }

            if (mode == OrbwalkerMode.LastHit || mode == OrbwalkerMode.None)
            {
                return;
            }

            var castType = GetSpellType(slot);
            if (castType == CastType.Unknow)
            {
                return;
            }

            if (slot == SpellSlot.Q)
            {
                if (mode == OrbwalkerMode.Combo)
                {
                    if (MenuWrapper.Q.Combo.Enabled)
                    {
                        var target = TargetSelector.GetTarget(QNormal.Range);
                        if (target != null && target.IsValidTarget(QNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                QNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                QCircle.CastIfHitchanceEquals(target, QCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                QLinear.CastIfHitchanceEquals(target, QLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.Q.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(QNormal.Range);
                        if (target != null && target.IsValidTarget(QNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                QNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                QCircle.CastIfHitchanceEquals(target, QCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                QLinear.CastIfHitchanceEquals(target, QLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.Q.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(QNormal.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(QNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                QNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                QCircle.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                QLinear.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                        }
                    }
                }
            }
            else if (slot == SpellSlot.W)
            {
                if (mode == OrbwalkerMode.Combo)
                {
                    if (MenuWrapper.W.Combo.Enabled)
                    {
                        var target = TargetSelector.GetTarget(WNormal.Range);
                        if (target != null && target.IsValidTarget(WNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                WNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                WCircle.CastIfHitchanceEquals(target, WCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                WLinear.CastIfHitchanceEquals(target, WLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.W.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(WNormal.Range);
                        if (target != null && target.IsValidTarget(WNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                WNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                WCircle.CastIfHitchanceEquals(target, WCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                WLinear.CastIfHitchanceEquals(target, WLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.W.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(WNormal.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(WNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                WNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                WCircle.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                WLinear.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                        }
                    }
                }
            }
            else if (slot == SpellSlot.E)
            {
                if (mode == OrbwalkerMode.Combo)
                {
                    if (MenuWrapper.E.Combo.Enabled)
                    {
                        var target = TargetSelector.GetTarget(ENormal.Range);
                        if (target != null && target.IsValidTarget(ENormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                ENormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                ECircle.CastIfHitchanceEquals(target, ECircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                ELinear.CastIfHitchanceEquals(target, ELinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.E.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(ENormal.Range);
                        if (target != null && target.IsValidTarget(ENormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                ENormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                ECircle.CastIfHitchanceEquals(target, ECircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                ELinear.CastIfHitchanceEquals(target, ELinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.E.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(ENormal.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(ENormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                ENormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                ECircle.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                ELinear.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                        }
                    }
                }
            }
            else if (slot == SpellSlot.R)
            {
                if (mode == OrbwalkerMode.Combo)
                {
                    if (MenuWrapper.R.Combo.Enabled)
                    {
                        var target = TargetSelector.GetTarget(RNormal.Range);
                        if (target != null && target.IsValidTarget(RNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                RNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                RCircle.CastIfHitchanceEquals(target, RCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                RLinear.CastIfHitchanceEquals(target, RLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.R.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(RNormal.Range);
                        if (target != null && target.IsValidTarget(RNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                RNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                RCircle.CastIfHitchanceEquals(target, RCircle.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                RLinear.CastIfHitchanceEquals(target, RLinear.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.R.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(RNormal.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(RNormal.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                RNormal.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                RCircle.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                RLinear.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                        }
                    }
                }
            }
        }

        private static CastType GetSpellType(SpellSlot slot)
        {
            switch (slot)
            {
                case SpellSlot.Q:
                {
                    var index = MenuWrapper.Q.Type.Index;
                    switch (index)
                    {
                            case 0:
                                return CastType.SelfCast;
                            case 1:
                                return CastType.Target;
                            case 2:
                                return CastType.Circle;
                            default:
                                return CastType.Linear;
                    }
                }
                case SpellSlot.W:
                {
                    var index = MenuWrapper.W.Type.Index;
                    switch (index)
                    {
                        case 0:
                            return CastType.SelfCast;
                        case 1:
                            return CastType.Target;
                        case 2:
                            return CastType.Circle;
                        default:
                            return CastType.Linear;
                    }
                }
                case SpellSlot.E:
                {
                    var index = MenuWrapper.E.Type.Index;
                    switch (index)
                    {
                        case 0:
                            return CastType.SelfCast;
                        case 1:
                            return CastType.Target;
                        case 2:
                            return CastType.Circle;
                        default:
                            return CastType.Linear;
                    }
                }
                case SpellSlot.R:
                {
                    var index = MenuWrapper.R.Type.Index;
                    switch (index)
                    {
                        case 0:
                            return CastType.SelfCast;
                        case 1:
                            return CastType.Target;
                        case 2:
                            return CastType.Circle;
                        default:
                            return CastType.Linear;
                    }
                }
                default:
                    return CastType.Unknow;
            }
        }

        private static void QRangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                QNormal.Range = MenuWrapper.Q.Range.Value;
                QCircle.Range = MenuWrapper.Q.Range.Value;
                QLinear.Range = MenuWrapper.Q.Range.Value;
            }
        }

        private static void WRangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                WNormal.Range = MenuWrapper.W.Range.Value;
                WCircle.Range = MenuWrapper.W.Range.Value;
                WLinear.Range = MenuWrapper.W.Range.Value;
            }
        }

        private static void ERangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                ENormal.Range = MenuWrapper.E.Range.Value;
                ECircle.Range = MenuWrapper.E.Range.Value;
                ELinear.Range = MenuWrapper.E.Range.Value;
            }
        }

        private static void RRangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                RNormal.Range = MenuWrapper.R.Range.Value;
                RCircle.Range = MenuWrapper.R.Range.Value;
                RLinear.Range = MenuWrapper.R.Range.Value;
            }
        }

        private static void QHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                QNormal.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
                QCircle.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
                QLinear.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
            }
        }

        private static void WHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                WNormal.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
                WCircle.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
                WLinear.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
            }
        }

        private static void EHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                ENormal.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
                ECircle.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
                ELinear.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
            }
        }

        private static void RHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                RNormal.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
                RCircle.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
                RLinear.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
            }
        }

        private static void QCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                QLinear.Collision = MenuWrapper.Q.Type.Index == 4;
            }
        }

        private static void WCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                WLinear.Collision = MenuWrapper.W.Type.Index == 4;
            }
        }

        private static void ECollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                ELinear.Collision = MenuWrapper.E.Type.Index == 4;
            }
        }

        private static void RCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                RLinear.Collision = MenuWrapper.R.Type.Index == 4;
            }
        }
    }
}
