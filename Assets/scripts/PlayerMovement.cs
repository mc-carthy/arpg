using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;
    private float walkStopRadius = 0.2f;
    private bool isInDirectMode = false;

    private void Awake ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    private void Start()
    {
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown (KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
            currentClickTarget = transform.position;
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
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    Debug.Log ("Move to enemy");
                    break;
                default:
                    Debug.Log ("PlayerMovement has fallen through switch");
                    return;
            }
        }
        Vector3 playerMoveVector = currentClickTarget - transform.position;
        if (playerMoveVector.sqrMagnitude >= Mathf.Pow (walkStopRadius, 2))
        {
            character.Move(playerMoveVector, false, false);
        }
        else
        {
            character.Move (Vector3.zero, false, false);
        }
    }
}

