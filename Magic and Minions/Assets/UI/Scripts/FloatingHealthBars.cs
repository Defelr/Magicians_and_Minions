using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBars : MonoBehaviour
{

    private GameObject healthBarPanel;
    private Image panelImage;

    private GameObject healthTxt;
    private Text healthT;
    private GameObject healthNum;
    private Text numText;

    private Color panelC;
    private Color textC;

    public string pieceHPTxt;

    private void Start()
    {
        healthBarPanel = GameObject.Find("FloatingHealth_Pnl");
        healthTxt = GameObject.Find("Health_Txt");
        healthNum = GameObject.Find("HealthNumber");
        panelImage = healthBarPanel.GetComponent<Image>();
        healthT = healthTxt.GetComponent<Text>();
        numText = healthNum.GetComponent<Text>();

        panelC = panelImage.color;
        textC = healthT.color;
        panelC.a = 0;
        textC.a = 0;
        panelImage.color = panelC;
        healthT.color = textC;
        numText.color = textC;
    }

    private void OnMouseOver()
    {
        panelC.a = 255;
        textC.a = 255;
        panelImage.color = panelC;
        healthT.color = textC;
        numText.color = textC;

        pieceHPTxt = this.GetComponent<MouseDetect>().HP.ToString();
        numText.text = pieceHPTxt;
    }

    private void OnMouseExit()
    {
        panelC.a = 0;
        textC.a = 0;
        panelImage.color = panelC;
        healthT.color = textC;
        numText.color = textC;
    }

}
