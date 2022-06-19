using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraspeed = 1f;
    [SerializeField] private float offsetY = 1f;
    private Vector3 pos;


    private void Awake()
    {
        if (!player) {
            player = FindObjectOfType<Hero>().transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        pos.z = -10.0f;
        pos.y += offsetY;
        transform.position = Vector3.Lerp(transform.position, pos, cameraspeed * Time.deltaTime);
    }
}
