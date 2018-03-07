using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {
    public Camera First;
    public Camera Second;

	public void Quit1() {
        //Application.Quit();
        //Debug.Log("Quit");
        SceneManager.LoadScene(0);

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Restart");
    }

    public void End_Turn()
    {
        if(DDOL.instance.turn % 2 == 0)
        {
            Second.enabled = true;
            First.enabled = false;
            DDOL.instance.currentCamera = Second;
        }
        else
        {
            Second.enabled = false;
            First.enabled = true;
            DDOL.instance.currentCamera = First;
        }
        DDOL.instance.turn++;
        
    }
}
