using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Singleplayer()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Signleplayer");
    }

    public void Multiplayer()
    {
        //SceneManager.LoadScene(3);
    }
}
