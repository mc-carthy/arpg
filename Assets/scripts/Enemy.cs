using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	public float HealthAsPercentage
    {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    private float maxHealthPoints = 100f;
    private float currentHealthPoints = 100f;
    private float attackRadius = 5f;
    private AICharacterControl aiCharacterControl = null;
    private GameObject player = null;

    private void Awake ()
    {
        aiCharacterControl = GetComponent<AICharacterControl> ();
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    private void Update ()
    {
        float distToPlayerSqr = Vector3.SqrMagnitude (transform.position - player.transform.position);
        if (distToPlayerSqr <= Mathf.Pow(attackRadius, 2))
        {
            aiCharacterControl.SetTarget (player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget (transform);
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0, maxHealthPoints);
    }

}
