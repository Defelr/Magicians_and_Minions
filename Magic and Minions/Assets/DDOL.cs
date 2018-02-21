using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {

    public static DDOL instance = null;
    public int turn = 0;
    //Grid size
    int x = 8; //n == x board is n X n length
    public List<List<GameObject>> locations;
    public List<GameObject> spaces;
    public GameObject currentObject; //The player whose action is being taken
    public GameObject currentObjectL;//The location of that player
    public GameObject currentTarget;
    public void Start()
    {
        locations = PossibleSpaces(GameObject.Find("Grid_Board"));
    }
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    public void MouseDown()
    {
        if(currentObject != null)
        {
            ClearSpaces();
        }
    }
    public void ClearSpaces()
    {
        foreach (GameObject c in spaces)
        {
            // Debug.Log(c.gameObject.name);
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = false;
        }
    }
    public List<List<GameObject>> PossibleSpaces(GameObject p)
    {
        Transform[] children = p.GetComponentsInChildren<Transform>();
        List<List<GameObject>> locations = new List<List<GameObject>>();
        for (int i = 0; i < x; i++)
        {
            locations.Add(new List<GameObject>());
        }
        int k = 1;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                locations[i].Add(children[k].gameObject);
                k++;
            }
        }
        return locations;
    }
    //MOVEMENT
    public List<GameObject> Movement(GameObject child)
    {
        spaces = new List<GameObject>();
 
        int range = 1;
        int row = Row(locations, child);
        int col = Col(locations, child);

        currentObjectL = locations[row][col];

        Debug.Log(row);
        Debug.Log(col);
        int n_row = row - range; // 1 == range of ability or movement this is just a test value ATM
        int n_col = col - range;
        int o_range = 3 + (2 * (range - 1));
        //Testing 1 position movement

        for(int i = 0; i < o_range; i++)
        {
            for(int j = 0; j < o_range; j++)
            {
                if ((n_row >= 0 && n_row < 8) && (n_col >= 0 && n_col < 8))
                {
                    Collider coll = locations[n_row][n_col].GetComponent<Collider>();
                    if (!coll.isTrigger)
                        spaces.Add(locations[n_row][n_col]);
                }
                n_col++;
            }
            n_row++;
            n_col = col - range;
        }
        foreach(GameObject c in spaces)
        {
            Debug.Log(c.name);
        }
        return spaces;
    }
    public void MoveCharacter(Transform new_p)
    {
        //Find the collider of the intial position and set it to false
        float timeLerped = 0.0f;
        Transform startPos = currentObject.transform;
        ClearSpaces();
        while (timeLerped < 1.0)
        {
            timeLerped += Time.deltaTime;
            currentObject.transform.position = Vector3.MoveTowards(startPos.position, new Vector3(new_p.transform.position.x, new_p.transform.position.y - 0.33f, new_p.transform.position.z), timeLerped);

        }
        Collider col = currentObjectL.GetComponent<Collider>();
        col.isTrigger = false;
        currentObject = null;
        spaces.Clear();
    }
    private int Row(List<List<GameObject>> l, GameObject c)
    {
       for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < x; j++)
            {
                if(l[i][j].name == c.name)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    private int Col(List<List<GameObject>> l, GameObject c)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (l[i][j].name == c.name)
                {
                    return j;
                }
            }
        }
        return -1;
    }
}
