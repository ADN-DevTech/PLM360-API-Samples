////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved 
// Written by Daniel Du 2013 - ADN/Developer Technical Services
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Authenticators;
using Autodesk.Adn.OAuthentication;

namespace Autodesk.Adn.PLM360API

{
    public class PLM360RestService
    {
        private string accessToken;
        private string accessSecret;
        private string customerId;
        private string tanentUrl;

        private RestClient m_client;
        private OAuthService m_OAuthService;

        private Dictionary<string, string> _cookies;

        public Dictionary<string,string> Cookies // cookie name, cookie value
        {
            get
            {
                if (_cookies == null)
                {
                    _cookies = new Dictionary<string, string>();
                }
                return _cookies;
            }

            private set
            {
                if (_cookies == null)
                {
                    _cookies = new Dictionary<string, string>();
                }
                _cookies = value;
            }
        }

        public PLM360RestService(string tanentName, OAuthService oauthSvc)
        {
            this.customerId = tanentName;
            this.tanentUrl = String.Format("https://{0}.autodeskplm360.net",tanentName);
            this.m_OAuthService = oauthSvc;

            m_client = new RestClient(tanentUrl);
        }

        public Session Login()
        {
            OAuth1Authenticator authenticator = OAuth1Authenticator.ForProtectedResource(
                    m_OAuthService.ConsumerKey, m_OAuthService.ConsumerSecret,
                    m_OAuthService.AccessToken, m_OAuthService.AccessTokenSecret);

            authenticator.ParameterHandling = RestSharp.Authenticators.OAuth.OAuthParameterHandling.HttpAuthorizationHeader;
            m_client.Authenticator = authenticator; 
          
            RestRequest request = new RestRequest("/api/v2/authentication/oxygen-login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept","application/json");

            //The oxygen-login endpoint doesn’t read anything from the header.  So the OAuth data you are putting in the header is unnecessary.
            //One of the annoyances I have with REST is that there are multiple channels for input.  
            //You have the URL, the query string, the HTTP header, the HTTP body and the HTTP verb.  Many times it’s not clear what data should go where.  
            //The PLM API will favor putting data in the HTTP body whenever possible.

            OxygenCredentials cred = new OxygenCredentials();
            cred.customerId = this.customerId.ToUpper(); //must be upper case?? Yes, must be UPPERCASE!!
            cred.validation =  authenticator.GetAuthorizationHeader();// using a revised version of restsharp
            request.AddBody(cred);
            
            IRestResponse<Session> response = m_client.Execute<Session>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //save the cookies for latter use
                foreach (var cookie in response.Cookies)
	            {
		             Cookies.Add(cookie.Name,cookie.Value);
	            }
                
                return response.Data;
                
            }
            else
            {
                return null;
            }
        }

        public bool Logout()
        {
            RestRequest request = new RestRequest("/api/v2/authentication/logout", Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Session> response = m_client.Execute<Session>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session emptySession =  response.Data;
                return true;
            }
            else
            {
                return false;
            }

        }

        public PagedCollection<Workspace> GetWorkspaces()
        {

            RestRequest request = new RestRequest("/api/v2/workspaces", Method.GET);
           
            request.AddHeader("Accept", "application/json");
   
            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<PagedCollection<Workspace>> response = m_client.Execute<PagedCollection<Workspace>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
            

        }

