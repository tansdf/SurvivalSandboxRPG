﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SwipeRemove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
