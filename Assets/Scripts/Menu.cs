using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;
    public void pauseMenu()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void home()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1;

    }
    public void resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void stopBackgroundMusic()
    {

    }
    public void stopSFX()
    {

    }

}
