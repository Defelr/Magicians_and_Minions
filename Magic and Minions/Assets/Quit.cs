using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {
    public GameObject P1;
    public GameObject P2;
    public GameObject Again;
    public GameObject Main;

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

    public void ResetWC()
    {
        P1.SetActive(false);
        P2.SetActive(false);
        Again.SetActive(false);
        Main.SetActive(false);
    }
}
