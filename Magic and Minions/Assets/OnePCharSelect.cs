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
        DDOL.instance.SetPlayer1(necromancer);
        player1Panel.SetActive(false);
        readyButton.SetActive(true);
        Debug.Log("Player 1 is necromancer");
        necr = true;
    }

    public void PickPald()
    {
        DDOL.instance.SetPlayer1(paladin);
        player1Panel.SetActive(false);
        readyButton.SetActive(true);
        Debug.Log("Player 1 is Paladin");
        necr = false;
    }

    public void Ready()
    {
        if (necr)
        {
            SceneManager.LoadScene(4);
        } else
        {
            SceneManager.LoadScene(3);
        }
    }
    

    public void Error()
    {
        errorPanel.SetActive(false);
    }
}
