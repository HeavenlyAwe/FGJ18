using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMission : MonoBehaviour {

    private bool done = false;

    public void SetDone(bool value) {
        done = value;
    }

    public bool IsDone() {
        return done;
    }
}
