//Not needed, using Event Triggers to activate and deactivate the tooltips


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour
{

    public GameObject toolTipPanel;
    private Image tipImage;

    public GameObject toolTipText;
    private Text tipText;


    private Color panelC;
    private Color textC;

    private int on = 255;
    private int off = 0;

    private void Start()
    {
        tipImage = toolTipPanel.GetComponent<Image>();
        tipText = toolTipText.GetComponent<Text>();

        panelC = tipImage.color;
        textC = tipText.color;
        panelC.a = off;
        textC.a = off;
    }

    public void Turnon()
    {
        panelC.a = on;
        textC.a = on;
        tipImage.color = panelC;
        tipText.color = textC;
    }

    public void Turnoff()
    {
        panelC.a = off;
        textC.a = off;
        tipImage.color = panelC;
        tipText.color = textC;
    }
}
