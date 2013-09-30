using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Autodesk.Adn.PLM360API
{
    /// <summary>
    /// Different transfer objects that are serialized to/from json
    /// </summary>

    public class UnknownEntity
    {

        public long? id { set; get; }

        public string body { set; get; }
    }

    public class Credentials
    {

        public string password { set; get; }

        public string userId { set; get; }

        public string customerId { set; get; }
    }

    public class OxygenCredentials
    {

        public string customerId { set; get; }

        public string validation { set; get; }
    }


    public class Session
    {

        public string userId { set; get; }

        public string sessionId { set; get; }

        public string customerToken { set; get; }
    }


    public class Error
    {

        public long? code { set; get; }

        public int? status { set; get; }

        public string body { set; get; }
    }


    public class Workspace
    {

        public long id { set; get; }

        public string url { set; get; }

        public string displayName { set; get; }

        public string systemName { set; get; }

        public string description { set; get; }

        public string workspaceTypeId { set; get; }
    }

    public class Item
    {


        public long id { set; get; }

        public long rootId { set; get; }

        public long workspaceId { set; get; }

        public long version { set; get; }

        public string url { set; get; }

        public string revision { set; get; }

        public string itemDescriptor { set; get; }

        public bool? deleted { set; get; }

        public Dictionary<string, string> fields { set; get; }

        public Dictionary<string, List<Picklist>> picklistFields { set; get; }
    }

    public class ItemDetail : Item
    {
        public bool isWorkingVersion { set; get; }

        public bool isLatestVersion { set; get; }

    }


    public class Picklist
    {

        public long? id { set; get; }

        public string displayName { set; get; }

        public string itemUrl { set; get; }
    }


    public class File
    {

        public long id { set; get; }

        public long itemId { set; get; }

        public long workspaceId { set; get; }

        public string url { set; get; }

        public string fileName { set; get; }

        public string title { set; get; }

        public string description { set; get; }

        public string status { set; get; }

        public string version { set; get; }

        public long size { set; get; }
    }


    public class Page
    {

        public long? index { set; get; }

        public long size { set; get; }

        public string nextUrl { set; get; }

        public string prevUrl { set; get; }
    }


    public class PagedCollection<T>
    {

        public Page page { set; get; }

        public List<T> elements { set; get; }
    }

    public class FileUploadRequest
    {
        public string fileName { set; get; }

        public string title { set; get; }

        public string description { set; get; }

    }
}
