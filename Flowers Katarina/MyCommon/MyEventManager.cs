namespace Flowers_Katarina.MyCommon
{
    #region

    using System;
    using System.Linq;

    using SharpDX;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.Events;
    using EnsoulSharp.SDK.MenuUI.Values;
    using EnsoulSharp.SDK.Utility;

    using Flowers_Katarina.MyBase;

    using Color = System.Drawing.Color;

    #endregion

    public class MyEventManager : MyLogic
    {
        public static void Initializer()
        {
            try
            {
                Tick.OnTick += Args => OnUpdate();
                GameObject.OnCreate += (sender, Args) => OnCreate(sender);
                GameObject.OnDelete += (sender, Args) => OnDestroy(sender);
                AIBaseClient.OnProcessSpellCast += OnProcessSpellCast;
                Drawing.OnDraw += Args => OnRender();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.Initializer." + ex);
            }
        }

        private static void OnUpdate()
        {
            try
            {
                Daggers.RemoveAll(x => Variables.GameTimeTickCount - x.CreateTime > 3850);

                if (Me.IsDead || Me.IsRecalling())
                {
                    return;
                }

                if (FleeMenu["FlowersKatarina.FleeMenu.Key"].GetValue<MenuKeyBind>().Active)
                {
                    FleeEvent();
                }

                if (MiscMenu["FlowersKatarina.MiscMenu.OneKeyEW"].GetValue<MenuKeyBind>().Active && E.IsReady() && W.IsReady())
                {
                    SemiEW();
                }

                KillStealEvent();

                if (isCastingUlt)
                {
                    //Chat.Print("IS CASTING R");
                    Orbwalker.AttackState = false;
                    Orbwalker.MovementState = false;

                    if (MiscMenu["FlowersKatarina.MiscMenu.AutoCancelR"].GetValue<MenuBool>().Enabled)
                    {
                        MyExtraManager.CancelUlt();
                    }

                    return;
                }

                Orbwalker.AttackState = true;
                Orbwalker.MovementState = true;

                switch (Orbwalker.ActiveMode)
                {
                    case OrbwalkerMode.Combo:
                        ComboEvent();
                        break;
                    case OrbwalkerMode.Harass:
                        HarassEvent();
                        break;
                    case OrbwalkerMode.LaneClear:
                        ClearEvent();
                        break;
                    case OrbwalkerMode.LastHit:
                        LastHitEvent();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnUpdate." + ex);
            }
        }

        private static void FleeEvent()
        {
            try
            {
                Me.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

                if (FleeMenu["FlowersKatarina.FleeMenu.W"].GetValue<MenuBool>().Enabled && W.IsReady())
                {
                    W.Cast();
                }

                if (FleeMenu["FlowersKatarina.FleeMenu.E"].GetValue<MenuBool>().Enabled && E.IsReady())
                {
                    var fleeList = MyExtraManager.badaoFleeLogic.ToArray();

                    if (fleeList.Any())
                    {
                        var nearest = fleeList.MinOrDefault(x => x.Position.Distance(Game.CursorPos));

                        if (nearest != null && nearest.Position.DistanceToCursor() < Me.DistanceToCursor() &&
                            nearest.Position.DistanceToPlayer() > 300)
                        {
                            var pos = nearest.Position.ToVector2().Extend(Game.CursorPos.ToVector2(), 150);
                            E.Cast(pos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.FleeEvent." + ex);
            }
        }

        private static void SemiEW()
        {
            try
            {
                if (E.IsReady())
                {
                    var fleeList = MyExtraManager.badaoFleeLogic.ToArray();

                    if (fleeList.Any())
                    {
                        var nearest = fleeList.MinOrDefault(x => x.Position.Distance(Game.CursorPos));

                        if (nearest != null && nearest.Position.DistanceToPlayer() <= E.Range)
                        {
                            var pos = nearest.Position.ToVector2().Extend(Game.CursorPos.ToVector2(), 150);

                            E.Cast(pos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.SetCoolDownTime." + ex);
            }
        }

        private static void KillStealEvent()
        {
            try
            {
                if (isCastingUlt && !KillStealMenu["FlowersKatarina.KillStealMenu.CancelR"].GetValue<MenuBool>().Enabled)
                {
                    return;
                }

                if (Me.CountEnemyHeroesInRange(E.Range) == 0)
                {
                    return;
                }

                foreach (
                    var target in 
                    GameObjects.EnemyHeroes.Where(x => !x.IsDead && x.IsValidTarget(E.Range + 300))
                        .OrderBy(x => x.Health))
                {
                    if (target.IsValidTarget(E.Range + 300))
                    {
                        if (KillStealMenu["FlowersKatarina.KillStealMenu.Q"].GetValue<MenuBool>().Enabled && target.Health < Q.GetDamage(target) && Q.IsReady() &&
                            target.IsValidTarget(Q.Range))
                        {
                            if (isCastingUlt)
                            {
                                MyExtraManager.CancelUlt(true);
                                Q.CastOnUnit(target);
                                return;
                            }
                            Q.CastOnUnit(target);
                            return;
                        }

                        if (KillStealMenu["FlowersKatarina.KillStealMenu.E"].GetValue<MenuBool>().Enabled && target.Health < E.GetDamage(target) && E.IsReady())
                        {
                            if (target.DistanceToPlayer() <= E.Range + 130)
                            {
                                var pos = Me.Position.Extend(target.Position, target.DistanceToPlayer() + 130);
                                if (isCastingUlt)
                                {
                                    MyExtraManager.CancelUlt(true);
                                    E.Cast(pos);
                                    return;
                                }
                                E.Cast(pos);
                                return;
                            }

                            if (target.IsValidTarget(E.Range))
                            {
                                if (isCastingUlt)
                                {
                                    MyExtraManager.CancelUlt(true);
                                    E.Cast(target);
                                    return;
                                }
                                E.Cast(target);
                                return;
                            }
                        }

                        if (KillStealMenu["FlowersKatarina.KillStealMenu.Q"].GetValue<MenuBool>().Enabled && target.Health < Q.GetDamage(target) + E.GetDamage(target) &&
                             KillStealMenu["FlowersKatarina.KillStealMenu.E"].GetValue<MenuBool>().Enabled && Q.IsReady() && E.IsReady() &&
                            target.IsValidTarget(E.Range))
                        {
                            if (isCastingUlt)
                            {
                                MyExtraManager.CancelUlt(true);
                                Q.CastOnUnit(target);
                                E.Cast(target);
                                return;
                            }
                            Q.CastOnUnit(target);
                            E.Cast(target);
                            return;
                        }

                        if (target.Health < MyExtraManager.GetKataPassiveDamage(target) + E.GetDamage(target) &&
                            KillStealMenu["FlowersKatarina.KillStealMenu.E"].GetValue<MenuBool>().Enabled && E.IsReady() &&
                            Daggers.Any(
                                x =>
                                    x.Dagger.IsValid &&
                                    x.Position.Distance(target.Position) <= PassiveRange &&
                                    x.Position.DistanceToPlayer() <= E.Range))
                        {
                            foreach (
                                var obj in
                                Daggers.Where(x => x.Position.Distance(target.Position) <= PassiveRange)
                                    .OrderBy(x => x.Position.Distance(target.Position)))
                            {
                                if (obj.Dagger != null && obj.Dagger.IsValid && obj.Position.DistanceToPlayer() <= E.Range)
                                {
                                    if (isCastingUlt)
                                    {
                                        MyExtraManager.CancelUlt(true);
                                        E.Cast(obj.Position);
                                        DelayAction.Add(100 + Game.Ping, () => E.Cast(target));
                                        return;
                                    }
                                    E.Cast(obj.Position);
                                    DelayAction.Add(100 + Game.Ping, () => E.Cast(target));
                                    return;
                                }
                            }
                        }

                        if (target.Health < R.GetDamage(target) * 0.6 && KillStealMenu["FlowersKatarina.KillStealMenu.R"].GetValue<MenuBool>().Enabled
                            && R.IsReady() && target.IsValidTarget(R.Range) && target.Health > 50 * target.Level)
                        {
                            R.Cast();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.KillStealEvent." + ex);
            }
        }

        private static void ComboEvent()
        {
            try
            {
                if (isCastingUlt)
                {
                    return;
                }

                var target = TargetSelector.GetTarget(E.Range + 300f);

                if (target != null && target.IsValidTarget(E.Range + 300f))
                {
                    if (ComboMenu["FlowersKatarina.ComboMenu.Ignite"].GetValue<MenuBool>().Enabled && IgniteSlot != SpellSlot.Unknown &&
                        Ignite.IsReady() && target.IsValidTarget(600) &&
                        (target.Health < MyExtraManager.GetComboDamage(target) && target.IsValidTarget(400) ||
                         target.Health < Me.GetIgniteDamage(target)))
                    {
                        Ignite.Cast(target);
                    }

                    //Item Hextech_Gunblade Bilgewater_Cutlass

                    if (ComboMenu["FlowersKatarina.ComboMenu.W"].GetValue<MenuBool>().Enabled &&
                        ComboMenu["FlowersKatarina.ComboMenu.WSmart"].GetValue<MenuBool>().Enabled && W.IsReady() &&
                        target.IsValidTarget(W.Range))
                    {
                        W.Cast();
                    }

                    switch (ComboMenu["FlowersKatarina.ComboMenu.Mode"].GetValue<MenuList>().Index)
                    {
                        case 0:
                            MyExtraManager.QEWLogic(target, ComboMenu["FlowersKatarina.ComboMenu.Q"].GetValue<MenuBool>().Enabled,
                                ComboMenu["FlowersKatarina.ComboMenu.W"].GetValue<MenuBool>().Enabled,
                                ComboMenu["FlowersKatarina.ComboMenu.E"].GetValue<MenuBool>().Enabled);
                            break;
                        case 1:
                            MyExtraManager.EQWLogic(target, ComboMenu["FlowersKatarina.ComboMenu.Q"].GetValue<MenuBool>().Enabled,
                                ComboMenu["FlowersKatarina.ComboMenu.W"].GetValue<MenuBool>().Enabled,
                                ComboMenu["FlowersKatarina.ComboMenu.E"].GetValue<MenuBool>().Enabled);
                            break;
                    }

                    if (ComboMenu["FlowersKatarina.ComboMenu.R"].GetValue<MenuBool>().Enabled && R.IsReady() &&
                        Me.CountEnemyHeroesInRange(R.Range) > 0 && !Q.IsReady() && !W.IsReady() && !E.IsReady())
                    {
                        if (ComboMenu["FlowersKatarina.ComboMenu.RAlways"].GetValue<MenuBool>().Enabled)
                        {
                            Orbwalker.AttackState = false;
                            Orbwalker.MovementState = false;
                            R.Cast();
                        }

                        if (ComboMenu["FlowersKatarina.ComboMenu.RKillAble"].GetValue<MenuBool>().Enabled &&
                            (target.Health <= MyExtraManager.GetComboDamage(target) ||
                             target.Health <= R.GetDamage(target) * 0.8) &&
                             target.Health > Q.GetDamage(target) + MyExtraManager.GetKataPassiveDamage(target)* 2)
                        {
                            Orbwalker.AttackState = false;
                            Orbwalker.MovementState = false;
                            R.Cast();
                        }

                        if (ComboMenu["FlowersKatarina.ComboMenu.RCountHit"].GetValue<MenuSliderButton>().Enabled &&
                            Me.CountEnemyHeroesInRange(R.Range) >= ComboMenu["FlowersKatarina.ComboMenu.RCountHit"].GetValue<MenuSliderButton>().Value)
                        {
                            Orbwalker.AttackState = false;
                            Orbwalker.MovementState = false;
                            R.Cast();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.ComboEvent." + ex);
            }
        }

        private static void HarassEvent()
        {
            try
            {
                if (isCastingUlt)
                {
                    return;
                }

                var target = TargetSelector.GetTarget(E.Range + 300f);

                if (target.IsValidTarget(E.Range + 300f))
                {
                    if (HarassMenu["FlowersKatarina.HarassMenu.W"].GetValue<MenuBool>().Enabled && W.IsReady() && target.IsValidTarget(W.Range))
                    {
                        W.Cast();
                    }

                    if (HarassMenu["FlowersKatarina.HarassMenu.Q"].GetValue<MenuBool>().Enabled && Q.IsReady() && Me.Level < 3 &&
                        target.IsValidTarget(Q.Range))
                    {
                        Q.CastOnUnit(target);
                    }

                    switch (HarassMenu["FlowersKatarina.HarassMenu.Mode"].GetValue<MenuList>().Index)
                    {
                        case 0:
                            MyExtraManager.QEWLogic(target, HarassMenu["FlowersKatarina.HarassMenu.Q"].GetValue<MenuBool>().Enabled,
                                HarassMenu["FlowersKatarina.HarassMenu.W"].GetValue<MenuBool>().Enabled,
                                HarassMenu["FlowersKatarina.HarassMenu.E"].GetValue<MenuBool>().Enabled);
                            break;
                        case 1:
                            MyExtraManager.EQWLogic(target, HarassMenu["FlowersKatarina.HarassMenu.Q"].GetValue<MenuBool>().Enabled,
                                HarassMenu["FlowersKatarina.HarassMenu.W"].GetValue<MenuBool>().Enabled,
                                HarassMenu["FlowersKatarina.HarassMenu.E"].GetValue<MenuBool>().Enabled);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.HarassEvent." + ex);
            }
        }

        private static void ClearEvent()
        {
            try
            {
                if (MyManaManager.SpellFarm)
                {
                    LaneClearEvent();
                    JungleClearEvent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.ClearEvent." + ex);
            }
        }

        private static void LaneClearEvent()
        {
            try
            {
                if (isCastingUlt)
                {
                    return;
                }

                if (ClearMenu["FlowersKatarina.ClearMenu.LaneClearQ"].GetValue<MenuBool>().Enabled && Q.IsReady())
                {
                    if (ClearMenu["FlowersKatarina.ClearMenu.LaneClearQOnlyLH"].GetValue<MenuBool>().Enabled)
                    {
                        var qMinions =
                            GameObjects.EnemyMinions.Where(x => x.IsValidTarget(Q.Range) && x.IsMinion()).ToArray();

                        foreach (var qMinion in qMinions.Where(x => x.IsValidTarget(Q.Range) && x.Health < Q.GetDamage(x)))
                        {
                            if (qMinion != null && qMinion.IsValidTarget(Q.Range))
                            {
                                Q.CastOnUnit(qMinion);
                            }
                        }
                    }
                    else
                    {
                        var qMinions =
                            GameObjects.EnemyMinions.Where(x => x.IsValidTarget(Q.Range) && x.IsMinion()).ToArray();

                        if (qMinions.Length >= 2)
                        {
                            Q.CastOnUnit(qMinions.FirstOrDefault());
                        }
                    }
                }

                if (ClearMenu["FlowersKatarina.ClearMenu.LaneClearW"].GetValue<MenuBool>().Enabled && W.IsReady())
                {
                    var wMinions =
                           GameObjects.EnemyMinions.Where(x => x.IsValidTarget(W.Range) && x.IsMinion()).ToArray();

                    if (wMinions.Length >= 3)
                    {
                        W.Cast();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.LaneClearEvent." + ex);
            }
        }

        private static void JungleClearEvent()
        {
            try
            {
                if (isCastingUlt)
                {
                    return;
                }

                var mobs = GameObjects.Jungle.Where(x => x.IsValidTarget(Q.Range) && x.GetJungleType() != JungleType.Unknown).ToArray();

                if (mobs.Any())
                {
                    var mob = mobs.FirstOrDefault();

                    if (mob != null && mob.IsValidTarget(Q.Range))
                    {
                        if (ClearMenu["FlowersKatarina.ClearMenu.JungleClearQ"].GetValue<MenuBool>().Enabled && Q.IsReady() && mob.IsValidTarget(Q.Range))
                        {
                            Q.CastOnUnit(mob);
                        }

                        if (ClearMenu["FlowersKatarina.ClearMenu.JungleClearW"].GetValue<MenuBool>().Enabled && W.IsReady() && mob.IsValidTarget(W.Range))
                        {
                            W.Cast();
                        }

                        if (ClearMenu["FlowersKatarina.ClearMenu.JungleClearE"].GetValue<MenuBool>().Enabled && E.IsReady())
                        {
                            if (Daggers.Any(
                                x =>
                                    mobs.Any(a => a.Distance(x.Position) <= PassiveRange) &&
                                    x.Position.DistanceToPlayer() <= E.Range))
                            {
                                foreach (
                                    var obj in
                                    Daggers.Where(x => x.Position.Distance(mob.Position) <= PassiveRange)
                                        .OrderByDescending(x => x.Position.Distance(mob.Position)))
                                {
                                    if (obj.Dagger != null && obj.Dagger.IsValid && obj.Position.DistanceToPlayer() <= E.Range)
                                    {
                                        E.Cast(obj.Position);
                                    }
                                }
                            }
                            else if (mob.DistanceToPlayer() <= E.Range + 130)
                            {
                                var pos = Me.Position.Extend(mob.Position, mob.DistanceToPlayer() + 130);

                                E.Cast(pos);
                            }
                            else if (mob.IsValidTarget(E.Range))
                            {
                                E.Cast(mob);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.JungleClearEvent." + ex);
            }
        }

        private static void LastHitEvent()
        {
            try
            {
                if (isCastingUlt)
                {
                    return;
                }

                if (LastHitMenu["FlowersKatarina.LastHitMenu.Q"].GetValue<MenuBool>().Enabled && Q.IsReady())
                {
                    var qMinions =
                        GameObjects.EnemyMinions.Where(x => x.IsValidTarget(Q.Range) && (x.IsMinion() || x.GetJungleType() != JungleType.Unknown))
                            .ToArray();

                    foreach (var minion in qMinions.Where(x => x.IsValidTarget(Q.Range)))
                    {
                        if (minion != null && minion.IsValidTarget(Q.Range))
                        {
                            Q.CastOnUnit(minion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.LastHitEvent." + ex);
            }
        }

        private static void OnCreate(GameObject sender)
        {
            try
            {
                if (!sender.Name.Contains("Katarina_"))
                {
                    return;
                }

                switch (sender.Name)
                {
                    case "Katarina_Base_Q_Dagger_Land_Stone":
                    case "Katarina_Base_Q_Dagger_Land_Water":
                    case "Katarina_Base_Q_Dagger_Land_Grass":
                    case "Katarina_Base_Q_Dagger_Land_Dirt":
                    case "Katarina_Base_W_Indicator_Ally":
                    case "Katarina_Base_E_Beam":
                    case "Katarina_Base_Dagger_Ground_Indicator":
                        Daggers.Add(new MyDaggerManager(sender, sender.Position, Variables.GameTimeTickCount));
                        break;
                    case "Katarina_Base_Dagger_PickUp_Cas":
                    case "Katarina_Base_Dagger_PickUp_Tar":
                        var firstDagger = Daggers.OrderBy(x => x.Dagger.Position.Distance(sender.Position))
                            .FirstOrDefault();
                        Daggers.Remove(firstDagger);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnCreate." + ex);
            }
        }

        private static void OnDestroy(GameObject sender)
        {
            try
            {
                if (!sender.Name.Contains("Katarina"))
                {
                    return;
                }

                switch (sender.Name)
                {
                    case "Katarina_Base_Q_Dagger_Land_Stone":
                    case "Katarina_Base_Q_Dagger_Land_Water":
                    case "Katarina_Base_Q_Dagger_Land_Grass":
                    case "Katarina_Base_Q_Dagger_Land_Dirt":
                    case "Katarina_Base_W_Indicator_Ally":
                    case "Katarina_Base_E_Beam":
                    case "Katarina_Base_Dagger_Ground_Indicator":
                        Daggers.RemoveAll(x => x.Dagger.NetworkId == sender.NetworkId);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnCreate." + ex);
            }
        }

        private static void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs Args)
        {
            try
            {
                if (sender.IsMe)
                {
                    if (Args.SData.Name.Contains("KatarinaW"))
                    {
                        lastWTime = Variables.GameTimeTickCount;
                    }

                    if (Args.SData.Name.Contains("KatarinaE"))
                    {
                        lastETime = Variables.GameTimeTickCount;

                        if (MiscMenu["FlowersKatarina.MiscMenu.OneKeyEW"].GetValue<MenuKeyBind>().Active && W.IsReady())
                        {
                            W.Cast();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnProcessSpellCast." + ex);
            }
        }

        private static void OnRender()
        {
            try
            {
                if (Me.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                {
                    return;
                }

                if (DrawMenu["FlowersKatarina.DrawMenu.Dagger"].GetValue<MenuBool>().Enabled)
                {
                    foreach (var position in Daggers.Where(x => !x.Dagger.IsDead && x.Dagger.IsValid).Select(x => x.Position))
                    {
                        if (position != Vector3.Zero)
                        {
                            Render.Circle.DrawCircle(position, PassiveRange, Color.FromArgb(69, 0, 255), 1);
                        }
                    }
                }

                if (DrawMenu["FlowersKatarina.DrawMenu.Q"].GetValue<MenuBool>().Enabled && Q.IsReady())
                {
                    Render.Circle.DrawCircle(Me.Position, Q.Range, Color.FromArgb(251, 0, 133), 1);
                }

                if (DrawMenu["FlowersKatarina.DrawMenu.E"].GetValue<MenuBool>().Enabled && E.IsReady())
                {
                    Render.Circle.DrawCircle(Me.Position, E.Range, Color.FromArgb(0, 136, 255), 1);
                }

                if (DrawMenu["FlowersKatarina.DrawMenu.R"].GetValue<MenuBool>().Enabled && R.IsReady())
                {
                    Render.Circle.DrawCircle(Me.Position, R.Range, Color.FromArgb(0, 255, 161), 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnRender." + ex);
            }
        }
    }
}
