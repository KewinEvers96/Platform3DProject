using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // CharacterController characterController;
     Animator animator;
    // bool isAttacking = false; 
    // bool isAttackPressed = false;
    
    public GameObject bullet;
    public GameObject chargeEnergy;
    public Transform spawnPoint;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;
    public float chargeTime ;
    public float shotForce = 1500;
    public float shotRate = 0.5f;

    private float shotRateTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // void awake(){
    //     characterController = GetComponent<CharacterController>();
    //     animator = GetComponent<Animator>();
    // }
    // Update is called once per frame
    void Update()
    {

       
        if(Input.GetButtonDown("Fire1")){
            if(Time.time>shotRateTime){
                animator.SetBool("isAttacking",true);
                GameObject energyballOBJRight;
                energyballOBJRight=Instantiate(chargeEnergy,spawnPointRight.position,spawnPointRight.rotation);
                energyballOBJRight.transform.SetParent(spawnPointRight);
                GameObject energyballOBJLeft;
                energyballOBJLeft=Instantiate(chargeEnergy,spawnPointLeft.position,spawnPointLeft.rotation);
                energyballOBJLeft.transform.SetParent(spawnPointLeft);
                Destroy(energyballOBJRight,chargeTime);
                Destroy(energyballOBJLeft,chargeTime);
                StartCoroutine("LanchFireBall");
                // GameObject newBullet;
                // newBullet = Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);
                // newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward*shotForce);
                // shotRateTime = Time.time * shotRate;
                // Destroy(newBullet,2);
            }
            
        }
        else{
            animator.SetBool("isAttacking",false);
        }
        
    }
    IEnumerator LanchFireBall(){
        yield return new WaitForSeconds(chargeTime-.1f);

        GameObject newBullet;
        newBullet = Instantiate(bullet,spawnPoint.position,spawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward*shotForce);
        shotRateTime = Time.time * shotRate;
        Destroy(newBullet,1);
        yield return null;
    }
}
