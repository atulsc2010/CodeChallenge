using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using AWSLambdaJParse;
using Amazon.Lambda.APIGatewayEvents;

namespace AWSLambdaJParseUnit.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void T1TestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T1RequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"','\'');

            Assert.Equal(SampleData.T1ResponseSample, response.Body);
        }

        [Fact]
        public void T2TestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T2RequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');

            Assert.Equal(SampleData.T2ResponseSample, response.Body);
        }

        [Fact]
        public void T3TestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T3RequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');

            Assert.Equal(SampleData.T3ResponseSample, response.Body);
        }

        [Fact]
        public void T1ErrTestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T1ErrRequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');

            Assert.Equal(SampleData.T1ErrResponseSample, response.Body);
        }

        [Fact]
        public void T2ErrTestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T2ErrRequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');
            
            //Assert.Contains("'error': 'Could not decode request:", response.Body);
            Assert.Contains(SampleData.T2ErrResponseSample, response.Body);
        }

        [Fact]
        public void T3ErrTestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T3ErrRequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');
            
            Assert.Equal(SampleData.T3ErrResponseSample, response.Body);
        }

        [Fact]
        public void T4ErrTestFunction()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            APIGatewayProxyRequest request = new APIGatewayProxyRequest();
            request.Body = SampleData.T4ErrRequestSample;

            APIGatewayProxyResponse response = function.FunctionHandler(request, context);

            response.Body = response.Body.Replace('"', '\'');

            Assert.Equal(SampleData.T4ErrResponseSample, response.Body);
        }

    }
}
