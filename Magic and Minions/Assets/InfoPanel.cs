using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {

    public GameObject interfacePnl;
    public GameObject infoPnl;
    public GameObject NecroTxt;
    public GameObject PalTxt;

    bool active = false;
	
	public void SetPanel () {
		if (active == false)
        {
            //interfacePnl.SetActive(false);
            infoPnl.SetActive(true);
            active = true;
        }
        else if (active == true)
        {
            infoPnl.SetActive(false);
            //interfacePnl.SetActive(true);
            active = false;
        }
	}
}
