using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class WFC3DModel
{

    public const int pX = 0;
    public const int pY = 1;
    public const int nX = 2;
    public const int nY = 3;
    public const int pZ = 4;
    public const int nZ = 5;
    public const string CONSTRAINT_BOTTOM = "bot";
    public const string CONSTRAINT_TOP = "top";
    private Vector3Int _size;
    public Stack<Vector3Int> stack = new Stack<Vector3Int>();

    public List<List<List<Dictionary<string, PrototypeData>>>> waveFunction = new List<List<List<Dictionary<string, PrototypeData>>>>();

    private Dictionary<Vector3Int, int> directionToIndex = new Dictionary<Vector3Int, int>()
    {
        {Vector3Int.left , 2},
        {Vector3Int.right , 0},
        {Vector3Int.back , 1}, // should be 3?
	    {Vector3Int.forward , 3}, // should be 1?
	    {Vector3Int.up , 4},
        {Vector3Int.down , 5}
    };


    public WFC3DModel(Vector3Int size, TextAsset prototypes)
    {
        _size = size;
        for (int z = 0; z < _size.z; z++)
        {
            List<List<Dictionary<string, PrototypeData>>> yArr = new List<List<Dictionary<string, PrototypeData>>>();
            for (int y = 0; y < _size.y; y++)
            {
                List<Dictionary<string, PrototypeData>> xArr = new List<Dictionary<string, PrototypeData>>();
                for (int x = 0; x < size.x; x++)
                {
                    xArr.Add(JsonConvert.DeserializeObject<Dictionary<string, PrototypeData>>(prototypes.ToString()));
                }
                yArr.Add(xArr);
            }
            waveFunction.Add(yArr);
        }
    }

    public bool IsCollapsed()
    {
        foreach (var z in waveFunction)
        {
            foreach (var y in z)
            {
                foreach (var x in y)
                {
                    if (x.Count > 1) return false;
                }
            }
        }
        return true;
    }

    public Dictionary<string, PrototypeData> GetPosibilities(Vector3Int coords)
    {
        return waveFunction[coords.z][coords.y][coords.x];
    }
    public void Iterate()
    {
        var coords = GetMinEntropyCoords();
        CollapseAt(coords);
        Propagate(coords);
    }
    private void CollapseAt(Vector3Int coords)
    {
        var possiblePrototypes = waveFunction[coords.z][coords.y][coords.x];
        var selection = WeightedChoice(possiblePrototypes);
        var prototype = possiblePrototypes[selection];
        waveFunction[coords.z][coords.y][coords.x] = new Dictionary<string, PrototypeData>() 
        {
            {selection,prototype}
        };
    }
    private string WeightedChoice(Dictionary<string, PrototypeData> prototypes)
    {
        var protoWeights = new Dictionary<float,KeyValuePair<string, PrototypeData>>();
        foreach(var p in prototypes)
        {
            var w = p.Value.weight;
            w += UnityEngine.Random.Range(-1f,1f);
            protoWeights[w] = p;
        }
        var weightMax = protoWeights.Keys.Max(x=>x);
        
        return prototypes.First().Key;
    }

    private float GetEntropy(Vector3Int coords)
    {
	    return waveFunction[coords.z][coords.y][coords.x].Count;
    }
    private Vector3Int GetMinEntropyCoords()
    {

        var min_entropy = float.MaxValue;
        var coords = new Vector3Int();

        for (int z = 0; z < _size.z; z++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++)
                {
                    var entropy = GetEntropy(new Vector3Int(x, y, z));
                    if(entropy > 1)
                    {
                        if (min_entropy == float.MaxValue)
                        {
                            min_entropy = entropy;
                            coords = new Vector3Int(x, y, z);
                        }
                        else if (entropy < min_entropy)
                        {
                            min_entropy = entropy;
                            coords = new Vector3Int(x, y, z);
                        }

                    }
                }
            }
        }
        return coords;
    }

    public void Propagate(Vector3Int? coords, bool singleIteration = false)
    {
        if (coords != null && coords != Vector3Int.zero)
        {
            stack.Push(coords.Value);
        }
        while (stack.Count > 0)
        {
            var currentCoords = stack.Pop();
            foreach (var direction in ValidDirections(currentCoords))
            {
                var otherCoords = currentCoords + direction;
                var posibleNeighbours = GetPossibleNeighbours(currentCoords, direction);
                var otherPosiblePrototypes = GetPosibilities(otherCoords).ToList();

                if (otherPosiblePrototypes.Count == 0) continue;

                foreach (var otherProto in otherPosiblePrototypes.ToDictionary(x=>x.Key, y=> y.Value))
                {
                    if (!posibleNeighbours.Contains(otherProto.Key))
                    {
                        Constrain(otherCoords, otherProto.Key);
                        if (!stack.Any(x => x == otherCoords))
                        {
                            stack.Push(otherCoords);
                        }
                    }
                }
            }
            if (singleIteration)
                break;
        }
    }

    private List<string> GetPossibleNeighbours(Vector3Int currentCoords, Vector3Int direction)
    {
        var validNeighbours = new List<string>();

        var prototypes = GetPosibilities(currentCoords);

        var dirIdx = directionToIndex[direction];

        foreach (var prototype in prototypes)
        {
            var neighbours = prototype.Value.valid_neighbours[dirIdx];
            foreach (var neighbour in neighbours)
            {
                if (!validNeighbours.Contains(neighbour))
                    validNeighbours.Add(neighbour);
            }
        }

        return validNeighbours;
    }
    private void Constrain(Vector3Int coords, string protoName)
    {
        waveFunction[coords.z][coords.y][coords.x].Remove(protoName);
    }



    public List<Vector3Int> ValidDirections(Vector3Int coords)
    {

        var x = coords.x;

        var y = coords.y;

        var z = coords.z;

        var width = _size.x;
        var height = _size.y;
        var length = _size.z;

        var dirs = new List<Vector3Int>();

        if (x > 0)
        {
            dirs.Add(Vector3Int.left);
        }
        if (x < width - 1)
        {
            dirs.Add(Vector3Int.right);
        }
        if (y > 0)
        {
            dirs.Add(Vector3Int.down);
        }
        if (y < height - 1)
        {
            dirs.Add(Vector3Int.up);
        }
        if (z > 0)
        {
            dirs.Add(Vector3Int.back);
        }
        if (z < length - 1)
        {
            dirs.Add(Vector3Int.forward);
        }

        return dirs;
    }
}