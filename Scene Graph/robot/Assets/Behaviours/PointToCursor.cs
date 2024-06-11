using System;
using UnityEngine;

public class PointToCursor : MonoBehaviour{

    private Camera _camera;
    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;

        transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);
    }
}
