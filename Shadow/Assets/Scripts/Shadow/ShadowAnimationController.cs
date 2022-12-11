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


    Animator animator;
    NavMeshAgent navMeshAgent;

    int velocityHash;
    int crounchHash;

    float crounchStartTime = 0;
    bool crounching = false;
    float crounchCurveMaxTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        velocityHash = Animator.StringToHash("Velocity");
        crounchHash = Animator.StringToHash("Crounching");

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
        crounching = true;
        crounchStartTime = Time.time;
    }

    public void GetUp()
    {
        crounching = false;
        crounchStartTime = Time.time;
    }

    public void ToggleCrounch()
    {
        crounching = !crounching;
        crounchStartTime = Time.time;
    }

    void OnValidate()
    {
        crounchCurve.preWrapMode = WrapMode.Clamp;
        crounchCurve.postWrapMode = WrapMode.Clamp;
    }
}
