using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance;
    public GameObject[] persistentList;
    
    void Awake()
    {
        if(Instance!= null)
        {
            CleanUpAndDestroy();
            return;
        }
        else
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObject();
        }
    }

    void MarkPersistentObject()
    {
        foreach (GameObject obj in persistentList)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
    //destroy previous's scene's objects
    void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentList)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
