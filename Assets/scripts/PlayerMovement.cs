using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof (ThirdPersonCharacter))]
[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (AICharacterControl))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private const int walkableNumber = 8;
    [SerializeField]
    private const int enemyNumber = 9;

    private ThirdPersonCharacter character = null;
    private AICharacterControl aiCharacterControl = null;
    private CameraRaycaster cameraRaycaster = null;
    private GameObject walkTarget = null;
    private Vector3 currentDestination;
    private Vector3 clickPoint;
    private float walkStopRadius = 0.2f;
    private float attackStopRadius = 5f;
    private bool isInDirectMode = false;

    private void Awake ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl> ();
        walkTarget = new GameObject("walkTarget");
    }

    private void Start()
    {
        currentDestination = transform.position;
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    private void ProcessDirectMovement ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)).normalized;
        Vector3 move = (v * camForward) + (h * Camera.main.transform.right);

        character.Move (move, false, false);
    }

    private void ProcessMouseClick (RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyNumber:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget (enemy.transform);
                break;
            case walkableNumber:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget (walkTarget.transform);
                break;
            default:
                Debug.LogWarning ("Switch fall through, investiage");
                return;
        }
    }


}

