﻿using Quaver.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public GameState[] States;
    public GameObject loadingScreenTest;
    private float test = 0;
    private bool tested = false;

    private void Start () {
        //Changes the cameramode so sprites dont clip
        GameObject.Find("Main Camera").GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;

        //Do game start stuff here





        //Starts play mode (TEST)
        States[0].StateStart();
    }

    private void Update () {

        //TEST. Remove later.
        test+= Time.deltaTime;
        if (!tested && test > 5)
        {
            //loadingScreenTest.active = true; // SHOW LOADING SCREEN
            States[0].StateEnd();
            States[1].StateStart();
            print("LOADED");
            tested = true;
        }
    }
}
