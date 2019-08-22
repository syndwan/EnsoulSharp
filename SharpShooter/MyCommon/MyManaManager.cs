namespace SharpShooter.MyCommon
{
    using EnsoulSharp;
    #region 

    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.MenuUI.Values;

    using Keys = System.Windows.Forms.Keys;

    #endregion

    internal class MyManaManager
    {
        internal static bool SpellFarm { get; set; } = true;
        internal static bool SpellHarass { get; set; } = true;

        private static bool FarmScrool { get; set; } = true;
        private static bool HarassScrool { get; set; } = true;

        internal static void AddFarmToMenu(Menu mainMenu)
        {
            if (mainMenu != null)
            {
                var farmMenu = new Menu("MyManaManager.SpellFarmSettings", "Spell Settings")
                {
                    new MenuList("MyManaManager.SpellFarmMode", "Enabled Spell Farm Control: ",
                        new[] {"Mouse scrool", "Key Toggle", "Off"}),
                    new MenuKeyBind("MyManaManager.SpellFarmKey", "Spell Farm Key", Keys.J,
                        KeyBindType.Toggle){ Active = true },
                    new MenuList("MyManaManager.SpellHarassMode", "Enabled Spell Harass Control: ",
                        new[] {"Mouse scrool", "Key Toggle", "Off"}, 1),
                    new MenuKeyBind("MyManaManager.SpellHarassKey", "Spell Harass Key", Keys.H,
                        KeyBindType.Toggle){ Active = true }
                };
                mainMenu.Add(farmMenu);

                Game.OnWndProc += delegate (WndEventArgs Args)
                {
                    if (Args.Msg == 519)
                    {
                        if (farmMenu["MyManaManager.SpellFarmMode"].GetValue<MenuList>().Index == 0)
                        {
                            FarmScrool = !FarmScrool;
                        }

                        if (farmMenu["MyManaManager.SpellHarassMode"].GetValue<MenuList>().Index == 0)
                        {
                            HarassScrool = !HarassScrool;
                        }
                    }
                };

                Game.OnUpdate += delegate
                {
                    SpellFarm = farmMenu["MyManaManager.SpellFarmMode"].GetValue<MenuList>().Index == 0 && FarmScrool ||
                                farmMenu["MyManaManager.SpellFarmMode"].GetValue<MenuList>().Index == 1 &&
                                farmMenu["MyManaManager.SpellFarmKey"].GetValue<MenuKeyBind>().Active ||
                                farmMenu["MyManaManager.SpellFarmMode"].GetValue<MenuList>().Index == 2;
                    SpellHarass = farmMenu["MyManaManager.SpellHarassMode"].GetValue<MenuList>().Index == 0 && FarmScrool ||
                                farmMenu["MyManaManager.SpellHarassMode"].GetValue<MenuList>().Index == 1 &&
                                farmMenu["MyManaManager.SpellHarassKey"].GetValue<MenuKeyBind>().Active;
                };
            }
        }

        internal static void AddDrawToMenu(Menu mainMenu)
        {
            // TODO rewrite
            //if (mainMenu != null)
            //{
            //    var newMenu = new Menu("MyManaManager.SpellFarmDraw", "Spell Farm")
            //        {
            //            new MenuBool("MyManaManager.DrawSpelFarm", "Draw Spell Farm Status"),
            //            new MenuBool("MyManaManager.DrawSpellHarass", "Draw Spell Harass Status")
            //        };
            //    mainMenu.Add(newMenu);

            //    Render.OnRender += delegate
            //    {
            //        if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen() || MenuGUI.IsShopOpen())
            //        {
            //            return;
            //        }

            //        if (newMenu["MyManaManager.DrawSpelFarm"].Enabled)
            //        {
            //            Vector2 MePos = Vector2.Zero;
            //            Render.WorldToScreen(ObjectManager.Player.Position, out MePos);

            //            Render.Text(MePos.X - 57, MePos.Y + 48, System.Drawing.Color.FromArgb(242, 120, 34),
            //                "Spell Farms:" + (SpellFarm ? "On" : "Off"));
            //        }

            //        if (newMenu["MyManaManager.DrawSpellHarass"].Enabled)
            //        {
            //            Vector2 MePos = Vector2.Zero;
            //            Render.WorldToScreen(ObjectManager.Player.Position, out MePos);

            //            Render.Text(MePos.X - 57, MePos.Y + 68, System.Drawing.Color.FromArgb(242, 120, 34),
            //                "Spell Harass:" + (SpellHarass ? "On" : "Off"));
            //        }
            //    };
            //}
        }
    }
}
