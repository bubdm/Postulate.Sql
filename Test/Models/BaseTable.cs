using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Postulate.Orm.Abstract;
using Postulate.Orm.Attributes;
using Postulate.Orm.Enums;

namespace Testing.Models
{
    public abstract class BaseTable : Record<int>
    {
        [ColumnAccess(Access.InsertOnly)]
        public DateTime DateCreated { get; set; }

        [ColumnAccess(Access.InsertOnly)]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [ColumnAccess(Access.UpdateOnly)]
        public DateTime? DateModified { get; set; }

        [ColumnAccess(Access.UpdateOnly)]
        [MaxLength(20)]
        public string ModifiedBy { get; set; }

        public override void BeforeSave(IDbConnection connection, string userName, SaveAction action)
        {
            switch (action)
            {
                case SaveAction.Insert:
                    CreatedBy = userName;
                    DateCreated = DateTime.Now;
                    break;

                case SaveAction.Update:
                    ModifiedBy = userName;
                    DateModified = DateTime.Now;
                    break;
            }
        }
    }
}
