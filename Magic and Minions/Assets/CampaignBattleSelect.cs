using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignBattleSelect : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    public GameObject necromancer;
    public GameObject paladin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PvsKN()
    {
        DDOL.instance.SetPlayer1(paladin);
        DDOL.instance.SetPlayer1(necromancer);
        SceneManager.LoadScene(5);
    }
}
