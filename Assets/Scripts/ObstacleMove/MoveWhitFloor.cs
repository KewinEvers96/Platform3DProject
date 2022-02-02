using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhitFloor : MonoBehaviour
{
    CharacterController player;
    [SerializeField]
    Vector3 groundPosition;
    [SerializeField]
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;
    bool touchPlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<CharacterController>();          
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.isGrounded)
        {
            RaycastHit hit;

            Vector3 sphereCenter = new Vector3(transform.position.x, 
                                                transform.position.y + 1,
                                                transform.position.z);

            if (Physics.SphereCast(sphereCenter, player.height/4.2f, -transform.up, out hit))
            {
                GameObject groundedIn = hit.collider.gameObject;
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;
                if (groundPosition != lastGroundPosition && groundName == lastGroundName)
                {
                    this.transform.position += groundPosition - lastGroundPosition;
                }

                lastGroundName = groundName;
                lastGroundPosition = groundPosition;


                if (groundedIn.CompareTag("Destructable") && !touchPlatform && !groundedIn.gameObject.GetComponent<DestructablePlatorm>().DestructionStarted)
                {
                    touchPlatform = true;
                    DestructablePlatorm destructable = groundedIn.GetComponent<DestructablePlatorm>();
                    destructable.startTimer();

                }

            }
        }
        else if (!player.isGrounded)
        {
            touchPlatform = false;
            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
        }
    }
    private void OnDrawGizmos ()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x , 
                        transform.position.y + 1, 
                        transform.position.z)
            ,player.height/4.2f);
    }
}
