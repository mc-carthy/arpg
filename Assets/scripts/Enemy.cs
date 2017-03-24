using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	public float HealthAsPercentage
    {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    [SerializeField]
    private GameObject projectileToUse;
    [SerializeField]
    private GameObject projectileSpawnPoint;

    private float maxHealthPoints = 100f;
    private float currentHealthPoints = 100f;
    private float attackRadius = 6f;
    private float damagePerProjectile = 9f;
    private float chaseRadius = 10f;
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
        if (distToPlayerSqr <= Mathf.Pow(chaseRadius, 2))
        {
            aiCharacterControl.SetTarget (player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget (transform);
        }

        if (distToPlayerSqr <= Mathf.Pow (attackRadius, 2))
        {
            FireProjectile ();
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0, maxHealthPoints);
    }

    private void FireProjectile ()
    {
        GameObject newProjectile = Instantiate (projectileToUse, projectileSpawnPoint.transform.position, Quaternion.identity);
        Projectile projectileComp = newProjectile.GetComponent<Projectile> ();
        projectileComp.damage = damagePerProjectile;
        Vector3 unitProjToPlayer = (player.transform.position - projectileSpawnPoint.transform.position).normalized;
        newProjectile.GetComponent<Rigidbody> ().velocity = unitProjToPlayer * projectileComp.speed;

    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, chaseRadius);
    }

}
