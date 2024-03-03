using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    const string disBeliefParam = "DisBelief";
    const string faceDownParam = "FaceDown";
    const string victoryParam = "Victory";
    const string yellParam = "Yell";
    const string clapParam = "Clap";
    float minRandom = 3, maxRandom = 10;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        NextAnimation();
    }
    private void NextAnimation()
    {
        int random = Random.Range(0, 5);
        switch (random)
        {
            case 0:
                animator.Play(disBeliefParam);
                break;
            case 1:
                animator.Play(faceDownParam);
                break;
            case 2:
                animator.Play(victoryParam);
                break;
            case 3:
                animator.Play(yellParam);
                break;
            case 4:
                animator.Play(clapParam);
                break;
            default:
                break;
        }
        Invoke(nameof(NextAnimation), Random.Range(minRandom, maxRandom));
    }
}
