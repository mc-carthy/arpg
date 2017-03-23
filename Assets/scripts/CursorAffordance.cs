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

	private CameraRaycaster cameraRaycaster;

    private void Awake ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster> ();
        cameraRaycaster.layerChangeObservers += OnCursorChanged;
    }

    private void Start ()
    {

    }

    private void OnCursorChanged ()
    {
        Debug.Log ("Delegate called");
        switch (cameraRaycaster.LayerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor (walkCursor, hotSpot, CursorMode.ForceSoftware);
                break;
            case Layer.Enemy:
                Cursor.SetCursor (swordCursor, hotSpot, CursorMode.ForceSoftware);
                break;
            default:
                Cursor.SetCursor (questionCursor, hotSpot, CursorMode.ForceSoftware);
                return;
        }
    }

}
