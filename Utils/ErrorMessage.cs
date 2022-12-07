using System.Collections.Specialized;
using System.Dynamic;

namespace PokemonApp.Utils;

class ErrorMessage
{
    public static NameValueCollection Errors { get; set; }
    public ErrorMessage()
    {
        Errors = new NameValueCollection();
        Errors.Add("SAVE_ERROR", "Something went wrong while saving");
        Errors.Add("OWNER_EXISTS", "Owner already exists");
    }
}