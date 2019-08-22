namespace SharpShooter.MyCommon
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SharpDX;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI.Values;

    using Color = System.Drawing.Color;

    #endregion

    internal class MyDamageIndicator // made by detuks and xcsoft
    {
        private class JungleMobOffsets
        {
            public string BaseSkinName;
            public int Height;
            public int Width;
            public int XOffset;
            public int YOffset;
        }

        private static readonly List<JungleMobOffsets> JungleMobOffsetsList = new List<JungleMobOffsets>
        {
            new JungleMobOffsets {BaseSkinName = "SRU_Red", Width = 139, Height = 8, XOffset = 13, YOffset = 24},
            new JungleMobOffsets {BaseSkinName = "SRU_Blue", Width = 139, Height = 8, XOffset = 13, YOffset = 24},
            new JungleMobOffsets {BaseSkinName = "SRU_Gromp", Width = 86, Height = 2, XOffset = 6, YOffset = 8},
            new JungleMobOffsets {BaseSkinName = "Sru_Crab", Width = 60, Height = 2, XOffset = 1, YOffset = 6},
            new JungleMobOffsets {BaseSkinName = "SRU_Krug", Width = 80, Height = 2, XOffset = 13, YOffset = 8},
            new JungleMobOffsets {BaseSkinName = "SRU_Razorbeak", Width = 74, Height = 3, XOffset = 18, YOffset = 8},
            new JungleMobOffsets {BaseSkinName = "SRU_Murkwolf", Width = 74, Height = 2, XOffset = 18, YOffset = 8},
            new JungleMobOffsets {BaseSkinName = "SRU_Baron", Width = 139, Height = 14, XOffset = -60, YOffset = -230},
            new JungleMobOffsets {BaseSkinName = "SRU_Dragon_Fire", Width = 139, Height = 8, XOffset = 13, YOffset = 24},
            new JungleMobOffsets
            {
                BaseSkinName = "SRU_Dragon_Water",
                Width = 139,
                Height = 8,
                XOffset = 13,
                YOffset = 24
            },
            new JungleMobOffsets {BaseSkinName = "SRU_Dragon_Air", Width = 139, Height = 8, XOffset = 13, YOffset = 24},
            new JungleMobOffsets
            {
                BaseSkinName = "SRU_Dragon_Earth",
                Width = 139,
                Height = 8,
                XOffset = 13,
                YOffset = 24
            },
        };

        private const int XOffset = 10;
        private static int YOffset = 18;
        private const int Width = 103;
        private const int Height = 9;

        private static readonly Color Color = Color.Lime;
        private static readonly Color FillColor = Color.Goldenrod;

        private static bool hero => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.EnabledHero"].GetValue<MenuBool>().Enabled;

        private static bool mob => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.EnabledMob"].GetValue<MenuBool>().Enabled;

        private static bool Fill => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.Fill"].GetValue<MenuBool>().Enabled;

        private static bool q => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.Q"].GetValue<MenuBool>().Enabled;

        private static bool w => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.W"].GetValue<MenuBool>().Enabled;

        private static bool e => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.E"].GetValue<MenuBool>().Enabled;

        private static bool r => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.R"].GetValue<MenuBool>().Enabled;

        private static bool attack => MyMenuExtensions.DrawOption
            .DamageHeroMenu["SharpShooter.DrawSettings.DamageIndicatorToHero.Attack"].GetValue<MenuBool>().Enabled;

        public static void OnDamageIndicator()
        {
            Drawing.OnEndScene += delegate
            {
                if (ObjectManager.Player.IsDead)
                {
                    return;
                }

                if (hero)
                {
                    foreach (
                        var target in
                        GameObjects.EnemyHeroes.Where(
                            h =>
                                h.IsValid && h.IsHPBarRendered))
                    {
                        Vector2 pos;
                        Drawing.WorldToScreen(target.PreviousPosition, out pos);

                        if (!pos.IsOnScreen())
                        {
                            return;
                        }

                        if (target.IsMelee)
                        {
                            YOffset = 12;
                        }
                        else if (target.CharacterName == "Annie" || target.CharacterName == "Jhin")
                        {
                            YOffset = 5;
                        }
                        else
                        {
                            YOffset = 18;
                        }

                        var damage = (float)target.GetComboDamage(q, w, e, r, attack);

                        if (damage > 2)
                        {
                            var barPos = target.HPBarPosition;
                            var percentHealthAfterDamage = Math.Max(0, target.Health - damage) / target.MaxHealth;
                            var yPos = barPos.Y + YOffset;
                            var xPosDamage = barPos.X + XOffset + Width * percentHealthAfterDamage;
                            var xPosCurrentHp = barPos.X + XOffset + Width * target.Health / target.MaxHealth;

                            if (damage > target.Health)
                            {
                                var X = (int)barPos.X + XOffset;
                                var Y = (int)barPos.Y + YOffset - 15;
                                var text = "KILLABLE: " + (target.Health - damage);
                                Drawing.DrawText(X, Y, Color.Red, text);
                            }

                            Drawing.DrawLine(xPosDamage, yPos, xPosDamage, yPos + Height, 5, Color);

                            if (Fill)
                            {
                                var differenceInHp = xPosCurrentHp - xPosDamage;
                                var pos1 = barPos.X + 9 + 107 * percentHealthAfterDamage;

                                for (var i = 0; i < differenceInHp; i++)
                                {
                                    Drawing.DrawLine(pos1 + i, yPos, pos1 + i, yPos + Height, 5, FillColor);
                                }
                            }
                        }
                    }
                }

                if (mob)
                {
                    foreach (
                        var unit in
                        ObjectManager.Get<AIMinionClient>()
                            .Where(h => h.IsValid && h.IsHPBarRendered && h.Team == GameObjectTeam.Neutral))
                    {
                        if (unit.IsValidTarget())
                        {
                            Vector2 pos;
                            Drawing.WorldToScreen(unit.PreviousPosition, out pos);

                            if (!pos.IsOnScreen())
                            {
                                return;
                            }

                            var mobOffset = JungleMobOffsetsList.Find(x => x.BaseSkinName == unit.CharacterName);
                            if (mobOffset != null)
                            {
                                var barPos = unit.HPBarPosition;
                                barPos.X += mobOffset.XOffset;
                                barPos.Y += mobOffset.YOffset;

                                var damage = (float)unit.GetComboDamage(q, w, e, r, attack);

                                if (damage > unit.Health)
                                {
                                    var X = (int)barPos.X + XOffset;
                                    var Y = (int)barPos.Y + YOffset;
                                    var text = "KILLABLE: " + (unit.Health - damage);
                                    Drawing.DrawText(X, Y, Color.Red, text);
                                }

                                if (damage > 0)
                                {
                                    var hpPercent = unit.Health / unit.MaxHealth * 100;
                                    var hpPrecentAfterDamage = (unit.Health - damage) / unit.MaxHealth * 100;
                                    var drawStartXPos = barPos.X + mobOffset.Width * (hpPrecentAfterDamage / 100);
                                    var drawEndXPos = barPos.X + mobOffset.Width * (hpPercent / 100);

                                    if (unit.Health < damage)
                                    {
                                        drawStartXPos = barPos.X;
                                    }

                                    Drawing.DrawLine(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, mobOffset.Height, FillColor);
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
