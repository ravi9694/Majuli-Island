using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Newtonsoft.Json;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxMaterial; // the Skybox material to modify
    public string JsonFileName;
    public string ResourceFolderName;
    public Texture[] cubemapTextures; // an array of cubemap images
    [SerializeField] private Transform cameraTransform; // Reference to the main camera transform
    [SerializeField] private GameObject Arrow; // The prefab to show when the camera is rotated within the specified range
    public int imageno=1;
    public string pref;
    public Dictionary<string, Dictionary<string, string>> photos;
    private bool isPrefabShown = false;
    public int currentCubemapIndex = 0;
    public float distanceFromCamera = 10f;
    float angle;

    void changecubemap(string currentLocation){
        (string, int) tuple = parser(currentLocation);
        for (int i = 0; i < cubemapTextures.Length; i++)
        {
            string textureName = cubemapTextures[i].name;
            if(textureName == tuple.Item1+tuple.Item2.ToString()){
                skyboxMaterial.SetTexture("_Tex", cubemapTextures[i]);
                imageno = tuple.Item2;
                return;
            }
        }

    }
    static string direction(double angle)
    {
        if (angle < 22.5 || angle >= 337.5) return "0";
        if (angle < 67.5 && angle >= 22.5) return "45";
        if (angle < 112.5 && angle >= 67.5) return "90";
        if (angle < 157.5 && angle >= 112.5) return "135";
        if (angle < 202.5 && angle >= 157.5) return "180";
        if (angle < 247.5 && angle >= 202.5) return "225";
        if (angle < 292.5 && angle >= 247.5) return "270";
        return "315";
    }
    static (string, int) parser(string str )
     {
        string text="";
        string number="";
        foreach (char c in str)
        {   if(c == '.')
                break;
            if (!(c >= '0' && c <= '9'))
            {
                text += c;
            }
            else
            { 
                number += c;
            }

        }


        int numb = int.Parse(number);
        (string, int) tuple = (text, numb);
        return tuple;
    }
     string currentimage()
    {
        return pref+ imageno.ToString() + ".JPG";
    }

    static string nextLocation(Dictionary<string, Dictionary<string, string>> imageDirections, string currentLocation, double angle){
        string dir = direction(angle);
        if (imageDirections.ContainsKey(currentLocation)){
            if (imageDirections[currentLocation].ContainsKey(dir))
            {
                return imageDirections[currentLocation][dir];
            }
        }
        return "null";
    }
    void Start()
    {
        // string[] filePaths = Directory.GetFiles(Application.dataPath + "/Resources/" + folderName, "*.jpg");
        // Debug.Log(filePaths.Length);
        // skyboxTextures = new Texture2D[filePaths.Length];
        // for (int i = 0; i < filePaths.Length; i++)
        // {
        //     byte[] bytes = File.ReadAllBytes(filePaths[i]);
        //     Texture2D texture = new Texture2D(1980, 1080);
        //     texture.LoadImage(bytes);
        //     skyboxTextures[i] = texture;
        // }

        // if (skyboxTextures.Length == 0)
        // {
        //     Debug.LogError("No textures found in folder. Make sure the folder name is correct and the textures are in the correct format.");
        // } else {
            // tex=skyboxTextures[0];
            // cubeMap = new Cubemap(tex.width, TextureFormat.RGB24, false);
            // cubeMap.SetPixel(CubemapFace.PositiveX, 0, 0, tex.GetPixel(0, 0));
            // cubeMap.SetPixel(CubemapFace.NegativeX, 0, 0, tex.GetPixel(tex.width - 1, 0));
            // cubeMap.SetPixel(CubemapFace.PositiveY, 0, 0, tex.GetPixel(0, tex.width - 1));
            // cubeMap.SetPixel(CubemapFace.NegativeY, 0, 0, tex.GetPixel(0, 0));
            // cubeMap.SetPixel(CubemapFace.PositiveZ, 0, 0, tex.GetPixel(0, 0));
            // cubeMap.SetPixel(CubemapFace.NegativeZ, 0, 0, tex.GetPixel(tex.width - 1, tex.width - 1));
            // cubeMap.Apply(false);
            // skyboxMaterial.SetTexture("_Tex", cubeMap);
        //     skyboxMaterial.SetTexture("_Tex", skyboxTextures[0]);
        // }
        cubemapTextures = Resources.LoadAll<Texture>(ResourceFolderName);
        RenderSettings.skybox = skyboxMaterial;
        Arrow.SetActive(false);
        string jsonText = System.IO.File.ReadAllText(Application.dataPath+"/JSON_Files/"+JsonFileName);
        photos = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonText);
        (string, int) tuple = parser(photos["start"]["0"]);
       // imageno = tuple.Item2;
        pref = tuple.Item1;
        imageno = tuple.Item2;
        changecubemap(currentimage());
    }
    
    // Update is called once per frame
    public void Onclick()
    {
        Debug.Log("Clicked ");
        if (isPrefabShown)
        {   
            (string, int) tuple = parser(nextLocation(photos, currentimage(), angle));
            pref = tuple.Item1;
            imageno = tuple.Item2;
            changecubemap(currentimage());
        }

    }
    // Update is called once per frame
    void Update()
    {
        // check if the Space key is pressed
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     // increment the current cubemap index
        //     currentCubemapIndex = (currentCubemapIndex + 1) % skyboxTextures.Length;

        //     // set the new cubemap image
        //     // tex=skyboxTextures[currentCubemapIndex];
        //     // cubeMap = new Cubemap(tex.width, TextureFormat.RGB24, false);
        //     // cubeMap.SetPixel(CubemapFace.PositiveX, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.NegativeX, 0, 0, tex.GetPixel(tex.width - 1, 0));
        //     // cubeMap.SetPixel(CubemapFace.PositiveY, 0, 0, tex.GetPixel(0, tex.width - 1));
        //     // cubeMap.SetPixel(CubemapFace.NegativeY, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.PositiveZ, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.NegativeZ, 0, 0, tex.GetPixel(tex.width - 1, tex.width - 1));
        //     // cubeMap.Apply(false);
        //     // skyboxMaterial.SetTexture("_Tex", cubeMap);
        //     skyboxMaterial.SetTexture("_Tex", skyboxTextures[currentCubemapIndex]);
        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     // increment the current cubemap index
        //     currentCubemapIndex = (currentCubemapIndex - 1 + skyboxTextures.Length) % skyboxTextures.Length;

        //     // set the new cubemap image
        //     // tex=skyboxTextures[currentCubemapIndex];
        //     // cubeMap = new Cubemap(tex.width, TextureFormat.RGB24, false);
        //     // cubeMap.SetPixel(CubemapFace.PositiveX, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.NegativeX, 0, 0, tex.GetPixel(tex.width - 1, 0));
        //     // cubeMap.SetPixel(CubemapFace.PositiveY, 0, 0, tex.GetPixel(0, tex.width - 1));
        //     // cubeMap.SetPixel(CubemapFace.NegativeY, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.PositiveZ, 0, 0, tex.GetPixel(0, 0));
        //     // cubeMap.SetPixel(CubemapFace.NegativeZ, 0, 0, tex.GetPixel(tex.width - 1, tex.width - 1));
        //     // cubeMap.Apply(false);
        //     // skyboxMaterial.SetTexture("_Tex", cubeMap);
        //     skyboxMaterial.SetTexture("_Tex", skyboxTextures[currentCubemapIndex]);
        // }

        Quaternion rotation = Camera.main.transform.rotation;
        Vector3 eulerRotation = rotation.eulerAngles;
        angle = eulerRotation.y;
        if(angle<0)
        {
            angle+=360;
        }
        string next = nextLocation(photos, currentimage(), angle);
    //    Debug.Log(next);
    //    Debug.Log(angle);

        // Check if the angle is within the specified range and the prefab isn't already shown

        if (next!="null")
        {
            //Debug.Log(next);
            if(!isPrefabShown)
            {
               Arrow.SetActive(true); // Show the prefab
               Arrow.transform.position= cameraTransform.position + (cameraTransform.forward*distanceFromCamera);
            }
            Arrow.transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
          //  Arrow.transform.position= cameraTransform.position + (cameraTransform.forward*distanceFromCamera);
            isPrefabShown = true;
        }
        // if (Input.GetKeyDown(KeyCode.UpArrow)){
        //     if (next!="null"){
        //         changecubemap(next);
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow)){
        //     float new_angle = (angle + 180) % 360;
        //     string back = nextLocation(photos, currentimage(), new_angle);
        //     if (back!="null"){
        //         changecubemap(back);
        //     }
        // }
        // If the angle is outside the specified range and the prefab is currently being shown
        else
        {
            Arrow.SetActive(false); // Hide the prefab
            isPrefabShown = false;
        }
    }
}
