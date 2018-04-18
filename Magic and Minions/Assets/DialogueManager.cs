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

    public GameObject Both;

    public Canvas Game;
    private Text T1;
    private Text T2;
	// Use this for initialization
	void Start () {
        T1 = Pl1.GetComponent<Text>();
        T2 = Pl2.GetComponent<Text>();
        Game.GetComponent<CanvasGroup>().alpha = 0;
        Game.GetComponent<CanvasGroup>().interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (DDOL.instance.StartingC)
        {
            if (DDOL.instance.StartingC.tag == "Necro")
            {
                P1.GetComponent<Image>().sprite = N;
            }
            else if (DDOL.instance.StartingC.tag == "Paladin")
            {
                P1.GetComponent<Image>().sprite = P;
            }
        }
        if (DDOL.instance.StartingC2)
        {
            if (DDOL.instance.StartingC2.tag == "Necro")
            {
                P2.GetComponent<Image>().sprite = N;
            }
            else if (DDOL.instance.StartingC2.tag == "Paladin")
            {
                P2.GetComponent<Image>().sprite = P;
            }
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
       
        if (P1.GetComponent<Image>().sprite == N && P2.GetComponent<Image>().sprite == N)
        {
            T1.text = "Play by the rules, shall we?\nI hope you’ll provide good practice.";
            T2.text = "Play by the rules, shall we?\nI hope you’ll provide good practice.";
        }
        if(P1.GetComponent<Image>().sprite == P && P2.GetComponent<Image>().sprite == N)
        {
            T1.text = "I’ll see to it that you’re defeated.\nMy light shall smother your darkness!";
            T2.text = "Justice shall be served...on a silver platter\nYou shall make a fine corpse to feed on.";
        }

        if (P2.GetComponent<Image>().sprite == P && P1.GetComponent<Image>().sprite == N)
        {
            T2.text = "I’ll see to it that you’re defeated.\nMy light shall smother your darkness!";
            T1.text = "Justice shall be served...on a silver platter\nYou shall make a fine corpse to feed on.";
        }
        if(P1.GetComponent<Image>().sprite == P && P2.GetComponent<Image>().sprite == P)
        {
            T1.text = "May the light be one with me!\nLet us see whose star shines brighter.";
            T2.text = "May the light be one with me!\nLet us see whose star shines brighter.";
        }
        Game.GetComponent<CanvasGroup>().alpha = 1;
        Game.GetComponent<CanvasGroup>().interactable = true;
    }
    public void ClearBoth()
    {
        Both.SetActive(false);
    }
    public void ShowBoth()
    {
        Both.SetActive(true);
    }
}
