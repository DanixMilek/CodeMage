using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Slider = UnityEngine.UI.Slider;

public class PlayerBars : MonoBehaviour
{
 // Siguiente Objetivo: Pantalla/Evento de Muerte
    public Slider Salud, Mana;
    public int SaludMax, ManaMax;
    public GameObject DeathMenu;

    public GameObject Hechizo_1;
    public GameObject Hechizo_2;

    public float TimeValue;

    void Start()
    {
        DeathMenu = GameObject.Find("Jugador/UI/DeathMenu");
        Salud = GameObject.Find("SaludSlider").GetComponent<Slider>();
        Mana = GameObject.Find("ManaSlider").GetComponent<Slider>();

        Salud.maxValue = SaludMax;
        Mana.maxValue = ManaMax;

        Salud.value = SaludMax;
        Mana.value = ManaMax;
    }

    void Update()
    {
        if(Salud.value <= 0)
        {
            Derrota();
        }

        if (Mana.value <= 0)
        {
            Mana.value = 0;
        }
    }

    void Derrota()
    {
        Salud.value = 0f;
        Time.timeScale = 0f;
        Cursor.visible = true;
        DeathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }
}
