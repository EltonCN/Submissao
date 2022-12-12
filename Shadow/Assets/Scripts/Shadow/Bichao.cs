using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bichao : MonoBehaviour
{
    private NavMeshAgent bichao;
    [SerializeField] GameObject target;
    [SerializeField] GameEvent bichaoCollision;

    bool stop = false;


    // Start is called before the first frame update
    void Start()
    {
        bichao = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stop)
        {
            bichao.isStopped = true;
            return;
        }
        bichao.isStopped = false;

        Vector3 dirToPlayer = transform.position - target.transform.position;
        Vector3 newPos = transform.position - dirToPlayer;

        bichao.SetDestination(newPos);
    }

    public void KillBichao()
    {
        Destroy(bichao.gameObject);
    }

    void OnCollisionEnter(Collision collider)
    {
        if (GameObject.ReferenceEquals(collider.gameObject, target))
        {
            bichaoCollision.Raise();
        }
    }

    public void Stop()
    {
        stop = true;
    }

    public void Continue()
    {
        stop = false;
    }
}
