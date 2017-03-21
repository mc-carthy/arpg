using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private GameObject player;

    private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    private void LateUpdate ()
    {
        transform.position = player.transform.position;
    }

}
