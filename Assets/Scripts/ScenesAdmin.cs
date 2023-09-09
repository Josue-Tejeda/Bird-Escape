using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesAdmin : MonoBehaviour
{

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public GameObject GameOverMenu;

    public AudioSource audioSource;

    //Index changer (It only adds or substrac x value to the index scene)/////////////////////////////////////////////

    public void PlusI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlusII()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlusIII()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void PlusIV()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void BackI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackII()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void BackIII()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void BackIV()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    //Pause, quit, continue and restart mechanics/////////////////////////////////////////////
    public void Pause()
    {
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

    //GameOver components/////////////////////////////////////////////
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

    public void Report()
    {
        float time = Time.timeScale;
        Debug.Log("Time scale is: " + time);
    }

}
