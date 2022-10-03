using FireSharp.Config;
using FireSharp.Interfaces;

namespace MoneyKeeper.Services
{
    public class DbService
    {
        private static DbService _ins;
        public static DbService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new DbService();
                return _ins;
            }
            set { _ins = value; }
        }

        private readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "2TLKiqhrB6TP7DKqS0MjDBYoAyCGFfoIePKa7E7h",
            BasePath = "https://money-keeper-e4af2-default-rtdb.firebaseio.com/"
        };

        private IFirebaseClient _client;
        public IFirebaseClient client
        {
            get { return _client; }
            private set { _client = value; }
        }

        public DbService()
        {
            client = new FireSharp.FirebaseClient(config);
        }

    }
}
