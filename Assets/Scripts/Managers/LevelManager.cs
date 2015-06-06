using UnityEngine;
using System.Collections;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.IO;

public struct ChallengeData
{
    public int challengeNumber;
    public int width;
    public int height;
    public int tileCount;
    public char[] layout;
}

public class LevelManager : MonoBehaviour 
{
    private static LevelManager instance = null;
    public static LevelManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        StartCoroutine("LoadXML");   
    }

    [SerializeField]
    private TextAsset levelXML;

    public ChallengeData[] challenges;

    public int totalLevels { get; set; }

    [SerializeField]
    private GameObject[] objects;

    public void BuildLevel(int challenge)
    {
        GameObject levelContainer = new GameObject();
        levelContainer.name = "levelContainer";
        levelContainer.transform.position = Vector3.zero;

        Vector2 startPoint = new Vector2(0, 0);
        Vector2 currentSpawn = new Vector2(startPoint.x, startPoint.y);

        for(int i = 0; i < challenges[challenge].tileCount ; i++)
        {
            if (challenges[challenge].layout[i] != 'e')
            {
                GameObject item = AddItem(challenges[challenge].layout[i]);
                item.transform.position = currentSpawn;
                item.transform.parent = levelContainer.transform;
            }
            currentSpawn = incrementSpawn(challenge, startPoint, currentSpawn);
        }
    }

    private GameObject AddItem(char item)
    {
        GameObject newObject;
        switch(item)
        {
            case 'S':
                for(int i = 0; i < objects.Length; i++)
                {
                    if(objects[i].name == "PlatformPlayer")
                    {
                        newObject = Instantiate(objects[i]) as GameObject;
                        newObject.name = objects[i].name;
                        return newObject;
                    }
                }
                break;
            case 'E':
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].name == "EndZone")
                    {
                        newObject = Instantiate(objects[i]) as GameObject; ;
                        newObject.name = objects[i].name;
                        return newObject;
                    }
                }
                break;
            case 'b':
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].name == "Block")
                    {
                        newObject = Instantiate(objects[i]) as GameObject; ;
                        newObject.name = objects[i].name;
                        return newObject;
                    }
                }
                break;
            case 's':
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].name == "Spike")
                    {
                        newObject = Instantiate(objects[i]) as GameObject; ;
                        newObject.name = objects[i].name;
                        return newObject;
                    }
                }
                break;
            case 'B':
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].name == "Button")
                    {
                        newObject = Instantiate(objects[i]) as GameObject; ;
                        newObject.name = objects[i].name;
                        return newObject;
                    }
                }
                break;
        }
        return new GameObject();
    }

    private Vector2 incrementSpawn(int challenge, Vector2 startPoint, Vector2 oldspawn) // incraments the point at which to put the next object
    {
        if (oldspawn.x == challenges[challenge].width - 1)
        {
            return new Vector2(startPoint.x, oldspawn.y - 1);
        }
        else
        {
            return new Vector2(oldspawn.x += 1, oldspawn.y);
        }
    }

    // XML Parsing Section

    private IEnumerator LoadXML() // Starts the XML loading from the web and starts parse when complete
    {
        WWW ChallengeXML = new WWW("http://triosdevelopers.com/R.OConnor/impairedLevelData/ChallengeData.xml");

        yield return ChallengeXML;

        ParseXML(ChallengeXML.text);
        Debug.Log("Parse Complete");
    }

    private void ParseXML(string xml) // parses the data out of the Xml
    {
        //XDocument doc = new XDocument(XDocument.Load(new StringReader(xml))); // Maybe a solution but maybe now

        /*
        XmlDocument doc = new XmlDocument();
        doc.Load(new StringReader(xml));
        string xmlPathPattern = "//challenges/challenge";
        XmlNodeList myNodeList = doc.SelectNodes(xmlPathPattern);

        challenges = new ChallengeData[myNodeList.Count];
        totalLevels = myNodeList.Count;
        int count = 0;
        foreach (XmlNode node in myNodeList)
        {
            challenges[count] = ParseNode(node);
            count++;
        }
        */ 
    }

    private ChallengeData ParseNode(XmlNode node) // creates a level out of a node
    {
        XmlNode nameNode = node.FirstChild;
        XmlNode widthNode = nameNode.NextSibling;
        XmlNode heightNode = widthNode.NextSibling;
        XmlNode layoutNode = heightNode.NextSibling;

        ChallengeData temp = new ChallengeData();

        temp.challengeNumber = int.Parse(nameNode.InnerXml);
        temp.width = int.Parse(widthNode.InnerXml);
        temp.height = int.Parse(heightNode.InnerXml);
        temp.tileCount = layoutNode.InnerXml.Length;
        temp.layout = new char[temp.tileCount];

        int count = 0;
        foreach (char tile in layoutNode.InnerXml)
        {
            temp.layout[count] = layoutNode.InnerXml[count];
            count++;
        }

        return temp;
    }
}
