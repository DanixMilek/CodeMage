using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastCircleSpell : MonoBehaviour
{
    public GameObject magicCirclePrefab;
    public GameObject thunderPrefab;
    public LayerMask groundLayer;
    public float delayBeforeThunder = 2f;
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = GameObject.Find("Vista Jugador").GetComponent<Camera>();

        if (playerCamera == null)
        {
            Debug.LogError("No se encontró la cámara 'Vista jugador'.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        if (playerCamera == null) return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log("Raycast impactó con: " + hit.collider.name);
            Vector3 spawnPosition = hit.point;
            GameObject circle = Instantiate(magicCirclePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(SpawnThunder(circle.transform.position));
        }
    }

    IEnumerator SpawnThunder(Vector3 position)
    {
        yield return new WaitForSeconds(delayBeforeThunder);
        Instantiate(thunderPrefab, position + Vector3.up * 5f, Quaternion.identity);
    }
}
