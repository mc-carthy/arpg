using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D swordCursor = null;
    [SerializeField]
    private Texture2D questionCursor = null;
    [SerializeField]
    private Vector2 hotSpot = new Vector2 (96f, 96f);

	private CameraRaycaster cameraRaycaster;

    private void Awake ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster> ();
    }

    private void Update ()
    {
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
