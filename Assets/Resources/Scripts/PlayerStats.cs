using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(menuName = "Unit Stats")]
public class PlayerStats : MonoBehaviour
{
    public int ataque, defensa, velocidadCasteo, velocidadMovimiento, regenMana, regenSalud;
    private int damage, salud, mana;
    private void Start()
    {
        ataque = (int)((float)ataque / 2);
    }
    void Update()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().projectilePrefab.GetComponent<DestroyProjectile>().damage;
        if (damage > 0)
        {
            damage += ataque;
        }
    }
}
