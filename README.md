# login-app
#Summary : contains two api,
1.getOTP: generates OTP and send to mobile nummber
2.validate OTP :validate OTP send to the user with mobile number

#steps for building docker file 
dokcer build -t tag_name -f docket file_path
#create container
docker create --name container_name tag_name
#start the container
docket start container_name

#case when docker image doesn't get build
#steps for running in VS Studio
1.Open VS 2019 
2.open the solution folder run the app from VS studio

#sample postmancollection
1.getOtp
curl --location --request POST 'https://localhost:44328/api/getotp' \
--header 'Content-Type: application/json' \
--data-raw '{
    "mobileNumber":""
}'

2.validated otp
curl --location --request POST 'https://localhost:44328/api/validateotp' \
--header 'Content-Type: application/json' \
--data-raw '{
    "mobileNumber":"",
    "OTP":
}'
