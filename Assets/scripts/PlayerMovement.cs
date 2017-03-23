using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentDestination;
    private Vector3 clickPoint;
    private float walkStopRadius = 0.2f;
    private float attackStopRadius = 5f;
    private bool isInDirectMode = false;

    private void Awake ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    private void Start()
    {
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown (KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
        }
        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseClickMovement();
        }
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

    private void ProcessMouseClickMovement ()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.Hit.point;
            switch (cameraRaycaster.LayerHit)
            {
                
                case Layer.Walkable:
                    currentDestination = ShortDestination (clickPoint, walkStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination (clickPoint, attackStopRadius);
                    break;
                default:
                    Debug.Log ("PlayerMovement has fallen through switch");
                    return;
            }
        }
        WalkToDestination ();
    }

    private void WalkToDestination ()
    {
        Vector3 playerMoveVector = currentDestination - transform.position;
        if (playerMoveVector.sqrMagnitude >= Mathf.Pow (walkStopRadius, 2))
        {
            character.Move(playerMoveVector, false, false);
        }
        else
        {
            character.Move (Vector3.zero, false, false);
        }
    }

    private Vector3 ShortDestination (Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos ()
    {
        // Draw movement gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine (transform.position, currentDestination);
        Gizmos.DrawSphere (currentDestination, 0.2f);
        Gizmos.DrawSphere (clickPoint, 0.1f);

        // Draw attack sphere
        Gizmos.color = new Color (255f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere (transform.position, attackStopRadius);
    }
}

