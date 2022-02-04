using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLevel1(){
        SceneManager.LoadScene(1);
    }
    public void PlayLevel2(){
        SceneManager.LoadScene(2);
    }
    public void Creditos(){
        SceneManager.LoadScene("Level2");
    }
    public void Regresar(){
        SceneManager.LoadScene("Level2");
    }
    public void Salir(){
        Application.Quit();
    }
}
