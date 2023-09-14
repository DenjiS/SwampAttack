using UnityEngine;

public class Shotgun : Gun
{
    private const int DoubleDivisor = 2;

    [Header("Shotgun Options")]
    [SerializeField] private int _bulletsAmount;
    [SerializeField] private float _spawnSpread;
    [SerializeField] private float _directionSpread;
    [SerializeField] private float _directionRaise;

    private float _spawnStep;
    private float _directionStep;

    protected override void Awake()
    {
        base.Awake();

        _spawnStep = _spawnSpread / _bulletsAmount;
        _directionStep = _spawnSpread / _bulletsAmount;
    }

    protected override void PerformAttack(Vector2 shootPoint, Vector2 aimPoint)
    {
        float lowestSpawnYPosition = shootPoint.y - (_spawnSpread / DoubleDivisor);
        float lowestDirectionYPosition = shootPoint.y - (_directionSpread / DoubleDivisor);

        for (int i = 0; i < _bulletsAmount; i++)
        {
            Vector2 bulletShootPoint = new(shootPoint.x, lowestSpawnYPosition + _spawnStep * i);
            Vector2 bulletAimPoint = new(aimPoint.x, lowestDirectionYPosition + _directionStep * i + _directionRaise);
            LaunchBullet(bulletShootPoint, bulletAimPoint);
        }
    }
}
