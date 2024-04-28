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
        public string NotebookId { get; set; }

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
                NoteId = (string)(dictionary["noteId"]),
                UserId = (string)(dictionary["userId"]),
                NotebookId = (string)(dictionary["notebookId"]),
                NoteTitle = (string)dictionary["noteTitle"],
                NoteContent = (string)dictionary["noteContent"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["dateCreated"]),
                DateUpdated = Helpers.ConvertFirestoreTimestamp(dictionary["dateUpdated"])
            };
        }

        public Note FromFirestore(object value)
        {
            Dictionary<string, object> dictionary = (Dictionary<string, object>)value;

            return new Note
            {
                NoteId = (string)(dictionary["noteId"]),
                UserId = (string)(dictionary["userId"]),
                NotebookId = (string)(dictionary["notebookId"]),
                NoteTitle = (string)dictionary["noteTitle"],
                NoteContent = (string)dictionary["noteContent"],
                DateCreated = Helpers.ConvertFirestoreTimestamp(dictionary["dateCreated"]),
                DateUpdated = Helpers.ConvertFirestoreTimestamp(dictionary["dateUpdated"])
            };
        }

        public object ToFirestore(Note value)
        {
            return new Dictionary<string, object>
            {
                { "noteId", value.NoteId },
                { "userId", value.UserId },
                { "notebookId", value.NotebookId },
                { "noteTitle", value.NoteTitle },
                { "noteContent", value.NoteContent },
                { "dateCreated", value.DateCreated },
                { "dateUpdated", value.DateUpdated }
            };
        }
    }
}
