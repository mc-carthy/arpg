using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	public float HealthAsPercentage
    {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    private float maxHealthPoints = 100f;
    private float currentHealthPoints = 100f;

    public void TakeDamage (float damage)
    {
        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0, maxHealthPoints);
    }

}
