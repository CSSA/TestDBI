using System;
using System.Data;

namespace SQLServerDB
{
    public class Common
    {

        #region Type Checkers

        public static bool isNull(object obj)
        {
            bool isNull = true;
            try {
                if (obj != null)
                    isNull = false;
            } catch {
                isNull = true;
            }
            return isNull;
        }

        #endregion


        #region DataSet Checkers

        public static bool hasData(DataSet dsObject)
        {
            if ((dsObject != null)
                && (dsObject.Tables.Count > 0)
                && (dsObject.Tables[0].Rows.Count > 0))
                return true;
            else return false;
        }

        #endregion

    }
}
