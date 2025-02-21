using FluentAssertions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.JsonWebToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.JsonWebToken.Model
{
    public class JsonWebTokenAuthenticator : AuthenticatorBase
    {
        // Base URL of the application
        private readonly string _baseUrl;

        // Username and password of the User
        private readonly User _userData;

        //Constructor for the JsonWebTokenAuthenticator child class, this is required because its parent 'AuthenticatorBase' class itself has a protected constructor.
        //It is the responsibility of the child class to call this constructor.
        public JsonWebTokenAuthenticator(string baseUrl, User userData) : base("")  
        {
            _baseUrl = baseUrl;
            _userData = userData;
        }

        //Implementation in the child class of the abstract method for the parent 'AuthenticatorBase' class.
        protected override ValueTask<Parameter> GetAuthenticationParameter(string accessToken) 
        {
            Token = string.IsNullOrEmpty(accessToken) ? GetToken() : Token;
            return new ValueTask<Parameter>(new HeaderParameter(KnownHeaders.Authorization, Token));
        }

        private string GetToken()
        {
            using (var client = new RestClient(_baseUrl))
            {
                //Create a Request for the User Registration /users/sign-up
                var regResponse = client.PostJson<User>("users/sign-up", _userData);
                //The PostJson API will perform the following
                // 1. Create a RestRequest of Type POST
                // 2. Serialise the given object into JSON representation
                // 3. Send the POST request
                regResponse.Should().Be(System.Net.HttpStatusCode.OK);

                //Create a Second Request for authenticating the created user /users/authenticate
                var authResponse = client.PostJson<User, JwtToken>("users/authenticate", _userData);
                //The PostJson API will perform the following
                // 1. Create a RestRequest of Type POST
                // 2. Serialise the given object into JSON representation
                // 3. Send the POST request
                // 4. Deserialise the response to a given type
                authResponse.Token.Should().NotBeNullOrEmpty();
                return $"Bearer {authResponse.Token}";

            }
        }
    }
}
