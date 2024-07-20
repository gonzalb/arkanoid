using UnityEngine;

public class PauseControl : MonoBehaviour{

    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused =! gameIsPaused;
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.musicON =! SoundManager.musicON;
        }
    }

    public static void PauseGame()
    {
        if (gameIsPaused)
        {
			//MessageScript.Message = "GAME PAUSED!";
            GameOverText.Message="  GAME PAUSED!";
            Time.timeScale = 0f;
        }
        else 
        {
			//MessageScript.Message="";
            GameOverText.Message="";
            Time.timeScale = 1;
        }
    }

	public static void PressPause()
	{
		gameIsPaused = true;
		PauseGame();
	}
}
