using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    float captureTime = 3;
    float time;
    public GameObject timeCanvas;
    public GameObject barrier;
    private Text timeText;
    private Image fillImg;
    
    private bool capturing = false;
    public GameObject capturingPlayer = null;
    private Color capturingcolor;
    private GameObject owner = null;
    private Color ownercolor = new Color(0f, 0f, 0f, 0f);

    private float captureDuration = 20;
    private float captureCount;

    void Start()
    {
        timeText = timeCanvas.transform.Find("Text").GetComponent<Text>();
        fillImg = timeCanvas.transform.Find("Image").GetComponent<Image>();
        barrier.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
    }
    public GameObject getOwner()
    {
        return owner;
    }

    void Update()
    {
        if (owner != null)
        {
            captureCount -= Time.deltaTime;
            ownercolor.a = captureCount / captureDuration;
            barrier.GetComponent<Renderer>().material.color = ownercolor;

            if (captureCount <= 0)
            {
                owner = null;
            }
        }
    }

    public void capture(GameObject player)
    {
        if (owner != player)
        {
            if (capturing == false)
            {
                Vector3 towerPos = Camera.main.WorldToViewportPoint(this.transform.position);
                timeText.transform.position = new Vector3(towerPos.x * Screen.width + 2, towerPos.y * Screen.height, 40);
                fillImg.transform.position = new Vector3(towerPos.x * Screen.width, towerPos.y * Screen.height, 40);
                fillImg.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
                time = captureTime;
                capturing = true;
                capturingPlayer = player;

                capturingcolor = player.GetComponent<Player>().color;
                capturingcolor.a = 0f;
            }
            else
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    fillImg.fillAmount = 1 - time / captureTime;
                    capturingcolor.a = 1 - time / captureTime;
                    barrier.GetComponent<Renderer>().material.color = capturingcolor;
                    timeText.text = time.ToString("F");
                }
                else
                {
                    timeText.text = "";
                    fillImg.fillAmount = 0;
                    owner = capturingPlayer;
                    ownercolor = capturingcolor;
                    Debug.Log("Captured by " + owner.name);
                    captureCount = captureDuration;
                    capturingPlayer = null;
                    capturing = false;
                }
            }
        }
    }
    public void stopCapture(GameObject player)
    {
        if (capturingPlayer == player || (owner != player && owner != null))
        {
            timeText.text = "";
            fillImg.fillAmount = 0;
            barrier.GetComponent<Renderer>().material.color = ownercolor;
            capturing = false;
            capturingPlayer = null;
        }
    }
}