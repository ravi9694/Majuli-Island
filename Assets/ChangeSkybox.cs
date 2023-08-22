using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = Resources.Load("DK") as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
