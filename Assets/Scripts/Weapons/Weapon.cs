using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuyed;

    [SerializeField] protected Bullet Bullet;

    public Sprite Icon => _icon;

    public string Label => _label;

    public int Price => _price;

    public bool IsBuyed => _isBuyed;

    public abstract bool CanAttack { get; }

    public void Buy()
    {
        _isBuyed = true;
    }

    public abstract void Shoot(Vector2 shootPoint, Vector2 aimPoint);
}
