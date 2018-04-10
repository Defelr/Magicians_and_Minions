using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBars : MonoBehaviour {

    GameObject healthBarPanel;

    public Text health;

    public static int pieceHP;

    string pieceHPTxt;

    private void Start()
    {
        healthBarPanel = GameObject.Find("FloatingHealth_Pnl");
    }

    private void OnMouseEnter()
    {
        healthBarPanel.SetActive(true);
        pieceHPTxt = pieceHP.ToString();
        health.text = pieceHPTxt;
    }

    private void OnMouseExit()
    {
        healthBarPanel.SetActive(false);
    }

}
