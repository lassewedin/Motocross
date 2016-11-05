using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public Life life;

    private float time;

	void Start () {

        //test

        //for (int y = 1; y <= 4; y++) {
        //    for (int x = 1; x <= 4; x++) {
        //        life.SpawnCreatureEmbryo(new Vector3(x*10f, y*10, 0f));
        //    }
        // }
        //life.SpawnCreatureEmbryo(new Vector3(50f, 50f, 0f));

        //life.SpawnCreatureEmbryo(new Vector3(10f, 20f, 0f));
        //life.SpawnCreatureEmbryo(new Vector3(10f, 30f, 0f));

    }

	void Update () {
       // life.UpdateGraphics();
	}

    void FixedUpdate() {
        time += Time.fixedDeltaTime;
        life.UpdateGrowth(time);
       // life.UpdatePhyscis(time);

    }
}
