using easyar;
using TMPro;
using UnityEngine;

public class TargetTextController : MonoBehaviour
{
    ImageTargetController target;

    [SerializeField] TMP_Text text;
    [SerializeField] string foundText = "Target Found";
    [SerializeField] string lostText = "Target Lost";

    void Start()
    {
        target = GetComponent<ImageTargetController>();
        text.text = "";

        //can keep methods as anonymous or create dedicated methods for readability
        target.TargetFound += () =>
        {
            text.text = foundText;
        };

        target.TargetLost += () =>
        {
            text.text = lostText;
        };
    }
}