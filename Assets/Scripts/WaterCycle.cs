using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCycle : MonoBehaviour
{
    public int loopDuration = 45;
    public int maxWaterLevel;
    public int minWaterLevel;

    WorldGenerator wg;

    float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        wg = this.GetComponent<WorldGenerator>();
    }

    void FixedUpdate()
    {
        float ratio = (float)timeElapsed / loopDuration;
        float waterLevel = minWaterLevel + (Mathf.Sin(ratio) + 1)/2 * (maxWaterLevel - minWaterLevel);
        wg.waterLevel = waterLevel;

        timeElapsed += Time.deltaTime;
    }
}
