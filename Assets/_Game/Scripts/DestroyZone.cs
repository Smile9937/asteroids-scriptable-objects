using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyLayers;
    [SerializeField] private float offset;

    private void Awake()
    {
        if (!cam.orthographic)
        {
            Debug.LogError("The main camera needs to be set to ortographic");
            return;
        }

        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        edgeCollider.isTrigger = true;

        Vector2 leftBottom = cam.ScreenToWorldPoint(new Vector3(-offset, -offset, cam.nearClipPlane));
        Vector2 leftTop = cam.ScreenToWorldPoint(new Vector3(-offset, cam.pixelHeight + offset, cam.nearClipPlane));
        Vector2 rightTop = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + offset, cam.pixelHeight + offset, cam.nearClipPlane));
        Vector2 rightBottom = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + offset, -offset, cam.nearClipPlane));

        Vector2[] edgePoints = new[] { leftBottom, leftTop, rightTop, rightBottom, leftBottom };

        edgeCollider.points = edgePoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyLayers == (destroyLayers | 1 << collision.gameObject.layer))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
