using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    // Organization
    public GameObject level;

    // On start switch
    public bool genOnStart = false;

    // Grid Properties
    public int gridSizeX, gridSizeY, gridSizeZ;
    [HideInInspector]
    public Vector3 gridSizeVector;
    public int numberOfRooms;
    private Vector3 startPos;
    // private Vector3 up, down, north, east, south, west;

    // Room Locations
    Room[,,] rooms;
    List<Vector3> takenPositions = new List<Vector3>();

    // Room Prefabs
    public GameObject blockPrefab, floorPrefab, wallPrefab, halfWallPrefab;

    // Room Contents
    public GameObject stairsPrefab;
    public int _blockChance, _blockAmount;
    private int roomType;
    public Material _redBlock;
    public Material _blueBlock;
    public Material _two;
    public Material _three;
    public Material _four;

    public delegate void directionPtr();

    directionPtr[] dirPtrs;

    

    // Rooms Dimensions
    public int roomSizeX, roomSizeY, roomSizeZ;
    [HideInInspector]
    public Vector3 roomSizeVector;

    // Room Creation Direction Chances
    public float upDownChance = 0.2f;
    public float forwardBackChance = 0.5f;
    public float positiveChance = 0.5f;

    private void Start()
    {
        //gridSizeX = (int)(gridSizeVector.x / 2);
        //gridSizeY = (int)(gridSizeVector.y / 2);
        //gridSizeZ = (int)(gridSizeVector.z / 2);
        CreateLevelHolder();
        if (genOnStart == true)
        {
            CreateRooms();
            SetRoomDoors();
            //SetStairs();
            InstantiateRooms();
        }


    }
    
    public void CreateLevelHolder()
    {
        level = new GameObject("Level");
        level.transform.position = Vector3.zero;
    }

    public void CreateRooms()
    {
        startPos = new Vector3(0, 0, 0);
        rooms = new Room[gridSizeX * 2, gridSizeY * 2, gridSizeZ * 2];
        rooms[gridSizeX, gridSizeY, gridSizeZ] = new Room(startPos, roomSizeX, roomSizeY, roomSizeZ, 0, blockPrefab, floorPrefab, wallPrefab, halfWallPrefab, stairsPrefab);
        //rooms[gridSizeX, 0, gridSizeZ].GenerateRoom();
        


        takenPositions.Insert(0, Vector3.zero);
        Vector3 checkPos = Vector3.zero;
        //Vector3 previousPosition = Vector3.zero;
        // Magic Numbers
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        // Add Rooms
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / ((float)numberOfRooms - 1);
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            
            // Grab new position
            checkPos = NewPosition();

            // Test new Position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                {
                    Debug.LogError("Could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
                }
            }
            // Finalize position
            /*
            bool willHaveStairs = false;
            if (previousPosition.y < checkPos.y)
            {
                rooms[(int)previousPosition.x + gridSizeX, (int)previousPosition.y + gridSizeY, (int)previousPosition.z + gridSizeZ].hasStairs = true;
            }
            else if (previousPosition.y > checkPos.y)
            {
                willHaveStairs = true;
            }
            previousPosition = checkPos;
            */
            roomType = Random.Range(1, 4);
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY, (int)checkPos.z + gridSizeZ] = new Room(checkPos, roomSizeX, roomSizeY, roomSizeZ, roomType, blockPrefab, floorPrefab, wallPrefab, halfWallPrefab, stairsPrefab);
            /*
            if (willHaveStairs == true)
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY, (int)checkPos.z + gridSizeZ].hasStairs = true;
            }
            */
            takenPositions.Insert(0, checkPos);
        }
    }

    Vector3 NewPosition()
    {
        int x = 0, y = 0, z = 0;
        Vector3 checkingPos = Vector3.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            z = (int)takenPositions[index].z;
            bool upDown = (Random.value < upDownChance);
            bool forwardBack = (Random.value < forwardBackChance);
            bool positive = (Random.value < positiveChance);
            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else if (forwardBack)
            {
                if (positive)
                {
                    z += 1;
                }
                else
                {
                    z -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector3(x, y, z);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY || z >= gridSizeZ || z < -gridSizeZ);
        return checkingPos;
    }

    Vector3 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0, z = 0;
        Vector3 checkingPos = Vector3.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            z = (int)takenPositions[index].z;
            bool upDown = (Random.value < upDownChance);
            bool forwardBack = (Random.value < forwardBackChance);
            bool positive = (Random.value < positiveChance);
            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else if (forwardBack)
            {
                if (positive)
                {
                    z += 1;
                }
                else
                {
                    z -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector3(x, y, z);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY || z >= gridSizeZ || z < -gridSizeZ);
        if (inc >= 100)
        {
            Debug.LogError("Could not find position with only one neighbor");
        }
        return checkingPos;
    }

    int NumberOfNeighbors(Vector3 checkingPos, List<Vector3> usedPositions)
    {
        int neighborTotal = 0;
        if (usedPositions.Contains(checkingPos + Vector3.right))
        {
            neighborTotal++;
        }
        if (usedPositions.Contains(checkingPos + Vector3.left))
        {
            neighborTotal++;
        }
        if (usedPositions.Contains(checkingPos + Vector3.forward))
        {
            neighborTotal++;
        }
        if (usedPositions.Contains(checkingPos + Vector3.back))
        {
            neighborTotal++;
        }
        return neighborTotal;
    }

    public void SetStairs()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                for (int z = 0; z < (gridSizeZ * 2); z++)
                {
                    if (rooms[x - 1, y, z] == null && rooms[x + 1, y, z] == null && rooms[x, y, z - 1] == null && rooms[x, y, z + 1] == null)
                    {
                        rooms[x, y, z].hasStairs = true;
                    }
                    if (rooms[x, y - 1, z].hasStairs)
                    {
                        rooms[x, y, z].belowHasStairs = true;
                    }
                    if (rooms[x, y + 1, z].hasStairs)
                    {
                        rooms[x, y, z].aboveHasStairs = true;
                    }
                }
            }
        }
    }

    public void foo(Room room)
    {


    }


    public void SetRoomDoors()
    {
        float doorChance = 0f;
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                for (int z = 0; z < (gridSizeZ * 2); z++)
                {
                    foo(rooms[x, y, z]);
                    if (rooms[x, y, z] == null)
                    {
                        continue;
                    }
                    if (z - 1 < 0) // Check Above
                    {
                        rooms[x, y, z].sDoor = false;
                    }
                    else
                    {
                        
                        if (rooms[x, y, z].totalDoors >= 1)
                        {
                            doorChance = Random.Range(0, 3);
                            if (doorChance > 1)
                            {
                                rooms[x, y, z].sDoor = (rooms[x, y, z - 1] != null);
                                rooms[x, y, z].totalDoors++;
                            }
                        }
                        else
                        {
                        
                        rooms[x, y, z].sDoor = (rooms[x, y, z - 1] != null);
                        rooms[x, y, z].totalDoors++;
                        }
                    }
                    if (z + 1 >= gridSizeZ * 2) // Check Below
                    {
                        rooms[x, y, z].nDoor = false;
                    }
                    else
                    {
                        
                        if (rooms[x, y, z].totalDoors >= 1)
                        {
                            doorChance = Random.Range(0, 3);
                            if (doorChance > 1)
                            {
                                rooms[x, y, z].nDoor = (rooms[x, y, z + 1] != null);
                                rooms[x, y, z].totalDoors++;
                            }
                        }
                        else
                        {
                        
                        rooms[x, y, z].nDoor = (rooms[x, y, z + 1] != null);
                        rooms[x, y, z].totalDoors++;
                        }

                    }
                    if (x - 1 < 0) // Check Left
                    {
                        rooms[x, y, z].wDoor = false;
                    }
                    else
                    {
                        
                        if (rooms[x, y, z].totalDoors >= 1)
                        {
                            doorChance = Random.Range(0, 3);
                            if (doorChance > 1)
                            {
                                rooms[x, y, z].wDoor = (rooms[x - 1, y, z] != null);
                                rooms[x, y, z].totalDoors++;
                            }
                        }
                        else
                        {
                        
                        rooms[x, y, z].wDoor = (rooms[x - 1, y, z] != null);
                        rooms[x, y, z].totalDoors++;
                        }
                    }
                    if (x + 1 >= gridSizeX * 2) // Check Right
                    {
                        rooms[x, y, z].eDoor = false;
                    }
                    else
                    {
                        
                        if (rooms[x, y, z].totalDoors >= 1)
                        {
                            doorChance = Random.Range(0, 3);
                            if (doorChance > 1)
                            {
                                rooms[x, y, z].eDoor = (rooms[x + 1, y, z] != null);
                                rooms[x, y, z].totalDoors++;
                            }
                        }
                        else
                        {
                        
                        rooms[x, y, z].eDoor = (rooms[x + 1, y, z] != null);
                        rooms[x, y, z].totalDoors++;
                        }
                    }

                    
                    // Add other half of doors
                    if (takenPositions.Contains(rooms[x, y, z].gridPos + Vector3.right))
                    {
                        if (rooms[x + 1, y, z].wDoor == true)
                        {
                            rooms[x, y, z].eDoor = true;
                            rooms[x, y, z].totalDoors++;
                        }
                    }
                    if (takenPositions.Contains(rooms[x, y, z].gridPos + Vector3.left))
                    {
                        if (rooms[x - 1, y, z].eDoor == true)
                        {
                            rooms[x, y, z].wDoor = true;
                            rooms[x, y, z].totalDoors++; ;
                        }
                    }
                    if (takenPositions.Contains(rooms[x, y, z].gridPos + Vector3.forward))
                    {
                        if (rooms[x, y, z + 1].sDoor == true)
                        {
                            rooms[x, y, z].nDoor = true;
                            rooms[x, y, z].totalDoors++;
                        }
                    }
                    if (takenPositions.Contains(rooms[x, y, z].gridPos + Vector3.back))
                    {
                        if (rooms[x, y, z - 1].nDoor == true)
                        {
                            rooms[x, y, z].sDoor = true;
                            rooms[x, y, z].totalDoors++;
                        }
                    }
                    
                }
            }
        }
    }

    public void InstantiateRooms()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                for (int z = 0; z < (gridSizeZ * 2); z++)
                {
                    if (rooms[x, y, z] == null)
                    {
                        continue;
                    }
                    else
                    {
                        rooms[x, y, z].blockAmount = _blockAmount;
                        rooms[x, y, z].blockChance = _blockChance;
                        rooms[x, y, z].GenerateRoom();
                        rooms[x, y, z].room.transform.parent = level.transform;
                        Debug.Log("(" + x + "," + y + "," + z + ") doors: " + rooms[x, y, z].totalDoors);
                    }
                }
            }
        }
    }
}