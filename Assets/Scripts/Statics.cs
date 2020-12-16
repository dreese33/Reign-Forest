using UnityEngine;

public static class Statics {
    public static bool PlayerAlive = true;
    public static ScoreController ScoreController;
    public static bool ComputerMode = false;

    public static CharacterType Gender = CharacterType.FEMALE;
    public static bool TerrainFirstIter = true;
    public static Terrain TerrainObj;
    public static bool GameIsPaused = false;
    public static bool GameSoundsEnabled = true;


    public static void ResetStatics()
    {
        Time.timeScale = 1;
        PlayerAlive = true;
        ScoreController = null;
        ComputerMode = false;
        Gender = CharacterType.FEMALE;
        TerrainFirstIter = true;
        TerrainObj = null;    //This one may cause errors
        GameIsPaused = false;
        GameSoundsEnabled = true;
    }
}