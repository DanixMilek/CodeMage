# Información de Avance CodeMage
Autora Danitsa Chandia
Proceso Actual: IA Enemigo

## Avances Actuales:
 - El enemigo ahora es capaz de **perseguir al jugador en movimiento**
 - El enemigo puede realizar acciones según la distancia en el que se encuentra con el jugador. Cuando este se encuentra fuera del alcance (el jugador "escapó" del enemigo), el enemigo realiza un comportamiento de rutina, la cuál está explorando en algunos lugares del mapa.

![Alt Acciones según comportamiento enemigo](https://github.com/DanixaChan/CodeMage/blob/CodeMage0.2/FotosGifsmd/IdleEnemy_Codemage.gif)


## Historial Actualizaciones:

## 01
 - El enemigo es capaz de calcular distancias, y según la distancia entre jugador-enemigo cambia estados (actualmente entre escapar, seguir, atacar)

  ![Radios de distancia de Comportamientos Enemigos](https://github.com/DanixaChan/CodeMage/blob/CodeMage0.2/FotosGifsmd/Radios_ComportamientosEnemigos.PNG)

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
