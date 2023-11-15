using MongoDb.Domain.Connection;
using ReactiveUI;

namespace MongoDb.UI.ViewModels.Connections;

public sealed class ConnectionViewModel : ReactiveObject
{
    private string _connectionName;
    private string _connectionString;
    private bool _isSaved;
    private string _connectionColor;

    public ConnectionViewModel(MongoConnectionEntity connection, bool isSaved, int order, DateTime lastUsed,
        int frequency)
    {
        Connection = connection;
        IsSaved = isSaved;
        Order = order;
        LastUsed = lastUsed;
        Frequency = frequency;
        IsNewConnection = false;
    }

    public ConnectionViewModel()
    {
        IsNewConnection = true;
        Connection = new MongoConnectionEntity
        {
            Id = Guid.NewGuid()
        };

        ConnectionName = "New Connection";
        ConnectionString = "mongodb://localhost:27017/";
    }

    public bool IsNewConnection { get; }
    public MongoConnectionEntity Connection { get; }

    public bool IsSaved
    {
        get => _isSaved;
        set => this.RaiseAndSetIfChanged(ref _isSaved, value);
    }
    public int Order { get; set; }
    public DateTime? LastUsed { get; private set; }
    public int Frequency { get; private set; }

    public string ConnectionName
    {
        get => _connectionName;
        set
        {
            this.RaiseAndSetIfChanged(ref _connectionName, value);
            Connection.Name = value;
        }
    }

    public string ConnectionString
    {
        get => _connectionString;
        set
        {
            this.RaiseAndSetIfChanged(ref _connectionString, value);
            Connection.ConnectionString = value;
        }
    }

    public string ConnectionColor
    {
        get => _connectionColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _connectionColor, value);
            Connection.Color = value;
        }
    }
}