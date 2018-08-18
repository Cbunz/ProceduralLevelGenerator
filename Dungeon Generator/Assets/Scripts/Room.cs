using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {

    // Position
    public Vector3 gridPos;
    public Vector3 worldPos;

    // Door Bools
    public bool nDoor, eDoor, sDoor, wDoor;
    public int totalDoors;

    // stair bools
    public bool hasStairs;
    public bool aboveHasStairs;
    public bool belowHasStairs;

    // Dimensions
    private int xTiles, yTiles, zTiles;
    private Vector3 roomSizeVector;

    // Room Contents
    private int roomType;
    public Color type0Color = Color.green;
    public Material redBlock;
    public Color type1Color = Color.blue;
    public Material blueBlock;
    public Color type2Color = Color.red;
    public Material two;
    public Material three;
    public Material four;
    public Color type3Color = Color.yellow;
    public Color type4Color = Color.black;
    public int blockAmount, blockChance;
    List<GameObject> blockList = new List<GameObject>();
    public GameObject levelGenerator = GameObject.Find("Level Generator");

    // Hiearchy Organizations
    public GameObject room, floor, walls, nWalls, eWalls, sWalls, wWalls, doors, stairs, blocks;

    // Room Prefabs
    private GameObject blockPrefab, floorPrefab, wallPrefab, halfWallPrefab, stairsPrefab;

    // Constructor
    public Room(Vector3 _gripPos, int _xTiles, int _yTiles, int _zTiles, int _roomType, GameObject _blockPrefab, GameObject _floorPrefab, GameObject _wallPrefab, GameObject _halfWallPrefab, GameObject _stairsPrefab)
    {
        gridPos = _gripPos;
        xTiles = _xTiles;
        yTiles = _yTiles;
        zTiles = _zTiles;
        roomSizeVector = new Vector3(xTiles, yTiles, zTiles);
        worldPos = Vector3.Scale(gridPos, 2 * roomSizeVector);
        roomType = _roomType;
        blockPrefab = _blockPrefab;
        floorPrefab = _floorPrefab;
        wallPrefab = _wallPrefab;
        halfWallPrefab = _halfWallPrefab;
        stairsPrefab = _stairsPrefab;
        totalDoors = 0;
    }

    public void GenerateRoom()
    {
        CreateRoomHiearchy();
        SetColors();

        for (int x = 0; x < xTiles; x++)
        {
            for (int y = 0; y < yTiles; y++)
            {
                for (int z = 0; z < zTiles; z++)
                {
                    // West
                    if (x == 0)
                    {
                        if (wDoor)
                        {
                            if (zTiles % 2 == 0)
                            {
                                if (z == zTiles / 2 - 1 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2, y * 2 + 1, z * 2 + .5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                                else if (z == zTiles / 2 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2, y * 2 + 1, z * 2 + 1.5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);

                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                            }
                            if (zTiles % 2 == 1)
                            {
                                if (z == (int)(zTiles / 2 + .5) && y == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                        }
                    }

                    // East
                    if (x == xTiles - 1)
                    {
                        if (eDoor)
                        {
                            if (zTiles % 2 == 0)
                            {
                                if (z == zTiles / 2 - 1 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + 2, y * 2 + 1, z * 2 + .5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                                else if (z == zTiles / 2 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + 2, y * 2 + 1, z * 2 + 1.5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);

                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                            }
                            if (zTiles % 2 == 1)
                            {
                                if (z == (int)(zTiles / 2 + .5) && y == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 2, y * 2 + 1, z * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                        }
                    }

                    // South
                    if (z == 0)
                    {
                        if (sDoor)
                        {
                            if (xTiles % 2 == 0)
                            {
                                if (x == xTiles / 2 - 1 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + .5f, y * 2 + 1, z * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                                else if (x == xTiles / 2 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + 1.5f, y * 2 + 1, z * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                            }
                            if (xTiles % 2 == 1)
                            {
                                if (x == (int)(xTiles / 2 + .5) && y == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                        }
                    }

                    // North
                    if (z == zTiles - 1)
                    {
                        if (nDoor)
                        {
                            if (xTiles % 2 == 0)
                            {
                                if (x == xTiles / 2 - 1 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + .5f, y * 2 + 1, z * 2 + 2), halfWallPrefab.transform.rotation, sWalls.transform);
                                }
                                else if (x == xTiles / 2 && y == 0)
                                {
                                    GameObject newHalfWall = GameObject.Instantiate(halfWallPrefab, room.transform.position + new Vector3(x * 2 + 1.5f, y * 2 + 1, z * 2 + 2), halfWallPrefab.transform.rotation, sWalls.transform);
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                                }
                            }
                            if (xTiles % 2 == 1)
                            {
                                if (x == (int)(xTiles / 2 + .5) && y == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = GameObject.Instantiate(wallPrefab, room.transform.position + new Vector3(x * 2 + 1, y * 2 + 1, z * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                        }
                    }

                    if (hasStairs == true)
                    {
                        // Stairs
                        GameObject newStairs = GameObject.Instantiate(stairsPrefab, room.transform.position + new Vector3(xTiles * 2 - 1, 0, zTiles * 2), stairsPrefab.transform.rotation, stairs.transform);
                        newStairs.transform.localScale += new Vector3(0, yTiles * 2 / 3, 0);
                        if (x > (xTiles - 1) && zTiles - 3 < z && z < zTiles)
                        {
                            continue;
                        }
                        else
                        {
                            // Floor
                            GameObject newFloor = GameObject.Instantiate(floorPrefab, room.transform.position + new Vector3(x * 2 + 1, 0, z * 2 + 1), floorPrefab.transform.rotation, floor.transform);
                            GenerateBlock(newFloor.transform.position);
                        }
                    }
                    if (aboveHasStairs || belowHasStairs)
                    {
                        if (x > xTiles - 1 && zTiles - 3 < z && z < zTiles)
                        {
                            continue;
                        }
                        else
                        {
                            // Floor
                            GameObject newFloor = GameObject.Instantiate(floorPrefab, room.transform.position + new Vector3(x * 2 + 1, 0, z * 2 + 1), floorPrefab.transform.rotation, floor.transform);
                            GenerateBlock(newFloor.transform.position);
                        }
                    }
                    else
                    {
                        // Floor
                        GameObject newFloor = GameObject.Instantiate(floorPrefab, room.transform.position + new Vector3(x * 2 + 1, 0, z * 2 + 1), floorPrefab.transform.rotation, floor.transform);
                        GenerateBlock(newFloor.transform.position);
                    }
                }
            }
        }
    }

    void SetColors()
    {
        LevelGenerator genScript = levelGenerator.GetComponent<LevelGenerator>();
        if (roomType == 0)
        {
            redBlock = genScript._redBlock;
            blockPrefab.GetComponent<MeshRenderer>().material = redBlock;
            wallPrefab.GetComponent<MeshRenderer>().material = redBlock;
            floorPrefab.GetComponent<MeshRenderer>().material = redBlock;
            stairsPrefab.GetComponent<MeshRenderer>().material = redBlock;
            halfWallPrefab.GetComponent<MeshRenderer>().material = redBlock;
        }
        else if (roomType == 1)
        {
            blueBlock = genScript._blueBlock;
            blockPrefab.GetComponent<MeshRenderer>().material = blueBlock;
            blockPrefab.GetComponent<MeshRenderer>().material = blueBlock;
            wallPrefab.GetComponent<MeshRenderer>().material = blueBlock;
            floorPrefab.GetComponent<MeshRenderer>().material = blueBlock;
            stairsPrefab.GetComponent<MeshRenderer>().material = blueBlock;
            halfWallPrefab.GetComponent<MeshRenderer>().material = blueBlock;
        }
        else if (roomType == 2)
        {
            two = genScript._two;
            blockPrefab.GetComponent<MeshRenderer>().material = two;
            blockPrefab.GetComponent<MeshRenderer>().material = two;
            wallPrefab.GetComponent<MeshRenderer>().material = two;
            floorPrefab.GetComponent<MeshRenderer>().material = two;
            stairsPrefab.GetComponent<MeshRenderer>().material = two;
            halfWallPrefab.GetComponent<MeshRenderer>().material = two;
        }
        else if (roomType == 3)
        {
            three = genScript._three;
            blockPrefab.GetComponent<MeshRenderer>().material = three;
            blockPrefab.GetComponent<MeshRenderer>().material = three;
            wallPrefab.GetComponent<MeshRenderer>().material = three;
            floorPrefab.GetComponent<MeshRenderer>().material = three;
            stairsPrefab.GetComponent<MeshRenderer>().material = three;
            halfWallPrefab.GetComponent<MeshRenderer>().material = three;
        }
        else if (roomType == 4)
        {
            four = genScript._four;
            blockPrefab.GetComponent<MeshRenderer>().material = four;
            blockPrefab.GetComponent<MeshRenderer>().material = four;
            wallPrefab.GetComponent<MeshRenderer>().material = four;
            floorPrefab.GetComponent<MeshRenderer>().material = four;
            stairsPrefab.GetComponent<MeshRenderer>().material = four;
            halfWallPrefab.GetComponent<MeshRenderer>().material = four;
        }
    }


    void CreateRoomHiearchy()
    {
        room = new GameObject("Room");
        room.transform.position = worldPos;
        floor = new GameObject("Floor");
        floor.transform.parent = room.transform;
        walls = new GameObject("Walls");
        walls.transform.parent = room.transform;
        nWalls = new GameObject("North Walls");
        nWalls.transform.parent = walls.transform;
        eWalls = new GameObject("East Walls");
        eWalls.transform.parent = walls.transform;
        sWalls = new GameObject("South Walls");
        sWalls.transform.parent = walls.transform;
        wWalls = new GameObject("West Walls");
        wWalls.transform.parent = walls.transform;
        doors = new GameObject("Doors");
        doors.transform.parent = room.transform;
        stairs = new GameObject("Stairs");
        stairs.transform.parent = room.transform;
        blocks = new GameObject("Blocks");
        blocks.transform.parent = room.transform;
    }
    
    void GenerateBlock(Vector3 tentativePosition)
    {
        Vector3 blockOffset = new Vector3(.5f, .5f, .5f);
        int randomBlock = Random.Range(0, 100);
        if (randomBlock < blockChance && blockList.Count < blockAmount)
        {
            GameObject newBlock = GameObject.Instantiate(blockPrefab, tentativePosition + blockOffset, blockPrefab.transform.rotation, blocks.transform);
            blockList.Add(newBlock);
        }
    }
}
