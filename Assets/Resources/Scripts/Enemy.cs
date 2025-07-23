using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;


#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public Estados estado;
    public float distanciaSeguir;
    public float distanciaAtacar;
    public float distanciaEscapar;

    public bool autoseleccionarTarget = true;
    public float distancia;

    public bool vivo = true;

    private void Awake()
    {
        if(autoseleccionarTarget)
            Jugador_Detectado = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(CalcularDistancia());
    }
    private void LateUpdate()
    {
        CheckEstado();
    }
    private void CheckEstado()
    {
        switch (estado)
        {
            case Estados.idle:
                EstadoIdle();
                break;

            case Estados.seguir:
                EstadoSeguir();
                break;

            case Estados.atacar:
                EstadoAtacar();
                break;

            case Estados.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }

    public void CambiarEstado(Estados e)
    {
        switch (e)
        {
            case Estados.idle:
                break;

            case Estados.seguir:
                ChasePlayer();
                break;
            case Estados.atacar:
                break;
            case Estados.muerto:
                vivo = false;
                break;
            default:
                break;
        }
        estado = e;
    }
    public virtual void EstadoIdle()
    {
        if (distancia < distanciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }

    public virtual void EstadoSeguir()
    {
        if (distancia < distanciaAtacar)
        {
            CambiarEstado(Estados.atacar);
        }else if (distancia > distanciaEscapar)
        {
            CambiarEstado(Estados.idle);
        }
    }
    public virtual void EstadoAtacar()
    {
        if (distancia > distanciaAtacar + 0.4f)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoMuerto()
    {
    }

    IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            if (Jugador_Detectado != null)
            {
                distancia = Vector3.Distance(transform.position, Jugador_Detectado.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaAtacar);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaSeguir);
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaEscapar);
    }

    private void OnDrawGizmos()
    {
        int icono = (int)estado;
        icono++;
        Gizmos.DrawIcon(transform.position + Vector3.up * 1f, "0"+icono+".png", false);
    }

    public enum Estados
    {
        idle   = 0,
        seguir = 1,
        atacar = 2,
        muerto = 3
    }    
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
        //else
        //{
        //    // Si está fuera del rango, persigue al jugador
        //    ChasePlayer();
        //}
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
