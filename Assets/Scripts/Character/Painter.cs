using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private LayerMask paintableLayers;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 hitPosition;
    private Paintable paintable;

    private void Awake()
    {
        paintable = FindObjectOfType<Paintable>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, paintableLayers))
            {
                hitPosition = hit.point + Vector3.back * 0.01f;
                paintable.Paint(hitPosition);
            }
        }
    }
}
