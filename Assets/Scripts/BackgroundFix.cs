﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFix : MonoBehaviour {

	void Update ()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, 0);
	}
}
