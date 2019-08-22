namespace SharpShooter.MyBase
{
    #region

    using EnsoulSharp;
    using EnsoulSharp.SDK;

    #endregion

    internal class MyLogic
    {
        internal static Spell Q { get; set; }
        internal static Spell Q2 { get; set; }
        internal static Spell QE { get; set; }
        internal static Spell EQ { get; set; }
        internal static Spell W { get; set; }
        internal static Spell W2 { get; set; }
        internal static Spell E { get; set; }
        internal static Spell E2 { get; set; }
        internal static Spell R { get; set; }
        internal static Spell R2 { get; set; }
        internal static Spell Flash { get; set; }
        internal static Spell Ignite { get; set; }

        internal static SpellSlot IgniteSlot { get; set; } = SpellSlot.Unknown;
        internal static SpellSlot FlashSlot { get; set; } = SpellSlot.Unknown;

        internal static int LastForcusTime { get; set; } = 0;

        internal static AIHeroClient Me => ObjectManager.Player;
    }
}
