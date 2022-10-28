using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bichao : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent bichao;
    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start() {
        bichao = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position - dirToPlayer;

        bichao.SetDestination( newPos );
    }

    void FixedUpdate() {

    }
}
