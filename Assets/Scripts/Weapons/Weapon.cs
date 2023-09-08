using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [Header("General Options")]
    [SerializeField] private AudioClip _attackSound;

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;

    [SerializeField] private float _timeBetweenAttacks;

    [SerializeField] private int _price;
    [SerializeField] private bool _isBuyed;

    private WaitForSeconds _attackDelay;

    private bool _canAttack = true;

    public Sprite Icon => _icon;
    public string Label => _label;
    public int Price => _price;
    public bool IsBuyed => _isBuyed;

    public virtual bool CanAttack => _canAttack;

    protected AudioSource AudioPlayer { get; private set; }

    private void Awake()
    {
        AudioPlayer = GetComponent<AudioSource>();
        _attackDelay = new WaitForSeconds(_timeBetweenAttacks);
    }

    public void Buy()
    {
        _isBuyed = true;
    }

    public virtual void Attack(Vector2 shootPoint, Vector2 aimPoint)
    {
        AudioPlayer.PlayOneShot(_attackSound);

        _canAttack = false;
        StartCoroutine(AttackWaiting());
    }

    private IEnumerator AttackWaiting()
    {
        yield return _attackDelay;
        _canAttack = true;
    }
}
