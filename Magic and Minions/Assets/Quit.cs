using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

	public void quit() {
        Application.Quit();
        Debug.Log("Quit");

    }

    public void restart()
    {
        SceneManager.LoadScene("Sprint1");
        Debug.Log("Restart");
    }
}
