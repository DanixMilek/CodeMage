using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform gunPoint; // Punto de disparo
    public float shootForce = 500f; // Fuerza del disparo
    public float fireRate = 0.5f; // Intervalo entre disparos
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, gunPoint.position, gunPoint.rotation);

        // Obtener Rigidbody del proyectil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aplicar fuerza al proyectil
            rb.AddForce(gunPoint.forward * shootForce);
            Debug.Log("Proyectil disparado.");
        }
        else
        {
            Debug.LogError("El prefab del proyectil no tiene un Rigidbody.");
        }

        // Destruir el proyectil después de 5 segundos
        Destroy(projectile, 5f);
    }
}

