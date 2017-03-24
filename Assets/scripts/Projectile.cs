using UnityEngine;

public class Projectile : MonoBehaviour {

	private void OnTriggerEnter (Collider other)
    {
        Component damageableComponent = other.gameObject.GetComponent (typeof (IDamageable));
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage (10f);
        }
    }

}
