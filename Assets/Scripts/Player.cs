using UnityEngine;
using UnityEngine.SceneManagement;

internal enum PlayerState
{
    Alive,
    HalfDead,
    Dead
}

public class Player : MonoBehaviour
{
    public RuntimeAnimatorController AliveAnimatorController;
    public RuntimeAnimatorController DeathAnimatorController;

    private PlayerState playerState;
    private Animator animator;

    private void Awake()
    {
        playerState = PlayerState.Alive;
        animator = GetComponent<Animator>();
    }

    public void OnHit()
    {
        switch (playerState)
        {
            case PlayerState.Alive:
                OnHitPlayerAlive();
                break;

            case PlayerState.HalfDead:
                OnHitPlayerHalfDead();
                break;

            default:
                break;
        }
    }

    private void OnHitPlayerAlive()
    {
        playerState = PlayerState.HalfDead;
        animator.runtimeAnimatorController = DeathAnimatorController;
    }

    private void OnHitPlayerHalfDead()
    {
        playerState = PlayerState.Dead;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}