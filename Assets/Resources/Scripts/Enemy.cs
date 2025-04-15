using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform Jugador_Detectado; // Referencia al jugador
    public GameObject Jugador;
    private PlayerBars StatsJugador;

    public float speed = 3f; // Velocidad de movimiento
    public float attackRange = 2f; // Distancia para atacar
    public int health = 100; // Salud del enemigo
    public int damage = 10; // Daño que inflige al jugador

    public Color damageColor = Color.red;
    private Color originalColor;
    private Renderer enemyRenderer; 

    private bool isAttacking = false;

    private void Start()
    {

        StatsJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBars>();
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
    }

    void Update()
    {
        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, Jugador_Detectado.position);

        // Si está dentro del rango de ataque
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            // Si está fuera del rango, persigue al jugador
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (!isAttacking)
        {
            // Moverse hacia el jugador
            Vector3 direction = (Jugador_Detectado.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Mirar hacia el jugador
            transform.LookAt(Jugador_Detectado);
        }
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Debug.Log("Atacando al jugador con " + damage + " de daño.");
            // Reducir la salud del jugador.
            StatsJugador.Salud.value -= 10;
            Invoke(nameof(ResetAttack), 1f); // Retraso para simular el tiempo de ataque
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemigo recibió " + damage + " de daño. Salud restante: " + health);

        // Inicia efectos visuales
        StartCoroutine(FlashEffect());

        if (health <= 0)
        {
            Die();
        }
    }
    private IEnumerator FlashEffect()
    {
        // Cambiar color y escala
        enemyRenderer.material.color = damageColor;
        transform.localScale *= 1.2f;

        yield return new WaitForSeconds(0.2f);

        // Restaurar color y escala
        enemyRenderer.material.color = originalColor;
        transform.localScale /= 1.2f;
    }

    void Die()
    {
        Debug.Log("El enemigo murió.");
        Destroy(gameObject); // Elimina al enemigo de la escena
    }
}
