using Application.Helpers;
using Google.Cloud.Firestore;

namespace Models
{
    [FirestoreData]
    public class Note
    {
        [FirestoreProperty]
        public string NoteId { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; }
        
        [FirestoreProperty]
        public string? NotebookId { get; set; }

        [FirestoreProperty]
        public string? NoteTitle { get; set; }

        [FirestoreProperty]
        public string? NoteContent { get; set; }

        [FirestoreProperty]
        public DateTime? DateCreated { get; set; }

        [FirestoreProperty]
        public DateTime? DateUpdated { get; set; }
    }

    public class NoteConverter : IFirestoreConverter<Note>
    { 
        public Note FromFirestore(DocumentSnapshot snapshot)
        {
            Dictionary<string, object> dictionary = snapshot.ToDictionary();

            return new Note
            {
                NoteId = (string)(dictionary["NoteId"]),
                UserId = (string)(dictionary["UserId"]),
                NotebookId = (string)(dictionary["NotebookId"]),
                NoteTitle = (string)dictionary["NoteTitle"],
                NoteContent = (string)dictionary["NoteContent"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["DateCreated"]),
                DateUpdated = Helpers.ConvertFirestoreTimestamp(dictionary["DateUpdated"])
            };
        }

        public Note FromFirestore(object value)
        {
            Dictionary<string, object> dictionary = (Dictionary<string, object>)value;

            return new Note
            {
                NoteId = (string)(dictionary["NoteId"]),
                UserId = (string)(dictionary["UserId"]),
                NotebookId = (string)(dictionary["NotebookId"]),
                NoteTitle = (string)dictionary["NoteTitle"],
                NoteContent = (string)dictionary["NoteContent"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["DateCreated"]),
                DateUpdated = Helpers.ConvertFirestoreTimestamp(dictionary["DateUpdated"])
            };
        }

        public object ToFirestore(Note value)
        {
            return new Dictionary<string, object>
            {
                { "NoteId", value.NoteId },
                { "UserId", value.UserId },
                { "NotebookId", value.NotebookId },
                { "NoteTitle", value.NoteTitle },
                { "NoteContent", value.NoteContent },
                { "DateCreated", value.DateCreated },
                { "DateUpdated", value.DateUpdated }
            };
        }
    }
}
