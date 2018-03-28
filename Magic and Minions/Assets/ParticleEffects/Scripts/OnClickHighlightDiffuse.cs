using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHighlightDiffuse : MonoBehaviour {

    public Shader shader1;
    public Shader shader2;
    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        // the following are defaults that keep overwriting the inputs.
        shader1 = Shader.Find("Standard");
        shader2 = Shader.Find("N3K/Outline");
    }

    void FixedUpdate()
    {     
        // Checks to see if the object is clicked on
        //  switches the shader between standard and highlighted (shader1 and shader2 respectively)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == this.name)
                {
                    if (rend.material.shader == shader1)
                        rend.material.shader = shader2;
                    else
                        rend.material.shader = shader1;
                }
            }


        }
    }
}