using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
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
                            Debug.Log(DDOL.instance.Coords[i][j].location.gameObject.name);
                            Debug.Log(col.gameObject.name);
                            if (DDOL.instance.Coords[i][j].location.gameObject.name == col.gameObject.name)
                            {
                                Debug.Log("IN ID ADDER");
                                DDOL.instance.Coords[i][j] = DDOL.instance.SetObject(this.gameObject.GetInstanceID(), 1, this.gameObject, DDOL.instance.Coords[i][j].location);
                                Debug.Log(this.gameObject.GetInstanceID());
                                Debug.Log(DDOL.instance.Coords[i][j].ID);
                                return;
                            }
                        }
                    }
                }

            }
        }
    }
    public void On_Clicked()
    {
        Debug.Log("Start");
        List<GameObject> spaces = new List<GameObject>();
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
            DDOL.instance.option = "move";
        }
    }
}
