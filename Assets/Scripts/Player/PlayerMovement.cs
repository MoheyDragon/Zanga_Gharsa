using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    bool canMove;
    bool isMoving;
    [SerializeField] GridPosition startingPoint;
    GridPosition playerPosition;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        canMove = true;
        isMoving = false;
        Invoke(nameof(StartLevel), .1f);
    }
    public void StartLevel()
    {
        canMove = true;
        playerPosition = startingPoint;
        MovePlayer();
    }
    void Update()
    {
        HandleMovement();
        HandleZombiesDetection();
        HandleProjecter();
    }
    private void HandleProjecter()
    {
        if(isInProjecterSeat)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Win();
            }
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
    AudioSource audioSource;
    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] AudioClip loseSound, WinSound;
    public void MovePlayer()
    {
        isMoving = true;
        canMove = false;
        Vector3 destenation = SeatsGrid.Singleton.seatsPositions[playerPosition.x, playerPosition.y].position;
        animator.SetBool(FallingParam, true);
        audioSource.clip = jumpSounds[Random.Range(0, 4)];
        audioSource.Play();
        LeanTween.move(gameObject, destenation + topOfSeatHeaigh, swipeToTopDuration).setOnComplete(() =>
        LeanTween.move(gameObject, destenation, swipeToTopDuration).setOnComplete(() =>
        {
            animator.SetBool(FallingParam, false);
            canMove = true;
            isMoving = false;
        }
        ));
    }
    List<Transform> zombiesSeeingPlayer=new List<Transform>();
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
    [Space]
    [Header("End Section")]
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject lose;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip realStory;
    private void Lose()
    {
        isMoving = false;
        canMove = false;
        print("Lose");
        audioSource.clip = loseSound;
        audioSource.Play();
        Instructions.SetActive(false);
        lose.SetActive(true);
        StartCoroutine(CO_RestartLevel());
    }
    private void Win()
    {
        audioSource.clip = WinSound;
        audioSource.Play();
        Instructions.SetActive(false);
        pressE.SetActive(false);
        isInProjecterSeat = false;
        canMove = false;
        videoPlayer.Stop();
        videoPlayer.clip = realStory;
        videoPlayer.Play();
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        Application.Quit();
    }

    IEnumerator CO_RestartLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
    const string projecterTag="projecter";
    [SerializeField] GameObject pressE;
    bool isInProjecterSeat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(projecterTag))
        {
            pressE.SetActive(true);
            isInProjecterSeat = true;
        }
    }
}
