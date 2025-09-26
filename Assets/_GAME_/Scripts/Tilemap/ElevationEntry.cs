using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Stair Entry Logic
/// Enable upper boundary when on elevation,
/// else enable ground.
/// </summary>
public class ElevationEntry : MonoBehaviour
{
    [HideInInspector] public GameObject elevationHigh;
    [HideInInspector] public GameObject elevationLow;
    [HideInInspector] public GameObject player;
    
    private Collider2D stairCollider;
    private float stairBottomY;
    
    private bool onStair;

    public void Start()
    {
        onStair = false;
        stairCollider = GetComponent<Collider2D>();
        EnableElevation(false);
    }
    
    void Update()
    {
        //Debug.Log($"onStair:{onStair}  ground:{elevationLow.activeInHierarchy}");
        if (onStair)
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 1; // avoid clipping
            float playerY = player.GetComponent<Rigidbody2D>().transform.position.y;
            stairBottomY = stairCollider.bounds.min.y; // stair's lowest y point
            //Debug.Log("stairbottomY:" +  stairBottomY);
            if (playerY < stairBottomY + Mathf.Epsilon)
            {
                EnableElevation(false);
            }
            else if (playerY > stairBottomY + Mathf.Epsilon)    // if player changes mind to go down
            {
                EnableElevation(true);
            }
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onStair = true;
            EnableElevation(true);  //assume player keeps going up
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onStair = false;
        }
    }
    private void EnableElevation(bool enable)
    {
        elevationHigh.SetActive(enable);
        elevationLow.SetActive(!enable);
    }

    void EnableAllComponents(GameObject gameObject, bool enable)
    {
        Behaviour[] behaviour = gameObject.GetComponents<Behaviour>();
        foreach (var comp in behaviour)
            comp.enabled = true;
    }
}
