using UnityEngine;
using UnityEngine.UI;

// UI VISUALIZATION SCRIPT: 
// Manages a dynamic line chart that connects data points (Normal -> 2013 -> Future).
// Uses Vector math to procedurally draw lines between UI elements.
public class ChartController : MonoBehaviour
{
    [Header("UI Elements (Dots)")]
    public RectTransform dotNormal;
    public RectTransform dot2013;
    public RectTransform dotFuture;

    [Header("UI Elements (Connecting Lines)")]
    public RectTransform line1; // Connects Normal -> 2013
    public RectTransform line2; // Connects 2013 -> Future

    // Chart Configuration
    private float chartHeight = 200f; // Max pixel height of the graph area
    private float maxDataValue = 160f; // The maximum value the graph can display

    void Start()
    {
        // INITIALIZATION: Hide all graph elements on startup.
        // The chart starts empty and reveals steps as the user interacts.
        dotNormal.gameObject.SetActive(false);
        dot2013.gameObject.SetActive(false);
        dotFuture.gameObject.SetActive(false);
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
    }

    // 1. DATA MAPPING: Converts raw data values into UI Y-coordinates
    // Normalizes the value (0 to 1) and scales it to the chart height.
    public void UpdateChartPositions(float valNormal, float val2013, float valFuture)
    {
        float y1 = (valNormal / maxDataValue) * chartHeight;
        float y2 = (val2013 / maxDataValue) * chartHeight;
        float y3 = (valFuture / maxDataValue) * chartHeight;

        SetDotPosition(dotNormal, y1);
        SetDotPosition(dot2013, y2);
        SetDotPosition(dotFuture, y3);
    }

    // 2. PROGRESSIVE REVEAL SYSTEM
    // These methods are called sequentially by the main 'WaterControl' script.
    
    public void ShowStep1()
    {
        dotNormal.gameObject.SetActive(true);
    }

    public void ShowStep2()
    {
        ShowStep1(); // Dependency: Ensure previous step is visible
        dot2013.gameObject.SetActive(true);
        
        // Procedurally draw the connection line
        ConnectDots(line1, dotNormal, dot2013);
    }

    public void ShowStep3()
    {
        ShowStep2(); // Dependency: Ensure previous step is visible
        dotFuture.gameObject.SetActive(true);
        
        // Procedurally draw the connection line
        ConnectDots(line2, dot2013, dotFuture);
    }

    // --- HELPER FUNCTIONS ---

    // Updates the anchored position of a specific dot
    void SetDotPosition(RectTransform dot, float y)
    {
        // Subtracts 100 to offset the pivot point (centering the chart vertically)
        dot.anchoredPosition = new Vector2(dot.anchoredPosition.x, y - 100);
    }

    // MATH & GEOMETRY: Calculates position, rotation, and size to connect two points
    void ConnectDots(RectTransform line, RectTransform startDot, RectTransform endDot)
    {
        line.gameObject.SetActive(true);

        // A. POSITION: Place the line at the midpoint between the two dots
        Vector3 centerPos = (startDot.position + endDot.position) / 2f;
        line.position = centerPos;

        // B. DIRECTION: Calculate vector from start to end
        Vector3 direction = endDot.position - startDot.position;
        float distance = direction.magnitude; // Length of the line

        // C. SCALE: Set width equal to distance, height fixed at 4px
        line.sizeDelta = new Vector2(distance, 4f);

        // D. ROTATION: Use Trigonometry (Atan2) to calculate the angle
        // Converts the direction vector into a rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        line.rotation = Quaternion.Euler(0, 0, angle);
    }
}