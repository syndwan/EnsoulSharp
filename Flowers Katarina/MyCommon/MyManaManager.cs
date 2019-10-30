namespace Flowers_Katarina.MyCommon
{
    #region 

    using System;

    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.MenuUI.Values;

    #endregion

    public class MyManaManager
    {
        public static bool SpellFarm { get; set; } = true;

        private static int limitTick { get; set; } = 0;

        public static void AddFarmToMenu(Menu mainMenu)
        {
            try
            {
                if (mainMenu != null)
                {
                    mainMenu.Add(new MenuSeparator("MyManaManager.SpellFarmSettings", ":: Spell Farm Logic"));
                    mainMenu.Add(new MenuBool("MyManaManager.SpellFarm", "Use Spell To Farm(Mouse Scrool)")).Permashow();

                    Game.OnWndProc += delegate (GameWndProcEventArgs Args)
                    {
                        try
                        {
                            if (Args.Msg == 519)
                            {
                                mainMenu["MyManaManager.SpellFarm"].GetValue<MenuBool>().Enabled = !mainMenu["MyManaManager.SpellFarm"].GetValue<MenuBool>().Enabled;
                                SpellFarm = mainMenu["MyManaManager.SpellFarm"].GetValue<MenuBool>().Enabled;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in MyManaManager.OnWndProcEvent." + ex);
                        }
                    };

                    Game.OnUpdate += delegate
                    {
                        if (Variables.GameTimeTickCount - limitTick > 20 * Game.Ping)
                        {
                            limitTick = Variables.GameTimeTickCount;
                            SpellFarm = mainMenu["MyManaManager.SpellFarm"].GetValue<MenuBool>().Enabled;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyManaManager.AddFarmToMenu." + ex);
            }
        }
    }
}