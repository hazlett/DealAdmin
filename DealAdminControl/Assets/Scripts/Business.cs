using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class Business {
    [XmlAttribute]
    public string BusinessName = "";
    [XmlAttribute]
    public string Deal = "";
    [XmlAttribute]
    public string GPS = "";
    [XmlElement]
    public List<string> Tags = new List<string>();
    [XmlAttribute]
    public string Rating;
    [XmlAttribute]
    public string PriceRange;


    internal Business() { }
    internal Business(string name, string deal, string gps, List<string> tags, string rating, string priceRange)
    {
        BusinessName = name;
        Deal = deal;
        GPS = gps;
        Tags = tags;
        Rating = rating;
        PriceRange = priceRange;
    }
    internal Business(string name, string deal)
    {
        BusinessName = name;
        Deal = deal;
    }
    internal string ToXML()
    {
        XmlSerializer xmls = new XmlSerializer(typeof(Business));
        StringWriter writer = new StringWriter();
        xmls.Serialize(writer, this);
        writer.Close();
        return writer.ToString();       
    }
	
}
