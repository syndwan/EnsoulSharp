namespace SharpShooter.MyBase
{
    #region

    using System;
    using System.Linq;

    using EnsoulSharp;
    using EnsoulSharp.SDK.MenuUI;
    using EnsoulSharp.SDK.MenuUI.Values;
    using SharpShooter.MyCommon;

    #endregion

    internal class MyChampions
    {
        private static readonly string[] all =
        {
            "Ashe", "Caitlyn", "Corki", "Draven", "Ezreal", "Graves", "Jhin", "Jinx", "Kalista", "KogMaw", "MissFortune",
            "Lucian", "Quinn", "Sivir", "Tristana", "TwistedFate", "Twitch", "Urgot", "Varus", "Vayne", "Xayah"
        };

        public MyChampions()
        {
            Initializer();
        }

        private static void Initializer()
        {
            MyMenuExtensions.myMenu = new Menu(ObjectManager.Player.CharacterName,
                "SharpShooter: " + ObjectManager.Player.CharacterName, true);
            MyMenuExtensions.myMenu.Attach();

            var supportMenu = new Menu("SharpShooter.SupportChampion", "Support Champion");
            {
                foreach (var name in all)
                {
                    supportMenu.Add(new MenuSeparator("SharpShooter.SupportChampion.SC_" + name, name));
                }
            }
            MyMenuExtensions.myMenu.Add(supportMenu);

            MyMenuExtensions.myMenu.Add(new MenuSeparator("ASDASDG", ""));

            if (
                all.All(
                    x =>
                        !string.Equals(x, ObjectManager.Player.CharacterName,
                            StringComparison.CurrentCultureIgnoreCase)))
            {
                MyMenuExtensions.myMenu.Add(
                    new MenuSeparator("NotSupport_" + ObjectManager.Player.CharacterName,
                        "Not Support: " + ObjectManager.Player.CharacterName));
                Console.WriteLine("SharpShooter: " + ObjectManager.Player.CharacterName +
                       " Not Support!");
                return;
            }

            LoadChampionsPlugin();

            Console.WriteLine("SharpShooter: " + ObjectManager.Player.CharacterName +
                              " Load Success, Made By NightMoon");
        }

        internal static object LoadChampionsPlugin()
        {
            var instance = Activator.CreateInstance("SharpShooter", "SharpShooter.MyPlugin." + ObjectManager.Player.CharacterName);
            return instance;
        }
    }
}