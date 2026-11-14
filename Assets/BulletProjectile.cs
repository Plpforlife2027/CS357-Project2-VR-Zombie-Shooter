
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BulletProjectile : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 25f;
    public float lifetime = 3f;
    public int damage = 1;

    [Header("Impact Effect")]
    public GameObject impactEffect;

    private Rigidbody rb;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = false;

        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;
    }

    public void Launch(
        Vector3 direction,
        Collider[] gunColliders)
    {
        Collider bulletCollider = GetComponent<Collider>();

        // Ignore collisions with our own gun
        foreach (Collider gunCollider in gunColliders)
        {
            if (gunCollider != null)
            {
                Physics.IgnoreCollision(
                    bulletCollider,
                    gunCollider
                );
            }
        }

        // Move bullet forward
        rb.linearVelocity = direction.normalized * speed;

        // Remove bullet after a few seconds
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
            return;

        hasHit = true;

        Debug.Log("Bullet hit: " +
                  collision.collider.name);

        // Check whether the bullet hit a zombie
        ZombieHealth zombie =
            collision.collider.GetComponentInParent<ZombieHealth>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        // Spawn impact particles if assigned
        if (impactEffect != null &&
            collision.contactCount > 0)
        {
            ContactPoint contact = collision.GetContact(0);

            GameObject effect = Instantiate(
                impactEffect,
                contact.point,
                Quaternion.LookRotation(contact.normal)
            );

            Destroy(effect, 1f);
        }

        // Destroy bullet after impact
        Destroy(gameObject);
    }
}
