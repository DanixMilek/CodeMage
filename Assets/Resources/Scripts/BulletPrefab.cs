using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    public float lifetime = 1f;
    public int damage = -10;
    private GameObject player; // Referencia al jugador
    public GameObject impactEffectPrefab;
    void Start()
    {
        // Busca al jugador en la escena (asegúrate de que tenga la etiqueta "Player").
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Ignora la colisión entre el proyectil y el jugador.
            Collider bulletCollider = GetComponent<Collider>();
            Collider playerCollider = player.GetComponent<Collider>();

            if (bulletCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(bulletCollider, playerCollider);
            }
        }

        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Crear el efecto de impacto en la posición de la colisión
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Accede al script del enemigo y aplica daño.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10); // Causa 10 de daño al enemigo.
            }
        }

        // Aquí puedes añadir lógica específica si el proyectil golpea algo.
        Debug.Log("El proyectil colisionó con: " + collision.gameObject.name);

        // Destruir el proyectil al colisionar.
        Destroy(gameObject);
    }
}
