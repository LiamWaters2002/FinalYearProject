using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    public GameTime gameTime;
    public GameObject BuildingCollection;
    public GameObject SteelEvents;
    public GameObject WheatEvents;
    public GameObject CarEvents;
    public GameObject IronEvents;


    public void DisplayRandomEvent()
    {
        int childCount = BuildingCollection.transform.childCount;
        int randomchild = Random.Range(0, childCount);

        Transform randomBuilding = BuildingCollection.transform.GetChild(randomchild);

        Transform graphViewObject = randomBuilding.Find("InGameGraph(Clone)").GetChild(0);


        Vector3 position = graphViewObject.position + new Vector3(15, 5, 0);
        Camera.main.transform.position = position;
        Camera.main.transform.LookAt(transform.position);

        string buildingName = randomBuilding.name;

        GameObject selectedEventType = null;
        if (buildingName.Equals("Steel Mill(Clone)"))
        {
            selectedEventType = SteelEvents.gameObject;
        }
        else if (buildingName.Equals("Farm(Clone)"))
        {
            selectedEventType = WheatEvents.gameObject;
        }
        else if (buildingName.Equals("Quarry(Clone)"))
        {
            selectedEventType = IronEvents.gameObject;
        }
        else if (buildingName.Equals("Car Factory(Clone)"))
        {
            selectedEventType = CarEvents.gameObject;
        }

        GetEventFromCollection(selectedEventType).SetActive(true);
    }

    private GameObject GetEventFromCollection(GameObject EventCollection)
    {
        int childCount = EventCollection.transform.childCount;
        int randomchild = Random.Range(0, childCount);
        Transform randomEvent = BuildingCollection.transform.GetChild(randomchild);
        return randomEvent.gameObject;
    }
}
