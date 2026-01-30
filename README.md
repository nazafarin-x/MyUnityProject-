
README - DanubeAR: Immersive Flood Visualization


Course: Immersive Analytics (Exercise Part 3)

Project: AR Flood Simulator (Passau 2013 Case Study)

1. Project Overview
This application is an Augmented Reality (AR) simulation designed to visualize historic and future flood data for the Danube river in Passau, Germany. It bridges the gap between abstract river gauge numbers (e.g., 12.89m) and physical reality by overlaying rising water, a survey ruler, and data visualization into the user's real-world environment.

Key Features:

Spatial Anchoring: A fixed-position Flood Ruler (3m offset).

Immersive Physics: "Infinite" water plane with smooth level transitions (Mathf.Lerp).

Multisensory Feedback: Dynamic audio volume and haptic vibration patterns based on flood severity.

Data Visualization: Synchronized UI Chart connecting historical data points.

2. How to Start the Application (Important!)
For the best experience and to avoid AR tracking issues, please follow this "Safety Protocol" when launching the app:

Stand Still: Stand in an open area with at least 3 meters of clear space in front of you.

Hold Phone Level: Hold the device at chest height, facing the open space.

Launch App: Open the application.

WAIT: Do not move your feet or the device for 3-5 seconds while the Unity logo is visible. This allows the AR Foundation system to establish the "World Origin" and floor plane.

Visual Confirmation: Once the Red Flood Ruler appears standing on the floor in front of you, you are free to walk around and explore.

3. Controls & Interaction
The interface consists of three buttons at the bottom of the screen.

A. "Normal" Button (Reset)
Visual: Water level drops to 0m (Dry floor).

Audio/Haptics: Silence / No vibration.

Use Case: Represents the safe, normal river level. Use this to reset the simulation.

B. "2013 Historic" Button
Visual: Water rises smoothly to 12.89m (Pegel Passau).

Audio: Moderate river flow sound.

Haptics: Single short vibration pulse (Warning).

Chart: Updates to show the orange data point.

C. "Future Extreme" Button
Visual: Water rises to 15.00m (Predicted extreme event).

Audio: Loud, rushing water sound (Volume max).

Haptics: Rapid, rhythmic vibration pattern (Danger Alarm).

Chart: Updates to show the red data point.

4. Troubleshooting & Vital Information
Issue: The Ruler is floating or too far away.

Cause: The app was started while facing a wall or the floor too closely.

Fix: Restart the app. Ensure you are looking into an open space (3m+) during the startup screen.

Issue: I cannot see the water.

Cause: You may be standing "uphill" or the floor detection failed.

Fix: Press the "2013 Historic" button again. Look down at the floor. The water is transparent; ensure you are looking for the blue tint over your real floor.

Technical Note on Data:
The height values displayed (8m, 10m, 12m) refer to the River Gauge Level (Pegelstand) of the Danube, not the depth of water inside the room. The simulation visualizes how this river level translates to the current elevation relative to the riverbed.

5. Development Environment
Engine: Unity 2022.3 (LTS)

Packages: AR Foundation, ARCore XR Plugin.

Platform: Android.
