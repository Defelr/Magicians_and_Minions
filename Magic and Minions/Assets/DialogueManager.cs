using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Sprite N;
    public Sprite P;
    public GameObject P1;
    public GameObject P2;

    public GameObject Pl1;
    public GameObject Pl2;

    private Text T1;
    private Text T2;
	// Use this for initialization
	void Start () {
        T1 = Pl1.GetComponent<Text>();
        T2 = Pl2.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (DDOL.instance.StartingC.tag == "Necro")
        {
            P1.GetComponent<Image>().sprite = N;
        }else if (DDOL.instance.StartingC.tag == "Paladin")
        {
            P1.GetComponent<Image>().sprite = P;
        }

        if(DDOL.instance.StartingC2.tag == "Necro")
        {
            P2.GetComponent<Image>().sprite = N;
        }else if (DDOL.instance.StartingC2.tag == "Paladin")
        {
            P2.GetComponent<Image>().sprite = P;
        }

    }
    public void Advantage()
    {

    }
    public void Disadvantage()
    {

    }
    public void WinLose()
    {

    }
    public void StartGame()
    {
        if(P1.GetComponent<Image>().sprite == N)
        {

        }
        else if(P1.GetComponent<Image>().sprite == P)
        {

        }

        if (P2.GetComponent<Image>().sprite == N)
        {

        }
        else if (P2.GetComponent<Image>().sprite == P)
        {

        }
    }
}
