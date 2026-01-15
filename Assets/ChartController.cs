using UnityEngine;
using UnityEngine.UI;

public class ChartController : MonoBehaviour
{
    [Header("Dots")]
    public RectTransform dotNormal;
    public RectTransform dot2013;
    public RectTransform dotFuture;

    [Header("Lines")]
    public RectTransform line1; // Connects Normal -> 2013
    public RectTransform line2; // Connects 2013 -> Future

    // Chart Settings
    private float chartHeight = 200f; 
    private float maxDataValue = 160f; 

    void Start()
    {
        // HIDE everything when the app starts!
        // We want the chart to look empty until the user clicks.
        dotNormal.gameObject.SetActive(false);
        dot2013.gameObject.SetActive(false);
        dotFuture.gameObject.SetActive(false);
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
    }

    // 1. Calculate positions (Same as before)
    public void UpdateChartPositions(float valNormal, float val2013, float valFuture)
    {
        float y1 = (valNormal / maxDataValue) * chartHeight;
        float y2 = (val2013 / maxDataValue) * chartHeight;
        float y3 = (valFuture / maxDataValue) * chartHeight;

        SetDotPosition(dotNormal, y1);
        SetDotPosition(dot2013, y2);
        SetDotPosition(dotFuture, y3);
    }

    // 2. Reveal Functions (Called by Buttons)
    public void ShowStep1()
    {
        dotNormal.gameObject.SetActive(true);
    }

    public void ShowStep2()
    {
        ShowStep1(); // Ensure step 1 is visible
        dot2013.gameObject.SetActive(true);
        // Draw the line between Dot 1 and Dot 2
        ConnectDots(line1, dotNormal, dot2013);
    }

    public void ShowStep3()
    {
        ShowStep2(); // Ensure step 2 is visible
        dotFuture.gameObject.SetActive(true);
        // Draw the line between Dot 2 and Dot 3
        ConnectDots(line2, dot2013, dotFuture);
    }

    // --- HELPER FUNCTIONS ---

    void SetDotPosition(RectTransform dot, float y)
    {
        // -100 because the pivot is center, moving from bottom
        dot.anchoredPosition = new Vector2(dot.anchoredPosition.x, y - 100);
    }

    void ConnectDots(RectTransform line, RectTransform startDot, RectTransform endDot)
    {
        line.gameObject.SetActive(true);

        // A. Move line to the center point between the two dots
        Vector3 centerPos = (startDot.position + endDot.position) / 2f;
        line.position = centerPos;

        // B. Calculate direction to figure out length and angle
        Vector3 direction = endDot.position - startDot.position;
        float distance = direction.magnitude;

        // C. Stretch the line (Width = distance, Height = 4)
        line.sizeDelta = new Vector2(distance, 4f);

        // D. Rotate the line to point at the next dot
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        line.rotation = Quaternion.Euler(0, 0, angle);
    }
}