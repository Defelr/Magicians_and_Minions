using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {

    public static DDOL instance = null;
    public int turn = 0;
    //Grid size
    int x = 8; //n == x board is n X n length
    public List<GameObject> loc;
    public void Start()
    {
        
    }
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    private List<List<GameObject>> PossibleSpaces(GameObject p)
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
    public List<List<GameObject>> PossibleSpacesPublic(GameObject p)
    {
        return PossibleSpaces(p);
    }
    public List<GameObject> Movement(GameObject p, GameObject child)
    {
        List<List<GameObject>> locations = PossibleSpaces(p);
        List<GameObject> spaces = new List<GameObject>();
        int current = 0;
        int.TryParse(child.name, out current);

        int range = 1;
        int row = Row(locations, child);
        int col = Col(locations, child);
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
        loc = spaces;
        return spaces;
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
