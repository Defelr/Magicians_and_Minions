using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotseatWin : MonoBehaviour {

    public GameObject winPnl;
    public GameObject player1Pnl;
    public GameObject player2Pnl;

    public GameObject interfacePnl;

    public static int winVar = 0;

    public void Update()
    {
        if (winVar == 1)
        {
            interfacePnl.SetActive(false);
            winPnl.SetActive(true);
            player1Pnl.SetActive(true);
        }
        else if (winVar == 2)
        {
            interfacePnl.SetActive(false);
            winPnl.SetActive(true);
            player2Pnl.SetActive(true);
        }
        DDOL.instance.Dialogue.GetComponent<DialogueManager>().WinLose(winVar);
    }
}
