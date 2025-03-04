using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReProjectContext : MonoBehaviour
{
    private static ReProjectContext instance;
    // Start is called before the first frame update
    public static ReProjectContext Instance
    {
        get{
            if(instance == null)
            instance = new ReProjectContext();
            return instance;
        }
    }

    public void Initialize(){

    }
}
