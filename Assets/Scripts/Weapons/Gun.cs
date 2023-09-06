using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Gun : Weapon
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _ammoSize;

    private AudioSource _sound;
    private WaitForSeconds _shootDelay;
    private WaitForSeconds _reloadDelay;

    private int _ammoCount;
    private bool _canShootNext = true;
    private bool _isReloading = false;

    public event UnityAction<int> AmmoChanged;
    //public event UnityAction Shooted;

    public override bool CanAttack => _canShootNext && _isReloading == false;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();

        _shootDelay = new WaitForSeconds(_timeBetweenShots);
        _reloadDelay = new WaitForSeconds(_reloadTime);

        _ammoCount = _ammoSize;
    }

    public override void Shoot(Vector2 shootPoint, Vector2 aimPoint)
    {
        if (_canShootNext == false || _isReloading)
            throw new UnityException("Weapon is not ready");

        _sound.Play();

        Bullet bullet = Instantiate(Bullet, shootPoint, Quaternion.identity);
        bullet.transform.parent = null;

        if (aimPoint.x > shootPoint.x)
            bullet.GetComponent<SpriteRenderer>().flipX = true;

        Vector2 force = (aimPoint - shootPoint).normalized * bullet.Force;
        bullet.Rigidbody.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(WaitAfterShoot());

        AmmoChanged?.Invoke(--_ammoCount);

        if (_ammoCount == 0)
            StartCoroutine(Reload());

    }

    private IEnumerator WaitAfterShoot()
    {
        _canShootNext = false;
        yield return _shootDelay;
        _canShootNext = true;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return _reloadDelay;

        _ammoCount = _ammoSize;
        AmmoChanged?.Invoke(_ammoCount);
        _isReloading = false;
    }
}
