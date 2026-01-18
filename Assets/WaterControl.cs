using UnityEngine;
using TMPro; 

public class WaterControl : MonoBehaviour
{
    [Header("Scene Objects")]
    public Transform arCamera; 
    public TMP_Text levelText;
    public ChartController myChart; 
    
    // LINK THE NEW SYSTEM HERE
    public Transform floodRuler; 

    private float targetHeight = 0.0f; 
    
    // Your floor calibration (Adjust if floor is too high/low)
    private float floorOffset = 13.0f; 

    // --- VISUAL DATA (What looks good in the room) ---
    private float waterVisual_Normal = 0f;
    private float waterVisual_2013 = 8f;   
    private float waterVisual_Future = 12f; 

    // --- REAL DATA (For the Chart & Text) ---
    private float realData_Normal = 0f;
    private float realData_2013 = 129f;   
    private float realData_Future = 150f;

    private float currentRealData = 0f;

    void Start()
    {
        if (myChart != null)
        {
            myChart.UpdateChartPositions(realData_Normal, realData_2013, realData_Future);
        }
    }

    void Update()
    {
        if (arCamera == null) return;

        // 1. Calculate Floor Level
        float cameraHeight = arCamera.position.y;
        float floorLevel = cameraHeight - floorOffset;

        // 2. Move the RULER to the floor
        if (floodRuler != null)
        {
            // Since we fixed the pivot using the "System" parent,
            // we can just place it directly at floorLevel!
            floodRuler.position = new Vector3(floodRuler.position.x, floorLevel, floodRuler.position.z);
        }

        // 3. Move the WATER
        float goalY = floorLevel + targetHeight;
        float currentY = transform.position.y;
        float smoothedY = Mathf.Lerp(currentY, goalY, Time.deltaTime * 2.0f);
        
        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);

        // 4. Update Text
        if (levelText != null)
        {
             float displayMeters = currentRealData / 10.0f;
             levelText.text = "Water Level: " + displayMeters.ToString("F2") + "m";
        }
    }

    // --- BUTTONS ---
    public void Click_Normal() {
        targetHeight = waterVisual_Normal; 
        currentRealData = realData_Normal; 
        if(myChart != null) myChart.ShowStep1(); 
    }

    public void Click_2013() {
        targetHeight = waterVisual_2013;
        currentRealData = realData_2013;
        if(myChart != null) myChart.ShowStep2(); 
    }

    public void Click_Future() {
        targetHeight = waterVisual_Future;
        currentRealData = realData_Future;
        if(myChart != null) myChart.ShowStep3(); 
    }
}