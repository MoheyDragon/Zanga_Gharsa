using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool canMove;
    bool isMoving;
    [SerializeField] GridPosition startingPoint;
    GridPosition playerPosition;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        canMove = true;
        isMoving = false;
    }
    public void StartLevel()
    {
        canMove = true;
        playerPosition = startingPoint;
        MovePlayer();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
        HandleMovement();
        HandleZombiesDetection();
    }
    private void HandleMovement()
    {
        if (!canMove) return;
        GridPosition destination = playerPosition;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            destination.x--;
            if (SeatsGrid.Singleton.IsPointValid(destination))
            {
                playerPosition.x--;
                MovePlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            destination.x++;
            if (SeatsGrid.Singleton.IsPointValid(destination))
            {
                playerPosition.x++;
                MovePlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            destination.y--;
            if (SeatsGrid.Singleton.IsPointValid(destination))
            {
                playerPosition.y--;
                MovePlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            destination.y++;
            if (SeatsGrid.Singleton.IsPointValid(destination))
            {
                playerPosition.y++;
                MovePlayer();
            }
        }
    }
    private void HandleZombiesDetection()
    {
        if (isMoving)
            if (zombiesSeeingPlayer.Count > 0)
                Lose();
    }
    [SerializeField] Animator animator;
    const string FallingParam = "Falling";
    Vector3 topOfSeatHeaigh=Vector3.up*2;
    [SerializeField] float swipeToTopDuration;
    [SerializeField] float swipeToBottomDuration;
    public void MovePlayer()
    {
        isMoving = true;
        canMove = false;
        Vector3 destenation = SeatsGrid.Singleton.seatsPositions[playerPosition.x, playerPosition.y].position;
        animator.SetBool(FallingParam, true);
        LeanTween.move(gameObject, destenation + topOfSeatHeaigh, swipeToTopDuration).setOnComplete(() =>
        LeanTween.move(gameObject, destenation, swipeToTopDuration).setOnComplete(() =>
        {
            animator.SetBool(FallingParam, false);
            canMove = true;
            isMoving = false;
        }
        ));
    }
    List<Transform> zombiesSeeingPlayer;
    public void PlayerEnteredZombieVision(Transform zombie)
    {
        if (!zombiesSeeingPlayer.Contains(zombie))
        {
            zombiesSeeingPlayer.Add(zombie);
        }
    }
    public void PlayerExitedZombieVision(Transform zombie)
    {
      zombiesSeeingPlayer.Remove(zombie);
    }
    private void Lose()
    {
        isMoving = false;
        canMove = false;
        print("Lose");
    }
}
