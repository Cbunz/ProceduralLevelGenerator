using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGRoom : MonoBehaviour {

    public int xTiles, yTiles, zTiles, blockAmount, blockGenerationPercentChance;
    public bool nDoor, eDoor, sDoor, wDoor;
    public GameObject blockPrefab, floorPrefab, wallPrefab, halfWallPrefab;

    List<GameObject> blockList = new List<GameObject>();

    private GameObject room, floor, walls, nWalls, eWalls, sWalls, wWalls, doors, blocks;

            
	void Start () {
        GenerateRoom();
	}
	
    void GenerateRoom()
    {
        CreateRoomHiearchy();

        for (int i=0; i < xTiles; i++)
        {
            for(int j=0; j < zTiles; j++)
            {
                for (int k=0; k < yTiles; k++)
                {
                    // West
                    if (i == 0)
                    {
                        if (wDoor)
                        {
                            if (zTiles % 2 == 0)
                            {
                                if (j == zTiles/2 - 1 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2, k * 2 + 1, j * 2 + .5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                                else if (j == zTiles / 2 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2, k * 2 + 1, j * 2 + 1.5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);

                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                            }
                            if (zTiles % 2 == 1)
                            {
                                Debug.Log("zTiles divis by 2. " + (int)(zTiles / 2 + .5));
                                if (j == (int)(zTiles / 2 + .5) && k == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 270, wallPrefab.transform.rotation.z)), wWalls.transform);
                        }
                    }

                    // East
                    if (i == xTiles - 1)
                    {
                        if (eDoor)
                        {
                            if (zTiles % 2 == 0)
                            {
                                if (j == zTiles / 2 - 1 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 + 2, k * 2 + 1, j * 2 + .5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                                else if (j == zTiles / 2 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 + 2, k * 2 + 1, j * 2 + 1.5f), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);

                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                            }
                            if (zTiles % 2 == 1)
                            {
                                Debug.Log("zTiles divis by 2. " + (int)(zTiles / 2 + .5));
                                if (j == (int)(zTiles / 2 + .5) && k == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 2, k * 2 + 1, j * 2 + 1), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 90, wallPrefab.transform.rotation.z)), eWalls.transform);
                        }
                    }

                    // South
                    if (j == 0) 
                    {
                        if (sDoor)
                        {
                            if (xTiles % 2 == 0)
                            {
                                if (i == xTiles / 2 - 1 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 +.5f, k * 2 + 1, j * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                                else if (i == xTiles / 2 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 + 1.5f, k * 2 + 1, j * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                            }
                            if (xTiles % 2 == 1)
                            {
                                Debug.Log("xTiles divis by 2. " + (int)(xTiles / 2 + .5));
                                if (i == (int)(xTiles / 2 + .5) && k == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2), Quaternion.Euler(new Vector3(wallPrefab.transform.rotation.x, 180, wallPrefab.transform.rotation.z)), sWalls.transform);
                        }
                    }

                    // North
                    if (j == zTiles - 1)
                    {
                        if (nDoor)
                        {
                            if (xTiles % 2 == 0)
                            {
                                if (i == xTiles / 2 - 1 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 + .5f, k * 2 + 1, j * 2 + 2), halfWallPrefab.transform.rotation, sWalls.transform);
                                }
                                else if (i == xTiles / 2 && k == 0)
                                {
                                    GameObject newHalfWall = Instantiate(halfWallPrefab, new Vector3(i * 2 + 1.5f, k * 2 + 1, j * 2 + 2), halfWallPrefab.transform.rotation, sWalls.transform);
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                                }
                            }
                            if (xTiles % 2 == 1)
                            {
                                Debug.Log("xTiles divis by 2. " + (int)(xTiles / 2 + .5));
                                if (i == (int)(xTiles / 2 + .5) && k == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                                }
                            }
                        }
                        else
                        {
                            GameObject newWall = Instantiate(wallPrefab, new Vector3(i * 2 + 1, k * 2 + 1, j * 2 + 2), wallPrefab.transform.rotation, nWalls.transform);
                        }
                    }
                }

                // Floor
                GameObject newFloor = Instantiate(floorPrefab, new Vector3(i * 2 + 1, 0, j * 2 + 1), floorPrefab.transform.rotation, floor.transform);
                GenerateBlock(newFloor.transform.position);
            }
        }
    }

    void CreateRoomHiearchy()
    {
        room = new GameObject("Room");
        room.transform.position = new Vector3(xTiles, 0, zTiles);
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
        blocks = new GameObject("Blocks");
        blocks.transform.parent = room.transform;
    }

    void GenerateBlock(Vector3 tentativePosition)
    {
        Vector3 blockOffset = new Vector3(.5f, .5f, .5f);
        int randomBlock = Random.Range(0, 100);
        if (randomBlock < blockGenerationPercentChance && blockList.Count < blockAmount)
        {
            GameObject newBlock = Instantiate(blockPrefab, tentativePosition + blockOffset, blockPrefab.transform.rotation, blocks.transform);
            blockList.Add(newBlock);
        }
    }
}
