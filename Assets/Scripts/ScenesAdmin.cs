using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesAdmin : MonoBehaviour
{

	//Score objects
	public Text scoreText;
	public Text FinalScoreText;
	public Text HigherScoreText;
	public float scoreAmount;
	public float pointIncreasePerSecond;
	public bool counter;
	
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public GameObject GameOverMenu;
	[SerializeField] public GameObject Score;
	
    public AudioSource audioSource;
	
	void Start()
    {
		scoreAmount = 0f;
		pointIncreasePerSecond = 1f;
		counter = true;

        try
        {
            HigherScoreText.text = PlayerPrefs.GetFloat("highCounter", 0).ToString();
        }
        catch (Exception e)
        {
            return;
        }

        //ResetHighScore();
		 
    }
	
	// Update is called once per frame
    void Update()
    {
		////////////////////Counter starts when duck is flying/////////////
		duck_movement instanceDuck = FindObjectOfType<duck_movement>();
		
		if (instanceDuck != null) {
			
			if (instanceDuck.startFlying == true)
			{
				Counter();
			}
			else 
			{
				Debug.Log("Mani el pajaro no se esta moviendo");
			}
		///////////////////////////////////////////////////////////////
		
		}
		
    }

    //Index changer (It only adds or substrac x value to the index scene)/////////////////////////////////////////////

    public void MainMenu()  {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void GameScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    public void OptionsMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsMenu");
    }
    public void CreditsMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CreditsMenu");
    }

    //Pause, quit, continue and restart mechanics/////////////////////////////////////////////
    public void Pause()
    {
		counter = false;
        Debug.Log("Pausing.... timeScale set to 0f");
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
		Score.SetActive(false);


        if (audioSource != null)
        {
            Debug.Log("Pausing AudioSource");
            audioSource.Pause();
        }
        else
        {
            Debug.LogError("AudioSource is not assigned. Please assign it in the Inspector.");
        }
    }

    public void Continue()
    {
		counter = true;
        Debug.Log("continuing.... timeScale set to 1f");
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
		Score.SetActive(true);

        if (audioSource != null)
        {
            Debug.Log("Unpausing AudioSource");
            audioSource.UnPause();
        }
        else
        {
            Debug.LogError("AudioSource is not assigned. Please assign it in the Inspector.");
        }
    }

    public void Restart()
    {
        Debug.Log("Restarting.... timeScale set to 1f");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Closing this cotopla");
        Time.timeScale = 1f;
        Application.Quit();
    }

    //GameOver components////////////////////////////////////////////
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(4f);
        GameOver();
    }

    public void GameOver()
    {
        Debug.Log("Mf is dead");
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        GameOverMenu.SetActive(true);
		Score.SetActive(false);
		
		//Converts the scoreAmount to a TimeSpan
		TimeSpan timeSpan = TimeSpan.FromSeconds(scoreAmount);
			
		//Format the TimeSpan as HH:mm:ss
		string formattedTime = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		FinalScoreText.text = formattedTime;
		
		HighCounter();

    }
	
	public void Counter()
	{
		
		if (counter == true)
		{
			scoreAmount += pointIncreasePerSecond * Time.deltaTime;
			
			//Converts the scoreAmount to a TimeSpan
			TimeSpan timeSpan = TimeSpan.FromSeconds(scoreAmount);
			
			//Format the TimeSpan as HH:mm:ss
			string formattedTime = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			scoreText.text = formattedTime;
		}
		else
		{
			Debug.Log("Couter has stopped");
		}
	}
	
	public void HighCounter()
	{
		if (scoreAmount > PlayerPrefs.GetFloat("highCounter", 0))
		{
			PlayerPrefs.SetFloat("highCounter", scoreAmount);
			
			//Converts the scoreAmount to a TimeSpan
			TimeSpan timeSpan = TimeSpan.FromSeconds(scoreAmount);
			
			//Format the TimeSpan as HH:mm:ss
			string formattedTime = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			HigherScoreText.text = formattedTime;
			
			Debug.Log("HighScore saved :D");
		}
		else
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("highCounter", 0));
			string formattedTime = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			HigherScoreText.text = formattedTime;
			Debug.Log("Did not reach a high score");
		}
	}
	
	//Tool to reset higher score
	public void ResetHighScore()
	{
		//Reset the high score by deleting the PlayerPrefs key
		PlayerPrefs.DeleteKey("highCounter");
		PlayerPrefs.Save();

		//Update the UI text to show that the high score has been reset
		HigherScoreText.text = "High Score: Not Set, it has been deleted";
	}
	
}
