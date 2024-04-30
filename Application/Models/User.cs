using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string UserName { get; set; }

        [FirestoreProperty]
        public string UserEmail { get; set; }
    }

    public class UserConverter : IFirestoreConverter<User>
    {
        public User FromFirestore(DocumentSnapshot snapshot)
        {
            Dictionary<string, object> dictionary = snapshot.ToDictionary();

            return new User
            {
                UserId = (string)dictionary["UserId"],
                UserName = (string)dictionary["UserName"], 
                UserEmail = (string)dictionary["UserEmail"]
            };
        }

        public User FromFirestore(object value)
        {
            Dictionary<string, object> dictionary = (Dictionary<string, object>)value;

            return new User
            {
                UserId = (string)(dictionary["UserId"]),
                UserName = (string)dictionary["UserName"],
                UserEmail = (string)dictionary["UserEmail"]
            };
        }

        public object ToFirestore(User value)
        {
            return new Dictionary<string, object>
            {
                { "UserId", value.UserId },
                { "UserName", value.UserName },
                { "UserEmail", value.UserEmail }
            };
        }
    }
}