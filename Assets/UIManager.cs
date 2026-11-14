using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private int count = 0;
    GameObject timerText;
    GameObject countText;
    GameObject LapTimeText;
    
    float time = 0.0f;
    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.countText = GameObject.Find("Count");
        this.LapTimeText = GameObject.Find("LapTime");
    }
    private void Awake()
    {
        Instance = this;
    }

    public void AddCount(int n)
    {
        this.count += n;
        this.countText.GetComponent<TextMeshProUGUI>().text =
            this.count.ToString() + " laps";
        if (count >= 2)
        {
            this.LapTimeText.GetComponent<TextMeshProUGUI>().text +=
                "LapTime: " + this.time.ToString("F1") + "s\n";

        }
    }

    void Update()
    {
        if(count >= 1)
        {
            this.time += Time.deltaTime;
            this.timerText.GetComponent<TextMeshProUGUI>().text =
                this.time.ToString("F1");
        }
       
    }

}
