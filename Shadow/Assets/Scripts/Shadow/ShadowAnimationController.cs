using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class ShadowAnimationController : MonoBehaviour
{

    [SerializeField] float velocity;
    [SerializeField] float crounchiness;

    [SerializeField] bool useNavMeshVelocity;
    [SerializeField] AnimationCurve crounchCurve;
    [SerializeField] bool startCrounched;


    Animator animator;
    NavMeshAgent navMeshAgent;

    int velocityHash;
    int crounchHash;
    int dieHash;
    int attackHash;
    int flipHash;
    int restartHash;

    float crounchStartTime = 0;
    bool crounching = false;
    float crounchCurveMaxTime;

    void Awake()
    {
        if(startCrounched)
        {
            Crounch();
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        velocityHash = Animator.StringToHash("Velocity");
        crounchHash = Animator.StringToHash("Crounching");
        dieHash = Animator.StringToHash("Die");
        attackHash = Animator.StringToHash("Attack");
        flipHash = Animator.StringToHash("Flip");
        restartHash = Animator.StringToHash("Restart");

        crounchCurveMaxTime = crounchCurve[crounchCurve.length-1].time;

    }

    void Update()
    {
        if(useNavMeshVelocity && navMeshAgent != null)
        {
            velocity = navMeshAgent.velocity.magnitude/navMeshAgent.speed;
        }

        if(crounching)
        {
            float t = Time.time - crounchStartTime;
            crounchiness = crounchCurve.Evaluate(t);
        }
        else
        {
            float t = crounchCurveMaxTime - (Time.time-crounchStartTime);
            crounchiness = crounchCurve.Evaluate(t);
        }
        

        animator.SetFloat(velocityHash, velocity);
        animator.SetFloat(crounchHash, crounchiness);

    }

    public void Crounch()
    {
        if(crounching)
        {
            return;
        }

        crounching = true;
        crounchStartTime = Time.time;
    }

    public void GetUp()
    {
        if(!crounching)
        {
            return;
        }

        crounching = false;
        crounchStartTime = Time.time;
    }

    public void ToggleCrounch()
    {
        crounching = !crounching;
        crounchStartTime = Time.time;
    }

    public void Die()
    {
        animator.applyRootMotion = true;
        animator.SetTrigger(dieHash);
    }

    public void Attack()
    {
        animator.SetTrigger(attackHash);
    }

    public void Flip()
    {
        animator.SetTrigger(flipHash);
    }

    public void Restart()
    {
        animator.SetTrigger(restartHash);
    }

    void OnValidate()
    {
        crounchCurve.preWrapMode = WrapMode.Clamp;
        crounchCurve.postWrapMode = WrapMode.Clamp;

        if(startCrounched)
        {
            Crounch();
        }
    }
}
