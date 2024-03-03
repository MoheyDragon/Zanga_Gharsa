using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class SeatsGrid : MonoBehaviour
{
    const int sectionCount = 5;
    const int seatsPerRowCount = 6;
    const int rowsCount = 12;
    public static SeatsGrid Singleton;
    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this);
    }
    public SeatInfo[,] seatsPositions = new SeatInfo[sectionCount * seatsPerRowCount, rowsCount];
    void Start()
    {
        IntiateGrid();
    }
    private void IntiateGrid()
    {
        Transform section;
        Transform row;
        for (int i = 0; i < sectionCount; i++)
        {
            section = transform.GetChild(i);
            for (int j = 0; j < rowsCount; j++)
            {
                row = section.GetChild(j);
                for (int z = 0; z < seatsPerRowCount; z++)
                {
                    Seat seat = row.GetChild(z).GetComponent<Seat>();
                    seat.AssignGridCoordinate(i * seatsPerRowCount + z, j);
                    seatsPositions[i * seatsPerRowCount + z, j] = new SeatInfo(seat.SeatPosition.position, seat.isAccessable());
                }
            }
        }
    }
    public  bool IsPointValid(GridPosition gridElement)
    {
        if (IsPointInCoordinates(gridElement.x, gridElement.y))
            return IsPointEmpty(gridElement.x, gridElement.y);
        else
            return false;

    }
    private bool IsPointInCoordinates(int x, int y)
    {
        return x >= 0 && y >= 0 && x < seatsPerRowCount*sectionCount && y < rowsCount;
    }
    private bool IsPointEmpty(int x,int y) 
    {
        return seatsPositions[x, y].isAccessable;
    }
    public void UpdateSeatAccessibility(int x,int y, bool isAccessable)
    {
        seatsPositions[x, y].isAccessable = isAccessable;
    }
}
