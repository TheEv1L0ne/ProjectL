using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class WFC3DMain : MonoBehaviour
{
    [SerializeField]
    private bool update;
    private WFC3DModel wfc;
    [SerializeField]
    private Vector3Int size;// = new  Vector3Int(8, 3, 8);
    

    const string meshPath = "Meshes";

    private List<GameObject> meshes = new List<GameObject>();
    private int my_seed = 1;
    private void Awake()
    {
        Generate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            my_seed += 1;
            Generate();
        }
    }
    private void Generate()
    {
        ClearMeshes();
        UnityEngine.Random.InitState(GetSeed());


        var prototypes = LoadPrototypeData();

        wfc = new WFC3DModel(size, prototypes);

        ApplyCustomConstraints();

        if (update)
        {

        }
        else
        {
            RegeNoUpdate();
        }
        //VisualizeWaveFunction();
        // 	while not wfc.is_collapsed():
        // 		wfc.iterate()
        // 		clear_meshes()
        // 		visualize_wave_function()
        // 		yield(get_tree(), "idle_frame")
        // 	clear_meshes()
        // else:
        // 	regen_no_update()

        // visualize_wave_function()
    }

    private void RegeNoUpdate()
    {
        while(!wfc.IsCollapsed())
        {
            wfc.Iterate();
        }
        VisualizeWaveFunction();
        if(meshes.Count == 0)
        {
            my_seed += 1;
            Generate();
        }

    }

    private void VisualizeWaveFunction(bool onlyCollapsed = true)
    {
        for (int z = 0; z < size.z; z++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var prototypes = wfc.waveFunction[z][y][x];
                    if (onlyCollapsed)
                    {
                        if (prototypes.Count > 1)
                            continue;
                    }
                    foreach(var prototype in prototypes)
                    {
                        var dict = wfc.waveFunction[z][y][x][prototype.Key];
                        var mesh_name = dict.mesh_name;
                        var mesh_rotation = dict.mesh_rotation;

                        if (mesh_name == "-1") continue;

                        var mesh = Instantiate(Resources.Load<GameObject>(meshPath + "/" + mesh_name), new Vector3Int(x, y, z),Quaternion.Euler(0, -Mathf.PI / 2 * mesh_rotation * Mathf.Rad2Deg, 0));
                        meshes.Add(mesh);


                    }
                }
            }
        }
    }


    private void ClearMeshes()
    {
        foreach (var mesh in meshes)
        {
            Destroy(mesh);
        }
    }

    private int GetSeed()
    {
        MD5 md5Hasher = MD5.Create();
        var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(my_seed.ToString()));
        return BitConverter.ToInt32(hashed, 0);
    }
    private TextAsset LoadPrototypeData()
    {
        return Resources.Load<TextAsset>("prototype_data");
    }

    //  This function isn't covered in the video but what we do here is basically
    //  go over the wavefunction and remove certain modules from specific places
    //  for example in my Blender scene I've marked all of the beach tiles with
    //  an attribute called "constrain_to" with the value "bot". This is recalled
    //  in this function, and all tiles with this attribute and value are removed
    //  from cells that are not at the bottom i.e., if y > 0: constrain.
    private void ApplyCustomConstraints()
    {
        for (int z = 0; z < size.z; z++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var coords = new Vector3Int(x, y, z);
                    var protos = wfc.GetPosibilities(coords);
                    if (y == size.y - 1)
                        foreach (var proto in protos.ToDictionary(x=>x.Key,y=>y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.pZ];
                            if (!neighs.Contains("p-1"))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    if (y > 0)
                    {
                        foreach (var proto in protos.ToDictionary(x=>x.Key,y=>y.Value))
                        {
                            var custom_constraint = proto.Value.constrain_to;
                            if(custom_constraint == WFC3DModel.CONSTRAINT_BOTTOM)
                            {
                                protos.Remove(proto.Key);
                                
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }
                    if (y < size.y - 1)
                    {
                        foreach (var proto in protos.ToDictionary(x=>x.Key,y=>y.Value))
                        {
                            var custom_constraint = proto.Value.constrain_to;
                            if(custom_constraint == WFC3DModel.CONSTRAINT_TOP)
                            {
                                protos.Remove(proto.Key);
                                
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }
                    if (y == 0)
                        foreach (var proto in protos.ToDictionary(x=>x.Key,y=>y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.nZ];
                            var custom_constraint = proto.Value.constrain_from;
                            if (!neighs.Contains("p-1") || (custom_constraint == WFC3DModel.CONSTRAINT_BOTTOM))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    if (x == size.x - 1)
                    {
                        foreach (var proto in protos.ToDictionary(x => x.Key, y => y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.pX];
                            if (!neighs.Contains("p-1"))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }
                    if (x == 0)
                    {
                        foreach (var proto in protos.ToDictionary(x => x.Key, y => y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.nX];
                            if (!neighs.Contains("p-1"))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }
                    if (z == size.z - 1)
                    {
                        foreach (var proto in protos.ToDictionary(x => x.Key, y => y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.nY];
                            if (!neighs.Contains("p-1"))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }
                    if(z == 0)
                    {
                         foreach (var proto in protos.ToDictionary(x => x.Key, y => y.Value))
                        {
                            var neighs = proto.Value.valid_neighbours[WFC3DModel.pY];
                            if (!neighs.Contains("p-1"))
                            {
                                protos.Remove(proto.Key);
                                if (!wfc.stack.Contains(coords))
                                {
                                    wfc.stack.Push(coords);
                                }
                            }
                        }
                    }

                }
            }
        }
        wfc.Propagate(null, false);
    }
}