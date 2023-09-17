using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesAdmin : MonoBehaviour
{

	//Score objects
	public Text scoreText;
	public float scoreAmount;
	public float pointIncreasePerSecond;
	private float i;
	public bool counter;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public GameObject GameOverMenu;
	
    public AudioSource audioSource;
	
	// Update is called once per frame
	void Start()
    {
		scoreAmount = 0f;
		pointIncreasePerSecond = 1f;
		counter = true;
    }
	
	// Update is called once per frame
    void Update()
    {
		i = 1f;
		scoreText.text = scoreAmount.ToString();
		
		if (counter == true)
		{
			i ++;
			scoreAmount += pointIncreasePerSecond * i;
		}
		else
		{
			Debug.Log("Couter has stopped");
		}
    }

    //Index changer (It only adds or substrac x value to the index scene)/////////////////////////////////////////////

    public void MainMenu()  {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void SampleScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
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

    //GameOver components///////////////////////////////////////////
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
    }

}
