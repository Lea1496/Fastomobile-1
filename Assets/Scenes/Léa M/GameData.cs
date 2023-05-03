
using System.Collections.Generic;

public static class GameData
{
    public static PlayerData P1 = new PlayerData() { Vie = 100 };
    public static PlayerData P2 = new PlayerData() { Vie = 100 };

    public static List<string> Ranks = new List<string>(12);

    public static List<Player> LesJoueurs = new List<Player>(12);

    
}
