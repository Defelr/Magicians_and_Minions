using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour {

    public GameObject MenuPnl;
    public GameObject MenuPnl2;
    public GameObject TipsPnl;

    public GameObject CharacterPnl;
    public GameObject EndTurnBtn;

    public AudioClip clickSound;
    private AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void OpenMenu()
    {
        source.PlayOneShot(clickSound, 1F);
        EndTurnBtn.SetActive(false);
        CharacterPnl.SetActive(false);
        MenuPnl.SetActive(true);
    }

    public void CloseMenu()
    {
        source.PlayOneShot(clickSound, 1F);
        MenuPnl.SetActive(false);
        CharacterPnl.SetActive(true);
        EndTurnBtn.SetActive(true);
    }

    public void OpenTips()
    {
        source.PlayOneShot(clickSound, 1F);
        MenuPnl2.SetActive(false);
        TipsPnl.SetActive(true);
    }

    public void CloseTips()
    {
        source.PlayOneShot(clickSound, 1F);
        TipsPnl.SetActive(false);
        MenuPnl2.SetActive(true);
    }

    public void Restart()
    {
        source.PlayOneShot(clickSound, 1F);
        SceneManager.LoadScene(2);
    }

    public void QuitToMain()
    {
        source.PlayOneShot(clickSound, 1F);
        SceneManager.LoadScene(0);
    }
}
