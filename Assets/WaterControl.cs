using UnityEngine;
using TMPro; 

public class WaterControl : MonoBehaviour
{
    public Transform arCamera; 
    public TMP_Text levelText; 
    
    private float targetHeight = 0.0f; // The "Goal" height (e.g. +20m)
    
    // ADJUST THIS NUMBER if the floor is wrong (You found -13.0f worked well)
    private float floorOffset = 13.0f; 

    void Update()
    {
        if (arCamera == null) return;

        // 1. CALCULATE THE GOAL
        // Where should the water be ideally?
        float cameraHeight = arCamera.position.y;
        float floorLevel = cameraHeight - floorOffset;
        float goalY = floorLevel + targetHeight;

        // 2. SMOOTHLY MOVE THERE (The "Lerp" Effect)
        // We look at where we are now (transform.position.y)
        // We look at where we want to be (goalY)
        // We move a small step closer (Time.deltaTime * 2.0f controls speed)
        float currentY = transform.position.y;
        float smoothedY = Mathf.Lerp(currentY, goalY, Time.deltaTime * 2.0f);

        // 3. APPLY POSITION
        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);

        // 4. DEBUG TEXT
        if (levelText != null)
        {
             levelText.text = "Water Level: " + targetHeight.ToString("F1") + "m";
        }
    }

    public void SetLevel(float meters)
    {
        targetHeight = meters;
    }
}