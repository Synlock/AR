using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class TargetTextFoundation : MonoBehaviour
{
    ARTrackedImageManager targetManager;
    [SerializeField] GameObject[] modelPrefabs;
    Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();

    [SerializeField] TMP_Text text;

    [SerializeField] string foundText = "Target Found";
    [SerializeField] string lostText = "Target Lost";


    //for a larger scale project i would create a script with the trackedImagesChanged and
    //ARTrackedImagesChangedEventArgs into its own script and inherit from it on other scripts to override the methods
    //instead of duplicating the code like I have done here
    void Awake()
    {
        targetManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject obj in modelPrefabs)
        {
            GameObject newModel = Instantiate(obj, Vector3.zero, Quaternion.identity);
            newModel.name = obj.name;
            models.Add(obj.name, newModel);
        }
    }

    void OnEnable() => targetManager.trackedImagesChanged += ImageChanged;

    void OnDisable() => targetManager.trackedImagesChanged -= ImageChanged;


    void ImageChanged(ARTrackedImagesChangedEventArgs e)
    {
        foreach (ARTrackedImage img in e.added)
        {
            UpdateImg(img);
        }
        foreach (ARTrackedImage img in e.updated)
        {
            UpdateImg(img);
        }
        foreach (ARTrackedImage img in e.removed)
        {
            models[img.name].SetActive(false);
            text.text = lostText;
        }
    }
    void UpdateImg(ARTrackedImage trackedImg)
    {
        string name = trackedImg.referenceImage.name;
        Vector3 pos = trackedImg.transform.position;

        GameObject prefab = models[name];
        prefab.transform.position = pos;
        prefab.SetActive(true);

        if (trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None ||
            trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {
            models[trackedImg.referenceImage.name].SetActive(false);
            text.text = lostText;
        }
        else text.text = foundText;
    }
}