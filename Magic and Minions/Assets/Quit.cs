using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {
	public void Quit1() {
        //Application.Quit();
        //Debug.Log("Quit");
        SceneManager.LoadScene(0);

    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Restart");
    }

}
