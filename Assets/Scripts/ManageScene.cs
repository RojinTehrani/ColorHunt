using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageScene : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioSource backgroundSound;
    public AudioClip clickSoundClip;
    public void changeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void playClickSound()
    {
        backgroundSound.Stop();
        clickSound.PlayOneShot(clickSoundClip);
    }
    public void gameQuit()
    {
        Application.Quit();
    }

    public void settingPanel()
    {
        //load main menu scene
        SceneManager.LoadSceneAsync(2);

    }
    public void closeWindow()
    {
        //close
        
        SceneManager.LoadSceneAsync(0);

    }
    public void playMusic()
    {
      
        backgroundSound.Play();

    }
    public void stopMusic()
    {
        backgroundSound.Stop();

    }
}
