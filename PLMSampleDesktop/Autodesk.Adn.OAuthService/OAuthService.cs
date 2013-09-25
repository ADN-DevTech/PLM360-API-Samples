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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;

namespace Autodesk.Adn.OAuthentication
{
    public class OAuthService
    {
        private string consumerKey;
        private string consumerSecret;
        private string oauthBaseUrl;

        private string accessToken="";
        private string accessTokenSecret="";
        private string authorizationString = "";

        private RestClient m_Client;

        private string m_sessionHandle;

        public OAuthService(string consumerKey, string consumerSecret, string oauthBaseUrl)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.oauthBaseUrl = oauthBaseUrl;
        }


        public string ConsumerKey
        {
            get { return consumerKey; }
            set { consumerKey = value; }
        }


        public string ConsumerSecret
        {
            get { return consumerSecret; }
            set { consumerSecret = value; }
        }


        public string OauthBaseUrl
        {
            get { return oauthBaseUrl; }
            set { oauthBaseUrl = value; }
        }



        public string AccessToken
        {
            get { return accessToken; }
        }


        public string AccessTokenSecret
        {
            get { return accessTokenSecret; }
        }

        public string AuthorizationString
        {
            get
            {
                return authorizationString;
            }
        }

        public bool StartOAuth()
        {

            m_Client = new RestClient(this.oauthBaseUrl);

            m_Client.Authenticator = OAuth1Authenticator.ForRequestToken(this.consumerKey, this.consumerSecret);

            // Build the HTTP request for a Request token and execute it against the OAuth provider
            var request = new RestRequest("OAuth/RequestToken", Method.POST);
            var response = m_Client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //logOutput.AppendText("\nOops! Something went wrong - couldn't request token from Autodesk oxygen provider" + "\n" + "-----------------------" + "\n");
                return false;
            }
            else
            {
                // The HTTP request succeeded. Get the request token and associated parameters.
                var qs = HttpUtility.ParseQueryString(response.Content);
                var reqToken = qs["oauth_token"];
                var reqTokenSecret = qs["oauth_token_secret"];
                var oauth_callback_confirmed = qs["oauth_callback_confirmed"];
                var x_oauth_client_identifier = qs["x_oauth_client_identifier"];
                var xoauth_problem = qs["xoauth_problem"];
                var oauth_error_message = qs["oauth_error_message"];


                // For in band authorization build URL for Authorization HTTP request
                RestRequest authorizeRequest = new RestRequest
                {
                    Resource = "OAuth/Authorize",
                };

                authorizeRequest.AddParameter("viewmode", "full");
                authorizeRequest.AddParameter("oauth_token", reqToken);
                Uri authorizeUri = m_Client.BuildUri(authorizeRequest);

                // Launch another window with browser control and navigate to the Authorization URL
                LoginForm frm = new LoginForm();
                frm.Uri = authorizeUri;
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    //logOutput.AppendText("In band Authorization failed" + "\n" + "-----------------------" + "\n");
                    return false;
                }

                // Build the HTTP request for an access token
                request = new RestRequest("OAuth/AccessToken", Method.POST);

                m_Client.Authenticator = OAuth1Authenticator.ForAccessToken(
                      consumerKey, consumerSecret, reqToken, reqTokenSecret
                      );

                // Execute the access token request
                response = m_Client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //logOutput.AppendText("\nOops! Something went wrong - couldn't get access token from your Autodesk account" + "\n" + "-----------------------" + "\n");
                    return false;
                }
                else
                {
                    // The request for access token is successful. Parse the response and store token,token secret and session handle
                    qs = HttpUtility.ParseQueryString(response.Content);
                    this.accessToken = qs["oauth_token"];
                    this.accessTokenSecret = qs["oauth_token_secret"];
                    var x_oauth_user_name = qs["x_oauth_user_name"];
                    var x_oauth_user_guid = qs["x_oauth_user_guid"];
                    var x_scope = qs["x_scope"];
                    xoauth_problem = qs["xoauth_problem"];
                    oauth_error_message = qs["oauth_error_message"];
                    this.m_sessionHandle = qs["oauth_session_handle"];

                    return true;
                }

            }

           
        }


    }
}
