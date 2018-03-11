using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimit: MonoBehaviour {
    void Awake() {
		// Turn off v-sync
		QualitySettings.vSyncCount = 0;
        Application.targetFrameRate=60;
    }
}