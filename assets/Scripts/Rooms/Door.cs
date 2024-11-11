using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private bool isFinalDoor;
    [SerializeField] private GameObject successScreen; 

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isFinalDoor)
            {
                // Show success screen
                successScreen.SetActive(true);
                // Optionally, stop player movement here if needed
            }
            else
            {
                if (collision.transform.position.x < transform.position.x)
                    cam.MoveToNewRoom(nextRoom);
                else
                    cam.MoveToNewRoom(previousRoom);
            }
        }
    }
}