using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Catchable : MonoBehaviour
{
    public int score;
    public bool caught = false;

    /*
    Fish is the only class that uses this class, but I could have other items
    like a treasure chest or a clam pearl that inherit from this class. This 
    class is necessary because if other said items are created, the Hook class just
    needs a Catchable object to operate on.
    */
}
