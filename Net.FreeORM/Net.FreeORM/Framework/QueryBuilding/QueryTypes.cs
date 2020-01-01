namespace Net.FreeORM.Framework.QueryBuilding
{ 
    public enum QueryTypes : ushort
    {
        Select =1,
        Insert = 2,
        Update = 4,
        Delete = 8,
        InsertAndGetId = 16,
        SelectWhereId = 32,
        SelectWhereChangeColumns = 64,
        SelectChangeColumns = 128,
        InsertAnyChange = 256
    };
}