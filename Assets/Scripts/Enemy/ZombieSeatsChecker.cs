using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSeatChecker : MonoBehaviour
{
    const string seatTag = "Seat";
    const string playerTag = "Player";
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

}
