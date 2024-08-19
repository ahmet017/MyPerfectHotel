#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class EditorScript : MonoBehaviour
{
#if UNITY_EDITOR
    public GameObject prefab;
    public int num, amountNum;
    public BuyPoint[] allGameObjects;
    public Room[] rooms;

    void Start()
    {

        //allGameObjects = FindObjectsOfType<BuyPoint>();




        //rooms = FindObjectsOfType<Room>();

        //foreach (Room room in rooms)
        //{
        //    s = room.transform.GetChild(0).Find("ToiletPath").gameObject.GetComponent<SplineComputer>();
        //    Vector3 pos = s.GetPointPosition(s.pointCount-1);

        //    GameObject newO = new GameObject();
        //    newO.name = "ToiletPoint";

        //    newO.transform.position = pos;

        //    newO.transform.parent = room.transform.GetChild(0);

        //    room.toiletPoint = newO.transform;

        //    EditorUtility.SetDirty(room);

        //}






        foreach (BuyPoint obj in allGameObjects)
        {
            //print(obj.gameObject.name);

            //prefab = obj.GetComponentInChildren<Transform>().transform.Find("ToiletPath").gameObject;
            //obj.toiletPath = obj.GetComponentInChildren<Transform>().transform.Find("ToiletPath").gameObject.GetComponent<SplineComputer>();

        //    obj.objToUnlock.SetActive(false);
         //   obj.objToHide.SetActive(true);

            //obj.objToUnlock.SetActive(true);
            //obj.objToHide.SetActive(false);

         //   EditorUtility.SetDirty(obj);
        }
        //obj.GetComponent<BuyPoint>().srNo = num;
        //num++;

        //obj.GetComponent<BuyPoint>().purchaseAmount = amountNum;
        //amountNum += 50;


        //path.gameObject.AddComponent<BoxCollider>();
        //path.GetComponent<BoxCollider>().center = path.bezierPath.GetPoint(path.bezierPath.NumPoints-1);
        //path.GetComponent<BoxCollider>().isTrigger = true;

        //EditorUtility.SetDirty(obj);

        //if (obj.name == "Room_01")
        //{
        //    if (obj.transform.childCount == 0)
        //    {


        //        GameObject buyP = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        //        buyP.transform.parent = obj.transform;
        //        buyP.transform.localPosition = Vector3.zero;
        //        buyP.transform.localEulerAngles = Vector3.zero;

        //        buyP.transform.tag = "RoomEnterRight";
        //        buyP.gameObject.name = "RoomEnterRight";
        //    }
        //}

        //if (obj.name == "GateEnterLeft")
        //{
        //    if (obj.transform.childCount == 0)
        //    {

        //        GameObject buyP = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        //        buyP.transform.parent = obj.transform;
        //        buyP.transform.localPosition = Vector3.zero;
        //        buyP.transform.localEulerAngles = Vector3.zero;

        //        buyP.transform.tag = "RoomEnterLeft";
        //        buyP.gameObject.name = "RoomEnterLeft";
        //        EditorUtility.SetDirty(buyP);
        //    }

        //}
        //    test = tra.gameObject;

        //    if (tra.gameObject.activeSelf)
        //    {

        //    }
        //    else 
        //    { 
        //        print(tra.gameObject);

        //        DestroyImmediate(tra.gameObject);

        //    }
        //}
        //}
        EditorSceneManager.MarkSceneDirty(gameObject.scene);

            //Room[] allGameObjects = FindObjectsOfType<Room>();

            //foreach (Room room in allGameObjects)
            //{
            //    if (room != null)
            //    {
            //if (Obj.name == "PriceBorder")
            //{
            //GameObject buyP = PrefabUtility.InstantiatePrefab(buyPoint) as GameObject;
            //buyP.transform.parent = Obj.transform.parent;
            //buyP.transform.localPosition = Obj.transform.localPosition;

            //DestroyImmediate(Obj);

            //buyP.GetComponent<BuyPoint>().srNo = Obj.srNo;
            //num++;

            //buyP.GetComponent<BuyPoint>().purchaseAmount = Obj.purchaseAmount;
            //amountNum += 50;

            //if (Obj.GetComponent<BuyPoint>().transform.parent.transform.Find("Opened").gameObject)
            //  Obj.GetComponent<BuyPoint>().objToUnlock = Obj.GetComponent<BuyPoint>().transform.parent.transform.Find("Opened").gameObject;

            //if (Obj.GetComponent<BuyPoint>().transform.parent.transform.Find("LockedRoot").gameObject)
            //  Obj.GetComponent<BuyPoint>().objToHide = Obj.GetComponent<BuyPoint>().transform.parent.transform.Find("LockedRoot").gameObject;



            //EditorUtility.SetDirty(room);
            //}
            // }
            //}

           // EditorSceneManager.MarkSceneDirty(gameObject.scene);
        //}
    }
#endif
}
