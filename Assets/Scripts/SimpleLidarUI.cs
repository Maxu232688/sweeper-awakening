using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sprint 0: Simple LIDAR UI showing top-down scan visualization
/// </summary>
public class SimpleLidarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform robot;
    [SerializeField] private RawImage displayImage;
    [SerializeField] private Material displayMaterial;

    [Header("Scan Settings")]
    [SerializeField] private int textureWidth = 256;
    [SerializeField] private int textureHeight = 256;
    [SerializeField] private float scanRange = 15f;
    [SerializeField] private int rayCount = 72;

    private Texture2D lidarTexture;
    private float scanRotation;

    void Start()
    {
        lidarTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB24, false);
        lidarTexture.filterMode = FilterMode.Point;
        displayImage.texture = lidarTexture;

        Color[] colors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        lidarTexture.SetPixels(colors);
        lidarTexture.Apply();
    }

    void Update()
    {
        scanRotation += Time.deltaTime * 30f;
        PerformScan();
    }

    void PerformScan()
    {
        Color[] colors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(0.1f, 0.1f, 0.1f);
        }

        Vector3 robotPos = robot.position;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = scanRotation + (i * 360f / rayCount);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            Ray ray = new Ray(robotPos + Vector3.up * 0.3f, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, scanRange))
            {
                Vector2 hitOffset = new Vector2(hit.point.x - robotPos.x, hit.point.z - robotPos.z);
                hitOffset = hitOffset * (textureWidth / (scanRange * 2)) + new Vector2(textureWidth / 2, textureHeight / 2);

                if (hitOffset.x >= 0 && hitOffset.x < textureWidth && hitOffset.y >= 0 && hitOffset.y < textureHeight)
                {
                    int x = Mathf.FloorToInt(hitOffset.x);
                    int y = Mathf.FloorToInt(hitOffset.y);

                    float brightness = 1f - (hit.distance / scanRange);
                    Color hitColor = new Color(brightness, brightness, brightness);

                    for (int dx = -2; dx <= 2; dx++)
                    {
                        for (int dy = -2; dy <= 2; dy++)
                        {
                            int px = x + dx;
                            int py = y + dy;
                            if (px >= 0 && px < textureWidth && py >= 0 && py < textureHeight)
                            {
                                colors[py * textureWidth + px] = hitColor;
                            }
                        }
                    }
                }

                Vector2 robot2D = new Vector2(textureWidth / 2, textureHeight / 2);
                DrawLine(colors, (int)robot2D.x, (int)robot2D.y, (int)hitOffset.x, (int)hitOffset.y, new Color(0.2f, 0.2f, 0.2f));
            }
        }

        int centerX = textureWidth / 2;
        int centerY = textureHeight / 2;
        for (int dx = -3; dx <= 3; dx++)
        {
            for (int dy = -3; dy <= 3; dy++)
            {
                if (dx * dx + dy * dy <= 9)
                {
                    int px = centerX + dx;
                    int py = centerY + dy;
                    if (px >= 0 && px < textureWidth && py >= 0 && py < textureHeight)
                    {
                        colors[py * textureWidth + px] = Color.green;
                    }
                }
            }
        }

        lidarTexture.SetPixels(colors);
        lidarTexture.Apply();
    }

    void DrawLine(Color[] colors, int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x0 >= 0 && x0 < textureWidth && y0 >= 0 && y0 < textureHeight)
            {
                colors[y0 * textureWidth + x0] = color;
            }

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }
}
