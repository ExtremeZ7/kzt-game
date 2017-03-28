using UnityEngine;
using AssemblyCSharp;

public class CameraControl : MonoBehaviour
{

    private PlayerController playerControl;
    private Vector2 cameraSize;

    [Space(10)]
    private Transform playerTransform;
    public Vector2 transformOffset;

    [Space(10)]
    public Transform topLeftBound;
    public Transform bottomRightBound;

    [Space(10)]
    public float speedScale = 3f;

    void Start()
    {
        Camera cam = Camera.main;
        cameraSize.y = 2f * cam.orthographicSize;
        cameraSize.x = cameraSize.y * cam.aspect;
    }

    void OnDrawGizmos()
    {
        if (topLeftBound != null && bottomRightBound != null)
        {

            int guideLines = 10;
            for (int i = 0; i < 4; i++)
            {
                Vector3 pointA = Vector3.zero;
                Vector3 pointB = Vector3.zero;
                switch (i)
                {
                    case 0:
                        pointA = topLeftBound.position;
                        pointB = new Vector3(bottomRightBound.position.x, topLeftBound.position.y);
                        break;
                    case 1:
                        pointA = new Vector3(bottomRightBound.position.x, topLeftBound.position.y);
                        pointB = bottomRightBound.position;
                        break;
                    case 2:
                        pointA = bottomRightBound.position;
                        pointB = new Vector3(topLeftBound.position.x, bottomRightBound.position.y);
                        break;
                    case 3:
                        pointA = new Vector3(topLeftBound.position.x, bottomRightBound.position.y);
                        pointB = topLeftBound.position;
                        break;
                }

                if (topLeftBound.position.x < bottomRightBound.position.x && topLeftBound.position.y > bottomRightBound.position.y)
                {
                    Gizmos.color = Color.white;
                    for (int j = 0; j < guideLines; j++)
                    {
                        float distance;
                        float lineLength;

                        distance = Vector3.Distance(pointA, pointB);
                        lineLength = (distance / (float)guideLines);

                        Gizmos.DrawLine(Vector3.MoveTowards(pointA, pointB, lineLength * j), Vector3.MoveTowards(pointA, pointB, lineLength * j + (lineLength / 2f)));
                    }
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(pointA, pointB);
                }
            }
        }

        FollowPlayerStartLocation();
    }

    void Update()
    {
        if (Help.WaitForPlayer(ref playerControl))
        {
            playerTransform = playerControl.transform;

            Vector3 targetPosition = playerTransform.position;

            if (playerTransform.position.x - (cameraSize.x / 2f) < topLeftBound.position.x)
                targetPosition.x = topLeftBound.position.x + (cameraSize.x / 2f);
            else if (playerTransform.position.x + (cameraSize.x / 2f) > bottomRightBound.position.x)
                targetPosition.x = bottomRightBound.position.x - (cameraSize.x / 2f);

            if (playerTransform.position.y + (cameraSize.y / 2f) > topLeftBound.position.y)
                targetPosition.y = topLeftBound.position.y - (cameraSize.y / 2f);
            else if (playerTransform.position.y - (cameraSize.y / 2f) < bottomRightBound.position.y)
                targetPosition.y = bottomRightBound.position.y + (cameraSize.y / 2f);

            targetPosition.x += transformOffset.x;
            targetPosition.y += transformOffset.y;

            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(targetPosition.x, targetPosition.y, -10f),
                Vector2.Distance(transform.position, targetPosition) * speedScale * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    void FollowPlayerStartLocation()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;

        Vector3 targetPosition = playerTransform.position;

        if (playerTransform.position.x - (cameraSize.x / 2f) < topLeftBound.position.x)
            targetPosition.x = topLeftBound.position.x + (cameraSize.x / 2f);
        else if (playerTransform.position.x + (cameraSize.x / 2f) > bottomRightBound.position.x)
            targetPosition.x = bottomRightBound.position.x - (cameraSize.x / 2f);

        if (playerTransform.position.y + (cameraSize.y / 2f) > topLeftBound.position.y)
            targetPosition.y = topLeftBound.position.y - (cameraSize.y / 2f);
        else if (playerTransform.position.y - (cameraSize.y / 2f) < bottomRightBound.position.y)
            targetPosition.y = bottomRightBound.position.y + (cameraSize.y / 2f);

        targetPosition.x += transformOffset.x;
        targetPosition.y += transformOffset.y;

        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(targetPosition.x, targetPosition.y, -10f),
            Vector2.Distance(transform.position, targetPosition) * speedScale * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
