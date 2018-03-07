using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Singleplayer()
    {
        SceneManager.LoadScene("CampaignMap_Test");
        Debug.Log("Signleplayer");
    }

    public void Multiplayer()
    {
        //SceneManager.LoadScene();
    }
}
