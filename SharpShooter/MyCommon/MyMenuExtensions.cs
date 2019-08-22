namespace SharpShooter.MyCommon
{
    #region

    using System.Linq;
    using System.Drawing;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.MenuUI.Values;
    using EnsoulSharp.SDK.Utility;

    using Keys = System.Windows.Forms.Keys;

    #endregion

    internal static class MyMenuExtensions
    {
        internal static Menu myMenu { get; set; }
        internal static Menu AxeMenu { get; set; }
        internal static Menu ComboMenu { get; set; }
        internal static Menu HarassMenu { get; set; }
        internal static Menu LaneClearMenu { get; set; }
        internal static Menu JungleClearMenu { get; set; }
        internal static Menu LastHitMenu { get; set; }
        internal static Menu FleeMenu { get; set; }
        internal static Menu KillStealMenu { get; set; }
        internal static Menu MiscMenu { get; set; }
        internal static Menu DrawMenu { get; set; }

        internal class AxeOption
        {
            private static Menu axeMenu => AxeMenu;

            internal static void AddMenu()
            {
                AxeMenu = new Menu("AxeSettings", "Axe Settings");
                myMenu.Add(AxeMenu);
            }

            internal static void AddSeperator(string name)
            {
                axeMenu.Add(new MenuSeparator("Axe" + name, name));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                axeMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                axeMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                axeMenu.Add(new MenuKeyBind(name, defaultName, Keys, type){ Active = enabled });
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                axeMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                axeMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return axeMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return axeMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return axeMenu[name].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return axeMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return axeMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class ComboOption
        {
            private static Menu comboMenu => ComboMenu;

            internal static void AddMenu()
            {
                ComboMenu = new Menu("ComboSettings", "Combo Settings");
                myMenu.Add(ComboMenu);
            }

            internal static bool UseQ
                =>
                    comboMenu["ComboQ"] != null &&
                    comboMenu["ComboQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    comboMenu["ComboW"] != null &&
                    comboMenu["ComboW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    comboMenu["ComboE"] != null &&
                    comboMenu["ComboE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    comboMenu["ComboR"] != null &&
                    comboMenu["ComboR"].GetValue<MenuBool>().Enabled;

            internal static void AddSeperator(string name)
            {
                comboMenu.Add(new MenuSeparator("Combo" + name, name));
            }

            internal static void AddQ(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboR", "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                comboMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                comboMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                comboMenu.Add(new MenuKeyBind(name, defaultName, Keys, type){Active = enabled });
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                comboMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                comboMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return comboMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return comboMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return comboMenu[name].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return comboMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return comboMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class HarassOption
        {
            private static Menu harassMenu => HarassMenu;

            internal static void AddMenu()
            {
                HarassMenu = new Menu("HarassSettings", "Harass Settings");
                myMenu.Add(HarassMenu);
            }

            internal static bool UseQ
                =>
                    harassMenu["HarassQ"] != null &&
                    harassMenu["HarassQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    harassMenu["HarassW"] != null &&
                    harassMenu["HarassW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    harassMenu["HarassE"] != null &&
                    harassMenu["HarassE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    harassMenu["HarassR"] != null &&
                    harassMenu["HarassR"].GetValue<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassR", "Use R", enabled));
            }

            internal static void AddTargetList()
            {
                harassMenu.Add(new MenuSeparator("HarassListSettings", "Harass Target List"));

                foreach (var target in GameObjects.EnemyHeroes)
                {
                    if (target != null)
                    {
                        harassMenu.Add(new MenuBool("HarassList" + target.CharacterName.ToLower(), target.CharacterName));
                    }
                }
            }

            internal static bool GetHarassTargetEnabled(string name)
            {
                return harassMenu["HarassList" + name.ToLower()] != null &&
                       harassMenu["HarassList" + name.ToLower()].GetValue<MenuBool>().Enabled;
            }

            internal static AIHeroClient GetTarget(float range)
            {
                return MyTargetSelector.GetTargets(range).FirstOrDefault(x => x.IsValidTarget(range) && GetHarassTargetEnabled(x.CharacterName));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                harassMenu.Add(new MenuSlider("HarassMana", "When Player ManaPercent >= x%", defalutValue, 1, 99));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.Player.ManaPercent >= GetSlider("HarassMana").Value &&
                    (underTurret || !ObjectManager.Player.IsUnderEnemyTurret());

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                harassMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                harassMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                harassMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }


            internal static void AddKey(string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                harassMenu.Add(new MenuKeyBind(name, defaultName, Keys, type){ Active = enabled });
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                harassMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return harassMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return harassMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return harassMenu[name].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return harassMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return harassMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class LaneClearOption
        {
            private static Menu laneClearMenu => LaneClearMenu;

            internal static void AddMenu()
            {
                LaneClearMenu = new Menu("LaneClearSettings", "LaneClear Settings");
                myMenu.Add(LaneClearMenu);
            }

            internal static bool UseQ
                =>
                    laneClearMenu["LaneClearQ"] != null &&
                    laneClearMenu["LaneClearQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    laneClearMenu["LaneClearW"] != null &&
                    laneClearMenu["LaneClearW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    laneClearMenu["LaneClearE"] != null &&
                    laneClearMenu["LaneClearE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    LaneClearMenu["LaneClearR"] != null &&
                    LaneClearMenu["LaneClearR"].GetValue<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearR", "Use R", enabled));
            }

            internal static void AddMana(int defalutValue = 60)
            {
                laneClearMenu.Add(new MenuSlider("LaneClearMana", "When Player ManaPercent >= x%", defalutValue, 1, 99));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.Player.ManaPercent >= GetSlider("LaneClearMana").Value && MyManaManager.SpellFarm &&
                    (underTurret || !ObjectManager.Player.IsUnderEnemyTurret());

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                laneClearMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                laneClearMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }


            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                laneClearMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return laneClearMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return laneClearMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return laneClearMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return laneClearMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class JungleClearOption
        {
            private static Menu jungleClearMenu => JungleClearMenu;

            internal static void AddMenu()
            {
                JungleClearMenu = new Menu("JungleClearSettings", "JungleClear Settings");
                myMenu.Add(JungleClearMenu);
            }

            internal static bool UseQ
                =>
                    jungleClearMenu["JungleClearQ"] != null &&
                    jungleClearMenu["JungleClearQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    jungleClearMenu["JungleClearW"] != null &&
                    jungleClearMenu["JungleClearW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    jungleClearMenu["JungleClearE"] != null &&
                    jungleClearMenu["JungleClearE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    jungleClearMenu["JungleClearR"] != null &&
                    jungleClearMenu["JungleClearR"].GetValue<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearR", "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                jungleClearMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                jungleClearMenu.Add(new MenuSlider("JungleClearMana", "When Player ManaPercent >= x%",
                    defalutValue, 1, 99));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                jungleClearMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                jungleClearMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.Player.ManaPercent >= GetSlider("JungleClearMana").Value && MyManaManager.SpellFarm &&
                    (underTurret || !ObjectManager.Player.IsUnderEnemyTurret());

            internal static MenuBool GetBool(string name)
            {
                return jungleClearMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return jungleClearMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return jungleClearMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return jungleClearMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class LastHitOption
        {
            private static Menu lastHitMenu => LastHitMenu;

            internal static void AddMenu()
            {
                LastHitMenu = new Menu("LastHitSettings", "LastHit Settings");
                myMenu.Add(LastHitMenu);
            }

            internal static bool HasEnouguMana
                => ObjectManager.Player.ManaPercent >= GetSlider("LastHitMana").Value && MyManaManager.SpellFarm;

            internal static bool UseQ
                =>
                    lastHitMenu["LastHitQ"] != null &&
                    lastHitMenu["LastHitQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    lastHitMenu["LastHitW"] != null &&
                    lastHitMenu["LastHitW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    lastHitMenu["LastHitE"] != null &&
                    lastHitMenu["LastHitE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    lastHitMenu["LastHitR"] != null &&
                    lastHitMenu["LastHitR"].GetValue<MenuBool>().Enabled;
            internal static void AddQ(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitR", "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                lastHitMenu.Add(new MenuSlider("LastHitMana", "When Player ManaPercent >= x%", defalutValue));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                lastHitMenu.Add(new MenuSlider(name, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                lastHitMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return lastHitMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return lastHitMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return lastHitMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return lastHitMenu[name].GetValue<MenuSliderButton>();
            }
        }

        internal class FleeOption
        {
            private static Menu fleeMenu => FleeMenu;

            internal static void AddMenu()
            {
                FleeMenu = new Menu("FleeSettings", "Flee Settings")
                {
                    new MenuKeyBind("FleeKey", "Flee Key", Keys.Z, KeyBindType.Press)
                };
                myMenu.Add(FleeMenu);
            }

            internal static bool isFleeKeyActive
                => fleeMenu["FleeKey"] != null && fleeMenu["FleeKey"].GetValue<MenuKeyBind>().Active;

            internal static bool UseQ
                =>
                    fleeMenu["FleeQ"] != null &&
                    fleeMenu["FleeQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    fleeMenu["FleeW"] != null &&
                    fleeMenu["FleeW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    fleeMenu["FleeE"] != null &&
                    fleeMenu["FleeE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    fleeMenu["FleeR"] != null &&
                    fleeMenu["FleeR"].GetValue<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeR", "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                fleeMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return fleeMenu[name].GetValue<MenuBool>();
            }
        }

        internal class KillStealOption
        {
            private static Menu killStealMenu => KillStealMenu;

            internal static bool UseQ
                =>
                    killStealMenu["KillStealQ"] != null &&
                    killStealMenu["KillStealQ"].GetValue<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    killStealMenu["KillStealW"] != null &&
                    killStealMenu["KillStealW"].GetValue<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    killStealMenu["KillStealE"] != null &&
                    killStealMenu["KillStealE"].GetValue<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    killStealMenu["KillStealR"] != null &&
                    killStealMenu["KillStealR"].GetValue<MenuBool>().Enabled;

            internal static void AddMenu()
            {
                KillStealMenu = new Menu("KillStealSettings", "KillSteal Settings");
                myMenu.Add(KillStealMenu);
            }

            internal static void AddQ(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealQ", "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealW", "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealE", "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealR", "Use R", enabled));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                killStealMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                killStealMenu.Add(new MenuSlider(name, defaultName, defalueValue, minValue, maxValue));
            }

            internal static MenuSlider GetSlider(string name)
            {
                return killStealMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuSliderButton GetSliderBool(string itemName)
            {
                return killStealMenu[itemName].GetValue<MenuSliderButton>();
            }

            internal static void AddTargetList()
            {
                killStealMenu.Add(new MenuSeparator("KillStealListSettings", "KillSteal Target List"));

                foreach (var target in GameObjects.EnemyHeroes)
                {
                    if (target != null)
                    {
                        killStealMenu.Add(new MenuBool("KillStealList" + target.CharacterName.ToLower(), "Use On: " + target.CharacterName));
                    }
                }
            }

            internal static bool GetKillStealTarget(string name)
            {
                return killStealMenu["KillStealList" + name.ToLower()] != null &&
                       killStealMenu["KillStealList" + name.ToLower()].GetValue<MenuBool>().Enabled;
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                killStealMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return killStealMenu[name].GetValue<MenuBool>();
            }
        }

        internal class MiscOption
        {
            private static Menu miscMenu => MiscMenu;

            internal static void AddMenu()
            {
                MiscMenu = new Menu("MiscSettings", "Misc Settings");
                myMenu.Add(MiscMenu);
            }

            internal static void AddBasic()
            {
                MyManaManager.AddFarmToMenu(miscMenu);
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                miscMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                miscMenu.Add(new MenuSlider(name, defaultName, defalueValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                miscMenu.Add(new MenuKeyBind(name, defaultName, Keys, type){ Active = enabled });
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                miscMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                miscMenu.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static void AddBool(string menuName, string name, string defaultName, bool enabled = true)
            {
                var subMeun = miscMenu["SharpShooter.MiscSettings." + menuName] as Menu;
                subMeun?.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddSlider(string menuName, string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                var subMeun = miscMenu["SharpShooter.MiscSettings." + menuName] as Menu;
                subMeun?.Add(new MenuSlider(name, defaultName, defalueValue, minValue, maxValue));
            }

            internal static void AddKey(string menuName, string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                var subMeun = miscMenu["SharpShooter.MiscSettings." + menuName] as Menu;
                subMeun?.Add(new MenuKeyBind(name, defaultName, Keys, type){Active = enabled });
            }

            internal static void AddList(string menuName, string name, string defaultName, string[] values, int defaultValue = 0)
            {
                var subMeun = miscMenu["SharpShooter.MiscSettings." + menuName] as Menu;
                subMeun?.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string menuName, string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                var subMeun = miscMenu["SharpShooter.MiscSettings." + menuName] as Menu;
                subMeun?.Add(new MenuSliderButton(name, defaultName, defaultValue, minValue, maxValue, enabled));
            }

            internal static void AddSetting(string name)
            {
                var nameMenu = new Menu("SharpShooter.MiscSettings." + name, name + " Settings");
                miscMenu.Add(nameMenu);
            }

            internal static void AddSubMenu(string name, string disableName)
            {
                var subMenu = new Menu("SharpShooter.MiscSettings." + name, disableName);
                miscMenu.Add(subMenu);
            }

            internal static void AddQ()
            {
                var qMenu = new Menu("SharpShooter.MiscSettings.Q", "Q Settings");
                miscMenu.Add(qMenu);
            }

            internal static void AddW()
            {
                var wMenu = new Menu("SharpShooter.MiscSettings.W", "W Settings");
                miscMenu.Add(wMenu);
            }

            internal static void AddE()
            {
                var eMenu = new Menu("SharpShooter.MiscSettings.E", "E Settings");
                miscMenu.Add(eMenu);
            }

            internal static void AddR()
            {
                var rMenu = new Menu("SharpShooter.MiscSettings.R", "R Settings");
                miscMenu.Add(rMenu);
            }

            internal static MenuBool GetBool(string name)
            {
                return miscMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return miscMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return miscMenu[name].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return miscMenu[name].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string name)
            {
                return miscMenu[name].GetValue<MenuSliderButton>();
            }

            internal static MenuBool GetBool(string menuName, string itemName)
            {
                return miscMenu["SharpShooter.MiscSettings." + menuName][itemName].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string menuName, string itemName)
            {
                return miscMenu["SharpShooter.MiscSettings." + menuName][itemName].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string menuName, string itemName)
            {
                return miscMenu["SharpShooter.MiscSettings." + menuName][itemName].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string menuName, string itemName)
            {
                return miscMenu["SharpShooter.MiscSettings." + menuName][itemName].GetValue<MenuList>();
            }

            internal static MenuSliderButton GetSliderBool(string menuName, string itemName)
            {
                return miscMenu["SharpShooter.MiscSettings." + menuName][itemName].GetValue<MenuSliderButton>();
            }
        }

        internal class DrawOption
        {
            private static Menu drawMenu => DrawMenu;

            internal static Menu spellMenu;
            internal static Menu DamageHeroMenu;

            internal static void AddMenu()
            {
                DrawMenu = new Menu("DrawSettings", "Draw Settings");
                myMenu.Add(DrawMenu);

                spellMenu = new Menu("SharpShooter.DrawSettings.SpellMenu", "Spell Range");
                DrawMenu.Add(spellMenu);
            }

            internal static void AddDamageIndicatorToHero(bool q, bool w, bool e, bool r, bool attack, bool enabledHero = true, bool enabledmob = false, bool fill = true)
            {
                DamageHeroMenu = new Menu("SharpShooter.DrawSettings.DamageIndicatorToHero", "Damage Indicator")
                {
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.EnabledHero", "Draw On Heros",
                        enabledHero),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.EnabledMob", "Draw On Mobs",
                        enabledmob),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.Q", "Draw Q Damage", q),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.W", "Draw W Damage", w),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.E", "Draw E Damage", e),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.R", "Draw R Damage", r),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.Attack", "Draw Attack Damage", attack),
                    new MenuBool("SharpShooter.DrawSettings.DamageIndicatorToHero.Fill", "Draw Fill Damage", fill)
                };

                DrawMenu.Add(DamageHeroMenu);

                MyDamageIndicator.OnDamageIndicator();
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                drawMenu.Add(new MenuBool(name, defaultName, enabled));
            }

            internal static void AddKey(string name, string defaultName, Keys Keys, KeyBindType type, bool enabled = false)
            {
                drawMenu.Add(new MenuKeyBind(name, defaultName, Keys, type){ Active = enabled });
            }

            internal static void AddSlider(string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                drawMenu.Add(new MenuSlider(name, defaultName, defalueValue, minValue, maxValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return drawMenu[name].GetValue<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return drawMenu[name].GetValue<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return drawMenu[name].GetValue<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return drawMenu[name].GetValue<MenuList>();
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                drawMenu.Add(new MenuList(name, defaultName, values, defaultValue));
            }

            internal static void AddRange(Spell spell, string name, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("Draw" + spell.Slot, "Draw" + name + " Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["Draw" + spell.Slot].GetValue<MenuBool>().Enabled &&
                        ObjectManager.Player.Spellbook.GetSpell(spell.Slot).Level > 0 &&
                        ObjectManager.Player.Spellbook.CanUseSpell(spell.Slot) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.FromArgb(199, 5, 255), 1);
                    }
                };
            }

            internal static void AddQ(Spell spell, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("DrawQ", "Draw Q Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["DrawQ"].GetValue<MenuBool>().Enabled && 
                        ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level > 0 && 
                        ObjectManager.Player.Spellbook.CanUseSpell(SpellSlot.Q) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.FromArgb(19, 130, 234), 1);
                    }
                };
            }

            internal static void AddW(Spell spell, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("DrawW", "Draw W Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["DrawW"].GetValue<MenuBool>().Enabled &&
                        ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level > 0 &&
                        ObjectManager.Player.Spellbook.CanUseSpell(SpellSlot.W) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.FromArgb(248, 246, 6), 1);
                    }
                };
            }

            internal static void AddE(Spell spell, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("DrawE", "Draw E Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["DrawE"].GetValue<MenuBool>().Enabled && 
                        ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level > 0 &&
                        ObjectManager.Player.Spellbook.CanUseSpell(SpellSlot.E) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.FromArgb(188, 6, 248), 1);
                    }
                };
            }

            internal static void AddR(Spell spell, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("DrawR", "Draw R Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["DrawR"].GetValue<MenuBool>().Enabled &&
                        ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level > 0 &&
                        ObjectManager.Player.Spellbook.CanUseSpell(SpellSlot.R) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.Red, 1);
                    }
                };
            }

            internal static void AddQExtend(Spell spell, bool enabled = false)
            {
                spellMenu.Add(new MenuBool("DrawQExtend", "Draw Q Extend Range", enabled));

                Drawing.OnDraw += delegate
                {
                    if (ObjectManager.Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen)
                    {
                        return;
                    }

                    if (spellMenu["DrawQExtend"].GetValue<MenuBool>().Enabled && 
                        ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level > 0 &&
                        ObjectManager.Player.Spellbook.CanUseSpell(SpellSlot.Q) == SpellState.Ready)
                    {
                        Render.Circle.DrawCircle(ObjectManager.Player.PreviousPosition, spell.Range, Color.FromArgb(0, 255, 161), 1);
                    }
                };
            }

            internal static void AddFarm()
            {
                MyManaManager.AddDrawToMenu(drawMenu);
            }
        }
    }
}