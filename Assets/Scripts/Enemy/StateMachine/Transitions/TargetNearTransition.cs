using UnityEngine;

[RequireComponent(typeof(TargetDistance))]
public class TargetNearTransition : Transition
{
    private float _transitionRange;

    private void Start()
    {
        _transitionRange = GetComponent<TargetDistance>().TransitionRange;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
            NeedTransit = true;
    }
}
