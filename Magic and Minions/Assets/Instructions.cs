using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject howPanel;
    public GameObject creditsPanel;

	
	public void ShowHow () {
        mainPanel.SetActive(false);
        howPanel.SetActive(true);
	}

    public void ShowCredits ()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void HideAll ()
    {
        howPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
