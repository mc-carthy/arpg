using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;

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
    [SerializeField]
    private float maxHealthPoints = 100f;
    [SerializeField]
    private float attackRadius = 6f;
    [SerializeField]
    private float chaseRadius = 10f;
    [SerializeField]
    private float damagePerProjectile = 9f;
    [SerializeField]
    private float secondsBetweenShots = 0.5f;

    private AICharacterControl aiCharacterControl = null;
    private GameObject player = null;
    private Vector3 aimAdjust = new Vector3 (0f, 1f, 0f);
    private float currentHealthPoints;
    private bool isAttacking = false;

    private void Awake ()
    {
        aiCharacterControl = GetComponent<AICharacterControl> ();
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    private void Start ()
    {
        currentHealthPoints = maxHealthPoints;
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

        if (distToPlayerSqr <= Mathf.Pow (attackRadius, 2) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine (FireProjectileRoutine ());
        }
        if (distToPlayerSqr > Mathf.Pow (attackRadius, 2))
        {
            StopAllCoroutines (); // Refactor to use StopCoroutine (FireProjectileRoutine ())
            isAttacking = false;
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0, maxHealthPoints);
        if (currentHealthPoints <= 0)
        {
            Destroy (gameObject);
        }
    }

    private void FireProjectile ()
    {
        GameObject newProjectile = Instantiate (projectileToUse, projectileSpawnPoint.transform.position, Quaternion.identity);
        Projectile projectileComp = newProjectile.GetComponent<Projectile> ();
        projectileComp.damage = damagePerProjectile;
        Vector3 unitProjToPlayer = (player.transform.position + aimAdjust - projectileSpawnPoint.transform.position).normalized;
        newProjectile.GetComponent<Rigidbody> ().velocity = unitProjToPlayer * projectileComp.speed;
    }

    private IEnumerator FireProjectileRoutine ()
    {
        FireProjectile();
        yield return new WaitForSeconds (secondsBetweenShots);
        StartCoroutine (FireProjectileRoutine ());
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, chaseRadius);
    }

}
