# Información de Avance CodeMage
Autora Danitsa Chandia
Proceso Actual: IA Enemigo

## Avances actuales:
 - El enemigo es capaz de calcular distancias, y según la distancia entre jugador-enemigo cambia estados (actualmente entre escapar, seguir, atacar)
 - El enemigo ya era capaz de seguir el jugador, sin embargo falta editar esa función para solo en caso de "Seguir" al jugador, **Cabe destacar que la funcionalidad que permitia mover el enemigo independiente del estado se encuentra deshabilitado**. Si quiere echar un vistazo, aqui el código comentado:

'''

    void Update()
    {
        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, Jugador_Detectado.position);

        // Si está dentro del rango de ataque
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        //------------FUNCIONALIDAD COMENTADA-----------------
        //else
        //{
        //    // Si está fuera del rango, persigue al jugador
        //    ChasePlayer();
        //}
        //------------------------------------------------------
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
'''