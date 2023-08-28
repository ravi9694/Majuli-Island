using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonclick : MonoBehaviour
{
    public void test(){
        Debug.Log("Clicked");
    }
    public void gotovideo1()
    {
        SceneManager.LoadScene("video_intro");
    }
    public void gotovideo2()
    {
        SceneManager.LoadScene("video_intro_2");
    }
    public void gotomap()
    {
        SceneManager.LoadScene("mapview");
    }
    // public void gotoBK(){
    //     SceneManager.LoadScene("BK1");
    // }
    public void gotoCK(){
        SceneManager.LoadScene("CK1");
    }
    public void gotoDK(){
        SceneManager.LoadScene("DK1");
    }
    public void gotoGK(){
        SceneManager.LoadScene("GK1");
    }
    public void gotoJK(){
        SceneManager.LoadScene("JK1");
    }
    public void gotoNK(){
        SceneManager.LoadScene("NK1");
    }
    public void gotoRK(){
        SceneManager.LoadScene("RK1");
    }
    public void gotoSK(){
        SceneManager.LoadScene("SK1");
    }
}
