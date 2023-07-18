using System.Collections.Generic;

public class PrototypeData
{
    public string mesh_name { get; set; }
    public int mesh_rotation { get; set; }
    public string posX { get; set; }
    public string negX { get; set; }
    public string posY { get; set; }
    public string negY { get; set; }
    public string posZ { get; set; }
    public string negZ { get; set; }
    public string constrain_to { get; set; }
    public string constrain_from { get; set; }
    public float weight { get; set; }
    public List<List<string>> valid_neighbours { get; set; }
}

