using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DungeonGenerator : MonoBehaviour
    {
        public Transform ParentRoot;
        [Range(5, 500)] public int RoomCount = 10;
        public LayerMask CellLayer;
        public GameObject InsteadDoor;
        public GameObject[] DoorPrefabs;
        public DungeonCell[] CellPrefabs;

        public void Generate()
        {
            Clear();
            List<Transform> CreatedExits = new();
            DungeonCell StartRoom = Instantiate(CellPrefabs[Random.Range(0, CellPrefabs.Length)], Vector3.zero, Quaternion.identity, ParentRoot);
            for (int i = 0; i < StartRoom.Exits.Length; i++)
            {
                CreatedExits.Add(StartRoom.Exits[i].transform);
            }
            StartRoom.TriggerBox.enabled = true;
            int limit = 1000, roomsLeft = RoomCount - 1;
            while (limit > 0 && roomsLeft > 0)
            {
                limit--;
                DungeonCell selectedPrefab = Instantiate(CellPrefabs[Random.Range(0, CellPrefabs.Length)], Vector3.zero, Quaternion.identity, ParentRoot);
                int lim = 100;
                bool collided;
                Transform selectedExit;
                Transform createdExit; // из списка созданных входов
                selectedPrefab.TriggerBox.enabled = false; // чтобы сам себя не проверял на наличие нахлеста ВЫКЛЮЧИЛ
                do
                {
                    lim--;
                    createdExit = CreatedExits[Random.Range(0, CreatedExits.Count)];
                    selectedExit = selectedPrefab.Exits[Random.Range(0, selectedPrefab.Exits.Length)].transform;
                    // rotation
                    float shiftAngle = createdExit.eulerAngles.y + 180 - selectedExit.eulerAngles.y;
                    selectedPrefab.transform.Rotate(new Vector3(0, shiftAngle, 0)); // выходы повернуты друг напротив друга
                    // position
                    Vector3 shiftPosition = createdExit.position - selectedExit.position;
                    selectedPrefab.transform.position += shiftPosition; // выходы состыковались
                    // check
                    Vector3 center = selectedPrefab.transform.position + selectedPrefab.TriggerBox.center.z * selectedPrefab.transform.forward
                        + selectedPrefab.TriggerBox.center.y * selectedPrefab.transform.up
                        + selectedPrefab.TriggerBox.center.x * selectedPrefab.transform.right; // selectedPrefab.TriggerBox.center
                    Vector3 size = selectedPrefab.TriggerBox.size / 2f; // half size
                    Quaternion rot = selectedPrefab.transform.localRotation;
                    collided = Physics.CheckBox(center, size, rot, CellLayer, QueryTriggerInteraction.Collide);

                } while (collided && lim > 0);
                selectedPrefab.TriggerBox.enabled = true; // ВКЛЮЧИЛ
                if (lim > 0)
                {
                    roomsLeft--;
                    for (int j = 0; j < selectedPrefab.Exits.Length; j++)
                    {
                        CreatedExits.Add(selectedPrefab.Exits[j].transform);
                    }
                    CreatedExits.Remove(createdExit);
                    CreatedExits.Remove(selectedExit);
                    Instantiate(DoorPrefabs[Random.Range(0, DoorPrefabs.Length)], createdExit.transform.position, createdExit.transform.rotation, ParentRoot);
                    DestroyImmediate(createdExit.gameObject);
                    DestroyImmediate(selectedExit.gameObject);
                }
                else
                {
                    DestroyImmediate(selectedPrefab.gameObject);
                }
            }
            // instead doors
            for (int i = 0; i < CreatedExits.Count; i++)
            {
                Instantiate(InsteadDoor, CreatedExits[i].position, CreatedExits[i].rotation, ParentRoot);
                DestroyImmediate(CreatedExits[i].gameObject);
            }
        }

        public void Clear()
        {
            while (ParentRoot.childCount > 0)
            {
                DestroyImmediate(ParentRoot.GetChild(0).gameObject);
            }
        }
    }
}