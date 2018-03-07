using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject howPanel;
    public GameObject creditsPanel;

    public AudioClip clickSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ShowHow () {
        source.PlayOneShot(clickSound, 1F);
        mainPanel.SetActive(false);
        howPanel.SetActive(true);
	}

    public void ShowCredits ()
    {
        source.PlayOneShot(clickSound, 1F);
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void HideAll ()
    {
        source.PlayOneShot(clickSound, 1F);
        howPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
