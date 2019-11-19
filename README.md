# Nine Coding Challenge
Repo for coding challenge from Nine. The API solution is built as per the requirements specified in the coding challege http://codingchallenge.nine.com.au/

This API is executed by sending POST request to https://myservice.altocumulus.it

## Design Overview

![design diagram](https://github.com/atulsc2010/NineCodeChallenge/blob/master/Nine%20Code%20Challenge%20Design.jpg) 

The API is developed based on Serverless application model that uses following components.

1. AWS Lambda : The application logic is written in .Net Core 2.1 that runs as a serverless AWS Lambda function triggered by API Gateway.
2. AWS APIGateway : Is used to host a REST api service end point.  
3. AWS Cerificate Manager (ACM): To create and manage SSL certificate for custom domain integration with API Gateway. 
4. Custom domain/Friendly URL: https://myservice.altocumulus.it was created by mapping a CNAME record to API gateway provided regional domain name.


## Development and Deployment Steps

1. Created initial version of the application logic as a standalone .Net Application.
2. Created AWS Lambda project using AWS .Net SDK using empty function blue print. 
3. Moved application code into Lambda project and tested the execution of Lambda function in Mock Testing runtime. 
4. Published the Lambda function to AWS.
4. Created an API Gateway end point that is configiured to trigger the Lambda Function. 
5. Modified the function code to interact with API Gateway using API Gateway Request and Response data types.   
5. Tested the API using Postman by invoking the unique URL provided by API Gateway.
6. Created a new certificate in AWS ACM as this was mandatory requirement for custom domain integration with API Gateway
7. Added target domain mapping in API Gateway for custom domain. 
8. Created a new CNAME record on the DNS server for the sub-domain (myservice.altocumulus.it) mapping to the Regional end point created by API Gateway. 


## .Net Application project (/AWSLambdaJParse)

### AWS Lambda Function

This project consists of:
* Function.cs - class file containing a class with a single function handler method
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS
 
### Summary
    This function was written as part of the codingchallege from Nine.
    This function takes a Payload JSON and rerturns a filtered message as per the 
    specification provided by http://codingchallenge.nine.com.au/
 
### Input
	<param name="input"></param>
    <param name="context"></param>

### Returns
    A list of shows from the request payload, with DRM enabled (drm: true) 
    and have at least one episode (episodeCount > 0).


## Test Cases 

### Success Scenarios 

1. Test for Happy Path : Provided the sample request JSON from specification output matched to sample response JSON
2. Test for Empty Reponse: Tested by providing an empty payload or disabling all DRM flags, output was an empty response.  
3. Test for modified request message : Request data was modified by either changing the DRM flag or changing the episodeCount values to check the correctness of the query.

### Failure Scenarios 

1. Test for invalid JSON handling : Request sent with empty JSON i.e. {} and received a payload not found error as expected.
2. Test for invalid JSON handling : Request sent with no JSON body i.e. blank request and received JSON parsing error. 
2. Test for invalid JSON handling : Request sent with INVALID JSON body i.e. {"payload":} and received JSON parsing error. 
3. Test for missing payload key   : Request sent with incorrect root key such as "pay" and recieved a payload not found error as expected.

