using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter m_Character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;
    private float walkStopRadius = 0.2f;

    private void Awake ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
    }

    private void Start()
    {
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
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
            m_Character.Move(playerMoveVector, false, false);
        }
        else
        {
            m_Character.Move (Vector3.zero, false, false);
        }
    }
}

