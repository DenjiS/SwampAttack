using UnityEngine;

public class TargetDistance : MonoBehaviour
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    public float TransitionRange => _transitionRange;

    private void Awake()
    {
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
    }
}
