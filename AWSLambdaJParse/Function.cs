using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaJParse
{
    public class Function
    {

        /// <summary>
        /// This function was written as part of the codingchallege from Nine.
        /// This function takes a Payload JSON and rerturns a filtered message as per the 
        /// specification provided by http://codingchallenge.nine.com.au/
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns>
        /// A list of shows from the request payload, with DRM enabled (drm: true) 
        /// and have at least one episode (episodeCount > 0).
        /// </returns>
        #region MainMethod
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            APIGatewayProxyResponse response;
            string json = input.Body;

            try
            {
                JObject payload = JObject.Parse(json);

                ValidatePayload(payload);

                var childList = FilterPayload(payload);

                JObject result = FormatResponseBody(childList);
                
                response = CreateResponse(result.ToString()); 

            }
            catch (Exception e)
            {
                var err = JObject.FromObject(new
                {
                    error = "Could not decode request: JSON parsing failed - " + e.Message
                }).ToString();

                response = CreateResponse(err);
            } 

            return response;
        }
        #endregion


        #region HelperMethods
        private APIGatewayProxyResponse CreateResponse(string result)
        {
            int statusCode = (result != null && !result.Contains("error")) ?
                (int)HttpStatusCode.OK :
                (int)HttpStatusCode.BadRequest;

            string body = result ?? string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
            };

            return response;
        }

        private void ValidatePayload(JObject p) 
        {
            bool rootExists = p.HasValues;
            bool payloadTokenExists = p.TryGetValue("payload", out _);

            if ((rootExists && payloadTokenExists) == false)
            {
                throw new Exception("Payload not found in input JSON");
            };

            //return root && payloadToken; 
        }

        private IEnumerable<JToken> FilterPayload(JObject p)
        {
            var result = p.SelectToken("payload").ToList()
                .Where(c => c.SelectToken("drm") != null && (bool)c.SelectToken("drm") == true)
                .Where(l => l.SelectToken("episodeCount") != null && (int)l.SelectToken("episodeCount") > 0);
            ;

            return result;
        }

        private JObject FormatResponseBody(IEnumerable<JToken> clist) 
        {
            JObject result = JObject.FromObject(new
            {
                response = clist.Select(c => new
                {
                    image = c.SelectToken("image.showImage"),
                    slug = c.SelectToken("slug"),
                    title = c.SelectToken("title")
                }
                )
            });

            return result;
        }

        #endregion
    }
}
