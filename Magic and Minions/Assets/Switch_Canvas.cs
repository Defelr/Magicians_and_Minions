﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Canvas : MonoBehaviour {

    public GameObject necroCanvas;
    public GameObject minionCanvas;
    public GameObject wraithImage;
    public GameObject skelImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {

                if (hitInfo.transform.gameObject.tag == "Player")
                {
                    minionCanvas.SetActive(false);
                    necroCanvas.SetActive (true);
                    wraithImage.SetActive(false);
                    skelImage.SetActive(false);

                } 
                if (hitInfo.transform.gameObject.tag == "Wraith")
                {
                    necroCanvas.SetActive(false);
                    minionCanvas.SetActive(true);
                    skelImage.SetActive(false);
                   wraithImage.SetActive(true);
                }
                else if (hitInfo.transform.gameObject.tag == "Skeleton")
                {
                    necroCanvas.SetActive(false);
                    minionCanvas.SetActive(true);
                    wraithImage.SetActive(false);
                    skelImage.SetActive(true);
                }
           } else
           {
                //minionCanvas.SetActive(false);
               // necroCanvas.SetActive(false);
                //skelImage.SetActive(false);
                //wraithImage.SetActive(false);
            }
       }
    }
}
