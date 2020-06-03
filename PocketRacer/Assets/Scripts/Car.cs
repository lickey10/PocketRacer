using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CarClass
{
    public class Car : MonoBehaviour
    {
        public string CarName = "Car";
        public List<GameObject> Landmines = new List<GameObject>();
        public GameObject DropPoint;
        public int currentWaypoint;
        public int currentLap = 1;
        public Transform lastWaypoint;
        public bool Stopped = false;
        public Vector3 VehiclePositionAtLastWaypoint;

        private int WaypointCount = 0; //Set the amount of Waypoints
        private static int WAYPOINT_VALUE = 100;
        private static int LAP_VALUE = 10000;
        private int cpt_waypoint = 0;
        private float lastDistance = 0;
        private float distanceVariance = 5;
        private bool gameOverTimerActive = false;
        float gameOverTimer = 30.0f;
        bool destroyed = false;

        // Use this for initialization
        public void Initialize()
        {

        }

        private void Start()
        {
            currentWaypoint = 0;
            currentLap = 1;
            cpt_waypoint = 0;

            WaypointCount = GameObject.FindGameObjectsWithTag("TrackWaypoint").Length;
        }

        private void Update()
        {
            if (Input.GetButton("Drop"))
            {
                //if we have a landmine drop it
                if (Landmines.Count > 0)
                {
                    GameObject.Instantiate(Landmines[0], DropPoint.transform.position, DropPoint.transform.rotation);

                    Landmines.RemoveAt(0);
                }
            }

            checkForMovement(GetDistance());

            if (gameOverTimerActive)
            {
                gameOverTimer -= Time.deltaTime;

                if (gameOverTimer < 0)
                {
                    gameOverTimerActive = false;
                    gameOverTimer = 30.0f;

                    //StopCar();
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TrackWaypoint")
            {
                string otherName = other.gameObject.name.Replace("TrackWaypoint_", "");

                if (int.TryParse(otherName, out currentWaypoint))
                {
                    if (cpt_waypoint == WaypointCount)
                    {
                        // completed a lap, so increase currentLap;
                        currentLap++;
                        cpt_waypoint = 0;

                        if (currentLap > 1)// don't reload on first lap
                            ReloadWeapons();
                    }
                    if (cpt_waypoint == currentWaypoint - 1)
                    {
                        lastWaypoint = other.transform;
                        VehiclePositionAtLastWaypoint = transform.position;
                        cpt_waypoint++;
                    }
                }
            }
        }

        public float GetDistance()
        {
            if (transform == null)
                return 0;

            if (lastWaypoint != null && transform.gameObject.activeSelf)
                return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
            else if (!transform.gameObject.activeSelf)
                return lastDistance;
            else
                return 0;
        }

        public int GetCarPosition(Car[] allCars)
        {
            float distance = GetDistance();
            int position = 1;

            foreach (Car car in allCars)
            {
                if (car != null && car.GetDistance() > distance)
                    position++;
            }
            return position;
        }

        private void checkForMovement(float distance)
        {
            if (Mathf.Abs(distance - lastDistance) < distanceVariance)//we haven't moved much since last check
            {
                //start a timer
                gameOverTimerActive = true;

            }
            else
            {
                gameOverTimerActive = false;
                gameOverTimer = 30.0f;
            }
        }

        public void StopCar()
        {
            Debug.Log("hi");

            if (gameObject.tag == "Player")
            {
                VehicleBehaviour.WheelVehicle wheelVehicle = gameObject.GetComponentInChildren<VehicleBehaviour.WheelVehicle>();
                wheelVehicle.Steering = 5;
                wheelVehicle.Handbrake = true;
            }
            else if (gameObject.tag == "AI")
            {
                UnityStandardAssets.Vehicles.Car.CarAIControl carAIControl = gameObject.GetComponentInChildren<UnityStandardAssets.Vehicles.Car.CarAIControl>();

                carAIControl.Driving = false;
            }

            Stopped = true;
        }

        public void ReloadWeapons()
        {
            Weapon weapon = gameObject.GetComponentInChildren<Weapon>();

            if (weapon != null)
                weapon.Reload();
        }

        public void DestroyVehicle(bool respawn)
        {
            if (respawn && !destroyed)
            {
                destroyed = true;
                FindObjectOfType<RaceManager>().RespawnVehicle(transform.root.gameObject, 1, VehiclePositionAtLastWaypoint);
            }

            Destroy(transform.root.gameObject);
        }
    }
}