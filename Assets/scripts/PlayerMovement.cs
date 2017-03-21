using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter m_Character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;

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
                    break;
            }
        }
        m_Character.Move(currentClickTarget - transform.position, false, false);
    }
}

