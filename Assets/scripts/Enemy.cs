using UnityEngine;

public class Enemy : MonoBehaviour {

	public float HealthAsPercentage
    {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    private float maxHealthPoints = 100f;
    private float currentHealthPoints = 100f;

}
