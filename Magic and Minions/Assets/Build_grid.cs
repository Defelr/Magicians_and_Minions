using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_grid : MonoBehaviour {
    public int board_size_x_;
    public int board_size_z_;
    public Transform tile_prefab_;
    // Use this for initialization
    void Start()
    {
        GameObject board = new GameObject();
        board.name = "Board";
        int count = 0;
        for (int x = 0; x < board_size_x_; x++)
        {
            for (int z = 0; z < board_size_z_; z++)
            {
                Transform tile = (Transform)Instantiate(tile_prefab_, new Vector3(x+2, 0, z+2), Quaternion.identity);
                tile.name = "Tile " + count;
                tile.parent = board.transform;
                count++;
            }
        }
    }
}
