using UnityEngine;

public static class Statics {

    private static bool _playerAlive = true;
    public static bool PlayerAlive {
        get => _playerAlive;
        set {
            if (!value) {
                PlayerDied();
            }
            _playerAlive = value;
        }
    }

    public static ScoreController ScoreController;
    public static bool ComputerMode = false;

    public static CharacterType Gender = CharacterType.FEMALE;
    public static bool TerrainFirstIter = true;
    public static Terrain TerrainObj;
    public static bool GameIsPaused = false;
    public static bool GameSoundsEnabled = true;


    public static void ResetStatics()
    {
        //Important game statics
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


    private static void PlayerDied() {
        Debug.Log("Player Died");
    }
}