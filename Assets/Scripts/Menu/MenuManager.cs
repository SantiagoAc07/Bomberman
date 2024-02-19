using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject OptionsWindow;

    private void Awake()
    {
        OptionsWindow.SetActive(false);
    }

    public void PlayOnePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionsWindow.SetActive(true);
    }

    public void Volver()
    {
        OptionsWindow.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliste de el juego");
    }
}
