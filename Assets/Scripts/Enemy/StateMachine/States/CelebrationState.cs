using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CelebrationState : State
{
    private const string CelebrationCommand = "Celebration";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(CelebrationCommand);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
