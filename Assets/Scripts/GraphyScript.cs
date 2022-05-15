using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphyScript : MonoBehaviour
{
    public static GraphyScript graphy_script { get; private set; }

    public bool show_active = false;

    private void Awake()
    {
     

    }

    private void Start()
    {
        graphy_script = this;
    }
}
