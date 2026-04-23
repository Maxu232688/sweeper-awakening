using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fake LIDAR Display UI
/// Sprint 0: Shows a simple scanning visualization
/// </summary>
public class LidarDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform robot;
    [SerializeField] private RawImage lidarImage;
    [SerializeField] private Material scanMaterial;

    [Header("Scan Settings")]
    [SerializeField] private float scanRange = 15f;
    [SerializeField] private int rayCount = 36;
    [SerializeField] private float scanSpeed = 2f;

    private float scanAngle;
    private LineRenderer[] lines;
    private Vector3[] hitPoints;

    void Start()
    {
        lines = new LineRenderer[rayCount];
        hitPoints = new Vector3[rayCount];

        for (int i = 0; i < rayCount; i++)
        {
            GameObject lineObj = new GameObject($"LidarRay_{i}");
            lineObj.transform.SetParent(robot);
            lines[i] = lineObj.AddComponent<LineRenderer>();
            lines[i].material = new Material(Shader.Find("Sprites/Default"));
            lines[i].startColor = Color.green;
            lines[i].endColor = Color.green;
            lines[i].startWidth = 0.02f;
            lines[i].endWidth = 0.02f;
            lines[i].positionCount = 2;
        }
    }

    void Update()
    {
        scanAngle += scanSpeed * Time.deltaTime;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = scanAngle + (i * 360f / rayCount);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            Ray ray = new Ray(robot.position + Vector3.up * 0.3f, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, scanRange))
            {
                hitPoints[i] = hit.point;
                lines[i].SetPosition(0, ray.origin);
                lines[i].SetPosition(1, hit.point);
                lines[i].startColor = Color.Lerp(Color.red, Color.green, hit.distance / scanRange);
                lines[i].endColor = lines[i].startColor;
            }
            else
            {
                hitPoints[i] = ray.origin + direction * scanRange;
                lines[i].SetPosition(0, ray.origin);
                lines[i].SetPosition(1, hitPoints[i]);
                lines[i].startColor = new Color(0, 0.5f, 0, 0.3f);
                lines[i].endColor = lines[i].startColor;
            }
        }
    }
}
