namespace Flowers_Katarina.MyCommon
{
    #region

    using System;

    using EnsoulSharp;
    using EnsoulSharp.SDK;

    using Flowers_Katarina.MyBase;

    #endregion

    public class MySpellManager
    {
        public static void Initializer()
        {
            try
            {
                MyLogic.Q = new Spell(SpellSlot.Q, 625f);

                MyLogic.W = new Spell(SpellSlot.W, 300f);

                MyLogic.E = new Spell(SpellSlot.E, 725f);

                MyLogic.R = new Spell(SpellSlot.R, 550f);
                MyLogic.R.SetCharged("KatarinaR", "KatarinaR", 550, 550, 1.0f);

                MyLogic.IgniteSlot = ObjectManager.Player.GetSpellSlot("summonerdot");

                if (MyLogic.IgniteSlot != SpellSlot.Unknown)
                {
                    MyLogic.Ignite = new Spell(MyLogic.IgniteSlot, 600);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MySpellManager.Initializer." + ex);
            }
        }
    }
}