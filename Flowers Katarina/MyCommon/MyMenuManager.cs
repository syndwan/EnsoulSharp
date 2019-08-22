namespace Flowers_Katarina.MyCommon
{
    #region 

    using System;

    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.MenuUI.Values;

    using Flowers_Katarina.MyBase;

    using Keys = System.Windows.Forms.Keys;

    #endregion

    public class MyMenuManager
    {
        public static void Initializer()
        {
            try
            {
                MyLogic.Menu = new Menu("FlowersKatarina", "Flowers Katarina", true);
                {
                    MyLogic.Menu.Add(new MenuSeparator("MadebyNightMoon", "Made by NightMoon"));
                }

                MyLogic.ComboMenu = new Menu("FlowersKatarina.ComboMenu", ":: Combo Settings");
                {
                    MyLogic.ComboMenu.Add(new MenuSeparator("FlowersKatarina.ComboMenu.QSettings", "-- Q Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.Q", "Use Q"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.QOnMinion", "Use Q| On Minion to Gapcloser", false));

                    MyLogic.ComboMenu.Add(new MenuSeparator("FlowersKatarina.ComboMenu.WSettings", "-- W Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.W", "Use W"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.WSmart", "Use W| Smart Gapcloser"));

                    MyLogic.ComboMenu.Add(new MenuSeparator("FlowersKatarina.ComboMenu.ESettings", "-- E Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.E", "Use E"));
                    MyLogic.ComboMenu.Add(new MenuKeyBind("FlowersKatarina.ComboMenu.EKillAble", "Use E| Only KillAble",
                        Keys.G, KeyBindType.Toggle)).Permashow();

                    MyLogic.ComboMenu.Add(new MenuSeparator("FlowersKatarina.ComboMenu.RSettings", "-- R Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.R", "Use R"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.RAlways", "Use R| Always Cast", false));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.RKillAble", "Use R| KillAble"));
                    MyLogic.ComboMenu.Add(new MenuSliderButton("FlowersKatarina.ComboMenu.RCountHit", "Use R| Min Hit Count >= x", 3, 1, 5));

                    MyLogic.ComboMenu.Add(new MenuSeparator("FlowersKatarina.ComboMenu.ModeSettings", "-- Other Settings"));
                    MyLogic.ComboMenu.Add(new MenuList("FlowersKatarina.ComboMenu.Mode", "Combo Mode: ", new[] {"QE", "EQ"}, 1)).Permashow();
                    MyLogic.ComboMenu.Add(new MenuKeyBind("FlowersKatarina.ComboMenu.SwitchMode",
                            "Switch Combo Mode Key", Keys.H, KeyBindType.Press)).ValueChanged +=
                        delegate(object sender, EventArgs e)
                        {
                            var list = sender as MenuKeyBind;
                            if (list != null && list.Active)
                            {
                                switch (MyLogic.ComboMenu["FlowersKatarina.ComboMenu.Mode"].GetValue<MenuList>().Index)
                                {
                                    case 0:
                                        MyLogic.ComboMenu["FlowersKatarina.ComboMenu.Mode"].GetValue<MenuList>().Index =
                                            1;
                                        break;
                                    case 1:
                                        MyLogic.ComboMenu["FlowersKatarina.ComboMenu.Mode"].GetValue<MenuList>().Index =
                                            0;
                                        break;
                                }
                            }
                        };
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersKatarina.ComboMenu.Ignite", "Use Ignite"));
                }
                MyLogic.Menu.Add(MyLogic.ComboMenu);

                MyLogic.HarassMenu = new Menu("FlowersKatarina.HarassMenu", ":: Harass Settings");
                {
                    MyLogic.HarassMenu.Add(new MenuSeparator("FlowersKatarina.HarassMenu.QSettings", "-- Q Settings"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersKatarina.HarassMenu.Q", "Use Q"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersKatarina.HarassMenu.QOnMinion", "Use Q| On Minion to Gapcloser", false));

                    MyLogic.HarassMenu.Add(new MenuSeparator("FlowersKatarina.HarassMenu.WSettings", "-- W Settings"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersKatarina.HarassMenu.W", "Use W", false));

                    MyLogic.HarassMenu.Add(new MenuSeparator("FlowersKatarina.HarassMenu.ESettings", "-- E Settings"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersKatarina.HarassMenu.E", "Use E", false));

                    MyLogic.HarassMenu.Add(new MenuSeparator("FlowersKatarina.HarassMenu.ModeSettings", "-- Mode Settings"));
                    MyLogic.HarassMenu.Add(new MenuList("FlowersKatarina.HarassMenu.Mode", "Harass Mode: ", new[] { "QE", "EQ" })).Permashow();
                }
                MyLogic.Menu.Add(MyLogic.HarassMenu);

                MyLogic.ClearMenu = new Menu("FlowersKatarina.ClearMenu", ":: Clear Settings");
                {
                    MyLogic.ClearMenu.Add(new MenuSeparator("FlowersKatarina.ClearMenu.LaneClearSettings", "-- LaneClear Settings"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.LaneClearQ", "Use Q"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.LaneClearQOnlyLH", "Use Q| Only LastHit"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.LaneClearW", "Use W"));

                    MyLogic.ClearMenu.Add(new MenuSeparator("FlowersKatarina.ClearMenu.JungleClearSettings", "-- JungleClear Settings"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.JungleClearQ", "Use Q"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.JungleClearW", "Use W"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersKatarina.ClearMenu.JungleClearE", "Use E"));
                }
                MyLogic.Menu.Add(MyLogic.ClearMenu);

                MyLogic.LastHitMenu = new Menu("FlowersKatarina.LastHitMenu", ":: LastHit Settings");
                {
                    MyLogic.LastHitMenu.Add(new MenuSeparator("FlowersKatarina.LastHitMenu.QSettings", "-- Q Settings"));
                    MyLogic.LastHitMenu.Add(new MenuBool("FlowersKatarina.LastHitMenu.Q", "Use Q"));
                }
                MyLogic.Menu.Add(MyLogic.LastHitMenu);

                MyLogic.FleeMenu = new Menu("FlowersKatarina.FleeMenu", ":: Flee Settings");
                {
                    MyLogic.FleeMenu.Add(new MenuSeparator("FlowersKatarina.FleeMenu.KeySettings", "-- Key Settings"));
                    MyLogic.FleeMenu.Add(new MenuKeyBind("FlowersKatarina.FleeMenu.Key", "Flee Active Key",
                        Keys.Z, KeyBindType.Press));

                    MyLogic.FleeMenu.Add(new MenuSeparator("FlowersKatarina.FleeMenu.WSettings", "-- W Settings"));
                    MyLogic.FleeMenu.Add(new MenuBool("FlowersKatarina.FleeMenu.W", "Use W"));

                    MyLogic.FleeMenu.Add(new MenuSeparator("FlowersKatarina.FleeMenu.ESettings", "-- E Settings"));
                    MyLogic.FleeMenu.Add(new MenuBool("FlowersKatarina.FleeMenu.E", "Use E"));
                }
                MyLogic.Menu.Add(MyLogic.FleeMenu);

                MyLogic.KillStealMenu = new Menu("FlowersKatarina.KillStealMenu", ":: KillSteal Settings");
                {
                    MyLogic.KillStealMenu.Add(new MenuSeparator("FlowersKatarina.KillStealMenu.QSettings", "-- Q Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersKatarina.KillStealMenu.Q", "Use Q"));

                    MyLogic.KillStealMenu.Add(new MenuSeparator("FlowersKatarina.KillStealMenu.ESettings", "-- E Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersKatarina.KillStealMenu.E", "Use E"));

                    MyLogic.KillStealMenu.Add(new MenuSeparator("FlowersKatarina.KillStealMenu.RSettings", "-- R Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersKatarina.KillStealMenu.R", "Use R"));

                    MyLogic.KillStealMenu.Add(new MenuSeparator("FlowersKatarina.KillStealMenu.OtherSettings", "-- Other Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersKatarina.KillStealMenu.CancelR", "Auto Cancel R to KS"));
                }
                MyLogic.Menu.Add(MyLogic.KillStealMenu);

                MyLogic.MiscMenu = new Menu("FlowersKatarina.MiscMenu", ":: Misc Settings");
                {
                    MyManaManager.AddFarmToMenu(MyLogic.MiscMenu);
                    MyLogic.MiscMenu.Add(new MenuSeparator("FlowersKatarina.MiscMenu.ESettings", "-- E Settings"));
                    MyLogic.MiscMenu.Add(new MenuSliderButton("FlowersKatarina.MiscMenu.EHumanizer",
                        "Enabled Humanizer| Delay <= x(ms)", 0, 0, 1500));
                    MyLogic.MiscMenu.Add(new MenuList("FlowersKatarina.MiscMenu.ETurret", "Disable E to Enemy Turret",
                        new[] {"Always", "Smart", "Off"}, 1));
                    MyLogic.MiscMenu.Add(new MenuSlider("FlowersKatarina.MiscMenu.ETurretHP",
                        "Smart Mode: When Player HealthPercent <= x%", 50, 1, 99));

                    MyLogic.MiscMenu.Add(new MenuSeparator("FlowersKatarina.MiscMenu.RSettings", "-- R Settings"));
                    MyLogic.MiscMenu.Add(new MenuBool("FlowersKatarina.MiscMenu.AutoCancelR", "Auto Cancel Ult"));

                    MyLogic.MiscMenu.Add(new MenuSeparator("FlowersKatarina.MiscMenu.OtherSettings", "-- Other Settings"));
                    MyLogic.MiscMenu.Add(new MenuKeyBind("FlowersKatarina.MiscMenu.OneKeyEW", "Semi EW Key",
                        Keys.A, KeyBindType.Press));
                }
                MyLogic.Menu.Add(MyLogic.MiscMenu);

                MyLogic.DrawMenu = new Menu("FlowersKatarina.DrawMenu", ":: Draw Settings");
                {
                    MyLogic.DrawMenu.Add(new MenuSeparator("FlowersKatarina.DrawMenu.RangeSettings", "-- Spell Range"));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersKatarina.DrawMenu.Q", "Draw Q Range", false));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersKatarina.DrawMenu.E", "Draw E Range", false));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersKatarina.DrawMenu.R", "Draw R Range", false));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersKatarina.DrawMenu.Dagger", "Draw Dagger Range", false));
                }
                MyLogic.Menu.Add(MyLogic.DrawMenu);

                MyLogic.Menu.Attach();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyMenuManager.Initializer." + ex);
            }
        }
    }
}