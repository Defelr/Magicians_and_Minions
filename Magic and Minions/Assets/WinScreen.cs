using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {
    public GameObject P1;
    public GameObject P2;
    public GameObject winPnl;

    public void ResetWC()
    {
        HotseatWin.winVar = 0;
        winPnl.SetActive(false);
        SceneManager.LoadScene(2);
        //P1.SetActive(false);
        //P2.SetActive(false);
    }
}
