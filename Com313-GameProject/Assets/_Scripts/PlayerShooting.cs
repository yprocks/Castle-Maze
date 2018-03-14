using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour
{
    public AudioSource RocketSound;
    public Transform FirePoint;
    public GameObject FireAnim;
    public Transform PlayerCam;
    public GameObject Explosion;
    //public AudioSource Bgm;
    public  static int scoreValue = 10;
    public static int finalScore;



    void Start()
    {
  
    }

    void FixedUpdate()
    {
        //this.Bgm.Play();
        if (Input.GetButtonDown("Fire1"))
        {
            this.RocketSound.Play();

            Instantiate(this.FireAnim, this.FirePoint.position, Quaternion.identity);

            // need a variable to hold the location of our Raycast Hit
            RaycastHit hit;
            if (Physics.Raycast(this.PlayerCam.position, this.PlayerCam.forward, out hit))
            {

                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                   Instantiate(this.Explosion, hit.point, Quaternion.identity);
                    //ScoreManager.Score += scoreValue;
                    //finalScore = ScoreManager.Score;
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        /*// PUBLIC VARIABLES FOR TESTING
        public Transform FlashPoint;
         
       
        public GameObject BulletImpact;
        public AudioSource RifleShotSound;
        public Transform PlayerCam;

        // PRIVATE VARIABLES

        // Use this for initialization
        void Start () {
        }

        // Update is called once per frame (for Physics)
        void FixedUpdate () {
            if (Input.GetButtonDown ("Fire1")) {
                // show the MuzzleFlash at the FlashPoint without any rotation
                

                // if raycast hits an object then do something...
                
                    } else {
                        Instantiate (this.BulletImpact, hit.point, Quaternion.identity);
                    }


                }

                // Play Rifle Sound
                this.RifleShotSound.Play();
            }
        }

        void OnTriggerEnter(Collider other) {
            SceneManager.LoadScene ("OutDoor");	
        }
        */
    }
}
