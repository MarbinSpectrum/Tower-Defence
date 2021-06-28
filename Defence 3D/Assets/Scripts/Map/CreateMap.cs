using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : Singleton<CreateMap>
{
    public static void Create() => Instance.pCreate();

    public GameObject grass;
    public GameObject road;
    public GameObject corner;
    public GameObject endRoad;

    public const int R = 5;
    public const int C = 5;
    public const int DIS = 2;
    public const int POINT = R * C;
    public const int DESTINATION = POINT - (((R % 2) == (C % 2) && (R % 2 == 0)) ? 1 : 0) * (C - 1);

    private bool [] Visit = new bool[POINT];
    private List<int>[] V = new List<int>[POINT];
    private int[] dx = { 0, 1, 0, -1 };
    private int[] dy = { 1, 0, -1, 0 };

    private List<List<int>> route = new List<List<int>>();
    private int[] routeTemp = new int[POINT];
    private List<int> nowRoute = new List<int>();
    private GameObject[,] tile = new GameObject[(C + 1) * DIS + 1, (R + 1) * DIS + 1];
    private int[] nextMovePos = new int[POINT + 1];

    public static Vector3 SpawnPos;
    public static Vector3 EndPos;

    private void pCreate()
    {
        for (int i = 0; i < POINT; i++)
        {
            int x = i % C;
            int y = i / C;
            for (int j = 0; j < 4; j++)
            {
                int ax = x + dx[j];
                int ay = y + dy[j];
                if (ax < 0 || ax >= C || ay < 0 || ay >= R)
                    continue;
                if (V[i] == null)
                    V[i] = new List<int>();
                V[i].Add(ax + ay * C);
            }
        }
        routeTemp[0] = 1;
        Visit[0] = true;
        MakeRoute(1, 1);

        nowRoute = route[Random.Range(0, route.Count)];

        for (int i = nowRoute.Count - 1; i >= 1; i--)
            nextMovePos[nowRoute[i]] = nowRoute[i - 1];

        for (int y = 1; y < (R + 1) * DIS; y++)
            for (int x = 1; x < (C + 1) * DIS; x++)
            {
                if (x % DIS == 0 && y % DIS == 0)
                    tile[x, y] = Instantiate(road, new Vector3(x, 0, y), Quaternion.identity);
                else
                    tile[x, y] = Instantiate(grass, new Vector3(x, 0, y), Quaternion.identity);
            }
        int backJ = -1;
        for (int i = 1; i < nowRoute.Count; i++)
        {
            int s = nowRoute[i - 1] - 1;
            int e = nowRoute[i] - 1;
            int x = s % C;
            int y = s / C;
            for (int j = 0; j < 4; j++)
            {
                int ax = x + dx[j];
                int ay = y + dy[j];

                if (ax == e % C && ay == e / C)
                {
                    int sX = (x + 1) * DIS;
                    int sY = (y + 1) * DIS;
                    int eX = ((e % C) + 1) * DIS;
                    int eY = ((e / C) + 1) * DIS;

                    while (sX != eX || sY != eY)
                    {
                        tile[sX, sY].SetActive(false);
                        tile[sX, sY] = Instantiate(road, tile[sX, sY].transform.position, Quaternion.Euler(0, j * 90, 0));
                        sX += dx[j];
                        sY += dy[j];
                    }

                    sX = (x + 1) * DIS;
                    sY = (y + 1) * DIS;

                    if (backJ != j)
                    {
                        tile[sX, sY].SetActive(false);
                        if (backJ == -1)
                            tile[sX, sY] = Instantiate(corner, tile[sX, sY].transform.position, Quaternion.Euler(0, j * 90, 0));
                        else if (j == (backJ + 1) % 4)
                            tile[sX, sY] = Instantiate(corner, tile[sX, sY].transform.position, Quaternion.Euler(0, (j + 1) * 90, 0));
                        else if (j == (backJ + 3) % 4)
                            tile[sX, sY] = Instantiate(corner, tile[sX, sY].transform.position, Quaternion.Euler(0, (j) * 90, 0));
                    }

                    if (i == 1)
                    {
                        int roadLen = 7;
                        while (roadLen-- > 0)
                        {
                            GameObject temp = Instantiate(road, new Vector3(sX, 0, sY), Quaternion.Euler(0, j * 90, 0)); ;
                            
                            if(1 <= sX && sX < (C + 1) * DIS + 1 && 1 <= sY && sY < (R + 1) * DIS + 1)
                            {
                                if(tile[sX, sY] != null)
                                {
                                    tile[sX, sY].SetActive(false);
                                    tile[sX, sY] = temp;
                                }
                            }
                            sX -= dx[j];
                            sY -= dy[j];
                            EndPos = new Vector3(sX, 0.2f, sY);
                        }
                    }
                    else if (i == POINT - 1)
                    {
                        int roadLen = 7;
                        while (roadLen-- > 0)
                        {
                            GameObject temp = Instantiate(road, new Vector3(eX, 0, eY), Quaternion.Euler(0, 180 + j * 90, 0)); ;

                            if (1 <= eX && eX < (C + 1) * DIS + 1 && 1 <= eY && eY < (R + 1) * DIS + 1)
                            {
                                if (tile[eX, eY] != null)
                                {
                                    tile[eX, eY].SetActive(false);
                                    tile[eX, eY] = temp;
                                }
                            }
                            eX += dx[j];
                            eY += dy[j];
                        SpawnPos = new Vector3(eX,0.2f, eY);
                        }

                    }

                    backJ = j;

                    break;
                }
            }
        }


        for (int y = 1; y < (R + 1) * DIS; y++)
            for (int x = 1; x < (C + 1) * DIS; x++)
                if (tile[x, y] != null)
                    if (tile[x, y].name.Contains("Tile_Set"))
                        TileManager.Instance.tile[x, y] = tile[x, y].GetComponent<Tile>();

    }
    private void MakeRoute(int now, int deep)
    {
        if (deep == POINT)
        {
            if (now == DESTINATION)
            {
                List<int> temp = new List<int>();
                foreach (int i in routeTemp)
                    temp.Add(i);
                route.Add(temp);
            }
        }
        else
        {
            foreach (int i in V[now - 1])
            {
                if (Visit[i])
                    continue;
                Visit[i] = true;
                routeTemp[deep] = i + 1;
                MakeRoute(i + 1, deep + 1);
                Visit[i] = false;
            }
        }
    }

    private int pNextMovePos(int n)
    {
        if (n == 0)
            return -1;
        if (n <= 0 || n > POINT)
            return DESTINATION;
        return nextMovePos[n];
    }

    public static int NextMovePos(int n)
    {
        return Instance.pNextMovePos(n);
    }

    public static Vector3 GetPosVector(int n)
    {
        return new Vector3((((n - 1) % C) + 1) * DIS, 0, (((n - 1) / C) + 1) * DIS);
    }
}
