# Nine Coding Challenge
Repo for coding challenge from Nine.

This API is excuted by sending POST request to https://myservice.altocumulus.it

## Design Overview

This API serverside was built as per the requiremnets specified in the coding challege http://codingchallenge.nine.com.au/

The API was developed based on Serverless application architecture that uses following comonents.

1. AWS APIGateway : Is used to host a REST api service end point.
2. AWS Lambda : The application logic is written in .Net Core 2.1 that runs as a Serverless AWS Lambda function triggered by API Gateway.  
3. Custom domain/Friendly URL: https://myservice.altocumulus.it was created by mapping a CNAME record to API gateway provided regional domain name.
4. AWS Cerificate Manager (ACM): To create and mangage certificates for custom domain integration with API Gateway 

## Development Steps

1. Created the initial version of the application logic as a standalone .Net Application.
2. Created a AWS Lambda project using AWS .Net SDK with the empty function blue print 
3. Moved the application logic into this project and tested the execution of Lambda function in Mock Testing runtime. 
4. Created an API Gateway end point that is configiured to trigger the Lambda Function. 
5. Modified the function code to interact with API Gateway using API Geteway Request and Response data types.   
5. Executed the function in Postman in the unique URL provided in API Gateway.
6. Created a new certificate in AWS ACM as this was mandatory requirement for custom domain integration with API Gateway
7. Added target domain mapping in API Gateway for custom domain. 
8. Created a new sub domain mapping by adding a new CNAME record on the DNS server and mapped to the Regional end point created by API Gateway. 


## Test Cases 

### Positive Scenarios 
	1. Test for Happy Path : Provided the sample request JSON from specification output matched to sample response JSON
	2. Test for Empty Reponse: Tested by providing an empty payload or disabling all DRM flags, output was an empty response.  
	3. Test for modified request message : Request data was modified by either changing the DRM flag or changing the episodeCount values to check the correctness of the query.

### Negative Scenarios 
	1. Test for invalid JSON handling : Request sent with empty JSON i.e. {} and received a payload missing error as expected.
	2. Test for invalid JSON handling : Request sent with no JSON body i.e. blank request and received JSON parsing error.  
	3. Test for missing payload key   : Request sent with incorrect root key such as "pay" and recieved a payload missing error as expected.

