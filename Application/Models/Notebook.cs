using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using Application.Helpers;

namespace Models
{
    [FirestoreData]
    public class Notebook
    {
        [FirestoreProperty]
        public string NotebookId { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string NotebookTitle { get; set; }

        [FirestoreProperty]
        public string? NotebookDescription { get; set; }

        [FirestoreProperty]
        public DateTime? DateCreated { get; set; }
        
        [FirestoreProperty]
        public IEnumerable<Note> Notes { get; set; }
    }

    public class NotebookConverter : IFirestoreConverter<Notebook>
    {
        public Notebook FromFirestore(DocumentSnapshot snapshot)
        {
            Dictionary<string, object> dictionary = snapshot.ToDictionary();

            return new Notebook
            {
                NotebookId = (string)(dictionary["NotebookId"]),
                UserId = (string)(dictionary["UserId"]),
                NotebookTitle = (string)dictionary["NotebookTitle"],
                NotebookDescription = (string)dictionary["NotebookDescription"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["DateCreated"]),
                Notes = new List<Note>()
            };
        }

        public Notebook FromFirestore(object value)
        {
            Dictionary<string, object> dictionary = (Dictionary<string, object>)value;

            return new Notebook
            {
                NotebookId = (string)(dictionary["NotebookId"]),
                UserId = (string)(dictionary["UserId"]),
                NotebookTitle = (string)dictionary["NotebookTitle"],
                NotebookDescription = (string)dictionary["NotebookDescription"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["DateCreated"]),
                Notes = new List<Note>()
            };
        }

        public object ToFirestore(Notebook value)
        {
            return new Dictionary<string, object>
            {
                { "NotebookId", value.NotebookId },
                { "UserId", value.UserId },
                { "NotebookTitle", value.NotebookTitle },
                { "NotebookDescription", value.NotebookDescription },
                { "DateCreated", value.DateCreated },
            };
        }
    }
}
