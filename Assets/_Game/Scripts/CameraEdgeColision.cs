using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEdgeColision : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();

        if(!camera.orthographic)
        {
            Debug.LogError("The main camera needs to be set to ortographic");
            return;
        }

        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        Vector2 leftBottom = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 leftTop = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        Vector2 rightTop = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));
        Vector2 rightBottom = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, camera.nearClipPlane));

        Vector2[] edgePoints = new[] { leftBottom, leftTop, rightTop, rightBottom, leftBottom };

        edgeCollider.points = edgePoints;
    }
}