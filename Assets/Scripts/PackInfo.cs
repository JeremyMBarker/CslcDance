using System.Collections.Generic;

public class PackInfo
{
    public string Path;             // path to pack
    public List<SongInfo> Songs;    // all songs found in the pack

    // Property that gets name from path
    public string Name { get { return Path.Substring(".\\Songs\\".Length); } }

    public PackInfo(string _Path)
    {
        Path = _Path;
        Songs = new List<SongInfo>();
    }
}