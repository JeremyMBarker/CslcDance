using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StepmaniaParser
{
    public List<PackInfo> LoadSongs()
    {
        List<PackInfo> PackList;
        var songLibraryPath = ".\\Songs\\";
        PackList = new List<PackInfo>();

        // search the songs directory for packs
        foreach (var pack in Directory.GetDirectories(songLibraryPath))
        {
            var packItem = new PackInfo(pack);
            foreach (var song in Directory.GetDirectories(pack))
            {
                // check for stepmania files
                var files = Directory.GetFiles(song, "*.sm");
                if (files.Length == 0)   // if no stepmania file
                    continue;

                // Get the data from the file
                var stepmania = File.ReadAllText(files[0]).Trim();
                var metadata = new Regex("#.*?;", RegexOptions.Singleline).Match(stepmania);

                // Add the song to the pack
                var songItem = new SongInfo(song);
                while (metadata.Success)
                {
                    // get the key value pairs
                    var datum = metadata.Value;
                    var key = datum.Substring(0, datum.IndexOf(":")).Trim('#').Trim(':');
                    var value = datum.Substring(datum.IndexOf(":")).Trim(':').Trim(';');

                    // TODO:
                    // -- Eliminate potential user comments in value
                    // -- Check that the song data is valid:
                    //     -- Check that song has title 
                    //     -- Check that song's audio file is present
                    //     -- Check that song has needed note information:
                    //         -- BPM info
                    //         -- Offset
                    //         -- Notes
                    //         -- *Check stepmainia online resources for help*

                    // I'm sure there's a better way to do this
                    switch (key.ToUpper())
                    {
                        case "TITLE":
                            songItem.Title = value;
                            break;
                        case "SUBTITLE":
                            songItem.Subtitle = value;
                            break;
                        case "ARTIST":
                            songItem.Artist = value;
                            break;
                        case "TITLETRANSLIT":
                            songItem.TitleTranslit = value;
                            break;
                        case "SUBTITLETRANSLIT":
                            songItem.SubtitleTranslit = value;
                            break;
                        case "ARTISTTRANSLIT":
                            songItem.ArtistTranslit = value;
                            break;
                        case "GENRE":
                            songItem.Genre = value;
                            break;
                        case "CREDIT":
                            songItem.Credit = value;
                            break;
                        case "BANNER":
                            songItem.Banner = value;
                            break;
                        case "BACKGROUND":
                            songItem.Background = value;
                            break;
                        case "LYRICSPATH":
                            songItem.LyricsPath = value;
                            break;
                        case "CDTITLE":
                            songItem.CDTitle = value;
                            break;
                        case "MUSIC":
                            songItem.Music = value;
                            break;
                        case "OFFSET":
                            songItem.Offset = value;
                            break;
                        case "SAMPLESTART":
                            songItem.SampleStart = value;
                            break;
                        case "SAMPLELENGTH":
                            songItem.SampleLength = value;
                            break;
                        case "DISPLAYBPM":
                            songItem.DisplayBPM = value;
                            break;
                        case "BPMs":
                            songItem.BPMs = value;
                            break;
                        case "STOPs":
                            songItem.Stops = value;
                            break;
                        case "BGCHANGES":
                            songItem.BGChanges = value;
                            break;
                        case "KEYSOUNDS":
                            songItem.KeySounds = value;
                            break;
                        case "ATTACKS":
                            songItem.Attacks = value;
                            break;
                        case "NOTES":
                            songItem.Notes.Add(value);
                            break;
                        default:
                            break;
                    }
                    metadata = metadata.NextMatch();
                }
                packItem.Songs.Add(songItem);
            }
            PackList.Add(packItem);
        }
        return PackList;
    }
}