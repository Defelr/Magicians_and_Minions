using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour
{
    public int HP;
    public int DMG;
    // Use this for initialization
    void Start()
    {

    }
    private 
    // Update is called once per frame
    void Update()
    {
        //NEEDS MORE CLEAN UP, AS IN CLEARING THE ARRAY LOCATION FOR WHEN IT'S DESTROYED
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
                if (DDOL.instance.currentObject != this && DDOL.instance.currentObject.gameObject.layer != this.gameObject.layer)
                {
                    this.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                }
            }
            else
            {
                GameObject grid_B = GameObject.Find("Grid_Board");
                DDOL.instance.ClearSpaces();
                foreach (Transform child in grid_B.transform)
                {
                    Collider col = child.GetComponent<Collider>();
                    Collider thiscol = this.GetComponent<Collider>();
                    if (col.bounds.Intersects(thiscol.bounds))
                    {
                        for (int i = 0; i < DDOL.instance.x; i++)
                        {
                            for (int j = 0; j < DDOL.instance.x; j++)
                            {
                                Debug.Log("Mouse Down");
                                if (DDOL.instance.Coords[i][j].location.gameObject.name == col.gameObject.name)
                                {
                                    Debug.Log("IN ID ADDER");
                                    DDOL.instance.Coords[i][j] = DDOL.instance.SetObject(this.gameObject.GetInstanceID(), 1, DDOL.instance.Coords[i][j].D, this.gameObject, DDOL.instance.Coords[i][j].location);
                                    return;
                                }
                            }
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
}
