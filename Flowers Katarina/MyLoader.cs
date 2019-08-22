namespace Flowers_Katarina
{
    #region 

    using EnsoulSharp;
    using EnsoulSharp.SDK;

    #endregion

    // port my script xD
    // all logic same with my Katarina (Aimkek version)
    // i dont have any update or do some change
    // just easy port to ensoulsharp
    public class MyLoader
    {
        public static void Main(string[] args)
        {
            GameEvent.OnGameLoad += () =>
            {
                if (ObjectManager.Player.CharacterName != "Katarina")
                {
                    return;
                }

                new MyBase.MyChampions();
            };
        }
    }
}
