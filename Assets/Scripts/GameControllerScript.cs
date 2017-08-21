using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{

    public enum origamiState
    {
        airplane,
        car,
        frog,
        ship
    }

    public static bool OrigamiStateCollide(origamiState state1, origamiState state2)
    {
        if (state1 == state2)
        {
            return false;
        }
        else if (state1 == origamiState.airplane || state2 == origamiState.airplane)
        {
            return true;
        }
        return false;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
