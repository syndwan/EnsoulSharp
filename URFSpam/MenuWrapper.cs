namespace URFSpam
{
    using EnsoulSharp.SDK.MenuUI.Values;

    internal class MenuWrapper
    {
        public class Q
        {
            public static readonly MenuSeparator Usage = new MenuSeparator("Usage", "Usage");
            public static readonly MenuBool Combo = new MenuBool("Combo", "Use in Combo");
            public static readonly MenuBool Harass = new MenuBool("Harass", "Use in Harass");
            public static readonly MenuBool JungleClear = new MenuBool("JungleClear", "Use in JungleClear");
            public static readonly MenuSeparator Settings = new MenuSeparator("Settings", "Settings");
            public static readonly MenuList Type = new MenuList("Type", "Spell Type", new[] {"Self", "Target", "Circle", "Linear (No Collision)", "Linear (Collision)"});
            public static readonly MenuSlider Range = new MenuSlider("Range", "Spell Range", 1000, 0, 5000);
            public static readonly MenuList Hitchance = new MenuList("Hitchance", "Spell Hitchance", new[] {"Very High", "High", "Medium", "Low"}, 2);
        }

        public class W
        {
            public static readonly MenuSeparator Usage = new MenuSeparator("Usage", "Usage");
            public static readonly MenuBool Combo = new MenuBool("Combo", "Use in Combo");
            public static readonly MenuBool Harass = new MenuBool("Harass", "Use in Harass");
            public static readonly MenuBool JungleClear = new MenuBool("JungleClear", "Use in JungleClear");
            public static readonly MenuSeparator Settings = new MenuSeparator("Settings", "Settings");
            public static readonly MenuList Type = new MenuList("Type", "Spell Type", new[] { "Self", "Target", "Circle", "Linear (No Collision)", "Linear (Collision)" });
            public static readonly MenuSlider Range = new MenuSlider("Range", "Spell Range", 1000, 0, 5000);
            public static readonly MenuList Hitchance = new MenuList("Hitchance", "Spell Hitchance", new[] { "Very High", "High", "Medium", "Low" }, 2);
        }

        public class E
        {
            public static readonly MenuSeparator Usage = new MenuSeparator("Usage", "Usage");
            public static readonly MenuBool Combo = new MenuBool("Combo", "Use in Combo");
            public static readonly MenuBool Harass = new MenuBool("Harass", "Use in Harass");
            public static readonly MenuBool JungleClear = new MenuBool("JungleClear", "Use in JungleClear");
            public static readonly MenuSeparator Settings = new MenuSeparator("Settings", "Settings");
            public static readonly MenuList Type = new MenuList("Type", "Spell Type", new[] { "Self", "Target", "Circle", "Linear (No Collision)", "Linear (Collision)" });
            public static readonly MenuSlider Range = new MenuSlider("Range", "Spell Range", 1000, 0, 5000);
            public static readonly MenuList Hitchance = new MenuList("Hitchance", "Spell Hitchance", new[] { "Very High", "High", "Medium", "Low" }, 2);
        }

        public class R
        {
            public static readonly MenuSeparator Usage = new MenuSeparator("Usage", "Usage");
            public static readonly MenuBool Combo = new MenuBool("Combo", "Use in Combo");
            public static readonly MenuBool Harass = new MenuBool("Harass", "Use in Harass");
            public static readonly MenuBool JungleClear = new MenuBool("JungleClear", "Use in JungleClear");
            public static readonly MenuSeparator Settings = new MenuSeparator("Settings", "Settings");
            public static readonly MenuList Type = new MenuList("Type", "Spell Type", new[] { "Self", "Target", "Circle", "Linear (No Collision)", "Linear (Collision)" });
            public static readonly MenuSlider Range = new MenuSlider("Range", "Spell Range", 1000, 0, 5000);
            public static readonly MenuList Hitchance = new MenuList("Hitchance", "Spell Hitchance", new[] { "Very High", "High", "Medium", "Low" }, 2);
        }

        public class Priority
        {
            public static readonly MenuSeparator TipsT = new MenuSeparator("TipsT", "This is for your Spell Cast Priority");
            public static readonly MenuSeparator TipsM = new MenuSeparator("TipsM", "4 is Max, 1 is Low");
            public static readonly MenuSlider QPriority = new MenuSlider("Q", "Q", 4, 1, 4);
            public static readonly MenuSlider WPriority = new MenuSlider("W", "W", 3, 1, 4);
            public static readonly MenuSlider EPriority = new MenuSlider("E", "E", 2, 1, 4);
            public static readonly MenuSlider RPriority = new MenuSlider("R", "R", 1, 1, 4);
        }

        public class Draw
        {
            public static readonly MenuBool QRange = new MenuBool("QRange", "Draw Q Range");
            public static readonly MenuColor QColor = new MenuColor("QColor", "^ Circle Color", SharpDX.Color.AliceBlue);
            public static readonly MenuBool WRange = new MenuBool("WRange", "Draw W Range");
            public static readonly MenuColor WColor = new MenuColor("WColor", "^ Circle Color", SharpDX.Color.GreenYellow);
            public static readonly MenuBool ERange = new MenuBool("ERange", "Draw E Range");
            public static readonly MenuColor EColor = new MenuColor("EColor", "^ Circle Color", SharpDX.Color.HotPink);
            public static readonly MenuBool RRange = new MenuBool("RRange", "Draw R Range");
            public static readonly MenuColor RColor = new MenuColor("RColor", "^ Circle Color", SharpDX.Color.White);
            public static readonly MenuSeparator Nothing = new MenuSeparator("Nothing", " ");
            public static readonly MenuBool DrawOnlyReady = new MenuBool("DrawOnlyReady", "Draw Only Spell Ready");
        }
    }
}
