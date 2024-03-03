using System.Collections;
using UnityEngine;

public class ZombieSeatChecker : MonoBehaviour
{
    const string seatTag = "Seat";
    const string playerTag = "Player";

    Animator animator;
    public Transform[] patrolPoints;
    public float waitDuration = 2f; // Adjust this value to set the wait duration
    private int currentPatrolIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (patrolPoints.Length!=0)
        StartCoroutine(CO_WaitBeforeNextPatrol());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(seatTag))
            other.GetComponent<Seat>().ZombieEnterSeatArea(transform);
        else if (other.CompareTag(playerTag))
            other.GetComponent<PlayerMovement>().PlayerEnteredZombieVision(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(seatTag))
            other.GetComponent<Seat>().ZombieExitSeatArea(transform);
        else if (other.CompareTag(playerTag))
            other.GetComponent<PlayerMovement>().PlayerExitedZombieVision(transform);
    }

    const string walkParam = "Walk";
    IEnumerator CO_WaitBeforeNextPatrol()
    {
        animator.SetBool(walkParam, false);
        yield return new WaitForSeconds(waitDuration);
        MoveToNextPatrolPoint();
    }
    [SerializeField] float speed=1;
    private void MoveToNextPatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        animator.SetBool(walkParam,true);
        transform.LookAt(patrolPoints[currentPatrolIndex]);
        float distanceToTarget = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);
        float duration = distanceToTarget / speed;
        LeanTween.move(gameObject, patrolPoints[currentPatrolIndex], duration).setOnComplete(()=>
        StartCoroutine(CO_WaitBeforeNextPatrol())); ;

    }
}