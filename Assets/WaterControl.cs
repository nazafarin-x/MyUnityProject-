using UnityEngine;
using TMPro; 

public class WaterControl : MonoBehaviour
{
    public Transform arCamera; 
    public TMP_Text levelText;
    public ChartController myChart; 

    private float targetHeight = 0.0f; 
    
    // ADJUST THIS IF NEEDED (You said -13.0 worked well)
    private float floorOffset = 13.0f; 

    // --- GROUP A: AR VISUALS (What looks good in the room) ---
    // These are the numbers you found that fit your room perfectly.
    private float waterVisual_Normal = 0f;
    private float waterVisual_2013 = 8f;   
    private float waterVisual_Future = 12f; 

    // --- GROUP B: REAL DATA (What looks good on the Graph) ---
    // These are the "Real" values (12.9m and 15.0m scaled x10)
    // The chart max is 160, so these will look tall and dramatic!
    private float realData_Normal = 0f;
    private float realData_2013 = 129f;   
    private float realData_Future = 150f;

    // We need to remember which "Real Data" is currently active for the text label
    private float currentRealData = 0f;

    void Start()
    {
        // 1. Send the DRAMATIC REAL DATA to the chart
        if (myChart != null)
        {
            myChart.UpdateChartPositions(realData_Normal, realData_2013, realData_Future);
        }
    }

    void Update()
    {
        if (arCamera == null) return;

        // --- MOVE WATER (Uses the Visual Numbers) ---
        float cameraHeight = arCamera.position.y;
        float floorLevel = cameraHeight - floorOffset;
        float goalY = floorLevel + targetHeight;

        float currentY = transform.position.y;
        float smoothedY = Mathf.Lerp(currentY, goalY, Time.deltaTime * 2.0f);
        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);

        // --- UPDATE TEXT (Uses the Real Data Numbers) ---
        // This makes the text say "12.90m" even though the water is only at height 8.
        if (levelText != null)
        {
             float displayMeters = currentRealData / 10.0f;
             levelText.text = "Water Level: " + displayMeters.ToString("F2") + "m";
        }
    }

    // --- BUTTONS ---

    public void Click_Normal() {
        targetHeight = waterVisual_Normal; // Set Water to 0
        currentRealData = realData_Normal; // Set Text to 0m
        
        if(myChart != null) myChart.ShowStep1(); 
    }

    public void Click_2013() {
        targetHeight = waterVisual_2013;   // Set Water to 8 (Looks good in room)
        currentRealData = realData_2013;   // Set Text to 12.9m (Historic accuracy)
        
        if(myChart != null) myChart.ShowStep2(); 
    }

    public void Click_Future() {
        targetHeight = waterVisual_Future; // Set Water to 12 (Looks good in room)
        currentRealData = realData_Future; // Set Text to 15.0m (Extreme accuracy)
        
        if(myChart != null) myChart.ShowStep3(); 
    }
}