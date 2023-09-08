using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponView : MonoBehaviour
{
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Image _reloadingIcon;
    [SerializeField] private TMP_Text _ammo;

    [SerializeField] private Player _player;

    private Gun _currentGun;

    private void OnEnable()
    {
        _player.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;

        if (_currentGun != null)
        {
            _currentGun.AmmoChanged -= OnAmmoChanged;
            _currentGun.ReloadingChanged -= OnReloadChanged;
        }
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _weaponIcon.sprite = weapon.Icon;

        if (_currentGun != null)
        {
            _currentGun.AmmoChanged -= OnAmmoChanged;
            _currentGun.ReloadingChanged -= OnReloadChanged;

            _reloadingIcon.fillAmount = 0;
        }

        SetGunData(weapon);
    }

    private void SetGunData(Weapon weapon)
    {
        if (weapon is Gun)
        {
            _ammo.enabled = true;

            Gun gun = weapon as Gun;
            gun.AmmoChanged += OnAmmoChanged;
            gun.ReloadingChanged += OnReloadChanged;

            _currentGun = gun;
        }
        else
        {
            _ammo.enabled = false;
            _currentGun = null;
        }
    }

    private void OnAmmoChanged(int ammo)
    {
        _ammo.text = ammo.ToString();
    }

    private void OnReloadChanged(float stage)
    {
        _reloadingIcon.fillAmount = stage;
    }
}
