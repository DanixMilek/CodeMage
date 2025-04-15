using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartState : MonoBehaviour
{
    public GameObject DeathMenu;
    void Start()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal
        DeathMenu = GameObject.Find("Jugador/UI/DeathMenu");
    }
    // Método para el botón de "Reintentar"
    public void RestartGame()
    {
        Cursor.visible = false;
        DeathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena
    }
}
