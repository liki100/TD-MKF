using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    private Camera _camera;

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    private void Update()
    {
        if(_camera == null) 
            return;
        
        var difference = _camera.ScreenToWorldPoint (Input.mousePosition) - transform.position;
        var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Clamp(rotateZ, -90, 90));
    }
}
