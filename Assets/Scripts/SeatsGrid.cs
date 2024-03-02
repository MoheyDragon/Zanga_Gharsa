using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatsGrid : MonoBehaviour
{
    public Transform[,] seatsPositions = new Transform[30, 12];
    GridElement playerPosition;
    void Start()
    {
        Transform section;
        Transform row;
        for (int i = 0; i < 5; i++)
        {
            section = transform.GetChild(i);
            for (int j = 0; j < 12; j++)
            {
                row = section.GetChild(j);
                for (int z = 0; z < 6; z++)
                {
                    seatsPositions[i*6 + z, j] = row.GetChild(z).GetComponent<Chair>().SeatPosition;
                }
            }
        }
        playerPosition = new GridElement(0, 11);
        player.position = seatsPositions[playerPosition.x, playerPosition.y].position;
        MovePlayer();
    }
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (playerPosition.x<29)
            {
                playerPosition.x++;
                MovePlayer();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (playerPosition.y < 11)
            {
                playerPosition.y++;
                MovePlayer();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerPosition.x >0)
            {
                playerPosition.x--;
                MovePlayer();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (playerPosition.y > 0)
            {
                playerPosition.y--;
                MovePlayer();
            }
        }
    }
    private void MovePlayer()
    {
        player.position = seatsPositions[playerPosition.x, playerPosition.y].position;
    }
}
public struct GridElement
{
    public int x; public int y;

    public GridElement(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
