using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    private float distanceToBackground = 100f;
    private Camera viewCamera;

    private RaycastHit hit;
    public RaycastHit Hit
    {
        get { return hit; }
    }

    private Layer layerHit;
    public Layer LayerHit
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange ();
    public OnLayerChange layerChangeObservers;

    void Start()
    {
        viewCamera = Camera.main;
        layerChangeObservers();
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            RaycastHit? hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                this.hit = hit.Value;
                if (layerHit != layer)
                {
                    layerHit = layer;
                    layerChangeObservers ();
                }
                return;
            }
        }

        // Otherwise return background hit
        hit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
