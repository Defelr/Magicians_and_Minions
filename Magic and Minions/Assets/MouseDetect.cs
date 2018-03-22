﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour
{
    public int HP;
    public int DMG;
    public int Mana;

    public int Movement_c; //AMOUNT OF TIMES THEY CAN MOVE
    public int Attack_c;  //AMOUNT OF TIMES THEY CAN ATTACK

    public int Moves;//Amount of times moved/attacked
    public int Attacks;



    // Use this for initialization
    void Start()
    {
        Moves = Movement_c;
        Attacks = Attack_c;
    }
    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void DamageHP(int DecHP)
    {
        HP -= DecHP; 
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (DDOL.instance.option == "attack")
            {
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if(DDOL.instance.TeamFinder(DDOL.instance.currentObject) != DDOL.instance.TeamFinder(this.gameObject))
                        {
                            this.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                            DDOL.instance.option = "";
                            return;
                        }
                    }
                }
            }
        }
    }
    public void Move()
    {
        Debug.Log("Start");
        List<GameObject> spaces = new List<GameObject>();
        DDOL.instance.option = "move";
        spaces = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (spaces.Count <= 0)
        {
            Debug.Log("Can't Move");
        }
        else
        {
            foreach (GameObject c in spaces)
            {
                Renderer R = c.GetComponent<Renderer>();
                R.enabled = true;
            }
        }
    }
    public void Attack()
    {
        Debug.Log("ATTACK STARTS");
        List<GameObject> spaces = new List<GameObject>();
        DDOL.instance.option = "attack";
        spaces = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (spaces.Count <= 0)
        {
            Debug.Log("Can't Attack Anyone");
            DDOL.instance.option = "";
        }
        else
        {
            foreach (GameObject c in spaces)
            {
                Renderer R = c.GetComponent<Renderer>();
                R.enabled = true;
            }
        }
    }
}
