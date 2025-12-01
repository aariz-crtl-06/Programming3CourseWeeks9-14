using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] Camera viewCamera;
    [SerializeField] float cameraDeltaScalar = 1f;

    Vector3 cameraStartPos;
    Vector3 layerStartPos;

    void Start()
    {
        cameraStartPos = viewCamera.transform.position;
        layerStartPos = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = viewCamera.transform.localPosition - cameraStartPos;

        float layerDeltaX = cameraDelta.x * cameraDeltaScalar;
        float layerDeltaY = cameraDelta.y * cameraDeltaScalar;


        Vector3 newLayerPos = layerStartPos + new Vector3(layerDeltaX, layerDeltaY);
        transform.position = Vector3.Lerp(transform.position, newLayerPos, cameraDeltaScalar);
    }

}
