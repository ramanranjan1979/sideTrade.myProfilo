//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sideTrade.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileManager
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Path { get; set; }
        public string Mode { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int FileManagerTypeId { get; set; }
    
        public virtual Profile Profile { get; set; }
        public virtual FileManagerType FileManagerType { get; set; }
    }
}
