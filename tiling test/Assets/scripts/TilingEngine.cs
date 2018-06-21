using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean;
using System.IO;

public class TilingEngine : MonoBehaviour
{
    public List<TileSprite> TileSprites;
    public Vector2 MapSize;
    public Sprite DefaultImage;
    public GameObject TileContainerPrefab;
    public GameObject TilePrefab;
    public Vector2 CurrentPosition;
    public Vector2 ViewPortSize;

    private TileSprite[,] _map;
    private GameObject controller;
    private GameObject _tileContainer;
    private List<GameObject> _tiles = new List<GameObject>();

    void Awake()
    {
        Application.targetFrameRate = 300;
    }

    public void Start()
    {
        controller = GameObject.Find("Controller");
        _map = new TileSprite[(int)MapSize.x, (int)MapSize.y];

        DefaultTiles();
        SetTiles();
    }

    private void DefaultTiles()
    {
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                _map[x, y] = new TileSprite("unset", DefaultImage, Tiles.Unset);
            }
        }
    }

    private void SetTiles() // it shoult be noted that the map is generated upside down so some values dont matched their intended image **
    {                       // 
        var index = 0;
        for (var y = 0; y < MapSize.y - 1; y++)
        {
            for (var x = 0; x < MapSize.x - 1; x++)
            {
                string sourceFilePath = @"C:\Users\Dagon\Documents\Visual Studio 2017\Projects\csv\csv\bin\Debug\file.txt";
                var item = File.ReadAllText(sourceFilePath);
                var nitem = item.Split(',');
                int xcord = (y * ((int)MapSize.x - 1)) + x ;
                var tim = nitem[xcord];    
                System.Int32.TryParse(tim,out index);
                if (index == 4)
                {
                    index = AssembleRoad(nitem, xcord, index);                   
                }
                else if (index == 5)
                {
                    index = AssembleRiver(nitem, xcord, index);
                }
                _map[x, y] = new TileSprite(TileSprites[index].Name, TileSprites[index].TileImage, TileSprites[index].TileType);
            }
            
        }
    }

    private int AssembleRoad(string[] nitem, int xcord, int index) // NTS: shift numbers up to make room for other base sprites.
    {
        var item1 = nitem[(xcord - 1)]; //west
        var item2 = nitem[(xcord + 1)]; //east
        var item3 = nitem[(xcord - ((int)MapSize.x - 1))]; //north
        var item4 = nitem[(xcord + ((int)MapSize.x - 1))]; //south

        var num1 = 0;
        var num2 = 0;
        var num3 = 0;
        var num4 = 0;

        System.Int32.TryParse(item1, out num1);
        System.Int32.TryParse(item2, out num2);
        System.Int32.TryParse(item3, out num3);
        System.Int32.TryParse(item4, out num4);

        bool b1 = num1 == 4;
        bool b2 = num2 == 4;
        bool b3 = num3 == 4;
        bool b4 = num4 == 4;

        if (!b1 && !b2 && !b3 && !b4) // neither value round the object is true
        {
            index = 4;
        }
        else if (b1 && !b2 && !b3 && !b4) // west object true only
        {
            index = 23;
        }
        else if (!b1 && b2 && !b3 && !b4) // east object true only
        {
            index = 23;
        }
        else if (!b1 && !b2 && b3 && !b4) // north onject true only
        {
            index = 24;
        }
        else if (!b1 && !b2 && !b3 && b4) // south object true only
        {
            index = 24;
        }
        else if (b1 && b2 && !b3 && !b4) // east and west objects true
        {
            index = 23;
        }
        else if (!b1 && !b2 && b3 && b4) // north and south objects true
        {
            index = 24;
        }
        else if (b1 && !b2 && b3 && !b4) // east and north objects true **
        {
            index = 28;
        }
        else if (b1 && !b2 && !b3 && b4) // east and south objects true **
        {
            index = 27;
        }
        else if (!b1 && b2 && b3 && !b4) // west and north objects true **
        {
            index = 26;
        }
        else if (!b1 && b2 && !b3 && b4) // West and south objects true **
        {
            index = 25;
        }
        else if (b1 && !b2 && b3 && b4) // West and south and North objects true
        {
            index = 29;
        }
        else if (b1 && b2 && b3 && !b4) // West and south and East objects true
        {
            index = 30;
        }
        else if (!b1 && b2 && b3 && b4) // North and south and East objects true
        {
            index = 31;
        }
        else if (b1 && b2 && !b3 && b4) // West and North and East objects true
        {
            index = 32;
        }
        else if (b1 && b2 && b3 && b4)
        {
            index = 4;
        }
        else // failed result 
        {
            Debug.Log("road tiler failed to match a result");
            index = 4;
        }
        return (index);
    }

    private int AssembleRiver(string[] nitem, int xcord, int index)
    {
        var item1 = nitem[(xcord - 1)]; //west
        var item2 = nitem[(xcord + 1)]; //east
        var item3 = nitem[(xcord - ((int)MapSize.x - 1))]; //north
        var item4 = nitem[(xcord + ((int)MapSize.x - 1))]; //south

        var num1 = 0;
        var num2 = 0;
        var num3 = 0;
        var num4 = 0;

        System.Int32.TryParse(item1, out num1);
        System.Int32.TryParse(item2, out num2);
        System.Int32.TryParse(item3, out num3);
        System.Int32.TryParse(item4, out num4);

        bool b1 = num1 == 5;
        bool b2 = num2 == 5;
        bool b3 = num3 == 5;
        bool b4 = num4 == 5;
        bool b5 = num1 == 1;
        bool b6 = num2 == 1;
        bool b7 = num3 == 1;
        bool b8 = num4 == 1;

        if (!b1 && !b2 && !b3 && !b4) // neither value round the object is true
        {
            index = 5;
        }
        else if (b1 && !b2 && !b3 && !b4 && b6) // west object true only
        {
            index = 45;
        }
        else if (b1 && !b2 && !b3 && !b4 && !b6) // west object true only
        {
            index = 33;
        }
        else if (!b1 && b2 && !b3 && !b4 && b5) // east object true only
        {
            index = 46;
        }
        else if (!b1 && b2 && !b3 && !b4 && !b5) // east object true only
        {
            index = 33;
        }
        else if (!b1 && !b2 && b3 && !b4 && b8) // north onject true only
        {
            index = 43;
        }
        else if (!b1 && !b2 && b3 && !b4 && !b8) // north onject true only
        {
            index = 34;
        }
        else if (!b1 && !b2 && !b3 && b4 && b7) // south object true only
        {
            index = 44;
        }
        else if (!b1 && !b2 && !b3 && b4 && !b7) // south object true only
        {
            index = 34;
        }
        else if (b1 && b2 && !b3 && !b4) // east and west objects true
        {
            index = 33;
        }
        else if (!b1 && !b2 && b3 && b4) // north and south objects true
        {
            index = 34;
        }
        else if (b1 && !b2 && b3 && !b4) // east and north objects true **
        {
            index = 38;
        }
        else if (b1 && !b2 && !b3 && b4) // east and south objects true **
        {
            index = 37;
        }
        else if (!b1 && b2 && b3 && !b4) // west and north objects true **
        {
            index = 36;
        }
        else if (!b1 && b2 && !b3 && b4) // West and south objects true **
        {
            index = 35;
        }
        else if (b1 && !b2 && b3 && b4) // West and south and North objects true
        {
            index = 39;
        }
        else if (b1 && b2 && b3 && !b4) // West and south and East objects true
        {
            index = 40;
        }
        else if (!b1 && b2 && b3 && b4) // North and south and East objects true
        {
            index = 41;
        }
        else if (b1 && b2 && !b3 && b4) // West and North and East objects true
        {
            index = 42;
        }
        else if (b1 && b2 && b3 && b4)
        {
            index = 5;
        }
        else // failed result 
        {
            Debug.Log("river tiler failed to match a result");
            index = 5;
        }
        return (index);
    }

    private int AssembleShore(string[] nitem, int xcord, int index) 
    {
        var item1 = nitem[(xcord - 1)];                        //west
        var item2 = nitem[(xcord + 1)];                        //east
        var item3 = nitem[(xcord - ((int)MapSize.x - 1))];     //north
        var item4 = nitem[(xcord + ((int)MapSize.x - 1))];     //south
        var item5 = nitem[(xcord - ((int)MapSize.x - 1) - 1)]; //north west
        var item6 = nitem[(xcord - ((int)MapSize.x - 1) + 1)]; //north east
        var item7 = nitem[(xcord + ((int)MapSize.x - 1) - 1)]; //south west
        var item8 = nitem[(xcord + ((int)MapSize.x - 1) + 1)]; //south east

        var num1 = 0;
        var num2 = 0;
        var num3 = 0;
        var num4 = 0;
        var num5 = 0;
        var num6 = 0;
        var num7 = 0;
        var num8 = 0;

        System.Int32.TryParse(item1, out num1);
        System.Int32.TryParse(item2, out num2);
        System.Int32.TryParse(item3, out num3);
        System.Int32.TryParse(item4, out num4);
        System.Int32.TryParse(item5, out num5);
        System.Int32.TryParse(item6, out num6);
        System.Int32.TryParse(item7, out num7);
        System.Int32.TryParse(item8, out num8);

        bool b1 = num1 == 6;
        bool b2 = num2 == 6;
        bool b3 = num3 == 6;
        bool b4 = num4 == 6;

        bool l1 = false;
        bool l2 = false;
        bool l3 = false;
        bool l4 = false;
        bool l5 = false;
        bool l6 = false;
        bool l7 = false;
        bool l8 = false;

        bool w1 = false;
        bool w2 = false;
        bool w3 = false;
        bool w4 = false;
        bool w5 = false;
        bool w6 = false;
        bool w7 = false;
        bool w8 = false;

        if ((num1 == 0) || (num1 >= 2 && num1 <= 5) || (num1 >= 8 && num1 <= 22)) // value is a land based tile
        {
            l1 = true;
        }
        if ((num2 == 0) || (num2 >= 2 && num2 <= 5) || (num2 >= 8 && num2 <= 22)) // value is a land based tile
        {
            l2 = true;
        }
        if ((num3 == 0) || (num3 >= 2 && num3 <= 5) || (num3 >= 8 && num3 <= 22)) // value is a land based tile
        {
            l3 = true;
        }
        if ((num4 == 0) || (num4 >= 2 && num4 <= 5) || (num4 >= 8 && num4 <= 22)) // value is a land based tile
        {
            l4 = true;
        }
        if ((num5 == 0) || (num5 >= 2 && num5 <= 5) || (num5 >= 8 && num5 <= 22)) // value is a land based tile
        {
            l5 = true;
        }
        if ((num6 == 0) || (num6 >= 2 && num6 <= 5) || (num6 >= 8 && num6 <= 22)) // value is a land based tile
        {
            l6 = true;
        }
        if ((num3 == 0) || (num3 >= 2 && num3 <= 5) || (num3 >= 8 && num3 <= 22)) // value is a land based tile
        {
            l7 = true;
        }
        if ((num4 == 0) || (num4 >= 2 && num4 <= 5) || (num4 >= 8 && num4 <= 22)) // value is a land based tile
        {
            l8 = true;
        }
        if (num1 == 1 || num1 == 7) // value is water of reef
        {
            w1 = true;
        }
        if (num2 == 1 || num2 == 7) // value is water of reef
        {
            w2 = true;
        }
        if (num3 == 1 || num3 == 7) // value is water of reef
        {
            w3 = true;
        }
        if (num4 == 1 || num4 == 7) // value is water of reef
        {
            w4 = true;
        }

        else // failed result 
        {
            Debug.Log("road tiler failed to match a result");
            index = 4;
        }
        return (index);
    }

    private void Update()
    {
            AddTilesToWorld();
    }

    private void AddTilesToWorld()
    {
        foreach (GameObject o in _tiles)
        {
            LeanPool.Despawn(o);
        }
        _tiles.Clear();
        LeanPool.Despawn(_tileContainer);
        _tileContainer = LeanPool.Spawn(TileContainerPrefab);
        var tileSize = .64f;
        var viewOffsetX = ViewPortSize.x / 2f;
        var viewOffsetY = ViewPortSize.y / 2f;
        for (var y = -viewOffsetY; y < viewOffsetY; y++)
        {
            for (var x = -viewOffsetX; x < viewOffsetX;x++)
            {
                var tX = x * tileSize;
                var tY = y * tileSize;

                var iX = x + CurrentPosition.x;
                var iY = y + CurrentPosition.y;

                if (iX < 0) continue;
                if (iY < 0) continue;
                if (iX > MapSize.x - 2) continue;
                if (iY > MapSize.y - 2) continue;
                
                var t = LeanPool.Spawn(TilePrefab);
                t.transform.position = new Vector3(tX, tY, 0);
                t.transform.SetParent(_tileContainer.transform);
                var renderer = t.GetComponent<SpriteRenderer>();
                renderer.sprite = _map[(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].TileImage;
                _tiles.Add(t);
              
            }
        }
    }

    private TileSprite FindTile(Tiles tile)
    {
        foreach (TileSprite tileSprite in TileSprites)
        {
            if (tileSprite.TileType == tile) return tileSprite;
        }
        return null;
    }
}