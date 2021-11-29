using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LockToScreenFoundation : MonoBehaviour
{
    ARTrackedImageManager targetManager;
    [SerializeField] GameObject[] modelPrefabs;
    Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();

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

    private void Start()
    {
        targetManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() => targetManager.trackedImagesChanged += OnChanged;

    void OnDisable() => targetManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs e)
    {
        foreach (ARTrackedImage newImage in e.added)
        {
            LockModel(newImage);
        }

        foreach (ARTrackedImage updatedImage in e.updated)
        {
            LockModel(updatedImage);
        }

        foreach (ARTrackedImage removedImage in e.removed)
        {
            removedImage.gameObject.SetActive(false);
        }
    }
    void LockModel(ARTrackedImage trackedImg)
    {
        string name = trackedImg.referenceImage.name;
        Vector3 pos = trackedImg.transform.position;
        GameObject prefab = models[name];

        if (trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None ||
            trackedImg.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {
            prefab.transform.SetParent(Camera.main.transform);
            prefab.transform.localPosition = Vector3.forward * 0.5f;
        }
        else
        {
            prefab.transform.position = pos;
            prefab.SetActive(true);
        }
    }
}