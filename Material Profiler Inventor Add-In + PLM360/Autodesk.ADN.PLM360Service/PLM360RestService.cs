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
using Newtonsoft.Json;
using RestSharp.Authenticators.OAuth;

namespace Autodesk.ADN.PLM360API
{
    public class PLM360RestService
    {
        private string _customerId;

        private RestClient _client;

        private Dictionary<string, string> _cookies;

        // cookie name, cookie value
        public Dictionary<string,string> Cookies 
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

        public PLM360RestService(string tanentName)
        {
            this._customerId = tanentName;

            string url = String.Format(
                "https://{0}.autodeskplm360.net", 
                tanentName);

            _client = new RestClient(url);
        }

        public async Task<Session> DoLoginAsync(
             string consumerKey,
             string consumerSecret,
             string accessToken,
             string accessTokenSecret)
        {
            OAuth1Authenticator authenticator = 
                OAuth1Authenticator.ForProtectedResource(
                    consumerKey,
                    consumerSecret,
                    accessToken,
                    accessTokenSecret);

            authenticator.ParameterHandling = 
                OAuthParameterHandling.HttpAuthorizationHeader;

            _client.Authenticator = authenticator;

            RestRequest request = new RestRequest(
                "/api/v2/authentication/oxygen-login", 
                Method.POST);
            
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            request.RequestFormat = DataFormat.Json;

            // The oxygen-login endpoint doesn’t read anything from the header.  
            // So the OAuth data you are putting in the header is unnecessary.
            // One of the annoyances I have with REST is that there are multiple channels for input.  
            // You have the URL, the query string, the HTTP header, 
            // the HTTP body and the HTTP verb.  Many times it’s not clear what data should go where.  
            // The PLM API will favor putting data in the HTTP body whenever possible.

            OxygenCredentials cred = new OxygenCredentials();

            // MUST be UPPERCASE!!
            cred.customerId = this._customerId.ToUpper();

            // using a revised version of restsharp
            cred.validation = authenticator.GetAuthorizationHeader();

            request.AddBody(cred);

            IRestResponse<Session> response = await _client.ExecuteAsync<Session>(
                request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //save the cookies for latter use
                foreach (var cookie in response.Cookies)
                {
                    Cookies.Add(cookie.Name, cookie.Value);
                }

                return response.Data;

            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DoLogoutAsync()
        {
            RestRequest request = new RestRequest(
                "/api/v2/authentication/logout", 
                Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Session> response = await _client.ExecuteAsync<Session>(
                request);

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

        public async Task<PagedCollection<Workspace>> GetWorkspacesAsync()
        {
            RestRequest request = new RestRequest(
                "/api/v2/workspaces", 
                Method.GET);
           
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");
   
            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<PagedCollection<Workspace>> response =
                await _client.ExecuteAsync<PagedCollection<Workspace>>(
                    request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<Workspace> GetWorkspaceAsync(long id)
        {
            RestRequest request = new RestRequest(
                string.Format("/api/v2/workspaces/{0}", id),
                Method.GET);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Workspace> response =
                await _client.ExecuteAsync<Workspace>(
                    request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<PagedCollection<Item>> GetItemsAsync(
            long workspaceId, 
            long? page, 
            long? pageSize)
        {          
            string resource = string.Format(
                "/api/v2/workspaces/{0}/items",
                workspaceId);

            if (page != null)
            {
                resource += "?page=" + page.ToString();
            }
            if (pageSize != null)
            {
                resource += "&page-size=" + pageSize.ToString();
            }
                       
            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<PagedCollection<Item>> response =
                await _client.ExecuteAsync<PagedCollection<Item>>(
                    request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<Item> GetItemAsync(long workspaceId, long itemId)
        {
            string resource = string.Format(
                "/api/v2/workspaces/{0}/items/{1}", 
                workspaceId, 
                itemId);

            RestRequest request = new RestRequest(resource, Method.GET);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Item> response = 
                await _client.ExecuteAsync<Item>(
                    request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateItemAsync(long workspaceId, Item item)
        {
            string resource = string.Format(
                "/api/v2/workspaces/{0}/items/", 
                workspaceId);

            RestRequest request = new RestRequest(resource, Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");

            string json = JsonConvert.SerializeObject(item);

            request.AddParameter(
                "application/json; charset=utf-8", 
                json, 
                ParameterType.RequestBody);

            request.RequestFormat = DataFormat.Json;

            //add cookies which contains the login information
            AddCookies(request);

            IRestResponse<Item> response =
                await _client.ExecuteAsync<Item>(
                    request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
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

    public static class RestClientExtensions
    {
        public static async Task<IRestResponse<T>> ExecuteAsync<T>(
            this RestClient client,
            RestRequest request) where T : new()
        {
            return await Task<IRestResponse<T>>.Factory.StartNew(() =>
            {
                try
                {
                    return client.Execute<T>(request);
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
