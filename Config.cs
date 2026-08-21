using System.Text.Json;

namespace vmbl;

public ref struct Defaults
{
    private static JsonReaderOptions _opts = new(){ AllowTrailingCommas=true, CommentHandling=JsonCommentHandling.Skip };
    private Utf8JsonReader _reader = new(File.ReadAllBytes("./config.json"), _opts);

    public static string? Version {get; set;}
    public static string? Author {get; set;}
    public static string? OutputPath {get; set;}

    public Defaults()
    {
        while (_reader.Read())
        {
            if(_reader.TokenType == JsonTokenType.PropertyName)
            {
                switch (_reader.GetString())
                {
                    case "author":
                        _reader.Skip();
                        Author = _reader.GetString();
                        break;
                    case "version":
                        _reader.Skip();
                        Version = _reader.GetString();
                        break;
                    case "output-path":
                        _reader.Skip();
                        OutputPath = _reader.GetString();
                        break;
                    default: 
                        _reader.Skip();
                        break;
                }
            }
        }
    }
}