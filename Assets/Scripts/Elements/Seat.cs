using System.Collections.Generic;
using UnityEngine;

public class Seat:MonoBehaviour
{
    GridPosition gridPosition;
    public Transform SeatPosition;
    List<Transform> zombiesNearby;
    bool isOccupied;
    bool hasZombiesNearby;
    private void Start()
    {
        zombiesNearby = new List<Transform>();
        if (SeatPosition.childCount > 0)
            OccupySeat();
    }
    public void AssignGridCoordinate(int x,int y)
    {
        gridPosition = new GridPosition(x,y);
    }
    private void UpdateAccessibility()
    {
        SeatsGrid.Singleton.UpdateSeatAccessibility(gridPosition.x, gridPosition.y,isAccessable());
    }
    public void OccupySeat()
    {
        isOccupied = true;
        UpdateAccessibility();
    }
    public bool isAccessable()
    {
        bool returnValue= !isOccupied && !hasZombiesNearby;
        print(returnValue);
        return returnValue;
    }
    public void ZombieEnterSeatArea(Transform zombie)
    {
        if(!zombiesNearby.Contains(zombie))
            zombiesNearby.Add(zombie);
        hasZombiesNearby = true;
        GetComponent<MeshRenderer>().material.color = Color.black;
        UpdateAccessibility();
    }
    public void ZombieExitSeatArea(Transform zombie)
    {
        zombiesNearby.Remove(zombie);
        if (zombiesNearby.Count == 0)
        {
            hasZombiesNearby = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            UpdateAccessibility();
        }

    }
}
