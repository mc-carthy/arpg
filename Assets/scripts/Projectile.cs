using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10f;
    public float damage = 10f;

	private void OnTriggerEnter (Collider other)
    {
        Component damageableComponent = other.gameObject.GetComponent (typeof (IDamageable));
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage (damage);
        }
    }

}
