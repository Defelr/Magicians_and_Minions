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

    //player paladin vs killer ai necr
    public void PvsKN()
    {
        SceneManager.LoadScene(5);
    }

    //player necromancer vs killer ai necr
    public void NvsKN()
    {
        SceneManager.LoadScene(6);
    }

    //player paladin vs survival ai necr
    public void PvsSN()
    {
        SceneManager.LoadScene(7);
    }

    //player necromancer vs survival ai necr
    public void NvsSN()
    {
        SceneManager.LoadScene(8);
    }

    //player paladin vs minion ai necr
    public void PvsMN()
    {
        SceneManager.LoadScene(9);
    }

    //player necromancer vs minion ai necr
    public void NvsMN()
    {
        SceneManager.LoadScene(10);
    }

    //player paladin vs killer ai pal
    public void PvsKP()
    {
        SceneManager.LoadScene(11);
    }

    //player necromancer vs killer ai pal
    public void NvsKP()
    {
        SceneManager.LoadScene(12);
    }

    //player paladin vs survival ai pal
    public void PvsSP()
    {
        SceneManager.LoadScene(13);
    }

    //player necromancer vs survival ai pal
    public void NvsSP()
    {
        SceneManager.LoadScene(14);
    }

    //player paladin vs minion ai pal
    public void PvsMP()
    {
        SceneManager.LoadScene(15);
    }

    //player necromancer vs minion ai pal
    public void NvsMP()
    {
        SceneManager.LoadScene(16);
    }
}
