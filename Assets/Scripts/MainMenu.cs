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

    public void YouLoseLevel1(){
        SceneManager.LoadScene(3);
    }
    public void YouLoseLevel2(){
        SceneManager.LoadScene(4);
    }
    public void YouWin(){
        SceneManager.LoadScene(5);
    }
    
    public void Regresar(){
        SceneManager.LoadScene(0);
    }
    public void Salir(){
        Application.Quit();
    }
}
