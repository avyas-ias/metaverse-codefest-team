using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        public CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        private Vector3 city_fps_starter = new Vector3(1489.63879f,72.651001f,-2395.80835f);
        private Vector3 town_fps_starter = new Vector3(1380f,18.5f,951.5f);
        private Vector3 platform_fps_starter = new Vector3(-409f,750f,-1614f);

        private List<string> slides = new List<string>{"s1","s2","s3","s4","s5","s6","s7","s8","s9","s10","s11"};
        private List<GameObject> slideObj = new List<GameObject>();

        private List<string> ads = new List<string>{"ad1","ad2","ad3","ad4","ad5","ad6","ad7","ad8","ad9","ad10","ad11","ad12","ad13","ad14"};
        private List<GameObject> adObj = new List<GameObject>();

        private int slideCounter = -1;
        private int adCounter = 0;

        

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            // GameObject s1 = GameObject.Find("s1");
            // GameObject s2 = GameObject.Find("s2");
            // GameObject s3 = GameObject.Find("s3");
            // GameObject s4 = GameObject.Find("s4");
            // GameObject s5 = GameObject.Find("s5");
            // GameObject s6 = GameObject.Find("s6");
            // GameObject s7 = GameObject.Find("s7");
            // GameObject s8 = GameObject.Find("s8");
            // GameObject s9 = GameObject.Find("s9");
            // GameObject s10 = GameObject.Find("s10");
            // GameObject s11 = GameObject.Find("s11");

            GameObject.Find(ads[0]).SetActive(false);

            for(int i=0;i<ads.Count;i++){
                adObj.Add(GameObject.Find(ads[i]));
                // if(i>0)
                try{
                    adObj[i].SetActive(false);
                } catch (Exception e) {
                    Debug.Log("...");
                }
                // adObj[i].material.color.a = 0.0f;
            }
            
            for(int i=0;i<=10;i++){
                slideObj.Add(GameObject.Find(slides[i]));
                // if(i>0)
                    slideObj[i].SetActive(false);
            }
            // slideObj[0].SetActive(true);
        }


        // Update is called once per frame
        private void Update()
        {   
            GameObject slide;

            if(Input.GetKeyUp(KeyCode.U)){
                if(adCounter>0){
                    adObj[adCounter-1].SetActive(false);
                    adObj[adCounter-2].SetActive(false);
                    adCounter -= 2;
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2))
            {
                if(Input.GetKeyUp(KeyCode.Alpha1)){
                    adObj[adCounter].SetActive(true);
                } else if(Input.GetKeyUp(KeyCode.Alpha2)) {
                    adObj[adCounter+1].SetActive(true);
                }
                adCounter += 2;

                // slideCounter += 1;
                // // slide=GameObject.Find(slides[slideCounter]);
                // slide=slideObj[slideCounter];
                // slide.SetActive(false);
                // slideObj[slideCounter+1].SetActive(true);
                // if(slideCounter>=10)
                //     slideCounter = -1;
            }

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            // Debug.Log("Collisding with" + hit.collider.gameObject.name);
            if(hit.collider.gameObject.name.Equals("picture3")){
                // Debug.Log("teleporting !");
                // m_CharacterController.transform.position = new Vector3(-78.7058105f,9.99665833f,154.828369f);
                // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":1380.407958984375,"y":18.290468215942384,"z":931.1295776367188},"rotation":{"x":0.0,"y":0.3691137135028839,"z":0.0,"w":-0.9293842911720276},"scale":{"x":1.0,"y":1.0,"z":1.0}}

                m_CharacterController.transform.position = town_fps_starter;
                m_CharacterController.height = 10;
                m_WalkSpeed = 10;
            } else if(hit.collider.gameObject.name.Equals("picture4")) {
                m_CharacterController.transform.position = city_fps_starter;
                m_CharacterController.height = 0.3f;
                m_WalkSpeed = 5;
            }

            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
