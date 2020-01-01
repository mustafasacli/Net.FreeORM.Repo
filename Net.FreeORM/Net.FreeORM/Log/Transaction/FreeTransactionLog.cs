namespace Net.FreeORM.Log.Transaction
{
    using Net.FreeORM.Framework.Base;
    using Net.FreeORM.Framework.Util;
    using Net.FreeORM.Log.Error;
    using System;

    public sealed class FreeTransactionLog : BaseBO, IDisposable
    {
        private int _UserId;
        private string _TransactionType;
        private int _LogObject;
        private DateTime _LogTime;
        private string _ControlTag;
        private string _ControlName;
        private string _FormName;
        private int _OBJID;

        public FreeTransactionLog(int userId)
        {
            UserId = userId;
            LogTime = DateTime.Now;
            OnPropertyChanged("MachineName");
        }

        public int OBJID
        {
            set { _OBJID = value; OnPropertyChanged("OBJID"); }
            get { return _OBJID; }
        }

        public string FormName
        {
            set { _FormName = value; OnPropertyChanged("FormName"); }
            get { return _FormName; }
        }

        public string ControlName
        {
            set { _ControlName = value; OnPropertyChanged("ControlName"); }
            get { return _ControlName; }
        }

        public string ControlTag
        {
            set { _ControlTag = value; OnPropertyChanged("ControlTag"); }
            get { return _ControlTag; }
        }

        public DateTime LogTime
        {
            private set { _LogTime = value; OnPropertyChanged("LogTime"); }
            get { return _LogTime; }
        }

        public int LogObject
        {
            set { _LogObject = value; OnPropertyChanged("LogObject"); }
            get { return _LogObject; }
        }

        public string TransactionType
        {
            set { _TransactionType = value; OnPropertyChanged("TransactionType"); }
            get { return _TransactionType; }
        }

        public int UserId
        {
            set { _UserId = value; OnPropertyChanged("UserId"); }
            get { return _UserId; }
        }

        public string MachineName
        {
            get
            {
                string _machineName = string.Empty;

                try
                {
                    _machineName = Environment.MachineName;
                }
                catch (Exception)
                {
                    _machineName = string.Empty;
                }

                return _machineName;
            }
        }

        public override string GetIdColumn()
        {
            return "OBJID";
        }

        public override string GetTableName()
        {
            return "TransactionLog";
        }

        public int Insert()
        {
            try
            {
                switch (SaveType)
                {
                    case SaveTypes.File:
                    case SaveTypes.Database:
                        using (TransactionLogDL transDL = new TransactionLogDL())
                        {
                            return transDL.Insert(this);
                        }

                    case SaveTypes.Cloud:
                        using (TransactionLogDL transDL = new TransactionLogDL("ext"))
                        {
                            return transDL.Insert(this);
                        }

                    default:
                        return -2;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SaveTypes SaveType
        {
            get
            {
                string saveType = ConfUtil.LogType;
                saveType = saveType.TrimStart().TrimEnd().ToLower();
                switch (saveType)
                {
                    case "db":
                    case "dbase":
                    case "database":
                    default:
                        return SaveTypes.Database;

                    case "cloud":
                    case "cld":
                        return SaveTypes.Cloud;
                }
            }
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}