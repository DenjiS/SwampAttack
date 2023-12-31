using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string WeaponBoolName = "hasGun";
    private const string AttackTriggerName = "attacked";

    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private int _maxHealth;

    private Animator _animator;
    private Weapon _currentWeapon;

    private int _currentWeaponNumber = 0;
    private int _health;

    public event UnityAction<Weapon> WeaponChanged;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    public int Money { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = _maxHealth;
        ChangeWeapon(_weapons[_currentWeaponNumber]);

        MoneyChanged?.Invoke(Money);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (aimPoint.x > transform.position.x)
                transform.localScale = new Vector2(-1f, 1f);
            else
                transform.localScale = Vector2.one;

            if (_currentWeapon.CanAttack)
            {
                _currentWeapon.Attack(_shootPoint.transform.position, aimPoint);
                _animator.SetTrigger(AttackTriggerName);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);

        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber >= _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber <= 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        WeaponChanged?.Invoke(weapon);

        _animator.SetBool(WeaponBoolName, weapon is Gun);
    }
}
