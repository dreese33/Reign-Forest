using UnityEngine;

public static class Statics {

    private static bool _playerAlive = true;
    public static bool PlayerAlive {
        get => _playerAlive;
        set {
            _playerAlive = value;
            if (!value) {
                PlayerDied();
            }
        }
    }

    public static DeathController DeathController;
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

        TerrainObj = null;
        ScoreController = null;
        DeathController = null;

        ComputerMode = false;
        Gender = CharacterType.FEMALE;
        TerrainFirstIter = true;
        GameIsPaused = false;
        GameSoundsEnabled = true;  //Reload from stored preferences
        PlayerAlive = true;
    }


    private static void PlayerDied() {
        DeathController.PlayerDied();
    }
}