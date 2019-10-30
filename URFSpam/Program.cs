namespace URFSpam
{
    using System;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.Events;
    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.Utility;

    internal class Program
    {
        private static void Main(string[] args)
        {
            GameEvent.OnGameLoad += OnGameLoad;
        }

        private static void OnGameLoad()
        {
            if (ObjectManager.Player == null)
            {
                return;
            }

            var MainMenu = new Menu("URFSpam", "URF Spam", true);
            MainMenu.Attach();

            var QMenu = new Menu("QSettings", "Q Settings")
            {
                MenuWrapper.Q.Usage,
                MenuWrapper.Q.Combo,
                MenuWrapper.Q.Harass,
                MenuWrapper.Q.JungleClear,
                MenuWrapper.Q.Settings,
                MenuWrapper.Q.Type,
                MenuWrapper.Q.Range,
                MenuWrapper.Q.Hitchance
            };
            MainMenu.Add(QMenu);

            var WMenu = new Menu("WSettings", "W Settings")
            {
                MenuWrapper.W.Usage,
                MenuWrapper.W.Combo,
                MenuWrapper.W.Harass,
                MenuWrapper.W.JungleClear,
                MenuWrapper.W.Settings,
                MenuWrapper.W.Type,
                MenuWrapper.W.Range,
                MenuWrapper.W.Hitchance
            };
            MainMenu.Add(WMenu);

            var EMenu = new Menu("ESettings", "E Settings")
            {
                MenuWrapper.E.Usage,
                MenuWrapper.E.Combo,
                MenuWrapper.E.Harass,
                MenuWrapper.E.JungleClear,
                MenuWrapper.E.Settings,
                MenuWrapper.E.Type,
                MenuWrapper.E.Range,
                MenuWrapper.E.Hitchance
            };
            MainMenu.Add(EMenu);

            var RMenu = new Menu("RSettings", "R Settings")
            {
                MenuWrapper.R.Usage,
                MenuWrapper.R.Combo,
                MenuWrapper.R.Harass,
                MenuWrapper.R.JungleClear,
                MenuWrapper.R.Settings,
                MenuWrapper.R.Type,
                MenuWrapper.R.Range,
                MenuWrapper.R.Hitchance
            };
            MainMenu.Add(RMenu);

            var PriorityMenu = new Menu("SpellPriority", "Spell Priority")
            {
                MenuWrapper.Priority.TipsT,
                MenuWrapper.Priority.TipsM,
                MenuWrapper.Priority.QPriority,
                MenuWrapper.Priority.WPriority,
                MenuWrapper.Priority.EPriority,
                MenuWrapper.Priority.RPriority
            };
            MainMenu.Add(PriorityMenu);

            var DrawMenu = new Menu("Draw", "Draw Settings")
            {
                MenuWrapper.Draw.QRange,
                MenuWrapper.Draw.QColor,
                MenuWrapper.Draw.WRange,
                MenuWrapper.Draw.WColor,
                MenuWrapper.Draw.ERange,
                MenuWrapper.Draw.EColor,
                MenuWrapper.Draw.RRange,
                MenuWrapper.Draw.RColor,
                MenuWrapper.Draw.Nothing,
                MenuWrapper.Draw.DrawOnlyReady
            };
            MainMenu.Add(DrawMenu);

            Console.WriteLine("URF Spam by NightMoon");
            Console.WriteLine("Some idea from Script Maker (HappyMajor)");
            Console.WriteLine("God know why i make this Assembly");

            SpellWrapper.Initializer();

            Tick.OnTick += OnTick;
            Drawing.OnDraw += OnDraw;
        }

        private static void OnTick(EventArgs args)
        {
            if (ObjectManager.Player == null || ObjectManager.Player.IsDead || ObjectManager.Player.IsRecalling())
            {
                return;
            }

            var mode = Orbwalker.ActiveMode;
            if (mode == OrbwalkerMode.LastHit || mode == OrbwalkerMode.None)
            {
                return;
            }

            var slot = SpellWrapper.GetPrioritySpell();
            if (slot == SpellSlot.Unknown || ObjectManager.Player.Spellbook.CanUseSpell(slot) != SpellState.Ready)
            {
                return;
            }

            SpellWrapper.CastSpell(slot, mode);
        }

        private static void OnDraw(EventArgs args)
        {
            if (ObjectManager.Player == null || ObjectManager.Player.IsDead)
            {
                return;
            }

            if (MenuGUI.IsChatOpen || MenuGUI.IsShopOpen || MenuGUI.IsScoreboardOpen)
            {
                return;
            }

            if (MenuWrapper.Draw.QRange.Enabled)
            {
                if (MenuWrapper.Draw.DrawOnlyReady.Enabled && SpellWrapper.Q.IsReady())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.Q.Range,
                        MenuWrapper.Draw.QColor.Color.ToSystemColor(), 2);
                }
                else if(!MenuWrapper.Draw.DrawOnlyReady.Enabled)
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.Q.Range,
                        MenuWrapper.Draw.QColor.Color.ToSystemColor(), 2);
                }
            }

            if (MenuWrapper.Draw.WRange.Enabled)
            {
                if (MenuWrapper.Draw.DrawOnlyReady.Enabled && SpellWrapper.W.IsReady())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.W.Range,
                        MenuWrapper.Draw.WColor.Color.ToSystemColor(), 2);
                }
                else if (!MenuWrapper.Draw.DrawOnlyReady.Enabled)
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.W.Range,
                        MenuWrapper.Draw.WColor.Color.ToSystemColor(), 2);
                }
            }

            if (MenuWrapper.Draw.ERange.Enabled)
            {
                if (MenuWrapper.Draw.DrawOnlyReady.Enabled && SpellWrapper.E.IsReady())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.E.Range,
                        MenuWrapper.Draw.EColor.Color.ToSystemColor(), 2);
                }
                else if (!MenuWrapper.Draw.DrawOnlyReady.Enabled)
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.E.Range,
                        MenuWrapper.Draw.EColor.Color.ToSystemColor(), 2);
                }
            }

            if (MenuWrapper.Draw.RRange.Enabled)
            {
                if (MenuWrapper.Draw.DrawOnlyReady.Enabled && SpellWrapper.R.IsReady())
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.R.Range,
                        MenuWrapper.Draw.RColor.Color.ToSystemColor(), 2);
                }
                else if (!MenuWrapper.Draw.DrawOnlyReady.Enabled)
                {
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, SpellWrapper.R.Range,
                        MenuWrapper.Draw.RColor.Color.ToSystemColor(), 2);
                }
            }
        }
    }
}
