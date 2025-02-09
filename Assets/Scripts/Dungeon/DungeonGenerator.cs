using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DungeonGenerator : MonoBehaviour
    {
        public Transform ParentRoot;
        [Range(5, 500)] public int RoomCount = 10;
        public LayerMask CellLayer;
        public bool SpawnFirstRoom = true;
        public GameObject FirstRoom;
        public GameObject InsteadDoor;
        public GameObject[] DoorPrefabs;
        public GameObject[] CellPrefabs;

        public void Generate()
        {
            Clear();
            List<Transform> CreatedExits = new();
            DungeonCell StartRoom;
            if (SpawnFirstRoom && FirstRoom != null)
            {
                StartRoom = Instantiate(FirstRoom, Vector3.zero, Quaternion.identity, ParentRoot).GetComponent<DungeonCell>();
            }
            else
            {
                StartRoom = Instantiate(CellPrefabs[Random.Range(0, CellPrefabs.Length)], Vector3.zero, Quaternion.identity, ParentRoot).GetComponent<DungeonCell>();
            }
            for (int i = 0; i < StartRoom.Exits.Length; i++)
            {
                CreatedExits.Add(StartRoom.Exits[i].transform);
            }
            StartRoom.TriggerBox.enabled = true;
            int limit = 1000, roomsLeft = RoomCount - 1;
            while (limit > 0 && roomsLeft > 0)
            {
                limit--;
                DungeonCell selectedRoom = Instantiate(CellPrefabs[SelectPrefab(CellPrefabs)], Vector3.zero, Quaternion.identity, ParentRoot).GetComponent<DungeonCell>();
                int lim = 100;
                bool collided;
                Transform selectedExit;
                Transform createdExit;
                selectedRoom.TriggerBox.enabled = false;
                do
                {
                    lim--;
                    createdExit = CreatedExits[Random.Range(0, CreatedExits.Count)];
                    selectedExit = selectedRoom.Exits[Random.Range(0, selectedRoom.Exits.Length)].transform;
                    // rotation
                    float shiftAngle = createdExit.eulerAngles.y + 180 - selectedExit.eulerAngles.y;
                    selectedRoom.transform.Rotate(new Vector3(0, shiftAngle, 0));
                    // position
                    Vector3 shiftPosition = createdExit.position - selectedExit.position;
                    selectedRoom.transform.position += shiftPosition;
                    // check
                    Vector3 center = selectedRoom.transform.position + selectedRoom.TriggerBox.center.z * selectedRoom.transform.forward
                        + selectedRoom.TriggerBox.center.y * selectedRoom.transform.up
                        + selectedRoom.TriggerBox.center.x * selectedRoom.transform.right;
                    Vector3 size = selectedRoom.TriggerBox.size / 2f;
                    Quaternion rot = selectedRoom.transform.localRotation;
                    collided = Physics.CheckBox(center, size, rot, CellLayer, QueryTriggerInteraction.Collide);

                } while (collided && lim > 0);
                selectedRoom.TriggerBox.enabled = true;
                if (lim > 0)
                {
                    roomsLeft--;
                    for (int j = 0; j < selectedRoom.Exits.Length; j++)
                    {
                        CreatedExits.Add(selectedRoom.Exits[j].transform);
                    }
                    CreatedExits.Remove(createdExit);
                    CreatedExits.Remove(selectedExit);
                    Instantiate(DoorPrefabs[Random.Range(0, DoorPrefabs.Length)], createdExit.transform.position, createdExit.transform.rotation, ParentRoot);
                    DestroyImmediate(createdExit.gameObject);
                    DestroyImmediate(selectedExit.gameObject);
                }
                else
                {
                    DestroyImmediate(selectedRoom.gameObject);
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

        private int SelectPrefab(GameObject[] List)
        {
            int VeritySumm = 0;
            for (int k = 0; k < List.Length; k++)
                VeritySumm += List[k].GetComponent<DungeonCell>().Chance;

            int CheckSumm = 0, i = 0;
            int IntRandom = Random.Range(1, VeritySumm);
            while (CheckSumm < IntRandom)
            {
                CheckSumm += List[i].GetComponent<DungeonCell>().Chance;
                i++;
            }
            i--;
            return i;
        }
    }
}