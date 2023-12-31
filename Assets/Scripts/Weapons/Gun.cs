using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gun : Weapon
{
    [Header("Gun Options")]
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _ammoSize;

    private int _ammo;
    private bool _isReloading = false;

    public event UnityAction<int> AmmoChanged;

    public event UnityAction<float> ReloadingChanged;

    public int CurrentAmmo => _ammo;

    public override bool CanAttack =>
        _isReloading == false && base.CanAttack;

    protected override void Awake()
    {
        base.Awake();

        _ammo = _ammoSize;
    }

    public override void Attack(Vector2 shootPoint, Vector2 aimPoint)
    {
        PerformAttack(shootPoint, aimPoint);

        AmmoChanged?.Invoke(--_ammo);

        if (_ammo <= 0)
            StartCoroutine(Reloading());

        base.Attack(shootPoint, aimPoint);
    }

    protected void LaunchBullet(Vector2 shootPoint, Vector2 aimPoint)
    {
        Bullet bullet = Instantiate(_bullet, shootPoint, Quaternion.identity);
        bullet.transform.parent = null;

        if (aimPoint.x > shootPoint.x)
            bullet.GetComponent<SpriteRenderer>().flipX = true;

        Vector2 force = (aimPoint - shootPoint).normalized * bullet.Force;
        bullet.Rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    protected virtual void PerformAttack(Vector2 shootPoint, Vector2 aimPoint) =>
        LaunchBullet(shootPoint, aimPoint);

    private IEnumerator Reloading()
    {
        float elapsed = 0f;

        _isReloading = true;

        while (elapsed < _reloadTime)
        {
            ReloadingChanged?.Invoke(Mathf.Lerp(0f, 1f, elapsed / _reloadTime));
            elapsed += Time.deltaTime;
            yield return null;
        }

        ReloadingChanged?.Invoke(0f);

        _ammo = _ammoSize;
        AmmoChanged?.Invoke(_ammo);

        _isReloading = false;

        AudioPlayer.PlayOneShot(_reloadSound);
    }
}
