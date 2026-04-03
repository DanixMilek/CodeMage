using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoMago : Enemy
{
    private NavMeshAgent agente;
    // Start is called before the first frame update

    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();

    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        agente.SetDestination(Jugador_Detectado.position);
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        agente.SetDestination(transform.position);
        transform.LookAt(Jugador_Detectado, Vector3.up);
    }

    public override void EstadoMuerto()
    {
        base.EstadoMuerto();
        agente.enabled = false;
    }
}