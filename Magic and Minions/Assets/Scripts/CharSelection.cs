using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelection : MonoBehaviour {

    bool select1 = false;
    float num = 0;

    GameObject player1;
    GameObject player2;

    public GameObject undoButton;
    public GameObject readyButton;
    public GameObject player1Panel;
    public GameObject player2Panel;
    public GameObject errorPanel;
    public GameObject necromancer;
    public GameObject paladin;
	
	public void PickNecro () {
        if (select1 == false)
        {
            select1 = true;
            player1 = necromancer;
            num = 1;
            player1Panel.SetActive(false);
            player2Panel.SetActive(true);
            undoButton.SetActive(true);
            Debug.Log("Player 1 is necromancer");
        }
        else
        {
            player2 = necromancer;
            num = 2;
            player2Panel.SetActive(false);
            readyButton.SetActive(true);
            Debug.Log("Player 2 is necromancer");
        }
	}

    public void PickPald ()
    {
        if (select1 == false)
        {
            select1 = true;
            player1 = paladin;
            num = 1;
            player1Panel.SetActive(false);
            player2Panel.SetActive(true);
            undoButton.SetActive(true);
            Debug.Log("Player 1 is Paladin");
        }
        else
        {
            player2 = paladin;
            num = 2;
            player2Panel.SetActive(false);
            readyButton.SetActive(true);
            Debug.Log("Player 2 is Paladin");
        }
    }

    public void Undo ()
    {
        if (num == 1)
        {
            select1 = false;
            num = 0;
            player2Panel.SetActive(false);
            player1Panel.SetActive(true);
            undoButton.SetActive(false);
            Debug.Log("Undo Player 1");
        }
        if (num == 2)
        {
            num = 1;
            readyButton.SetActive(false);
            player2Panel.SetActive(true);
            Debug.Log("Undo Player 2");
        }
    }

    public void Change ()
    {
        if(num == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            errorPanel.SetActive(true);
        }
    }

    public void Error ()
    {
        errorPanel.SetActive(false);
    }
}
