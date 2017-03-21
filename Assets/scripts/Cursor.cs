using UnityEngine;

public class Cursor : MonoBehaviour {

	private CameraRaycaster cameraRaycaster;

    private void Awake ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster> ();
    }

    private void Update ()
    {
    }

}
