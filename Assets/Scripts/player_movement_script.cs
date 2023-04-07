using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class player_movement_script : MonoBehaviour
{
    public Slider slider;
    public GameObject game_over_overlay;
    struct model_t
    {
        public float inertia;
        public float theta;
        public float omega;
        public float alpha;
        public float x;
        public float y;
        public float radius;
        public float beta;
    };
    public Rigidbody rb_player;
    public float radius_of_motion;
    public float radius_of_detection;
    public LayerMask lm;
    model_t model;
    Collider pivot_collider;
    bool last_attached;
    bool attached;
    bool jump_command;
    int rand;
    [Range(0, 20)]
    public float omega_max;
    public float max_time;
    float detach_time;
    bool ball_lost;
    public static bool won;

    void Start()
    {
        Time.timeScale = 1;
        detach_time = Time.deltaTime + max_time;
        float inertia = rb_player.mass * Mathf.Pow(radius_of_motion, 2);
        float r = radius_of_motion;
        InitModel(inertia, r);
        game_over_overlay.SetActive(false);
        won = false;
        ball_lost = false;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Portal(Clone)")
        {
            /*AnalyticsResult analytics_result = Analytics.CustomEvent("Portal Traversed", new Dictionary<string, object>
                     {
                     {"Level",FindObjectOfType<persistent_data_script>().level_no}
                     }
                );
             Debug.Log("Analytics Result: " + analytics_result);

             Debug.Log("Portal Traversed");


             FindObjectOfType<persistent_data_script>().show_video_ad();*/

             won = true;
         }
     }

     void Update()
     {
         Collider[] pivot_colliders = Physics.OverlapSphere(transform.position, radius_of_detection, lm);


         if (pivot_colliders.Length != 0 && !won && !ball_lost)
         {
             if (Input.touchCount > 0)
             {
                 jump_command = (Input.GetTouch(0).phase == TouchPhase.Began);
             }
             else
             {
                 jump_command = Input.GetKey(KeyCode.Space);
             }


             if (jump_command) //Jump
             {
                 Destroy(pivot_collider.gameObject);
                 rand = Random.Range(1, 3);
                 switch (rand)
                 {
                     case 1:
                         FindObjectOfType<AudioManager>().Play("Pop 1");
                         break;
                     case 2:
                         FindObjectOfType<AudioManager>().Play("Pop 2");
                         break;
                     default:
                         break;
                 }
                 FindObjectOfType<AudioManager>().Play("Jump");
             }

             pivot_collider = pivot_colliders[0];
             //Debug.Log(pivot_collider.gameObject.transform.position);
             attached = true;
         }
         else
         {
             pivot_collider = null;

             attached = false;
         }



         if (attached && !last_attached)
         {
             SetCatchState();
             rb_player.isKinematic = true;
             score_script.attached++;
             Debug.Log("Attaching");

         }
         else if (!attached && last_attached)
         {
             SetVelocityToPass();
             rb_player.isKinematic = false;
             detach_time = Time.time;
             Debug.Log("Detaching");
         }
         last_attached = attached;

         if (attached)
         {
             SetNextPos();
         }
         if (!ball_lost && !attached && !won)
         {
             if ((Time.time - detach_time) > max_time)
             {

                 /*AnalyticsResult analytics_result = Analytics.CustomEvent("Rogue Planet", new Dictionary<string, object> 
                     { 
                     {"Level",FindObjectOfType<persistent_data_script>().level_no}
                     }
                 );
                 Debug.Log("Analytics Result: " + analytics_result);*/
            Debug.Log("lost ho gaya hai");
                ball_lost = true;
                game_over_overlay.SetActive(true);
                Time.timeScale = 0.5f;

            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EEEEEEE");
            FindObjectOfType<persistent_data_script>().show_video_ad();
        }

    }

    private void FixedUpdate()
    {
        float input = Mathf.Clamp(GetInput() * 2,-1,1);
        //input = slider.value;//USE SLIDER AS INPUT
        if (pivot_collider != null)
        {
            Simulate(/*Input.GetAxis("Horizontal")*/input*50);
        }
        slider.value = input;
        //Debug.Log(input);
        
    }

    void InitModel(float inertia, float radius)
    {
        model.inertia = inertia;
        model.radius = radius;
        model.theta = Mathf.PI/2;
        model.omega = 0;
        model.alpha = 0;
    }

    void Simulate(float user_input)
    {
        float torque = rb_player.mass * -9.81f * Mathf.Sin(model.theta) * model.radius + user_input;
        model.alpha = torque / model.inertia - model.omega * model.beta;
        float dt = Time.fixedDeltaTime;
        model.omega += model.alpha * dt;
        //Debug.Log(model.omega);
        model.omega = Mathf.Clamp(model.omega,-omega_max, omega_max);
        model.theta += model.omega * dt;
        //theta *= Mathf.PI / 180f;
        model.x =  Mathf.Sin(model.theta) * model.radius;
        model.y = -Mathf.Cos(model.theta) * model.radius;

    }
    void SetCatchState()
    {
        Vector3 pivot_player_direction = (transform.position - pivot_collider.gameObject.transform.position).normalized;
        model.theta = Mathf.Atan2(pivot_player_direction.x, -pivot_player_direction.y);
        Vector3 player_circle_tangent_vector = Vector3.Cross(pivot_player_direction, Vector3.back);
        model.x = Mathf.Sin(model.theta) * model.radius;
        model.y = -Mathf.Cos(model.theta) * model.radius;
        model.omega = Vector3.Dot(player_circle_tangent_vector, rb_player.velocity)/model.radius;

        //Debug.Log("Theta: " + model.theta);
        //Debug.Log("Pivot Player Direction" + pivot_player_direction);

    }
    void SetNextPos()
    {
        transform.position = new Vector3(model.x, model.y, 0) + pivot_collider.transform.position;
    }

    void SetVelocityToPass()
    {
        Vector3 velocity_direction = Vector3.Cross(new Vector3(model.x, model.y, 0).normalized, Vector3.back); 
        rb_player.velocity = model.radius * model.omega * velocity_direction;

    }
    float GetInput()
    {
        Vector3 gravity = Input.gyro.gravity.normalized;
        //Debug.Log(gravity);
        float input = Vector3.Dot(gravity, Vector3.right);
        return input;
    }

}

