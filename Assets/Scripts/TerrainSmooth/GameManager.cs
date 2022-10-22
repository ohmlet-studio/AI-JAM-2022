using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject glider;
    public GameObject world;
    public GameObject enterIPCanvas;

    private GameObject gliderModel;
    private bool isRunning;

    private WebSensorHandler sensors;

    // Start is called before the first frame update
    void Start()
    {
        this.sensors = FindObjectOfType<WebSensorHandler>();
        this.gliderModel = glider.transform.GetChild(0).gameObject;
        this.isRunning = true;

        stopGame();
    }

    private void stopGame() {
        gliderModel.SetActive(false); // disable the pglider model
        glider.GetComponent<GliderMovement>().enabled = false; // disable the glider movement
        world.SetActive(false); // disable the world
        enterIPCanvas.SetActive(true); // enable connection UI
    }

    // Update is called once per frame
    void Update()
    {

    }
}
