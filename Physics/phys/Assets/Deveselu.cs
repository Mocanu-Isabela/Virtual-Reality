using UnityEngine;

public class Deveselu : MonoBehaviour
{
    [SerializeField] public Rigidbody projectileAnti;
    private Camera _camera;
    // Start is called before the first frame update


    private void Start()
    {
        _camera = Camera.main;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
            FireAtPointAnti(hit.point);
    }

    private void FireAtPointAnti(Vector3 point)
    {
        var transformPosition = transform.position;
        // point.x = point.x / 3;
        // point.y = point.y / 3;
        // point.z = point.z / 3;
        var velocity = (point - transformPosition)*5;
        Debug.Log(velocity);
        projectileAnti.transform.position = transformPosition;
        projectileAnti.velocity = velocity;
    }
}