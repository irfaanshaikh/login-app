using login_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace login_app.Utility
{
    public class otpService
    {
        private readonly IMongoCollection<otpDetails> _otpDetails;

        public otpService(IOtpDetailsDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.databaseName);

            _otpDetails = database.GetCollection<otpDetails>(settings.collectionName);
        }

        public otpDetails Create(otpDetails otpDetails)
        {
            _otpDetails.InsertOne(otpDetails);
            return otpDetails;
        }

        public void Update(string _id, otpDetails otpDetails) =>
            _otpDetails.ReplaceOne(otp => otp._id == _id, otpDetails);
        public otpDetails Get(string mobileNumber)
        {
           return _otpDetails.Find<otpDetails>(query => query.mobileNumber == mobileNumber).FirstOrDefault();
        }

    }
}
