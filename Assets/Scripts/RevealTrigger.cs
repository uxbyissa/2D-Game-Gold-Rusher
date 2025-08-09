using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    public GameObject levelEnd;
    public Transform groundEndMarker;

    // Start is called before the first frame update
    void Start()
    {
        if (levelEnd != null && groundEndMarker != null)
        {
            levelEnd.transform.position = groundEndMarker.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
