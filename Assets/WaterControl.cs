using UnityEngine;
using TMPro; 
using System.Collections; 

// CONTROLLER SCRIPT: Manages the core flood simulation logic including 
// water physics, data visualization, audio dynamics, and haptic feedback.
public class WaterControl : MonoBehaviour
{
    [Header("Scene References")]
    public Transform arCamera;      // Reference to the AR Camera to calculate floor height
    public TMP_Text levelText;      // UI Text for displaying data
    public ChartController myChart; // External script controlling the graph
    public Transform floodRuler;    // The visual measurement pole
    
    // Audio Source for dynamic water sound
    public AudioSource floodAudio; 

    // Calculation variables
    private float targetHeight = 0.0f; 
    private float floorOffset = 13.0f; 
    private float fixedFloorLevel;  // Stores the calculated floor Y-position

    // --- VISUAL DATA (Unity World Units) ---
    private float waterVisual_Normal = 0f;
    private float waterVisual_2013 = 8f;   
    private float waterVisual_Future = 10f; 

    // --- REAL DATA (River Gauge in Meters) ---
    private float realData_Normal = 0f;
    private float realData_2013 = 129f;   // Represents 12.9m
    private float realData_Future = 140f; // Represents 14.0m

    private float currentRealData = 0f;

    // Volume Control Target
    private float targetVolume = 0.1f; 

    void Start()
    {
        // 1. ANCHORING: Calculate floor level once at startup
        // This locks the simulation to the physical room and prevents AR drift.
        if (arCamera != null)
        {
            fixedFloorLevel = arCamera.position.y - floorOffset;
        }

        // 2. PLACEMENT: Snap the Ruler to the calculated floor level
        if (floodRuler != null)
        {
            floodRuler.position = new Vector3(floodRuler.position.x, fixedFloorLevel, floodRuler.position.z);
        }

        // 3. INITIALIZATION: Setup the chart with historical data
        if (myChart != null)
        {
            myChart.UpdateChartPositions(realData_Normal, realData_2013, realData_Future);
        }
    }

    void Update()
    {
        // 1. PHYSICS: Smooth Water Movement
        // Uses Mathf.Lerp (Linear Interpolation) to transition fluidly between heights
        // instead of snapping instantly.
        float goalY = fixedFloorLevel + targetHeight;
        float currentY = transform.position.y;
        float smoothedY = Mathf.Lerp(currentY, goalY, Time.deltaTime * 2.0f);
        
        transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);

        // 2. UI: Update Text Display
        // Formats the integer data into a float string (e.g., "12.89m")
        if (levelText != null)
        {
             float displayMeters = currentRealData / 10.0f;
             levelText.text = "Water Level: " + displayMeters.ToString("F2") + "m";
        }

        // 3. AUDIO: Dynamic Volume Control
        // Smoothly fades volume up/down based on the severity of the flood.
        if (floodAudio != null)
        {
            floodAudio.volume = Mathf.Lerp(floodAudio.volume, targetVolume, Time.deltaTime * 1.0f);
        }
    }

    // --- UI BUTTON EVENTS ---

    public void Click_Normal() {
        // Set targets to baseline
        targetHeight = waterVisual_Normal; 
        currentRealData = realData_Normal; 
        targetVolume = 0.1f; // Quiet audio

        // Update Chart Visuals
        if(myChart != null) myChart.ShowStep1(); 
    }

    public void Click_2013() {
        // Set targets to Historic 2013 Flood
        targetHeight = waterVisual_2013;
        currentRealData = realData_2013;
        targetVolume = 0.5f; // Medium volume

        if(myChart != null) myChart.ShowStep2(); 
        
        // HAPTICS: Single vibration pulse for warning
        Handheld.Vibrate(); 
    }

    public void Click_Future() {
        // Set targets to Future Extreme Scenario
        targetHeight = waterVisual_Future;
        currentRealData = realData_Future;
        targetVolume = 1.0f; // Max volume for immersion

        if(myChart != null) myChart.ShowStep3(); 
        
        // HAPTICS: Trigger custom vibration pattern for danger
        StartCoroutine(VibratePattern_Danger());
    }

    // COROUTINE: Custom Haptic Pattern
    // Creates a rhythmic "Alarm" sensation (Pulse - Wait - Pulse)
    IEnumerator VibratePattern_Danger()
    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.2f); 
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.2f);
        Handheld.Vibrate();
    }
}