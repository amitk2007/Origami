using UnityEngine;
using System.Collections;

public class PlayButtonScript : MonoBehaviour
{
    public static bool move = false;
    public static float speed = 5.0f;
    public static float reachDist = 0.5f;
    // Use this for initialization
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        move = true;
    }
}
