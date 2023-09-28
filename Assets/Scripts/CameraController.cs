
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Vector3 offset;
    public float smoothValue;
    void Start()
    {
        offset = transform.position - target.transform.position;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, smoothValue*Time.deltaTime);
    }
}
