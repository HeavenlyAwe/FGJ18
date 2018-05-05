using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    public TrapLogic trapLogicPrefab;

    private Dictionary<string, TrapLogic> trapMap;


    private void Awake() {
        GameObject serverObject = GameObject.Find("Server");
        if (serverObject != null) {
            Server server = serverObject.GetComponent<Server>();
            if (server != null) {
                server.SetTrapManager(this);
            }
        }
    }

    private void Start() {
        trapMap = new Dictionary<string, TrapLogic>();
    }


    //// Update is called once per frame
    //void Update() {
    //    if (Input.GetKeyDown(KeyCode.G)) {
    //        TrapLogic trapLogic = GameObject.Instantiate<TrapLogic>(trapLogicPrefab, new Vector3(10, 0, 10), Quaternion.identity);
    //        trapLogic.Initialize(0, 0, "da bomb", 3.0f);
    //    }
    //}


    public void PlaceTrap(int userId, int trapId, string type, Vector3 position) {
        TrapLogic trapLogic = GameObject.Instantiate<TrapLogic>(trapLogicPrefab, position, Quaternion.identity);
        float detonationTimer = 3.0f;   // TODO: This probably shouldn't be a hard-coded value...
        float blastRadius = 2.0f;       // TODO: This probably shouldn't be a hard-coded value...
        trapLogic.Initialize(userId, trapId, type, detonationTimer, blastRadius);

        string key = GetKey(userId, trapId);
        trapMap.Add(key, trapLogic);
    }

    public void ActivateTrap(int userId, int trapId) {
        string key = GetKey(userId, trapId);
        trapMap[key].Activate();
        trapMap.Remove(key);
    }

    private string GetKey(int userId, int trapId) {
        return userId + ":" + trapId;
    }
}
