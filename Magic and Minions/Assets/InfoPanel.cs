using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoPanel : MonoBehaviour {

    public GameObject interfacePnl;
    public GameObject infoPnl;
    public GameObject NecroTxt;
    public GameObject PalTxt;
    public Text Class;

    bool active = false;
	
	public void SetPanel () {
		if (active == false)
        {
            //interfacePnl.SetActive(false);
            infoPnl.SetActive(true);
            active = true;
            if(DDOL.instance.player == 0)
            {
                if(DDOL.instance.StartingC.gameObject.tag == "Necro")
                {
                    NecroTxt.SetActive(true);
                    PalTxt.SetActive(false);
                    Class.text = "Necromancer";
                }else if (DDOL.instance.StartingC.gameObject.tag == "Paladin")
                {
                    NecroTxt.SetActive(false);
                    PalTxt.SetActive(true);
                    Class.text = "Paladin";
                }
            }
            else
            {
                if (DDOL.instance.StartingC2.gameObject.tag == "Necro")
                {
                    NecroTxt.SetActive(true);
                    PalTxt.SetActive(false);
                    Class.text = "Necromancer";
                }
                else if (DDOL.instance.StartingC2.gameObject.tag == "Paladin")
                {
                    NecroTxt.SetActive(false);
                    PalTxt.SetActive(true);
                    Class.text = "Paladin";
                }
            }
        }
        else if (active == true)
        {
            infoPnl.SetActive(false);
            //interfacePnl.SetActive(true);
            active = false;
            NecroTxt.SetActive(false);
            PalTxt.SetActive(false);
        }
	}
}
