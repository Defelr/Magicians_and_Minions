/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnePCharSelect : MonoBehaviour {

    GameObject player1;

    public GameObject undoButton;
    public GameObject readyButton;
    public GameObject player1Panel;
    public GameObject StartGame;
    public GameObject errorPanel;
    public GameObject necromancer;
    public GameObject paladin;

    public Canvas SelectionScreen;
    public GameObject SelectionStart;
    public GameObject SelectionPaladin;
    public GameObject SelectionNecro;

    void Start()
    {
        SelectionScreen.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void PickNecro()
    {
        select1 = true;
        DDOL.instance.SetPlayer1(necromancer);
        num = 1;
        player1Panel.SetActive(false);
        undoButton.SetActive(true);
        Debug.Log("Player 1 is necromancer");
    }

    public void PickPald()
    {
        select1 = true;
        DDOL.instance.SetPlayer1(paladin);
        num = 1;
        player1Panel.SetActive(false);
        undoButton.SetActive(true);
        Debug.Log("Player 1 is Paladin");
    }

    public void Undo()
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
    IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(SelectionStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + SelectionStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);

    }
    public void Change()
    {
        if (num == 2)
        {
            SelectionStart.GetComponent<Animator>().SetTrigger("Begin");
            StartMatch();
            SelectionScreen.GetComponent<CanvasGroup>().alpha = 0;
            SelectionScreen.GetComponent<CanvasGroup>().interactable = false;
            Destroy(SelectionPaladin);
            Destroy(SelectionNecro);
        }
        else
        {
            errorPanel.SetActive(true);
        }
    }

    public void Error()
    {
        errorPanel.SetActive(false);
    }
}*/
