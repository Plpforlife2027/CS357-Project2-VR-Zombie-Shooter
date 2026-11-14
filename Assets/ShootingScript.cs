
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform muzzlePoint;
    public BulletProjectile bulletPrefab;
    public float fireCooldown = 0.2f;
    public float spawnOffset = 0.15f;

    [Header("Gun Sound")]
    public AudioSource audioSource;
    public AudioClip gunshotSound;

    private float nextFireTime;

    public void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        if (muzzlePoint == null)
        {
            Debug.LogWarning("MuzzlePoint is missing!");
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is missing!");
            return;
        }

        nextFireTime = Time.time + fireCooldown;

        // Create bullet in front of gun barrel
        BulletProjectile bullet = Instantiate(
            bulletPrefab,
            muzzlePoint.position +
            muzzlePoint.forward * spawnOffset,
            muzzlePoint.rotation
        );

        // Launch the bullet forward
        bullet.Launch(
            muzzlePoint.forward,
            GetComponentsInChildren<Collider>()
        );

        // Play shooting sound
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }

        Debug.Log("Bang! Bullet fired!");
    }
}
