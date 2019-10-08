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
        public static Spell Q;
        public static Spell W;
        public static Spell E;
        public static Spell R;

        public static void Initializer()
        {
            //Setup Spell
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R);

            //Set Range
            Q.Range = MenuWrapper.Q.Range.Value;
            W.Range = MenuWrapper.W.Range.Value;
            E.Range = MenuWrapper.E.Range.Value;
            R.Range = MenuWrapper.R.Range.Value;

            //Set Hitchance
            Q.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
            W.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
            E.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
            R.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);

            //On Range Value Change
            MenuWrapper.Q.Range.ValueChanged += QRangeValueChanged;
            MenuWrapper.W.Range.ValueChanged += WRangeValueChanged;
            MenuWrapper.E.Range.ValueChanged += ERangeValueChanged;
            MenuWrapper.R.Range.ValueChanged += RRangeValueChanged;
            
            //On Hitchange Value Change
            MenuWrapper.Q.Hitchance.ValueChanged += QHitchanceValueChanged;
            MenuWrapper.W.Hitchance.ValueChanged += WHitchanceValueChanged;
            MenuWrapper.E.Hitchance.ValueChanged += EHitchanceValueChanged;
            MenuWrapper.R.Hitchance.ValueChanged += RHitchanceValueChanged;

            //On SpellType Value Change
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
                        var target = TargetSelector.GetTarget(Q.Range);
                        if (target != null && target.IsValidTarget(Q.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                Q.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                Q.CastIfHitchanceEquals(target, Q.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                Q.CastIfHitchanceEquals(target, Q.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.Q.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(Q.Range);
                        if (target != null && target.IsValidTarget(Q.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                Q.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                Q.CastIfHitchanceEquals(target, Q.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                Q.CastIfHitchanceEquals(target, Q.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.Q.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(Q.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(Q.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                Q.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                Q.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                Q.CastIfHitchanceEquals(target, HitChance.Medium);
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
                        var target = TargetSelector.GetTarget(W.Range);
                        if (target != null && target.IsValidTarget(W.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                W.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                W.CastIfHitchanceEquals(target, W.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                W.CastIfHitchanceEquals(target, W.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.W.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(W.Range);
                        if (target != null && target.IsValidTarget(W.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                W.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                W.CastIfHitchanceEquals(target, W.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                W.CastIfHitchanceEquals(target, W.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.W.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(W.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(W.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                W.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                W.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                W.CastIfHitchanceEquals(target, HitChance.Medium);
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
                        var target = TargetSelector.GetTarget(E.Range);
                        if (target != null && target.IsValidTarget(E.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                E.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                E.CastIfHitchanceEquals(target, E.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                E.CastIfHitchanceEquals(target, E.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.E.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(E.Range);
                        if (target != null && target.IsValidTarget(E.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                E.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                E.CastIfHitchanceEquals(target, E.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                E.CastIfHitchanceEquals(target, E.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.E.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(E.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(E.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                E.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                E.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                E.CastIfHitchanceEquals(target, HitChance.Medium);
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
                        var target = TargetSelector.GetTarget(R.Range);
                        if (target != null && target.IsValidTarget(R.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                R.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                R.CastIfHitchanceEquals(target, R.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                R.CastIfHitchanceEquals(target, R.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.Harass)
                {
                    if (MenuWrapper.R.Harass.Enabled)
                    {
                        var target = TargetSelector.GetTarget(R.Range);
                        if (target != null && target.IsValidTarget(R.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                R.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                R.CastIfHitchanceEquals(target, R.MinHitChance);
                            }
                            else if (castType == CastType.Linear)
                            {
                                R.CastIfHitchanceEquals(target, R.MinHitChance);
                            }
                        }
                    }
                }
                else if (mode == OrbwalkerMode.LaneClear)
                {
                    if (MenuWrapper.R.JungleClear.Enabled)
                    {
                        var target = GameObjects.Jungle.Where(x => x.IsValidTarget(R.Range) && x.IsJungle())
                            .OrderByDescending(x => x.MaxHealth).FirstOrDefault();
                        if (target != null && target.IsValidTarget(R.Range))
                        {
                            if (castType == CastType.SelfCast)
                            {
                                R.Cast();
                            }
                            else if (castType == CastType.Circle)
                            {
                                R.CastIfHitchanceEquals(target, HitChance.Medium);
                            }
                            else if (castType == CastType.Linear)
                            {
                                R.CastIfHitchanceEquals(target, HitChance.Medium);
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
                Q.Range = MenuWrapper.Q.Range.Value;
            }
        }

        private static void WRangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                W.Range = MenuWrapper.W.Range.Value;
            }
        }

        private static void ERangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                E.Range = MenuWrapper.E.Range.Value;
            }
        }

        private static void RRangeValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuSlider)
            {
                R.Range = MenuWrapper.R.Range.Value;
            }
        }

        private static void QHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                Q.MinHitChance = (HitChance)(4 - MenuWrapper.Q.Hitchance.Index);
            }
        }

        private static void WHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                W.MinHitChance = (HitChance)(4 - MenuWrapper.W.Hitchance.Index);
            }
        }

        private static void EHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                E.MinHitChance = (HitChance)(4 - MenuWrapper.E.Hitchance.Index);
            }
        }

        private static void RHitchanceValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                R.MinHitChance = (HitChance)(4 - MenuWrapper.R.Hitchance.Index);
            }
        }

        private static void QCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                var index = MenuWrapper.Q.Type.Index;
                switch (index)
                {
                    case 2:
                        Q.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
                        break;
                    case 3:
                        Q.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);
                        break;
                    case 4:
                        Q.SetSkillshot(0.25f, 60f, float.MaxValue, true, false, SkillshotType.Line);
                        break;
                }
            }
        }

        private static void WCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                var index = MenuWrapper.W.Type.Index;
                switch (index)
                {
                    case 2:
                        W.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
                        break;
                    case 3:
                        W.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);
                        break;
                    case 4:
                        W.SetSkillshot(0.25f, 60f, float.MaxValue, true, false, SkillshotType.Line);
                        break;
                }
            }
        }

        private static void ECollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                var index = MenuWrapper.E.Type.Index;
                switch (index)
                {
                    case 2:
                        E.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
                        break;
                    case 3:
                        E.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);
                        break;
                    case 4:
                        E.SetSkillshot(0.25f, 60f, float.MaxValue, true, false, SkillshotType.Line);
                        break;
                }
            }
        }

        private static void RCollisionValueChanged(object sender, EventArgs args)
        {
            if (sender is MenuList)
            {
                var index = MenuWrapper.R.Type.Index;
                switch (index)
                {
                    case 2:
                        R.SetSkillshot(0.25f, 80f, float.MaxValue, false, false, SkillshotType.Circle);
                        break;
                    case 3:
                        R.SetSkillshot(0.25f, 60f, float.MaxValue, false, false, SkillshotType.Line);
                        break;
                    case 4:
                        R.SetSkillshot(0.25f, 60f, float.MaxValue, true, false, SkillshotType.Line);
                        break;
                }
            }
        }
    }
}
