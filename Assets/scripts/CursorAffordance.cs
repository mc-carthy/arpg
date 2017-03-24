using UnityEngine;

[RequireComponent (typeof (CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D swordCursor = null;
    [SerializeField]
    private Texture2D questionCursor = null;
    [SerializeField]
    private Vector2 hotSpot = new Vector2 (0f, 0f);
    [SerializeField]
    private const int walkableNumber = 8;
    [SerializeField]
    private const int enemyNumber = 9;

	private CameraRaycaster cameraRaycaster;

    private void Awake ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster> ();
        cameraRaycaster.notifyLayerChangeObservers += OnCursorChanged;
    }

    private void Start ()
    {

    }

    private void OnCursorChanged (int newLayer)
    {
        switch (newLayer)
        {
            case walkableNumber:
                Cursor.SetCursor (walkCursor, hotSpot, CursorMode.ForceSoftware);
                break;
            case enemyNumber:
                Cursor.SetCursor (swordCursor, hotSpot, CursorMode.ForceSoftware);
                break;
            default:
                Cursor.SetCursor (questionCursor, hotSpot, CursorMode.ForceSoftware);
                return;
        }
    }

}
