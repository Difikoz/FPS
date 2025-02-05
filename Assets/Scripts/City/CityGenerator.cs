using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [ExecuteInEditMode]
    public class CityGenerator : MonoBehaviour
    {
        public int CityZoneCount = 200;
        public int BigSectorCount = 500;
        public int CellLength = 50;
        public Transform ParentRoot;
        public CityCell[] SmallCells;
        public CityCell[] LongCells;
        public CityCell[] BigCells;
        public CityCell[] AngleCells;

        private Vector3Int _parentRootPosition;
        private int _mapLength;
        private int _currentBigSectorCount;
        private int[,] _intMap;
        private List<Vector2Int> _vacant;

        public void Generate()
        {
            Clear();
            _parentRootPosition.x = Mathf.RoundToInt(ParentRoot.position.x);
            _parentRootPosition.y = Mathf.RoundToInt(ParentRoot.position.y);
            _parentRootPosition.z = Mathf.RoundToInt(ParentRoot.position.z);
            _vacant = new List<Vector2Int>();
            _mapLength = Mathf.FloorToInt(Mathf.Sqrt(CityZoneCount * 4));
            _intMap = new int[_mapLength, _mapLength];
            _intMap[_mapLength / 2, _mapLength / 2] = 1;
            _currentBigSectorCount = BigSectorCount;
            CheckNeighbour(_mapLength / 2, _mapLength / 2);
            CalculateCityZone();
            CalculateBigSectors();
            BuildCity();
        }

        public void Clear()
        {
            while (ParentRoot.childCount > 0)
            {
                DestroyImmediate(ParentRoot.GetChild(0).gameObject);
            }
        }

        private void CalculateCityZone()
        {
            for (int i = 0; i < CityZoneCount; i++)
            {
                Vector2Int Pos = _vacant[Random.Range(0, _vacant.Count - 1)];
                _vacant.Remove(Pos);
                _intMap[Pos.x, Pos.y] = 1;
                CheckNeighbour(Pos.x, Pos.y);
            }
        }

        private void CalculateBigSectors()
        {
            // scaning city zone
            for (int x = 0; x < _intMap.GetLength(0); x++)
            {
                for (int y = 0; y < _intMap.GetLength(1); y++)
                {
                    if (_intMap[x, y] == 1) _vacant.Add(new Vector2Int(x, y));
                }
            }
            // random instantiate big sectors in array
            int limit = CityZoneCount;
            while (_currentBigSectorCount > 0 && limit > 0)
            {
                int i = Random.Range(0, 3);
                switch (i)
                {
                    case (0):
                        {
                            InsertLong1x2();
                            break;
                        }
                    case (1):
                        {
                            InsertAngle();
                            break;
                        }
                    case (2):
                        {
                            InsertSqare();
                            break;
                        }
                    default:
                        break;
                }
                limit--;
            }
        }

        private void InsertLong1x2()
        {
            int k = Random.Range(0, _vacant.Count - 1);
            Vector2Int Pos = _vacant[k];
            bool rotate = Random.Range(0, 2) == 1; // 90 degrees if true
            if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x + 1, Pos.y] == 1 && !rotate)
            {
                _intMap[Pos.x, Pos.y] = 21;
                _intMap[Pos.x + 1, Pos.y] = 9;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x + 1, Pos.y));
                _currentBigSectorCount--;
            }
            else if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x, Pos.y + 1] == 1 && rotate)
            {
                _intMap[Pos.x, Pos.y] = 22;
                _intMap[Pos.x, Pos.y + 1] = 0;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x, Pos.y + 1));
                _currentBigSectorCount--;
            }
        }

        private void InsertAngle()
        {
            int k = Random.Range(0, _vacant.Count - 1);
            Vector2Int Pos = _vacant[k];
            bool rotate = Random.Range(0, 2) == 1; // -90 degrees if true
            if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x + 1, Pos.y] == 1 && _intMap[Pos.x, Pos.y + 1] == 1 && !rotate)
            {
                _intMap[Pos.x, Pos.y] = 31;
                _intMap[Pos.x + 1, Pos.y] = 0;
                _intMap[Pos.x, Pos.y + 1] = 0;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x + 1, Pos.y));
                _vacant.Remove(new Vector2Int(Pos.x, Pos.y + 1));
                _currentBigSectorCount--;
            }
            else if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x, Pos.y + 1] == 1 && _intMap[Pos.x - 1, Pos.y] == 1 && rotate)
            {
                _intMap[Pos.x, Pos.y] = 32;
                _intMap[Pos.x, Pos.y + 1] = 0;
                _intMap[Pos.x - 1, Pos.y] = 0;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x, Pos.y + 1));
                _vacant.Remove(new Vector2Int(Pos.x - 1, Pos.y));
                _currentBigSectorCount--;
            }
        }

        private void InsertSqare()
        {
            int k = Random.Range(0, _vacant.Count - 1);
            Vector2Int Pos = _vacant[k];
            bool rotate = Random.Range(0, 2) == 1; // -90 degrees if true
            if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x + 1, Pos.y] == 1 && _intMap[Pos.x, Pos.y + 1] == 1 && _intMap[Pos.x + 1, Pos.y + 1] == 1 && !rotate)
            {
                _intMap[Pos.x, Pos.y] = 41;
                _intMap[Pos.x + 1, Pos.y] = 0;
                _intMap[Pos.x, Pos.y + 1] = 0;
                _intMap[Pos.x + 1, Pos.y + 1] = 0;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x + 1, Pos.y));
                _vacant.Remove(new Vector2Int(Pos.x, Pos.y + 1));
                _vacant.Remove(new Vector2Int(Pos.x + 1, Pos.y + 1));
                _currentBigSectorCount--;
            }
            else if (_intMap[Pos.x, Pos.y] == 1 && _intMap[Pos.x - 1, Pos.y] == 1 && _intMap[Pos.x, Pos.y - 1] == 1 && _intMap[Pos.x - 1, Pos.y - 1] == 1 && rotate)
            {
                _intMap[Pos.x, Pos.y] = 42;
                _intMap[Pos.x - 1, Pos.y] = 0;
                _intMap[Pos.x, Pos.y - 1] = 0;
                _intMap[Pos.x - 1, Pos.y - 1] = 0;
                _vacant.Remove(Pos);
                _vacant.Remove(new Vector2Int(Pos.x, Pos.y - 1));
                _vacant.Remove(new Vector2Int(Pos.x - 1, Pos.y));
                _vacant.Remove(new Vector2Int(Pos.x - 1, Pos.y - 1));
                _currentBigSectorCount--;
            }
        }

        private void BuildCity()
        {
            for (int x = 1; x < _intMap.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < _intMap.GetLength(1) - 1; y++)
                {
                    int selected;
                    switch (_intMap[x, y])
                    {
                        case (1): // 1x1
                            {
                                selected = SelectPrefab(SmallCells);
                                Instantiate(SmallCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, 90 * Random.Range(0, 4), 0),
                                    ParentRoot);
                                break;
                            }
                        case (21): // 1x2
                            {
                                selected = SelectPrefab(LongCells);
                                Instantiate(LongCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, 0, 0),
                                    ParentRoot);
                                break;
                            }
                        case (22): // 1x2 rotate -90
                            {
                                selected = SelectPrefab(LongCells);
                                Instantiate(LongCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, -90, 0),
                                    ParentRoot);
                                break;
                            }
                        case (31): // angle
                            {
                                selected = SelectPrefab(AngleCells);
                                Instantiate(AngleCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, 0, 0),
                                    ParentRoot);
                                break;
                            }
                        case (32): // angle rotate -90
                            {
                                selected = SelectPrefab(AngleCells);
                                Instantiate(AngleCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, -90, 0),
                                    ParentRoot);
                                break;
                            }
                        case (41): // sqare
                            {
                                selected = SelectPrefab(BigCells);
                                Instantiate(BigCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, 0, 0),
                                    ParentRoot);
                                break;
                            }
                        case (42): // sqare shift
                            {
                                selected = SelectPrefab(BigCells);
                                Instantiate(BigCells[selected].gameObject,
                                    new Vector3Int(x - _mapLength / 2, 0, y - _mapLength / 2) * CellLength + _parentRootPosition,
                                    Quaternion.Euler(0, 180, 0),
                                    ParentRoot);
                                break;
                            }
                        default:
                            break;
                    }
                }
                // yield return new WaitForSeconds(.001f);
            }
        }

        private void CheckNeighbour(int x, int y)
        {
            if (_intMap[x, y + 1] == 0) _vacant.Add(new Vector2Int(x, y + 1)); else _vacant.Remove(new Vector2Int(x, y + 1));
            if (_intMap[x, y - 1] == 0) _vacant.Add(new Vector2Int(x, y - 1)); else _vacant.Remove(new Vector2Int(x, y - 1));
            if (_intMap[x + 1, y] == 0) _vacant.Add(new Vector2Int(x + 1, y)); else _vacant.Remove(new Vector2Int(x + 1, y));
            if (_intMap[x - 1, y] == 0) _vacant.Add(new Vector2Int(x - 1, y)); else _vacant.Remove(new Vector2Int(x - 1, y));
        }

        private int SelectPrefab(CityCell[] List)
        {
            int VeritySumm = 0;
            for (int k = 0; k < List.Length; k++)
                VeritySumm += List[k].Chance;

            int CheckSumm = 0, i = 0;
            int IntRandom = Random.Range(1, VeritySumm);
            while (CheckSumm < IntRandom)
            {
                CheckSumm += List[i].Chance;
                i++;
            }
            i--;
            return i;
        }
    }
}