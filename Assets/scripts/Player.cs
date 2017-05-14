using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	public float HealthAsPercentage
    {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    [SerializeField]
    private float maxHealthPoints;
    [SerializeField]
    private float meleeRadius;
    [SerializeField]
    private float damagePerHit;
    [SerializeField]
    private float timeBetweenHits;

    private CameraRaycaster cameraRaycaster;
    private GameObject currentTarget;
    private int enemyLayer = 9;
    private float currentHealthPoints;
    private float lastHitTime = 0f;

    private void Awake ()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
    }

    private void Start ()
    {
        cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
        currentHealthPoints = maxHealthPoints;
    }

    public void TakeDamage (float damage)
    {
        currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0, maxHealthPoints);
    }

    private void OnMouseClicked (RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            GameObject enemy = raycastHit.collider.gameObject;
            currentTarget = enemy;
            float dist = Vector3.Distance (enemy.transform.position, transform.position);
            if (Time.time - lastHitTime > timeBetweenHits && dist < meleeRadius)
            {
                Enemy enemyComp = enemy.GetComponent<Enemy> ();
                enemyComp.TakeDamage (damagePerHit);
                lastHitTime = Time.time;
            }
        }
    }

}