        public Workspace GetWorkspace(long id)
        {
            string resource = string.Format("/api/v2/workspaces/{0}",id);
            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Workspace> response = m_client.Execute<Workspace>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public PagedCollection<Item> GetItems(long workspaceId, long? page, long? pageSize)
        {
           
            string resource = string.Format("/api/v2/workspaces/{0}/items", workspaceId);
            if (page != null)
            {
                resource += "?page=" + page.ToString();
            }
            if (pageSize != null)
            {
                resource += "&page-size=" + pageSize.ToString();
            }
                       
            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<PagedCollection<Item>> response = m_client.Execute<PagedCollection<Item>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public Item GetItem(long workspaceId, long itemId)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}", workspaceId, itemId);
            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Item> response = m_client.Execute<Item>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public bool DeleteItem(Item item)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}", item.workspaceId, item.id);
            RestRequest request = new RestRequest(resource, Method.DELETE);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Item> response = m_client.Execute<Item>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK
                || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Obsolete("not completed yet, do not use.")]
        public ItemDetail AddItem(long workspaceId, ItemDetail newItem)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items", workspaceId);
            RestRequest request = new RestRequest(resource, Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            request.AddBody(newItem);

            IRestResponse<ItemDetail> response = m_client.Execute<ItemDetail>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        [Obsolete("not completed yet, do not use.")]
        public ItemDetail UpdateItem(long workspaceId, long itemId, ItemDetail newItem)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}", workspaceId, itemId);
            RestRequest request = new RestRequest(resource, Method.PUT);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            request.AddBody(newItem);

            IRestResponse<ItemDetail> response = m_client.Execute<ItemDetail>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public PagedCollection<File> GetFiles(Item item)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files", item.workspaceId, item.id);

            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<PagedCollection<File>> response = m_client.Execute<PagedCollection<File>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public File CheckoutFile(File file)
        {
            return CheckoutFile(file.workspaceId, file.itemId, file.id);
        }

        public File CheckoutFile(long workspaceId, long itemId, long fileId)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}/checkout", workspaceId, itemId, fileId);

            RestRequest request = new RestRequest(resource, Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<File> response = m_client.Execute<File>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public bool UndoCheckout(File file)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}/checkout", file.workspaceId, file.itemId, file.id);

            RestRequest request = new RestRequest(resource, Method.DELETE);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<File> response = m_client.Execute<File>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK 
                ||response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [Obsolete]
        public File Checkin(long workspaceId, long itemId, long fileId, byte[] fileContent)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}/checkout", workspaceId, itemId, fileId);

            RestRequest request = new RestRequest(resource, Method.POST);

            request.AddHeader("content-type", "multipart/mixed");
            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            

            IRestResponse<File> response = m_client.Execute<File>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        [Obsolete("not completed yet, do not use.")]
        public File AddFile(long workspaceId, long itemId, FileUploadRequest fileUpReq, byte[] fileContent)
        {

            if (fileContent.Length > 512*1024*1024)
            {
                throw new Exception("File is too large beyond of PLM360's file limitation: 512M");
            }

            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files", workspaceId, itemId);

            RestRequest request = new RestRequest(resource, Method.POST);
            //request.RequestFormat = DataFormat.Json;

            request.AddHeader("content-type", "multipart/mixed");
            //request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            request.AddBody(fileUpReq);
            //request.AddBody(fileContent);
            //request.AddParameter("multipart/mixed", fileContent, ParameterType.RequestBody);
            
            //request.AddFile("file", fileContent, fileUpReq.fileName);
            
            IRestResponse<PagedCollection<File>> response = m_client.Execute<PagedCollection<File>>(request);

            PagedCollection<File> allFile;

            if (response.StatusCode == System.Net.HttpStatusCode.OK
                || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                allFile = response.Data;

                if (allFile != null)
                {
                    return allFile.elements.SingleOrDefault<File>(f => f.fileName == fileUpReq.fileName);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public bool DeleteFile(File file)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}", file.workspaceId, file.itemId, file.id);

            RestRequest request = new RestRequest(resource, Method.DELETE);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<File> response = m_client.Execute<File>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK
                || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public File GetFile(long workspaceId, long itemId, long fileId)
        {
            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}", workspaceId, itemId, fileId);

            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<File> response = m_client.Execute<File>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public bool GetFileContent(File file, string fileName)
        {

            string resource = string.Format("/api/v2/workspaces/{0}/items/{1}/files/{2}", file.workspaceId, file.itemId, file.id);

            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("Accept", "application/octet-stream"); // "Accept", A must be captal 

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse response = m_client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    System.IO.File.WriteAllBytes(fileName, response.RawBytes);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        private void AddCookies(RestRequest request)
        {
            foreach (var cookie in Cookies)
            {
                request.AddCookie(cookie.Key, cookie.Value);
            }
        }


    }
}
