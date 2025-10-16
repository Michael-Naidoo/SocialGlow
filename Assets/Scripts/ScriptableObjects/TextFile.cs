using UnityEngine;

[CreateAssetMenu(fileName = "Text File", menuName = "TextFile", order = 0)]
public class TextFile : ScriptableObject
{
    public string opener;
    public string[] playerOptions = new string[3];
    public string[] response = new string[3];
    public string[] PlayerResponsesStrings1 = new string[3];
    public string[] PlayerResponsesStrings2 = new string[3];
    public string[] PlayerResponsesStrings3 = new string[3];
    
}
