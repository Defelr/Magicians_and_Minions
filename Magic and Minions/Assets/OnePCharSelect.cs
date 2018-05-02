using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnePCharSelect : MonoBehaviour {

    GameObject player1;
    bool necr;

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
        //DDOL.instance.SetPlayer1(necromancer);
        player1Panel.SetActive(false);
        readyButton.SetActive(true);
        Debug.Log("Player 1 is necromancer");
        necr = true;
    }

    public void PickPald()
    {
        //DDOL.instance.SetPlayer1(paladin);
        player1Panel.SetActive(false);
        readyButton.SetActive(true);
        Debug.Log("Player 1 is Paladin");
        necr = false;
    }

    public void Ready()
    {
        if (necr)
        {
            Debug.Log("Necromancer map");
            SceneManager.LoadScene(4);
        } else
        {
            Debug.Log("Paladin map");
            SceneManager.LoadScene(3);
        }
    }

    IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(SelectionStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + SelectionStart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);

    }

    public void PvsN()
    {
        DDOL.instance.SetPlayer1(paladin);
        DDOL.instance.SetPlayer2(necromancer);
        SelectionStart.GetComponent<Animator>().SetTrigger("Begin");
        StartMatch();
        SelectionScreen.GetComponent<CanvasGroup>().alpha = 0;
        SelectionScreen.GetComponent<CanvasGroup>().interactable = false;
    }
    

    public void Error()
    {
        errorPanel.SetActive(false);
    }
}
