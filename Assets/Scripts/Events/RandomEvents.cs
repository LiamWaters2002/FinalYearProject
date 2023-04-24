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


        Vector3 position = graphViewObject.position + new Vector3(3, 3, 0);
        Camera.main.transform.position = position;
        Camera.main.transform.LookAt(graphViewObject);
        Camera.main.transform.position = Camera.main.transform.position - new Vector3(1, 0, 0);

        string buildingName = randomBuilding.name;
        Debug.Log(buildingName);

        GameObject selectedEventType = null;
        if (buildingName.Equals("Steel Mill(Clone)"))
        {
            selectedEventType = SteelEvents.gameObject;
            Debug.Log("1");
        }
        else if (buildingName.Equals("Farm(Clone)"))
        {
            selectedEventType = WheatEvents.gameObject;
            Debug.Log("2");
        }
        else if (buildingName.Equals("Quarry(Clone)"))
        {
            selectedEventType = IronEvents.gameObject;
            Debug.Log("3");
        }
        else if (buildingName.Equals("Car Factory(Clone)"))
        {
            selectedEventType = CarEvents.gameObject;
            Debug.Log("4");
        }

        GetEventFromCollection(selectedEventType).GetComponent<Canvas>().enabled = true;
    }

    private GameObject GetEventFromCollection(GameObject EventCollection)
    {
        int childCount = EventCollection.transform.childCount;
        int randomchild = Random.Range(0, childCount);
        Transform randomEvent = EventCollection.transform.GetChild(randomchild);
        Debug.Log("the name is: " + randomEvent.name);
        return randomEvent.gameObject;
    }
}
