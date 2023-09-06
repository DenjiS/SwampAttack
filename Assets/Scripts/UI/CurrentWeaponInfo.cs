using TMPro;
using UnityEngine;

public class CurrentWeaponInfo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private TMP_Text _ammoCount;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _iconRenderer.sprite = weapon.Icon;

        if (weapon is Gun)
        {
            _ammoCount.enabled = true;
            Gun gun = weapon as Gun;
            //gun.AmmoChanged += OnAmmoChanged;
        }
        else
        {
            _ammoCount.enabled = false;
        }
    }

    private void OnAmmoChanged(int ammo)
    {
        _ammoCount.text = ammo.ToString();
    }
}
