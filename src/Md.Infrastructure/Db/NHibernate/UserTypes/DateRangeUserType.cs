using System;
using Md.Infrastructure.Clr;

namespace Md.Infrastructure.Data.NHibernate.UserTypes
{
    public class DateRangeUserType : CompositeUserTypeBase<DateRange>
    {
        public DateRangeUserType()
        {
            MapProperty(prop => prop.Start);
            MapProperty(prop => prop.Finish);
        }

        protected override DateRange CreateInstance(object[] propertyValues)
        {
            return new DateRange((DateTime)propertyValues[0], (DateTime)propertyValues[1]);
        }

        protected override DateRange PerformDeepCopy(DateRange source)
        {
            return new DateRange(source.Start, source.Finish);
        }

        public override bool IsMutable
        {
            get { return false; }
        }
    }


    ///// <summary>User type to deal with <see cref="DateRange"/> persistence for time sheet tracking.</summary>
    //public class DateRangeUserType : IUserType
    //{

    //    public SqlType[] SqlTypes
    //    {
    //        get
    //        {
    //            var types = new SqlType[2];
    //            types[0] = new SqlType(DbType.DateTime);
    //            types[1] = new SqlType(DbType.DateTime);
    //            return types;

    //        }
    //    }

    //    public Type ReturnedType
    //    {
    //        get { return typeof(DateRange); }
    //    }

    //    /// <summary>Just return <see cref="DateRange.Equals(object)"/></summary>
    //    public new bool Equals(object x, object y)
    //    {
    //        return x != null && x.Equals(y);
    //    }

    //    /// <summary>Just return <see cref="DateRange.GetHashCode"/></summary>
    //    public int GetHashCode(object x)
    //    {
    //        return x.GetHashCode();
    //    }

    //    public object NullSafeGet(IDataReader rs, string[] names, object owner)
    //    {
    //        var start = (DateTime)NHibernateUtil.DateTime.NullSafeGet(rs, names[0]);
    //        var end = (DateTime)NHibernateUtil.DateTime.NullSafeGet(rs, names[1]);

    //        return new DateRange(start, end);
    //    }

    //    public void NullSafeSet(IDbCommand cmd, object value, int index)
    //    {
    //        var dateRange = ((DateRange)value);

    //        NHibernateUtil.DateTime.NullSafeSet(cmd, dateRange.Start, index);
    //        NHibernateUtil.DateTime.NullSafeSet(cmd, dateRange.Finish, index);
    //    }

    //    public object DeepCopy(object value)
    //    {
    //        var dateRange = ((DateRange)value);
    //        return new DateRange(dateRange.Start, dateRange.Finish);
    //    }

    //    public bool IsMutable
    //    {
    //        get { return false; }
    //    }

    //    public object Replace(object original, object target, object owner)
    //    {
    //        //because it is immutable so we can just return it as is  
    //        return original;
    //    }

    //    public object Assemble(object cached, object owner)
    //    {
    //        //Used for caching, as it is immutable we can just return it as is  
    //        return cached;
    //    }

    //    public object Disassemble(object value)
    //    {
    //        //Used for caching, as it is immutable we can just return it as is  
    //        return value;
    //    }
    //}
}