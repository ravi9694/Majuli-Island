using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickarrow : MonoBehaviour
{
    public GameObject mainCamera;
    public void OnMouseDown()
    {
        // Find the Main Camera
        Debug.Log("clickeddddddd");
        // Check if the Main Camera has the MyFunctionScript attached
           mainCamera.GetComponent<SkyboxChanger>().Onclick();
       
    }
}
